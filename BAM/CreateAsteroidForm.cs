using Guna.UI2.WinForms;
using HelixToolkit.Wpf;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using static BAM.Program;
using Colors = System.Windows.Media.Colors;
using Point3D = System.Windows.Media.Media3D.Point3D;
using Vector3D = System.Windows.Media.Media3D.Vector3D;


namespace BAM
{
    public partial class CreateAsteroidForm : Form
    {

        private HelixViewport3D helixViewport;
        private ModelVisual3D currentModel;
        private bool funcCheck;
        public CreateAsteroidForm()
        {
            InitializeComponent();
            InitializeHelix();

            guna2CustomCheckBox2.CheckedChanged += (s, e) =>
            {
                funcCheck = guna2CustomCheckBox2.Checked;
            };

        }

        private void UserHelp(object sender, EventArgs e)
        {
            MessageBox.Show($"Smoothness - how detailed the model will be; [10;200]" +
                $"\nRadius - asteroid radius; [3;15]" +
                $"\nN. of Craters - how many craters will be on the surface of the model; [0;50]" +
                $"\n\nХ,Y,Z - deformation along the axis; [0.1;2]", "Manual", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void InitializeHelix()
        {
            helixViewport = new HelixViewport3D();
            helixViewport.IsPanEnabled = true; // not to move the model
            helixViewport.BringIntoView();

            // camera
            var cam = new PerspectiveCamera
            {
                Position = new Point3D(0, 0, 50),
                LookDirection = new Vector3D(0, 0, -50),
                UpDirection = new Vector3D(0, 1, 0),
                FieldOfView = 50
            };
            helixViewport.Camera = cam;

            // camera change coordinates
            helixViewport.CameraChanged += (s, e) =>
            {
                var pos = cam.Position;
                var dist = (pos - new Point3D(0, 0, 0)).Length; // distance to the center

                double minDist = 35;
                double maxDist = 100;

                if (dist < minDist)
                {
                    var dir = cam.LookDirection;
                    dir.Normalize();
                    cam.Position = new Point3D(-dir.X * minDist, -dir.Y * minDist, -dir.Z * minDist);
                }
                else if (dist > maxDist)
                {
                    var dir = cam.LookDirection;
                    dir.Normalize();
                    cam.Position = new Point3D(-dir.X * maxDist, -dir.Y * maxDist, -dir.Z * maxDist);
                }
            };

            // light
            helixViewport.Children.Add(new DefaultLights());

            // binding
            elementHost1.Child = helixViewport;
        }

        private void BtnLoadModel_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "3D Models (*.obj;*.stl)|*.obj;*.stl";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    LoadModel(ofd.FileName);
                }
            }
        }



        private void ButtonGenerateAsteroid_Click(object sender, EventArgs e)
        {
            // 1. Валидация и считывание всех параметров (если ошибка — метод сразу прерывается)
            if (!TryReadInt(textBox1, " resolution ", 10, 200, out int resolution)) return;

            if (!TryReadDouble(textBox2, "radius", 3.0, 15.0, out double radius)) return;
            AsteroidData.radius = radius;

            if (!TryReadInt(textBox3, "number of craters", 0, 50, out int craterNum)) return;

            if (!TryReadDouble(textBox4, "lengths along X", 0.1, 2.0, out double asteroidX)) return;
            if (!TryReadDouble(textBox5, "lengths along Y", 0.0, 2.0, out double asteroidY)) return;
            if (!TryReadDouble(textBox6, "lengths along Z", 0.0, 2.0, out double asteroidZ)) return;

            // 2. Генерация
            string asteroidPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "asteroid.obj");
            AsteroidGenerator.NoiseFn noise = funcCheck ? (AsteroidGenerator.NoiseFn)NoiseLib.Perlin3D : AsteroidGenerator.TrigNoise3D;

            AsteroidGenerator.GenerateAsteroid(asteroidPath, resolution, radius, 0.4f, 3.0f, craterNum, asteroidX, asteroidY, asteroidZ, noise);

