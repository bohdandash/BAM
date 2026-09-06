namespace BAM
{
    partial class MainContainerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainContainerForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CLoseButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.QuestionButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.CraterVisualisationButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.SimulationButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.CreateModelButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.guna2GradientButton1 = new Guna.UI2.WinForms.Guna2GradientButton();
            this.buttonToHide = new Guna.UI2.WinForms.Guna2GradientButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Snow;
            this.label1.Location = new System.Drawing.Point(97, 92);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.MaximumSize = new System.Drawing.Size(150, 23);
            this.label1.MinimumSize = new System.Drawing.Size(0, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 23);
            this.label1.TabIndex = 27;
            this.label1.Text = "Asteroid";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Snow;
            this.label2.Location = new System.Drawing.Point(97, 164);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.MaximumSize = new System.Drawing.Size(150, 23);
            this.label2.MinimumSize = new System.Drawing.Size(0, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 23);
            this.label2.TabIndex = 28;
            this.label2.Text = "Simulation";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Snow;
            this.label3.Location = new System.Drawing.Point(97, 236);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.MaximumSize = new System.Drawing.Size(150, 23);
            this.label3.MinimumSize = new System.Drawing.Size(0, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 23);
            this.label3.TabIndex = 29;
            this.label3.Text = "Visualization";
            // 
            // CLoseButton
            // 
            this.CLoseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CLoseButton.BorderColor = System.Drawing.Color.DarkGray;
            this.CLoseButton.BorderRadius = 10;
            this.CLoseButton.BorderThickness = 2;
            this.CLoseButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.CLoseButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.CLoseButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.CLoseButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.CLoseButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.CLoseButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CLoseButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.CLoseButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CLoseButton.ForeColor = System.Drawing.Color.White;
            this.CLoseButton.HoverState.FillColor = System.Drawing.Color.Red;
            this.CLoseButton.HoverState.FillColor2 = System.Drawing.Color.Maroon;
            this.CLoseButton.Image = global::BAM.Properties.Resources.icons8_x_100;
            this.CLoseButton.ImageSize = new System.Drawing.Size(40, 40);
            this.CLoseButton.Location = new System.Drawing.Point(32, 365);
            this.CLoseButton.Name = "CLoseButton";
            this.CLoseButton.Size = new System.Drawing.Size(60, 66);
            this.CLoseButton.TabIndex = 26;
            this.CLoseButton.Click += new System.EventHandler(this.CLoseButton_Click);
            // 
            // QuestionButton
            // 
            this.QuestionButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.QuestionButton.BorderColor = System.Drawing.Color.DarkGray;
            this.QuestionButton.BorderRadius = 10;
            this.QuestionButton.BorderThickness = 2;
            this.QuestionButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.QuestionButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.QuestionButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.QuestionButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.QuestionButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.QuestionButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.QuestionButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.QuestionButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.QuestionButton.ForeColor = System.Drawing.Color.White;
            this.QuestionButton.HoverState.FillColor = System.Drawing.Color.DeepSkyBlue;
            this.QuestionButton.HoverState.FillColor2 = System.Drawing.Color.SteelBlue;
            this.QuestionButton.Image = global::BAM.Properties.Resources.icons8_github_100;
            this.QuestionButton.ImageSize = new System.Drawing.Size(40, 40);
            this.QuestionButton.Location = new System.Drawing.Point(32, 292);
            this.QuestionButton.Name = "QuestionButton";
            this.QuestionButton.Size = new System.Drawing.Size(60, 66);
            this.QuestionButton.TabIndex = 25;
            this.QuestionButton.Click += new System.EventHandler(this.QuestionButton_Click);
            // 
            // CraterVisualisationButton
            // 
            this.CraterVisualisationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CraterVisualisationButton.BorderColor = System.Drawing.Color.DarkGray;
            this.CraterVisualisationButton.BorderRadius = 10;
            this.CraterVisualisationButton.BorderThickness = 2;
            this.CraterVisualisationButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.CraterVisualisationButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.CraterVisualisationButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.CraterVisualisationButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.CraterVisualisationButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.CraterVisualisationButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CraterVisualisationButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.CraterVisualisationButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CraterVisualisationButton.ForeColor = System.Drawing.Color.White;
            this.CraterVisualisationButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.CraterVisualisationButton.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.CraterVisualisationButton.Image = global::BAM.Properties.Resources.icons8_grid_100;
            this.CraterVisualisationButton.ImageSize = new System.Drawing.Size(40, 40);
            this.CraterVisualisationButton.Location = new System.Drawing.Point(32, 219);
            this.CraterVisualisationButton.Name = "CraterVisualisationButton";
            this.CraterVisualisationButton.Size = new System.Drawing.Size(60, 65);
            this.CraterVisualisationButton.TabIndex = 3;
            this.CraterVisualisationButton.Click += new System.EventHandler(this.CraterVisualisationButton_Click);
            // 
            // SimulationButton
            // 
            this.SimulationButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.SimulationButton.BorderColor = System.Drawing.Color.DarkGray;
            this.SimulationButton.BorderRadius = 10;
            this.SimulationButton.BorderThickness = 2;
            this.SimulationButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.SimulationButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.SimulationButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SimulationButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.SimulationButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.SimulationButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.SimulationButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.SimulationButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.SimulationButton.ForeColor = System.Drawing.Color.White;
            this.SimulationButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.SimulationButton.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.SimulationButton.Image = global::BAM.Properties.Resources.icons8_graph_64;
            this.SimulationButton.ImageSize = new System.Drawing.Size(40, 40);
            this.SimulationButton.Location = new System.Drawing.Point(32, 147);
            this.SimulationButton.Name = "SimulationButton";
            this.SimulationButton.Size = new System.Drawing.Size(60, 65);
            this.SimulationButton.TabIndex = 2;
            this.SimulationButton.Click += new System.EventHandler(this.SimulationButton_Click);
            // 
            // CreateModelButton
            // 
            this.CreateModelButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CreateModelButton.BorderColor = System.Drawing.Color.DarkGray;
            this.CreateModelButton.BorderRadius = 10;
            this.CreateModelButton.BorderThickness = 2;
            this.CreateModelButton.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.CreateModelButton.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.CreateModelButton.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.CreateModelButton.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.CreateModelButton.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.CreateModelButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.CreateModelButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.CreateModelButton.FocusedColor = System.Drawing.Color.White;
            this.CreateModelButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.CreateModelButton.ForeColor = System.Drawing.Color.White;
            this.CreateModelButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.CreateModelButton.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.CreateModelButton.Image = global::BAM.Properties.Resources.icons8_3d_model_100;
            this.CreateModelButton.ImageSize = new System.Drawing.Size(40, 40);
            this.CreateModelButton.Location = new System.Drawing.Point(32, 75);
            this.CreateModelButton.Name = "CreateModelButton";
            this.CreateModelButton.Size = new System.Drawing.Size(60, 65);
            this.CreateModelButton.TabIndex = 1;
            this.CreateModelButton.Click += new System.EventHandler(this.CreateModelButton_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Snow;
            this.label4.Location = new System.Drawing.Point(97, 310);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.MaximumSize = new System.Drawing.Size(150, 23);
            this.label4.MinimumSize = new System.Drawing.Size(0, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 23);
            this.label4.TabIndex = 30;
            this.label4.Text = "Github";
            this.label4.UseMnemonic = false;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Snow;
            this.label5.Location = new System.Drawing.Point(97, 383);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.MaximumSize = new System.Drawing.Size(150, 23);
            this.label5.MinimumSize = new System.Drawing.Size(0, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 23);
            this.label5.TabIndex = 31;
            this.label5.Text = "Exit";
            // 
            // guna2GradientButton1
            // 
            this.guna2GradientButton1.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton1.BorderRadius = 10;
            this.guna2GradientButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.guna2GradientButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2GradientButton1.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.guna2GradientButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.guna2GradientButton1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.guna2GradientButton1.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.guna2GradientButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2GradientButton1.ForeColor = System.Drawing.Color.White;
            this.guna2GradientButton1.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.guna2GradientButton1.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.guna2GradientButton1.Location = new System.Drawing.Point(16, 28);
            this.guna2GradientButton1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.guna2GradientButton1.Name = "guna2GradientButton1";
            this.guna2GradientButton1.Size = new System.Drawing.Size(86, 427);
            this.guna2GradientButton1.TabIndex = 32;
            // 
            // buttonToHide
            // 
            this.buttonToHide.BackColor = System.Drawing.Color.Transparent;
            this.buttonToHide.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonToHide.BorderRadius = 10;
            this.buttonToHide.BorderThickness = 2;
            this.buttonToHide.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.buttonToHide.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.buttonToHide.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonToHide.DisabledState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.buttonToHide.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.buttonToHide.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.buttonToHide.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.buttonToHide.FocusedColor = System.Drawing.Color.White;
            this.buttonToHide.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.buttonToHide.ForeColor = System.Drawing.Color.White;
            this.buttonToHide.HoverState.FillColor = System.Drawing.Color.Red;
            this.buttonToHide.HoverState.FillColor2 = System.Drawing.Color.Maroon;
            this.buttonToHide.Image = ((System.Drawing.Image)(resources.GetObject("buttonToHide.Image")));
            this.buttonToHide.ImageSize = new System.Drawing.Size(28, 28);
            this.buttonToHide.Location = new System.Drawing.Point(32, 37);
            this.buttonToHide.Name = "buttonToHide";
            this.buttonToHide.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonToHide.Size = new System.Drawing.Size(28, 31);
            this.buttonToHide.TabIndex = 34;
            this.buttonToHide.UseTransparentBackground = true;
            this.buttonToHide.Click += new System.EventHandler(this.buttonToHide_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(16)))), ((int)(((byte)(16)))));
            this.ClientSize = new System.Drawing.Size(1639, 953);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonToHide);
            this.Controls.Add(this.CLoseButton);
            this.Controls.Add(this.QuestionButton);
            this.Controls.Add(this.CraterVisualisationButton);
            this.Controls.Add(this.SimulationButton);
            this.Controls.Add(this.CreateModelButton);
            this.Controls.Add(this.guna2GradientButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientButton CreateModelButton;
        private Guna.UI2.WinForms.Guna2GradientButton SimulationButton;
        private Guna.UI2.WinForms.Guna2GradientButton CraterVisualisationButton;
        private Guna.UI2.WinForms.Guna2GradientButton CLoseButton;
        private Guna.UI2.WinForms.Guna2GradientButton QuestionButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2GradientButton guna2GradientButton1;
        private Guna.UI2.WinForms.Guna2GradientButton buttonToHide;
    }
}

