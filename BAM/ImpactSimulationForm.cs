using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static BAM.Program;
using TextBox = System.Windows.Forms.TextBox;


namespace BAM
{
    public partial class ImpactSimulationForm : Form
    {
        
        public ImpactSimulationForm()
        {
            InitializeComponent();
            
            chart1.Visible = false;
            chart2.Visible = false;
            chart3.Visible = false;
            
            guna2GradientButton4.Visible = false;
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StartSimulationButton_Click(object sender, EventArgs e)
        {
            chart1.Visible = true;
            chart2.Visible = true;
            chart3.Visible = true;
            
            guna2GradientButton4.Visible = true;

            chart1.Series.Clear();

            // different metrics to display
            var velocitySeries = new Series("Velocity")
            {
                ChartType = SeriesChartType.Point,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 4,
                Color = System.Drawing.Color.FromArgb(255, 0, 0) // Red
            };

            var heightSeries = new Series("Height")
            {
                ChartType = SeriesChartType.Point,
                MarkerStyle = MarkerStyle.Diamond,
                MarkerSize = 4,
                Color = System.Drawing.Color.FromArgb(0, 0, 255)// Blue
                
            };

            var temperatureSeries = new Series("Temperature")
            {
                ChartType = SeriesChartType.Point,
                MarkerStyle = MarkerStyle.Triangle,
                MarkerSize = 4,
                Color = System.Drawing.Color.FromArgb(255, 165, 0) // Orange
            };

            // add metrics
            chart1.Series.Add(velocitySeries);
            chart1.Series.Add(heightSeries);
            chart1.Series.Add(temperatureSeries);

            // diagramm
            chart1.ChartAreas[0].AxisX.Title = "Time (s)";
            chart1.ChartAreas[0].AxisY.Title = "Value";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;

            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(128, 128, 128); // Gray
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(128, 128, 128); // Gray

            chart1.ChartAreas[0].AxisX.TitleForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.TitleForeColor = Color.White;
            chart1.ChartAreas[0].AxisY2.TitleForeColor = Color.White;



            chart1.ChartAreas[0].BackColor = Color.FromArgb(16, 16, 16);
            chart1.ChartAreas[0].BorderColor = Color.FromArgb(16, 16, 16);
            chart1.Legends[0].BackColor = Color.FromArgb(16, 16, 16);
            chart1.Legends[0].ForeColor = Color.White;

            // for temperature
            chart1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            chart1.ChartAreas[0].AxisY2.Title = "Temperature (K)";
            chart1.ChartAreas[0].AxisY2.LabelStyle.Format = "N0";

            chart1.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Gray;

            temperatureSeries.YAxisType = AxisType.Secondary;

            chart1.Series[0]["DrawingStyle"] = "Cylinder"; // it will be smooth, but it's not quite a radius


            // legend
            chart1.Legends.Add(new Legend("SimulationLegend"));
            var legend = chart1.Legends[0];
            legend.Docking = Docking.Bottom;                 // Top / Bottom / Left / Right
            legend.Alignment = StringAlignment.Center;        // center alignment
            legend.LegendStyle = LegendStyle.Row;             // in one line (or Table/Column)
            legend.IsDockedInsideChartArea = false;           // outside the graph area
            legend.DockedToChartArea = "ChartArea1";


            double asteroidMass, asteroidRadius, asteroidDensity, initialHeight, initialVelocity;
            double entryAngleDeg;

            if (checkBox1.Checked == true) // when User Preferences:
            {
                double ReadPositiveDouble(TextBox textBox, string parameterName)
                {
                    double value;
                    while (true)
                    {
                        if (!double.TryParse(textBox.Text, out value) || value <= 0)
                        {
                            MessageBox.Show($"The value of {parameterName} should be > 0");
                            textBox.Focus();
                            textBox.SelectAll();
                            return double.NaN;
                        }
                        return value;
                    }
                }

                double ReadAngleValue(TextBox textBox, string angleValue)
                {
                    double angleEntry;
                    while(true)
                    {
                        if(!double.TryParse(textBox.Text, out angleEntry) || angleEntry <= 0 || angleEntry >= 90)
                        {
                            MessageBox.Show($"The value of {angleValue} should be > 0 but less than 90");
                            textBox.Focus();
                            textBox.SelectAll();
                            return double.NaN; // throw an exception
                        }

                        return angleEntry;
                    }
                    
                }

                // checkup
                asteroidMass = ReadPositiveDouble(textBox1, "Atseroid's weight"); // kg
                asteroidRadius = ReadPositiveDouble(textBox2, "Atseroid's radius"); // m
                asteroidDensity = ReadPositiveDouble(textBox3, "Atseroid's density"); // kg/m³
                initialHeight = ReadPositiveDouble(textBox4, "Initial height"); // m 
                initialVelocity = ReadPositiveDouble(textBox5, "Initial velocity"); // m/s
                entryAngleDeg = ReadAngleValue(textBox6, "Entry angle");

                // if all are correct?
                if (double.IsNaN(asteroidMass) || double.IsNaN(asteroidRadius) ||
                    double.IsNaN(asteroidDensity) || double.IsNaN(initialHeight) ||
                    double.IsNaN(initialVelocity) || double.IsNaN(entryAngleDeg))
                {
                    // If even one value is incorrect, we do not continue.
                    return;
                }

                if (asteroidMass < 100 || asteroidMass > 50000000)
                {
                    MessageBox.Show("Wrong value. Try again.",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Focus();
                    return; 
                }

                if (asteroidRadius < 3 || asteroidRadius > 50)
                {
                    MessageBox.Show("Wrong value. Try again.",
                                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return; 
                }

                if (asteroidDensity < 0)
                {
                    MessageBox.Show("Wrong value. Try again.",
                                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox3.Focus();
                    return; 
                }

                if (initialHeight < 1 || initialHeight > 300000000)
                {
                    MessageBox.Show("Wrong value. Try again.",
                                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox4.Focus();
                    return; 
                }

                if (initialVelocity < 10 || initialVelocity > 100)
                {
                    MessageBox.Show("Wrong value. Try again.",
                                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox5.Focus();
                    return; 
                }

                if (entryAngleDeg < 1 || entryAngleDeg > 90)
                {
                    MessageBox.Show("Wrong value. Try again.",
                                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox6.Focus();
                    return; 
                }

                
            }

            else
            {
                // Asteeroid's Parametrs
                asteroidMass = 10000000; // kg
                asteroidRadius = 55.0; // m
                asteroidDensity = 30000; // kg/m³ typical for stone bodies)
                initialHeight = 70000; // m 
                initialVelocity = 100;
                entryAngleDeg = 70;
            }

            // if the model has already been created, then try to get the size and mass
            if (ModelOfAsteroid.Model != null)
            {
                var bounds = ModelOfAsteroid.Model.Bounds;
                double avgSize = AsteroidData.surfaceArea;
                asteroidRadius = AsteroidData.radius;
                asteroidMass = (4.0 / 3.0) * Math.PI * Math.Pow(asteroidRadius, 3) * asteroidDensity;
            }

            // values for Simulation
            // m/s
            double timeStep = 0.5;         // s the less - the smoother
            double simulationTime = 200;   // s 

            // const
            double earthRadius = 6371000;  // m
            double earthMass = 5.972e24;   // kg
            double gravitationalConstant = 6.674e-11; // m³/(kg·s²)
            double airDensitySeaLevel = 1.225; // kg/m³
            double dragCoefficient = 0.47; // for objects that are +- spherical

            // thermal values
            double initialTemperature = 273; // K 
            double ablationTemperature = 1600; // K at what temperature will melting begin
            double heatCapacity = 800; // J/(kg·K)
            double stefanBoltzmann = 5.67e-8; // W/(m²·K⁴)
            double emissivity = 0.9; // dimensionless

            // variables
            double height = initialHeight;
            double velocity = initialVelocity;
            double temperature = initialTemperature;
            double mass = asteroidMass;
            double radius = asteroidRadius;
            double crossSectionalArea = Math.PI * radius * radius;

            // to output the final info
            double maxVelocity = velocity;
            double maxTemperature = temperature;
            double impactVelocity = 0;
            double impactTime = 0;


            // what initial velocity consist of 
            double vx = initialVelocity * Math.Cos(entryAngleDeg * Math.PI / 180.0);
            double vy = -initialVelocity * Math.Sin(entryAngleDeg * Math.PI / 180.0); // down

            double horizontal = 0;

            // where to save info
            List<double> timePoints = new List<double>();
            List<double> velocityPoints = new List<double>();
            List<double> heightPoints = new List<double>();
            List<double> temperaturePoints = new List<double>();

            // Simulation
            for (double time = 0; time < simulationTime && height > 0; time += timeStep)
            {
                // 1. Renewal of height from the sea level
                double altitudeFromCenter = earthRadius + height;

                // 2. gravity acceleration (varies depending on altitude)
                double gravitationalAcceleration = gravitationalConstant * earthMass / (altitudeFromCenter * altitudeFromCenter);

                // 3. wind thickness at current altitude (exponential atmospheric model)
                double scaleHeight = 8500; // m
                double airDensity = airDensitySeaLevel * Math.Exp(-height / scaleHeight);

                double v = Math.Sqrt(vx * vx + vy * vy);

                // drag force
                double dragForce = 0.5 * airDensity * dragCoefficient * crossSectionalArea * v * v;
                double dragFx = dragForce * (vx / v);
                double dragFy = dragForce * (vy / v);

                // 5. acceleration
                double ax = -dragFx / mass;
                double ay = -gravitationalAcceleration - dragFy / mass;

                // 6. updating the speed and position
                vx += ax * timeStep;
                vy += ay * timeStep;

                // Coordinates update
                horizontal += vx * timeStep;
                height += vy * timeStep;

                // 7. warming up calculation
                //kineticall power converted into heat
                double kineticHeatingPower = 0.5 * airDensity * Math.Pow(velocity, 3) * crossSectionalArea * 0.1; // 10% kinetic energy transfers from thermal energy

                // Radiation cooling (Stefan-Boltzmann law)
                double surfaceArea = 4 * Math.PI * radius * radius;
                double radiativeCoolingPower = emissivity * stefanBoltzmann * surfaceArea * Math.Pow(temperature, 4);

                // pure thermal tension
                double netHeatPower = kineticHeatingPower - radiativeCoolingPower;

                // temperature change
                double temperatureChange = netHeatPower / (mass * heatCapacity) * timeStep;
                temperature += temperatureChange;

                // 8. when the temperature exceeds the threshold value
                if (temperature > ablationTemperature)
                {
                    // ruin model
                    double ablationRate = 0.001 * (temperature - ablationTemperature) * timeStep;
                    double volumeReduction = ablationRate * crossSectionalArea;

                    // 1% is the max value for one step
                    double volumeLoss = Math.Min(volumeReduction, (4.0 / 3.0) * Math.PI * Math.Pow(radius, 3) * 0.01);

                    //update time
                    mass -= volumeLoss * asteroidDensity;

                    // update radius from mass
                    radius = Math.Pow((3 * mass) / (4 * Math.PI * asteroidDensity), 1.0 / 3.0);
                    crossSectionalArea = Math.PI * radius * radius;

                    // ablation the heat during ruining
                    temperature -= ablationRate * 1000 / (mass * heatCapacity);
                }

                // 9. max. significance
                if (Math.Abs(velocity) > maxVelocity) maxVelocity = Math.Abs(velocity);
                if (temperature > maxTemperature) maxTemperature = temperature;

                // saving the points while th simulation is on
                if (time % 2 < timeStep || height <= 0)
                {
                    timePoints.Add(time);
                    velocityPoints.Add(Math.Abs(velocity));
                    heightPoints.Add(height);
                    temperaturePoints.Add(temperature);

                    // + points to model
                    velocitySeries.Points.AddXY(time, Math.Abs(velocity));
                    heightSeries.Points.AddXY(time, Math.Max(0, height)); // Ensure non-negative
                    temperatureSeries.Points.AddXY(time, temperature);

                    // saving info if impact
                    if (height <= 0 && impactTime == 0)
                    {
                        impactTime = time;
                        impactVelocity = Math.Sqrt(vx * vx + vy * vy);
                    }
                }
            }

            // convert impact into TNT
            double impactEnergy = 0.5 * mass * impactVelocity * impactVelocity;
            double tntEquivalent = impactEnergy / 4.184e9; // 1 ton of TNT = 4.184 GJ

            // scale 
            chart1.ChartAreas[0].RecalculateAxesScale();

            // the results
            guna2GradientButton4.Text += ($"Initial parameters:\n");
            guna2GradientButton4.Text += ($"- Mass: {asteroidMass:N0} kg\n");
            guna2GradientButton4.Text += ($"- Radius: {asteroidRadius:N2} m\n");
            guna2GradientButton4.Text += ($"- Initial height: {initialHeight:N0} m\n");
            guna2GradientButton4.Text += ($"- Initial speed: {initialVelocity:N2} m/s\n\n");

            guna2GradientButton4.Text += ($"Result:\n");
            guna2GradientButton4.Text += ($"- Impact time: {impactTime:N2} seconds\n");
            guna2GradientButton4.Text += ($"- Maximum speed: {impactVelocity:N0} m/s ({impactVelocity * 3.6:N0} km/h)\n");
            guna2GradientButton4.Text += ($"- Maximum temperature: {maxTemperature:N0} K ({maxTemperature - 273.15:N0}°C)\n");
            guna2GradientButton4.Text += ($"- Speed ​​at impact: {impactVelocity:N0} m/s ({impactVelocity * 3.6:N0} km/h)\n");
            guna2GradientButton4.Text += ($"- Impact energy: {tntEquivalent:N2} tons of TNT equivalent\n\n");

            // +- what to expect
            guna2GradientButton4.Text += ($"Damage:\n");
            if (tntEquivalent < 100)
                guna2GradientButton4.Text += ("- Minor local damage. Similar to a small explosion of conventional explosives.");
            else if (tntEquivalent < 10000)
                guna2GradientButton4.Text += ("- Serious local destruction. Compared to the Tunguska event in 1908 (forest destruction).");
            else if (tntEquivalent < 1000000)
                guna2GradientButton4.Text += ("- Regional disaster. A city-sized area of ​​destruction.");
            else
                guna2GradientButton4.Text += ("- A global catastrophe. Similar to the impact that led to the extinction of the dinosaurs.");

            MessageBox.Show("The simulation is complete! The results are displayed on the graph and in the text box.");

            if (tntEquivalent < 1)
            { 
                guna2GradientButton4.Text += ("- Minor impact. The meteorite will likely disintegrate completely in the atmosphere."); 
                chart2.Visible = false; 
                chart3.Visible = false; 
            }
            else
            {
                SetupCraterVisualization(tntEquivalent * 4.184e9, impactVelocity, radius);
            }

                int angle = (int)Math.Round(entryAngleDeg);

            CraterData.CraterEntryAngle = angle;
        }

        private void SetupCraterVisualization(double impactEnergy, double impactVelocity, double asteroidRadius)
        {
            chart2.Series.Clear();
            chart3.Series.Clear();

            // calculation using formulas from sources 
            // using simplified scaling laws for crater formation

            // Depth and diameter are proportional to impact energy and velocity
            double craterDiameter = 2 * asteroidRadius * Math.Pow(impactEnergy / 1e9, 0.25) * Math.Max(1, Math.Log10(impactVelocity / 100));
            double craterDepth = craterDiameter * 0.2; // typical depth to diameter ratio
            double rimHeight = craterDepth * 0.3; // typical rim height relative to depth
            double ejectaBlanketRadius = craterDiameter * 1.5; // the ejection usually extends beyond the crater

            // charts
            var craterProfileSeries = new Series("Crater profile")
            {
                ChartType = SeriesChartType.Line,
                Color = System.Drawing.Color.Brown,
                BorderWidth = 2
            };

            var groundLevelSeries = new Series("Sea level")
            {
                ChartType = SeriesChartType.Line,
                Color = System.Drawing.Color.Black,
                BorderWidth = 1,
                BorderDashStyle = ChartDashStyle.Dash
            };

            // top
            var craterTopViewSeries = new Series("Crater border")
            {
                ChartType = SeriesChartType.Point,
                MarkerStyle = MarkerStyle.Circle,
                MarkerSize = 5,
                Color = System.Drawing.Color.Brown
            };

            var craterCenterSeries = new Series("Point of impact")
            {
                ChartType = SeriesChartType.Point,
                MarkerStyle = MarkerStyle.Cross,
                MarkerSize = 10,
                MarkerColor = System.Drawing.Color.Red
            };

            var ejectaSeries = new Series("Ejection")
            {
                ChartType = SeriesChartType.Point,
                MarkerStyle = MarkerStyle.Triangle,
                MarkerSize = 3,
                Color = System.Drawing.Color.SandyBrown
            };

            // dots for side view
            double resolution = 100;
            double maxDistance = ejectaBlanketRadius * 1.2;

            // land line
            groundLevelSeries.Points.AddXY(-maxDistance, 0);
            groundLevelSeries.Points.AddXY(maxDistance, 0);

            // crater 
            for (double x = -maxDistance; x <= maxDistance; x += maxDistance / resolution)
            {
                double y;
                double distFromCenter = Math.Abs(x);

                if (distFromCenter <= craterDiameter / 2)
                {
                    // crater parabola
                    double normalized = distFromCenter / (craterDiameter / 2);
                    double baseParabola = (1 - normalized * normalized);
                    double curveNoise = 1 + 0.1 * Math.Sin(distFromCenter * 5) + 0.05 * Math.Cos(distFromCenter * 7);
                    y = -craterDepth * baseParabola * curveNoise;



                    // central "mountain" for large craters
                    if (craterDiameter > 1000 && distFromCenter < craterDiameter * 0.1)
                    {
                        double hill = craterDepth * 0.3 * (1 - normalized / 0.1);
                        y += hill;
                    }
                }
                else if (distFromCenter <= craterDiameter * 0.6)
                {
                    // crater rim
                    double rimNorm = (distFromCenter - craterDiameter / 2) / (craterDiameter * 0.1);
                    y = rimHeight * Math.Max(0, 1 - rimNorm * rimNorm);

                    // slight "roughness" of the shaft
                    y += (new Random().NextDouble() - 0.5) * rimHeight * 0.05;
                }
                else if (distFromCenter <= ejectaBlanketRadius)
                {
                    // zone of ejection
                    double normalizedDist = (distFromCenter - craterDiameter * 0.6) / (ejectaBlanketRadius - craterDiameter * 0.6);
                    y = rimHeight * 0.3 * Math.Max(0, 1 - normalizedDist);
                }
                else
                {
                    y = 0;
                }

                craterProfileSeries.Points.AddXY(x, y);
            }

            // dots
            int circlePoints = 60;

            // center of impact
            craterCenterSeries.Points.AddXY(0, 0);

            // how to make a crater
            // 1. random before the loop
            Random random1 = new Random();

            // an array to store random noise to smooth it out (optional, but looks better)

            for (int i = 0; i <= circlePoints; i++) //  <= to connect the last point with the first
            {
                double angle = 2 * Math.PI * i / circlePoints;

                // 2. noise formula
                double noise = 1.0
                    // basic waviness (4 "petals" or bulges)
                    + 0.05 * Math.Sin(4 * angle)
                    // asymmetry (shift to one side). Multiplier 1 to close the loop!
                    + 0.03 * Math.Cos(angle)
                    // random noise. (NextDouble() - 0.5) gives the range [-0.5; 0.5]
                    + (random1.NextDouble() - 0.5) * 0.08;

                double R = (craterDiameter / 2) * noise;

                double x = R * Math.Cos(angle);
                double y = R * Math.Sin(angle);

                craterTopViewSeries.Points.AddXY(x, y);
            }

            // ejection
            Random random = new Random(42);
            int ejectaPoints = 300;

            for (int i = 0; i < ejectaPoints; i++)
            {
                double angle = 2 * Math.PI * random.NextDouble();
                double distance = craterDiameter / 2 + (ejectaBlanketRadius - craterDiameter / 2) * random.NextDouble();

                // different distance
                distance *= 0.8 + 0.4 * random.NextDouble();

                double x = distance * Math.Cos(angle);
                double y = distance * Math.Sin(angle);

                ejectaSeries.Points.AddXY(x, y);
            }

            // all charts
            chart2.Series.Add(craterProfileSeries);
            chart2.Series.Add(groundLevelSeries);

            chart3.Series.Add(craterTopViewSeries);
            chart3.Series.Add(craterCenterSeries);
            chart3.Series.Add(ejectaSeries);

            // side profile
            chart2.ChartAreas[0].AxisX.Title = "Distance from center (m)";
            chart2.ChartAreas[0].AxisY.Title = "Depth (m)";
            chart2.ChartAreas[0].AxisX.LabelStyle.Format = "N0";
            chart2.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            chart2.ChartAreas[0].AxisY.IsReversed = false; // Positive is up
            chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chart2.Titles.Add(new Title("Side profile", Docking.Top,Font,Color.White));

            chart2.ChartAreas[0].BackColor = Color.FromArgb(16, 16, 16);
            chart2.ChartAreas[0].BorderColor = Color.FromArgb(16, 16, 16);

            chart2.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart2.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;

            chart2.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(128, 128, 128); // Gray
            chart2.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(128, 128, 128); // Gray

            chart2.ChartAreas[0].AxisX.TitleForeColor = Color.White;
            chart2.ChartAreas[0].AxisY.TitleForeColor = Color.White;
            chart2.ChartAreas[0].AxisY2.TitleForeColor = Color.White;

            // top view
            chart3.ChartAreas[0].AxisX.Title = "Distance along X (m)";
            chart3.ChartAreas[0].AxisY.Title = "Distance along У (m)";
            chart3.ChartAreas[0].AxisX.LabelStyle.Format = "N0";
            chart3.ChartAreas[0].AxisY.LabelStyle.Format = "N0";
            chart3.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chart3.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chart3.ChartAreas[0].AxisX.Interval = Math.Ceiling(craterDiameter / 4);
            chart3.ChartAreas[0].AxisY.Interval = Math.Ceiling(craterDiameter / 4);
            chart3.Titles.Add(new Title("Top view:", Docking.Top, Font, Color.White));

            // align all the graphs 
            chart3.ChartAreas[0].AxisX.Minimum = -ejectaBlanketRadius * 1.1;
            chart3.ChartAreas[0].AxisX.Maximum = ejectaBlanketRadius * 1.1;
            chart3.ChartAreas[0].AxisY.Minimum = -ejectaBlanketRadius * 1.1;
            chart3.ChartAreas[0].AxisY.Maximum = ejectaBlanketRadius * 1.1;

            chart3.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart3.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;

            chart3.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(128, 128, 128); // Gray
            chart3.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(128, 128, 128); // Gray

            chart3.ChartAreas[0].AxisX.TitleForeColor = Color.White;
            chart3.ChartAreas[0].AxisY.TitleForeColor = Color.White;
            chart3.ChartAreas[0].AxisY2.TitleForeColor = Color.White;

            chart3.ChartAreas[0].BackColor = Color.FromArgb(16, 16, 16);
            chart3.ChartAreas[0].BorderColor = Color.FromArgb(16, 16, 16);


            chart3.PostPaint += (sender, e) => {
                var chartArea = chart3.ChartAreas[0];
                if (chartArea.Position.Width > chartArea.Position.Height)
                {
                    var diff = chartArea.Position.Width - chartArea.Position.Height;
                    chartArea.Position.X += diff / 2;
                    chartArea.Position.Width = chartArea.Position.Height;
                }
                else if (chartArea.Position.Height > chartArea.Position.Width)
                {
                    var diff = chartArea.Position.Height - chartArea.Position.Width;
                    chartArea.Position.Y += diff / 2;
                    chartArea.Position.Height = chartArea.Position.Width;
                }
            };



            // analysis
            guna2GradientButton4.Text += ("\n\nCrater analysis:\n");
            guna2GradientButton4.Text += ($"- Approximate crater diameter: {craterDiameter:N0} m\n");
            guna2GradientButton4.Text += ($"- Approximate crater depth: {craterDepth:N0} m\n");
            guna2GradientButton4.Text += ($"- Approximate rim height: {rimHeight:N0} m\n");
            guna2GradientButton4.Text += ($"- Estimated ejection radius: {ejectaBlanketRadius:N0} m\n");


            // morphology
            string craterDescription = "";
            if (craterDiameter < 50)
                craterDescription = "A small impact crater with minimal emissions.";
            else if (craterDiameter < 500)
                craterDescription = "A medium-sized crater with a clearly defined rim and an ejecta zone.";
            else if (craterDiameter < 5000)
                craterDescription = "A large complex crater with a central mountain and terraced slopes.";
            else
                craterDescription = "A giant impact basin with ring structures and large-scale ejecta.";

            guna2GradientButton4.Text += ($"- Crater morphology: {craterDescription}\n");

            // double to int
            int depth = (int)Math.Round(craterDepth);
            int diameter = (int)Math.Round(craterDiameter);

            CraterData.CraterDepth = depth;
            CraterData.CraterDiameter = diameter;
            CraterData.HasValidData = true;

            MessageBox.Show($"Data saved:" +
                $"\nDepth: {depth} m" +
                $"\nDiameter: {diameter} m" +
                $"\n\nHere comes the Visualization");
        }

        private void UserHelp(object sender, EventArgs e)
        {
            MessageBox.Show($"m - asteroid mass; [100;5000]" +
                $"\nR - asteroid radius; [3;15]" +
                $"\nP - asteroid density;" +
                $"\n\nH - the height from which the asteroid falls; [1;300];" +
                $"\nV - initial velocity; [10;100]" +
                $"\nA - the angle at which the asteroid falls; [1;90]", "Manual", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