            MessageBox.Show("Loading...");
            LoadModel(asteroidPath);
        }

        private bool TryReadInt(TextBox textBox, string paramName, int min, int max, out int value)
        {
            if (!int.TryParse(textBox.Text.Trim(), out value))
            {
                ShowValidationError(textBox, $"Enter integer(0, 1, 2, 3,...) number of: {paramName}.");
                return false;
            }

            if (value < min || value > max)
            {
                ShowValidationError(textBox, $"Value for '{paramName}' should be between {min} and {max}.");
                return false;
            }

            return true;
        }

        private bool TryReadDouble(TextBox textBox, string paramName, double min, double max, out double value)
        {
            // Заменяем точку на запятую для корректного парсинга независимо от региональных настроек ОС
            string text = textBox.Text.Trim().Replace('.', ',');

            if (!double.TryParse(text, out value))
            {
                ShowValidationError(textBox, $"Enter double(0.1, 2.1, 3.3,...) value of: {paramName}.");
                return false;
            }

            if (value < min || value > max)
            {
                ShowValidationError(textBox, $"Value for '{paramName}' should be between {min} and {max}.");
                return false;
            }

            return true;
        }

        private void ShowValidationError(TextBox textBox, string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBox.Focus();
            textBox.SelectAll();
        }

        private void LoadModel(string path)
        {
            if (currentModel != null)
            {
                helixViewport.Children.Remove(currentModel);
            }

            var importer = new ModelImporter
            {
                DefaultMaterial = new DiffuseMaterial(new SolidColorBrush(Colors.LightGray))
            };

            try
            {
                var model = importer.Load(path);
                currentModel = new ModelVisual3D { Content = model };
                helixViewport.Children.Add(currentModel);

                ModelOfAsteroid.Model = model;

                ShowModelInfo(model, path);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public void ShowModelInfo(Model3D model, string path)
        {
            var bounds = model.Bounds;
            

            // MeshGeometry3D
            int totalVertices = 0;
            int totalTriangles = 0;

            double totalSurfaceArea = 0;
            TraverseModel(model, ref totalVertices, ref totalTriangles, ref totalSurfaceArea);

            guna2GradientButton4.Text += ($"Info:\n");
            guna2GradientButton4.Text += ($"- Vertices: {totalVertices}\n\n");
            guna2GradientButton4.Text += ($"- Triangles: {totalTriangles}\n\n");
            guna2GradientButton4.Text += ($"- Total Surface: {totalSurfaceArea:F2} units²\n");

            AsteroidData.surfaceArea = totalSurfaceArea;
        }

        private void TraverseModel(Model3D model, ref int totalVertices, ref int totalTriangles, ref double totalSurfaceArea)
        {
            if (model is GeometryModel3D geometryModel)
            {
                if (geometryModel.Geometry is MeshGeometry3D mesh)
                {
                    totalVertices += mesh.Positions.Count;
                    if (mesh.TriangleIndices != null)
                    {
                        totalTriangles += mesh.TriangleIndices.Count / 3;

                        for (int i = 0; i < mesh.TriangleIndices.Count; i += 3)
                        {
                            var p0 = mesh.Positions[mesh.TriangleIndices[i]];
                            var p1 = mesh.Positions[mesh.TriangleIndices[i + 1]];
                            var p2 = mesh.Positions[mesh.TriangleIndices[i + 2]];

                            var a = (p1 - p0);
                            var b = (p2 - p0);
                            var cross = Vector3D.CrossProduct(a, b);
                            double area = cross.Length / 2.0;
                            totalSurfaceArea += area;
                        }
                    }
                }
            }
            else if (model is Model3DGroup group)
            {
                foreach (var child in group.Children)
                {
                    TraverseModel(child, ref totalVertices, ref totalTriangles,
                        ref totalSurfaceArea);
                }
            }
        }

        public static class AsteroidGenerator
        {

            public delegate double NoiseFn(double x, double y, double z);

            public static void GenerateAsteroid(string filePath, int resolution, double baseRadius, 
                float noiseAmplitude, float noiseFrequency, int numCraters, double astLengthX, double astLengthY, double astLengthZ, NoiseFn noise)
            {
                var random = new Random();
                var vertices = new List<Point3D>();
                var faces = new List<Tuple<int, int, int>>();

                int latSegments = resolution;
                int lonSegments = resolution;


                
                // craters on a model
                var craterCenters = new List<(Point3D center, double radius, double depth, double rimHeight)>();
                for (int i = 0; i < numCraters; i++)
                {
                    double theta = random.NextDouble() * Math.PI; // the whole (pi/2 when half)
                    double phi = random.NextDouble() * 2 * Math.PI;

                    double nx = Math.Sin(theta) * Math.Cos(phi);
                    double ny = Math.Cos(theta);
                    double nz = Math.Sin(theta) * Math.Sin(phi);

                    double radius = 0.1 + random.NextDouble() * 0.1;       
                    double depth = 0.04 + random.NextDouble() * 0.08;      
                    double rim = 0.01 + random.NextDouble() * 0.03; // height of border

                    craterCenters.Add((new Point3D(nx, ny, nz), radius, depth, rim));
                }

                for (int i = 0; i <= latSegments; i++)
                {
                    double theta = Math.PI * i / latSegments;
                    for (int j = 0; j <= lonSegments; j++)
                    {
                        double phi = 2 * Math.PI * j / lonSegments;

                        //form of asteroid
                        double nx = Math.Sin(theta) * Math.Cos(phi) * astLengthX;
                        double ny = Math.Cos(theta) * astLengthY;
                        double nz = Math.Sin(theta) * Math.Sin(phi) * astLengthZ;

                        var dir = new Point3D(nx, ny, nz);

                        //Noise3D - noise based on sines and cosines
                        //Noise3D * Noise3D(noiseFrequency * 2) - multiply the same function but with some noise

                        double n1 = noise(nx * noiseFrequency, ny * noiseFrequency, nz * noiseFrequency);
                        double n2 = noise(nx * noiseFrequency * 2, ny * noiseFrequency * 2, nz * noiseFrequency * 2);
                        double noiseVal = 1 + (n1 * n2) * noiseAmplitude;

                        foreach (var (center, radius, depth, rimHeight) in craterCenters)
                        {
                            double angle = Math.Acos(DotProduct(dir, center));
                            if (angle < radius)
                            {
                                double falloff = Math.Cos(Math.PI * angle / radius);
                                noiseVal -= depth * Math.Pow(falloff, 2);
                                if (angle > radius * 0.75)
                                    noiseVal += rimHeight * Math.Pow((angle / radius - 0.75) / 0.25, 2);
                            }
                        }

                        double x = nx * baseRadius * noiseVal;
                        double y = ny * baseRadius * noiseVal;
                        double z = nz * baseRadius * noiseVal;

                        vertices.Add(new Point3D(x, y, z));
                    }
                }

                for (int i = 0; i < latSegments; i++)
                {
                    for (int j = 0; j < lonSegments; j++)
                    {
                        int a = i * (lonSegments + 1) + j;
                        int b = a + 1;
                        int c = a + (lonSegments + 1);
                        int d = c + 1;

                        faces.Add(Tuple.Create(a + 1, c + 1, b + 1));
                        faces.Add(Tuple.Create(b + 1, c + 1, d + 1));
                    }
                }

                using (var sw = new StreamWriter(filePath))
                {
                    foreach (var v in vertices)
                        sw.WriteLine($"v {v.X.ToString(CultureInfo.InvariantCulture)} {v.Y.ToString(CultureInfo.InvariantCulture)} {v.Z.ToString(CultureInfo.InvariantCulture)}");

                    foreach (var f in faces)
                        sw.WriteLine($"f {f.Item1} {f.Item2} {f.Item3}");
                }
            }

            private static double DotProduct(Point3D a, Point3D b)
            {
                return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
            }

            public static double TrigNoise3D(double x, double y, double z)
            {
                return (
                    Math.Sin(x + 0.1 * Math.Sin(y + 0.1 * Math.Sin(z))) +
                    Math.Cos(y + 0.1 * Math.Cos(z + 0.1 * Math.Cos(x))) +
                    Math.Sin(z + 0.1 * Math.Sin(x + 0.1 * Math.Sin(y)))
                ) / 3.0;
            }
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            Close();
        }

    }

    public static class NoiseLib
    {
        // must match: (double x, double y, double z) -> double [-1;1]
        public static double Perlin3D(double x, double y, double z)
        {
            int X = (int)Math.Floor(x) & 255;
            int Y = (int)Math.Floor(y) & 255;
            int Z = (int)Math.Floor(z) & 255;

            x -= Math.Floor(x);
            y -= Math.Floor(y);
            z -= Math.Floor(z);

            double u = Fade(x);
            double v = Fade(y);
            double w = Fade(z);

            int A = p[X] + Y;
            int AA = p[A] + Z;
            int AB = p[A + 1] + Z;

            int B = p[X + 1] + Y;
            int BA = p[B] + Z;
            int BB = p[B + 1] + Z;

            double res = Lerp(w,
                Lerp(v,
                    Lerp(u, Grad(p[AA], x, y, z),
                            Grad(p[BA], x - 1, y, z)),
                    Lerp(u, Grad(p[AB], x, y - 1, z),
                            Grad(p[BB], x - 1, y - 1, z))
                ),
                Lerp(v,
                    Lerp(u, Grad(p[AA + 1], x, y, z - 1),
                            Grad(p[BA + 1], x - 1, y, z - 1)),
                    Lerp(u, Grad(p[AB + 1], x, y - 1, z - 1),
                            Grad(p[BB + 1], x - 1, y - 1, z - 1))
                )
            );

            return res; // result between [-1; 1]
        }

        //additional func
        private static double Fade(double t)
        {
            return t * t * t * (t * (t * 6 - 15) + 10);
        }

        private static double Lerp(double t, double a, double b)
        {
            return a + t * (b - a);
        }

        private static double Grad(int hash, double x, double y, double z)
        {
            int h = hash & 15;
            double u = h < 8 ? x : y;
            double v = h < 4 ? y : (h == 12 || h == 14 ? x : z);
            return ((h & 1) == 0 ? u : -u) +
                   ((h & 2) == 0 ? v : -v);
        }

        // classical Perlin-permutation array
        private static readonly int[] p = new int[512]
        { 151,160,137,91,90,15,
            131,13,201,95,96,53,194,233,7,225,140,36,103,30,
            69,142,8,99,37,240,21,10,23,
            190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,
            35,11,32,57,177,33,88,237,149,56,87,174,20,125,136,171,
            168, 68,175,74,165,71,134,139,48,27,166,77,146,158,231,
            83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,
            40,244,102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,
            187,208, 89,18,169,200,196,135,130,116,188,159,86,164,
            100,109,198,173,186, 3,64,52,217,226,250,124,123,5,202,
            38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,
            17,182,189,28,42,223,183,170,213,119,248,152, 2,44,154,
            163, 70,221,153,101,155,167, 43,172, 9,129,22,39,253, 19,
            98,108,110,79,113,224,232,178,185,112,104,218,246,97,
            228,251,34,242,193,238,210,144,12,191,179,162,241, 81,
            51,145,235,249,14,239,107, 49,192,214, 31,181,199,106,
            157,184, 84,204,176,115,121,50,45,127,  4,150,254,138,
            236,205, 93,222,114, 67,29,24,72,243,141,128,195,78,66,
            215,61,156,180,
            
            // second time - so as not to do mod 256
            151,160,137,91,90,15,
            131,13,201,95,96,53,194,233,7,225,140,36,103,30,
            69,142,8,99,37,240,21,10,23,
            190, 6,148,247,120,234,75,0,26,197,62,94,252,219,203,117,
            35,11,32,57,177,33,88,237,149,56,87,174,20,125,136,171,
            168, 68,175,74,165,71,134,139,48,27,166,77,146,158,231,
            83,111,229,122,60,211,133,230,220,105,92,41,55,46,245,
            40,244,102,143,54, 65,25,63,161, 1,216,80,73,209,76,132,
            187,208, 89,18,169,200,196,135,130,116,188,159,86,164,
            100,109,198,173,186, 3,64,52,217,226,250,124,123,5,202,
            38,147,118,126,255,82,85,212,207,206,59,227,47,16,58,
            17,182,189,28,42,223,183,170,213,119,248,152, 2,44,154,
            163, 70,221,153,101,155,167, 43,172, 9,129,22,39,253, 19,
            98,108,110,79,113,224,232,178,185,112,104,218,246,97,
            228,251,34,242,193,238,210,144,12,191,179,162,241, 81,
            51,145,235,249,14,239,107, 49,192,214, 31,181,199,106,
            157,184, 84,204,176,115,121,50,45,127,  4,150,254,138,
            236,205, 93,222,114, 67,29,24,72,243,141,128,195,78,66,
            215,61,156,180
        };

    }
}
