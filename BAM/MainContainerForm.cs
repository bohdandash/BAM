using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static BAM.SimulationContext;

namespace BAM
{
    public partial class MainContainerForm : Form
    {
        private int craterDepth;
        private int craterDiameter;
        private int craterAngle;

        bool modelcreationButton_Click = false;
        bool simulationButton_Click = false;
        bool impactButton_Click = false;

        bool sidebarExpanded = true;
        public MainContainerForm()
        {
            InitializeComponent();
            

        }

        public MainContainerForm(int craterDepth, int craterDiameter, int craterAngle)
        {
            InitializeComponent();
            this.craterDepth = craterDepth;
            this.craterDiameter = craterDiameter;
            this.craterAngle = craterAngle;
        }

        private void CLoseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CreateModelButton_Click(object sender, EventArgs e) 
        {
            SetFlagAndRepaint(fb: true);
            CreateAsteroidForm f2 = new CreateAsteroidForm();
            f2.ShowDialog();
            
        } 

        private void SimulationButton_Click(object sender, EventArgs e)
        {

            SetFlagAndRepaint(sb: true);
            Form f3 = new ImpactSimulationForm();
            f3.ShowDialog();
            
        }

        private void CraterVisualisationButton_Click(object sender, EventArgs e)
        {
            if(CraterData.HasValidData)
            {

                SetFlagAndRepaint(tb: true);
                Form f5 = new ImpactVisualizationForm(CraterData.CraterDepth, CraterData.CraterDiameter, CraterData.CraterEntryAngle);
                f5.ShowDialog();
                
            }
            else
            {
                SetFlagAndRepaint(tb: true);
                // Используем значения по умолчанию или конструктор по умолчанию
                MessageBox.Show("No Asteroid and Simulation was added. Applying default values.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Form f5 = new ImpactVisualizationForm(50, 200, 45);
                f5.ShowDialog();
                

            }
            

        }

        private void QuestionButton_Click(object sender, EventArgs e)
        {
            string url = "https://github.com/bohdandash/ProjectHeightify";

            // open url
            System.Diagnostics.Process.Start(url);
        }

        void ColorChange()
        {
            if (modelcreationButton_Click == true )
            {
                CreateModelButton.FillColor = Color.DimGray;
                CreateModelButton.FillColor2 = Color.DimGray;


                SimulationButton.FillColor = Color.FromArgb(32, 32, 32);
                SimulationButton.FillColor2 = Color.FromArgb(16, 16, 16);

                CraterVisualisationButton.FillColor = Color.FromArgb(32, 32, 32);
                CraterVisualisationButton.FillColor2 = Color.FromArgb(16, 16, 16);
                modelcreationButton_Click = false;
            }
            

            else if (simulationButton_Click == true)
            {
                SimulationButton.FillColor = Color.DimGray;
                SimulationButton.FillColor2 = Color.DimGray;


                CreateModelButton.FillColor = Color.FromArgb(32, 32, 32);
                CreateModelButton.FillColor2 = Color.FromArgb(16, 16, 16);

                CraterVisualisationButton.FillColor = Color.FromArgb(32, 32, 32);
                CraterVisualisationButton.FillColor2 = Color.FromArgb(16, 16, 16);
                simulationButton_Click = false;
            }


            else if (impactButton_Click == true)
            {
                SimulationButton.FillColor = Color.FromArgb(32, 32, 32);
                SimulationButton.FillColor2 = Color.FromArgb(16, 16, 16);


                CreateModelButton.FillColor = Color.FromArgb(32, 32, 32);
                CreateModelButton.FillColor2 = Color.FromArgb(16, 16, 16);

                CraterVisualisationButton.FillColor = Color.DimGray;
                CraterVisualisationButton.FillColor2 = Color.DimGray;
                impactButton_Click = false;
            }

        }

        void SetFlagAndRepaint(bool fb = false, bool sb = false, bool tb = false)
        {
            modelcreationButton_Click = fb; simulationButton_Click = sb; impactButton_Click = tb;
            ColorChange();
        }

        private void buttonToHide_Click(object sender, EventArgs e)
        {
            sidebarExpanded = !sidebarExpanded;

            var labels = new List<Label> { label1, label2, label3, label4, label5 };
   
            

            foreach (var lbl in labels)
            { lbl.Visible = sidebarExpanded; }

            if (sidebarExpanded)
            { 
                buttonToHide.FillColor = Color.FromArgb(32, 32, 32); 
                buttonToHide.FillColor2 = Color.FromArgb(16, 16, 16); 
            }

            else 
            { 
                buttonToHide.FillColor = Color.Red; 
                buttonToHide.FillColor2 = Color.Maroon;
            }

        }

        

        
    }
}
