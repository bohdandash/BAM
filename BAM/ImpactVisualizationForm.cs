using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Globalization; // To correctly write dots in numbers
using System.Drawing;
using System.Windows.Forms;

namespace BAM
{
    public partial class ImpactVisualizationForm : Form
    {

        private GLControl glControl;

        private float[] vertices;
        private int[] indices;

        private int vao, vbo, ebo;
        private int shaderProgram;
        private int uMvpLocation;

        // camera parametrs
        private float cameraDistance = 6.0f;
        private float cameraYaw = 0.0f;
        private float cameraPitch = 20.0f;

        private Point lastMousePos;
        private bool rotating = false;

        private int Craterheight;
        private int Craterwidth;
        private int CraterAngle;

        public ImpactVisualizationForm(int Craterheight, int Craterwidth, int CraterAngle)
        {
            InitializeComponent();


            this.Craterheight = Craterheight;
            this.Craterwidth = Craterwidth;
            this.CraterAngle = CraterAngle;

            glControl = new GLControl(new GraphicsMode(32, 24, 0, 4))
            {
                Dock = DockStyle.Fill
            };

            panel1.Controls.Add(glControl);

            glControl.Load += GlControl_Load;
            glControl.Paint += GlControl_Paint;
            glControl.Resize += GlControl_Resize;

            // for mouse control
            glControl.MouseDown += GlControl_MouseDown;
            glControl.MouseUp += GlControl_MouseUp;
            glControl.MouseMove += GlControl_MouseMove;
            glControl.MouseWheel += GlControl_MouseWheel;

            guna2TextBox1.Text = @"Fall Angle =" + (CraterAngle) + "degrees";

        }

        private void GlControl_Load(object sender, EventArgs e)
        {
            GL.ClearColor(16f / 255f, 16f / 255f, 16f / 255f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            // mesh generation
            GenerateMesh(150, 150, Craterwidth / 10, Craterheight / 10, CraterAngle, 180);

            //anti-pattern?
            //SaveToObj("crater.obj");

            AddImpactArrow(Craterheight / 10, CraterAngle, 180,
               heightAbove: 5,
               shaftLen: Craterheight / 10 * 0.9f,
               shaftRad: Craterheight / 10 * 0.03f,
               headLen: Craterheight / 10 * 0.2f,
               headRad: Craterheight / 10 * 0.08f);

            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();
            ebo = GL.GenBuffer();

            GL.BindVertexArray(vao);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindVertexArray(0);

            // === shaders ===
            string vertexShaderSource = @"
    #version 330 core
    layout(location = 0) in vec3 aPosition;

    uniform mat4 uMVP;

    out vec3 FragPos;

    void main()
    {
        FragPos = aPosition;
        gl_Position = uMVP * vec4(aPosition, 1.0);
    }";

            string fragmentShaderSource = @"
    #version 330 core
    in vec3  FragPos;
    out vec4 FragColor;

    void main()
    {
        float t = clamp((FragPos.y + 2.0) / 4.0, 0.0, 1.0);

        vec3 lowColor = vec3(0.3, 0.2, 0.1);
        vec3 highColor = vec3(0.8, 0.7, 0.5);

        vec3 finalColor = mix(lowColor, highColor, t);
        FragColor = vec4(finalColor, 1.0); 
    }";

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);
            Console.WriteLine(GL.GetShaderInfoLog(vertexShader));

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);
            Console.WriteLine(GL.GetShaderInfoLog(fragmentShader));

            shaderProgram = GL.CreateProgram();
            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            uMvpLocation = GL.GetUniformLocation(shaderProgram, "uMVP");

            // IMPORTANT: enable the shader and pass the matrix for the first time
            GL.UseProgram(shaderProgram);

            var projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                (float)glControl.Width / glControl.Height, 0.1f, 100f);
            var view = Matrix4.LookAt(new Vector3(3, 3, 3), Vector3.Zero, Vector3.UnitY);
            var model = Matrix4.Identity;

