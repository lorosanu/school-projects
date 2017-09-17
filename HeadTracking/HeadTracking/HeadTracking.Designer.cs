namespace HeadTracking
{
    partial class HeadTracking
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
            this.lblBattery = new System.Windows.Forms.Label();
            this.grpBattery = new System.Windows.Forms.GroupBox();
            this.pbBattery = new System.Windows.Forms.ProgressBar();
            this.grpApplication = new System.Windows.Forms.GroupBox();
            this.cboWindows = new System.Windows.Forms.ListBox();
            this.btnLearn = new System.Windows.Forms.Button();
            this.grpBattery.SuspendLayout();
            this.grpApplication.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBattery
            // 
            this.lblBattery.AutoSize = true;
            this.lblBattery.ForeColor = System.Drawing.Color.MediumBlue;
            this.lblBattery.Location = new System.Drawing.Point(178, 22);
            this.lblBattery.MaximumSize = new System.Drawing.Size(50, 13);
            this.lblBattery.Name = "lblBattery";
            this.lblBattery.Size = new System.Drawing.Size(23, 13);
            this.lblBattery.TabIndex = 9;
            this.lblBattery.Text = "0%";
            // 
            // grpBattery
            // 
            this.grpBattery.Controls.Add(this.pbBattery);
            this.grpBattery.Controls.Add(this.lblBattery);
            this.grpBattery.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpBattery.Location = new System.Drawing.Point(10, 12);
            this.grpBattery.Name = "grpBattery";
            this.grpBattery.Size = new System.Drawing.Size(248, 48);
            this.grpBattery.TabIndex = 83;
            this.grpBattery.TabStop = false;
            this.grpBattery.Text = "Battery";
            // 
            // pbBattery
            // 
            this.pbBattery.Location = new System.Drawing.Point(17, 19);
            this.pbBattery.Maximum = 70;
            this.pbBattery.Name = "pbBattery";
            this.pbBattery.Size = new System.Drawing.Size(149, 23);
            this.pbBattery.Step = 1;
            this.pbBattery.TabIndex = 6;
            // 
            // grpApplication
            // 
            this.grpApplication.Controls.Add(this.cboWindows);
            this.grpApplication.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpApplication.Location = new System.Drawing.Point(10, 72);
            this.grpApplication.Name = "grpApplication";
            this.grpApplication.Size = new System.Drawing.Size(248, 99);
            this.grpApplication.TabIndex = 84;
            this.grpApplication.TabStop = false;
            this.grpApplication.Text = "Choose  Application";
            // 
            // cboWindows
            // 
            this.cboWindows.BackColor = System.Drawing.Color.AliceBlue;
            this.cboWindows.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cboWindows.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboWindows.ForeColor = System.Drawing.Color.MediumBlue;
            this.cboWindows.FormattingEnabled = true;
            this.cboWindows.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cboWindows.IntegralHeight = false;
            this.cboWindows.Items.AddRange(new object[] {
            "Tetris",
            "Pacman",
            "Need For Speed Underground 2",
            "Quake III Arena"});
            this.cboWindows.Location = new System.Drawing.Point(17, 25);
            this.cboWindows.Margin = new System.Windows.Forms.Padding(20);
            this.cboWindows.Name = "cboWindows";
            this.cboWindows.Size = new System.Drawing.Size(208, 61);
            this.cboWindows.TabIndex = 85;
            this.cboWindows.SelectedIndexChanged += new System.EventHandler(this.cboWindows_SelectedIndexChanged);
            // 
            // btnLearn
            // 
            this.btnLearn.BackColor = System.Drawing.Color.White;
            this.btnLearn.FlatAppearance.BorderColor = System.Drawing.Color.AliceBlue;
            this.btnLearn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnLearn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnLearn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLearn.ForeColor = System.Drawing.Color.MediumBlue;
            this.btnLearn.Location = new System.Drawing.Point(84, 182);
            this.btnLearn.Name = "btnLearn";
            this.btnLearn.Size = new System.Drawing.Size(104, 30);
            this.btnLearn.TabIndex = 81;
            this.btnLearn.Text = "Start Learning";
            this.btnLearn.UseVisualStyleBackColor = false;
            this.btnLearn.Click += new System.EventHandler(this.btnLearn_Click_1);
            // 
            // HeadTracking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(268, 223);
            this.Controls.Add(this.grpBattery);
            this.Controls.Add(this.grpApplication);
            this.Controls.Add(this.btnLearn);
            this.MaximizeBox = false;
            this.Name = "HeadTracking";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Head Tracking";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HeadTracking_FormClosing);
            this.Load += new System.EventHandler(this.HeadTracking_Load_1);
            this.grpBattery.ResumeLayout(false);
            this.grpBattery.PerformLayout();
            this.grpApplication.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblBattery;
        public System.Windows.Forms.GroupBox grpBattery;
        public System.Windows.Forms.ProgressBar pbBattery;
        private System.Windows.Forms.GroupBox grpApplication;
        private System.Windows.Forms.Button btnLearn;
        private System.Windows.Forms.ListBox cboWindows;
    }
}