            var mvp = model * view * projection;
            GL.UniformMatrix4(uMvpLocation, false, ref mvp);

            GL.UseProgram(0);
        }

        private void GlControl_Paint(object sender, PaintEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // matrix
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                (float)glControl.Width / glControl.Height,
                0.1f, 100f);

            // camera position in spherical coordinates
            float camX = cameraDistance * (float)(Math.Cos(MathHelper.DegreesToRadians(cameraPitch)) * Math.Cos(MathHelper.DegreesToRadians(cameraYaw)));
            float camY = cameraDistance * (float)(Math.Sin(MathHelper.DegreesToRadians(cameraPitch)));
            float camZ = cameraDistance * (float)(Math.Cos(MathHelper.DegreesToRadians(cameraPitch)) * Math.Sin(MathHelper.DegreesToRadians(cameraYaw)));

            Matrix4 view = Matrix4.LookAt(new Vector3(camX, camY, camZ), Vector3.Zero, Vector3.UnitY);
            Matrix4 model = Matrix4.Identity;

            Matrix4 mvp = model * view * projection;

            GL.UseProgram(shaderProgram);
            GL.UniformMatrix4(uMvpLocation, false, ref mvp);

            GL.BindVertexArray(vao);
            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
            GL.BindVertexArray(0);

            glControl.SwapBuffers();
        }

        private void GlControl_Resize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, glControl.Width, glControl.Height);
        }

        // ==== mouse control ====
        private void GlControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                rotating = true;
                lastMousePos = e.Location;
            }
        }

        private void GlControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                rotating = false;
        }

        private void GlControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (rotating)
            {
                float dx = e.X - lastMousePos.X;
                float dy = e.Y - lastMousePos.Y;

                cameraYaw += dx * 0.5f;
                cameraPitch -= dy * 0.5f;

                // limit the angle up/down
                cameraPitch = Math.Max(-89f, Math.Min(89f, cameraPitch));

                lastMousePos = e.Location;
                glControl.Invalidate();
            }
        }

        private void GlControl_MouseWheel(object sender, MouseEventArgs e)
        {
            cameraDistance -= e.Delta * 0.01f;
            if (cameraDistance < 1.0f) cameraDistance = 1.0f;
            if (cameraDistance > 50.0f) cameraDistance = 50.0f;
            glControl.Invalidate();
        }

        // impactAngleDeg — angle from the horizon (0° - almost sliding, 90° - vertical)
        // azimuthDeg     — flight direction ideally: 0° = +X, 90° = +Z
        private void GenerateMesh(int gridW, int gridH,
                          float craterDepth, float craterRadius,
                          float impactAngleDeg, float azimuthDeg)
        {
            vertices = new float[(gridW + 1) * (gridH + 1) * 3];
            indices = new int[gridW * gridH * 6];

            // impact geometry
            double alpha = Math.Max(0.0, Math.Min(90.0, impactAngleDeg)) * Math.PI / 180.0; // radian
            double azimuth = azimuthDeg * Math.PI / 180.0;

            double dirX = Math.Cos(azimuth);
            double dirZ = Math.Sin(azimuth);

            double strength = Math.Pow(Math.Cos(alpha), 1.2); // 0..1
            double stretchDown = 1.0 + 0.7 * strength;           // stretching along the flight
            double stretchCross = 1.0 - 0.25 * (stretchDown - 1.0);
            double floorSlope = 0.15 * strength * (craterDepth / craterRadius);
            double rimAsym = 0.8 * strength;

            int v = 0;
            for (int z = 0; z <= gridH; z++)
            {
                for (int x = 0; x <= gridW; x++)
                {
                    double xf = (x - gridW / 2.0) * 0.2;
                    double zf = (z - gridH / 2.0) * 0.2;

                    // rotation in (u,w)
                    double u = xf * dirX + zf * dirZ;         // along the flight
                    double w = -xf * dirZ + zf * dirX;        // across

                    // elliptical radius
                    double rEll = Math.Sqrt(
                        (u / stretchDown) * (u / stretchDown) +
                        (w / stretchCross) * (w / stretchCross)
                    );

                    double distPlanar = Math.Max(1e-6, Math.Sqrt(u * u + w * w));
                    double cosPhi = u / distPlanar;           // +1 up, -1 down

                    double y;
                    if (rEll < craterRadius)
                    {
                        double t = rEll / craterRadius;       // 0..1
                        y = -(1.0 - t * t) * craterDepth;     // U-form
                        y += -u * floorSlope;                 // bottom slope
                    }
                    else if (rEll < craterRadius * 1.35)
                    {
                        double t = (rEll - craterRadius) / (craterRadius * 0.35); // 0..1
                        double baseRim = (1.0 - t * t) * (craterDepth * 0.05);

                        double asymMul = 1.0 + rimAsym * cosPhi;
                        if (alpha <= 12.0 * Math.PI / 180.0 && cosPhi < -0.2) asymMul *= 0.2;

                        y = baseRim * asymMul;
                    }
                    else
                    {
                        y = 0.0;
                    }

                    vertices[v++] = (float)xf;
                    vertices[v++] = (float)y;
                    vertices[v++] = (float)zf;
                }
            }

            int i = 0;
            for (int z = 0; z < gridH; z++)
            {
                for (int x = 0; x < gridW; x++)
                {
                    int topLeft = z * (gridW + 1) + x;
                    int topRight = topLeft + 1;
                    int bottomLeft = topLeft + (gridW + 1);
                    int bottomRight = bottomLeft + 1;

                    indices[i++] = topLeft; indices[i++] = bottomLeft; indices[i++] = topRight;
                    indices[i++] = topRight; indices[i++] = bottomLeft; indices[i++] = bottomRight;
                }
            }
        }

        // === IN ADDITION: adding to dynamic buffers ===
        static void AddTri(ref List<float> v, ref List<int> i,
                           (float x, float y, float z) a,
                           (float x, float y, float z) b,
                           (float x, float y, float z) c,
                           int baseIndex)
        {
            v.AddRange(new float[] { a.x, a.y, a.z, b.x, b.y, b.z, c.x, c.y, c.z });
            i.Add(baseIndex); i.Add(baseIndex + 1); i.Add(baseIndex + 2);
        }

        static (float x, float y, float z) TransformPoint(
            float lx, float ly, float lz,
            // basis: right, up, forward (columnwise)
            (float x, float y, float z) right,
            (float x, float y, float z) up,
            (float x, float y, float z) fwd,
            (float x, float y, float z) pos)
        {
            float wx = right.x * lx + up.x * ly + fwd.x * lz + pos.x;
            float wy = right.y * lx + up.y * ly + fwd.y * lz + pos.y;
            float wz = right.z * lx + up.z * ly + fwd.z * lz + pos.z;
            return (wx, wy, wz);
        }

        static (float x, float y, float z) Normalize((float x, float y, float z) v)
        {
            float len = (float)Math.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
            return (v.x / len, v.y / len, v.z / len);
        }

        static (float x, float y, float z) Cross((float x, float y, float z) a,
                                               (float x, float y, float z) b)
        {
            return (a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

        // === BUILDING AND ORIENTING THE ARROW ===
        // impactAngleDeg — from the horizon (0° - almost sliding, 90° - vertically downwards)
        // azimuthDeg — 0°=+X, 90°=+Z
        void AddImpactArrow(float craterRadius, float impactAngleDeg, float azimuthDeg,
                            float heightAbove = 0.9f,   // height above the center
                            float shaftLen = 1.2f,
                            float shaftRad = 0.03f,
                            float headLen = 0.25f,
                            float headRad = 0.10f,
                            int segments = 16)
        {
            // convert the original arrays into lists
            var vList = new List<float>(vertices);
            var iList = new List<int>(indices);
            int startVertCount = vList.Count / 3;

            // === FLIGHT DIRECTION ===
            double a = Math.Max(0.0, Math.Min(90.0, impactAngleDeg)) * Math.PI / 180.0;
            double az = azimuthDeg * Math.PI / 180;

            // horizontal projection
            float dirX = (float)Math.Cos(az);
            float dirZ = (float)Math.Sin(az);
            float ch = (float)Math.Cos(a);
            float sh = (float)Math.Sin(a);

            // the forward vector (where it falls) is slightly down along the Y axis.
            var fwd = Normalize((ch * dirX, -sh, ch * dirZ));

            // ortho-normal basis for local geometry rotation (right, up, fwd)
            var worldUp = (0f, 1f, 0f);
            var right = Normalize(Cross(worldUp, fwd));
            var up = Normalize(Cross(fwd, right));

            // the position of the arrow base is above the center of the crater (0,0,0)
            var basePos = (0f, heightAbove + craterRadius * 0.1f, 0f);

            // === GEOMETRY: cylinder (rod) + cone (tip), along local +Z ===
            // rod: two circles z=[0, shaftLen]
            int baseIndex = vList.Count / 3;
            for (int s = 0; s < segments; s++)
            {
                double t0 = 2.0 * Math.PI * s / segments;
                double t1 = 2.0 * Math.PI * (s + 1) / segments;
                float x0 = (float)Math.Cos(t0) * shaftRad, y0 = (float)Math.Sin(t0) * shaftRad;
                float x1 = (float)Math.Cos(t1) * shaftRad, y1 = (float)Math.Sin(t1) * shaftRad;

                // 4 side wall points in local coordinates
                var p00 = TransformPoint(x0, y0, 0, right, up, fwd, basePos);
                var p01 = TransformPoint(x1, y1, 0, right, up, fwd, basePos);
                var p10 = TransformPoint(x0, y0, shaftLen, right, up, fwd, basePos);
                var p11 = TransformPoint(x1, y1, shaftLen, right, up, fwd, basePos);

                // two triangles
                AddTri(ref vList, ref iList, p00, p10, p01, baseIndex); baseIndex += 3;
                AddTri(ref vList, ref iList, p01, p10, p11, baseIndex); baseIndex += 3;
            }

            // Tip: cone from z=shaftLen to z=shaftLen+headLen, apex at the end
            var tip = TransformPoint(0, 0, shaftLen + headLen, right, up, fwd, basePos);
            for (int s = 0; s < segments; s++)
            {
                double t0 = 2.0 * Math.PI * s / segments;
                double t1 = 2.0 * Math.PI * (s + 1) / segments;
                float x0 = (float)Math.Cos(t0) * headRad, y0 = (float)Math.Sin(t0) * headRad;
                float x1 = (float)Math.Cos(t1) * headRad, y1 = (float)Math.Sin(t1) * headRad;

                var b0 = TransformPoint(x0, y0, shaftLen, right, up, fwd, basePos);
                var b1 = TransformPoint(x1, y1, shaftLen, right, up, fwd, basePos);

                // sidewall of the cone
                AddTri(ref vList, ref iList, b0, tip, b1, baseIndex); baseIndex += 3;
            }

            // (optional) a small arc showing the angle from the horizon
            // the center of the arc is the same, radius = shaftLen*0.35
            float arcR = shaftLen * 0.35f;
            int arcSeg = 20;
            var horizDir = Normalize((dirX, 0f, dirZ));      // horizontal projection
            var arcRight = Normalize(Cross(worldUp, horizDir));
            var arcUp = worldUp;
            var arcBase = basePos; // above the center
            (float x, float y, float z) prev = TransformPoint(arcR, 0, 0, arcRight, arcUp, horizDir, arcBase); // local axis: right=x, up=y, fwd=horizDir=z
            for (int s = 1; s <= arcSeg; s++)
            {
                double t = a * s / arcSeg;                   // from 0 to angle a (rad)
                                                             // rotate the point in the plane (right/up) along an arc
                float x = arcR * (float)Math.Cos(t);
                float y = arcR * (float)Math.Sin(t);
                var cur = TransformPoint(x, y, 0, arcRight, arcUp, horizDir, arcBase);

                // a thin "ribbon" of an arc - like a narrow triangle (almost like a line)
                float w = arcR * 0.03f;
                var off = Normalize(Cross(horizDir, Normalize((cur.x - prev.x, cur.y - prev.y, cur.z - prev.z))));
                var p0 = (prev.x - off.x * w, prev.y - off.y * w, prev.z - off.z * w);
                var p1 = (prev.x + off.x * w, prev.y + off.y * w, prev.z + off.z * w);
                var p2 = (cur.x + off.x * w, cur.y + off.y * w, cur.z + off.z * w);
                var p3 = (cur.x - off.x * w, cur.y - off.y * w, cur.z - off.z * w);

                AddTri(ref vList, ref iList, p0, p2, p1, baseIndex); baseIndex += 3;
                AddTri(ref vList, ref iList, p0, p3, p2, baseIndex); baseIndex += 3;

                prev = cur;
            }

            // update arrays
            vertices = vList.ToArray();
            indices = iList.ToArray();
        }


        private void ReturnButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // optimizing gpu resources when closing the window
            if (shaderProgram != 0)
            {
                GL.DeleteProgram(shaderProgram);
            }

            if (vao != 0)
            {
                GL.DeleteVertexArray(vao);
            }

            if (vbo != 0)
            {
                GL.DeleteBuffer(vbo);
            }

            if (ebo != 0)
            {
                GL.DeleteBuffer(ebo);
            }

            base.OnFormClosing(e);
        }

        private void BtnExportObj_Click(object sender, EventArgs e)
        {
            if (vertices == null || vertices.Length == 0)
            {
                MessageBox.Show("No generated crater mesh to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Wavefront 3D Object (*.obj)|*.obj|All files (*.*)|*.*";
                sfd.Title = "Export Crater 3D Model";
                sfd.FileName = $"crater_a{CraterAngle}_d{Craterheight}.obj";
                sfd.RestoreDirectory = true;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    SaveToObj(sfd.FileName);
                    MessageBox.Show("Crater mesh successfully saved.", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void SaveToObj(string filePath)
        {
            // using StringBuilder to quickly generate text
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("# Crater Mesh Generated by C#");
            sb.AppendLine($"# Vertices: {vertices.Length / 3}");
            sb.AppendLine($"# Faces: {indices.Length / 3}");

            // 1. write the vertices (v x y z)
            // the array vertices is [x0, y0, z0, x1, y1, z1...], the step is +3
            for (int i = 0; i < vertices.Length; i += 3)
            {
                // CultureInfo.InvariantCulture for "."
                float x = vertices[i];
                float y = vertices[i + 1]; // Y - высота в вашем коде
                float z = vertices[i + 2];

                sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "v {0} {1} {2}", x, y, z));
            }

            // 2. write the faces/triangles (f v1 v2 v3)
            for (int i = 0; i < indices.Length; i += 3)
            {
                // IMPORTANT: in OBJ indices start with 1
                int idx1 = indices[i] + 1;
                int idx2 = indices[i + 1] + 1;
                int idx3 = indices[i + 2] + 1;

                sb.AppendLine($"f {idx1} {idx2} {idx3}");
            }

            // 3. save
            try
            {
                File.WriteAllText(filePath, sb.ToString());
                MessageBox.Show($"Saved: {filePath}");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Failed to export mesh: {ex.Message}", "File I/O Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
