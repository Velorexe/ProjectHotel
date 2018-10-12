namespace HotelSimulatie
{
    partial class MainForm
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SettingsGroup = new System.Windows.Forms.GroupBox();
            this.StaircaseTime = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.HTEFactorLabel = new System.Windows.Forms.Label();
            this.HTEFactor = new System.Windows.Forms.NumericUpDown();
            this.StartSimulation = new System.Windows.Forms.Button();
            this.SettingsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StaircaseTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HTEFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Lucida Fax", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.titleLabel.Location = new System.Drawing.Point(288, 84);
            this.titleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(363, 76);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Sim-Hotel";
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Lucida Fax", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startButton.Location = new System.Drawing.Point(390, 257);
            this.startButton.Margin = new System.Windows.Forms.Padding(2);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(133, 32);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Font = new System.Drawing.Font("Lucida Fax", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(390, 333);
            this.exitButton.Margin = new System.Windows.Forms.Padding(2);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(133, 32);
            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // SettingsGroup
            // 
            this.SettingsGroup.Controls.Add(this.StartSimulation);
            this.SettingsGroup.Controls.Add(this.StaircaseTime);
            this.SettingsGroup.Controls.Add(this.label2);
            this.SettingsGroup.Controls.Add(this.HTEFactorLabel);
            this.SettingsGroup.Controls.Add(this.HTEFactor);
            this.SettingsGroup.Location = new System.Drawing.Point(656, 84);
            this.SettingsGroup.Name = "SettingsGroup";
            this.SettingsGroup.Size = new System.Drawing.Size(242, 281);
            this.SettingsGroup.TabIndex = 9;
            this.SettingsGroup.TabStop = false;
            this.SettingsGroup.Text = "Settings";
            this.SettingsGroup.Visible = false;
            // 
            // StaircaseTime
            // 
            this.StaircaseTime.Location = new System.Drawing.Point(10, 79);
            this.StaircaseTime.Name = "StaircaseTime";
            this.StaircaseTime.Size = new System.Drawing.Size(226, 20);
            this.StaircaseTime.TabIndex = 6;
            this.StaircaseTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Staircase Time";
            // 
            // HTEFactorLabel
            // 
            this.HTEFactorLabel.AutoSize = true;
            this.HTEFactorLabel.Location = new System.Drawing.Point(7, 20);
            this.HTEFactorLabel.Name = "HTEFactorLabel";
            this.HTEFactorLabel.Size = new System.Drawing.Size(62, 13);
            this.HTEFactorLabel.TabIndex = 1;
            this.HTEFactorLabel.Text = "HTE-Factor";
            // 
            // HTEFactor
            // 
            this.HTEFactor.Location = new System.Drawing.Point(10, 36);
            this.HTEFactor.Name = "HTEFactor";
            this.HTEFactor.Size = new System.Drawing.Size(226, 20);
            this.HTEFactor.TabIndex = 4;
            this.HTEFactor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // StartSimulation
            // 
            this.StartSimulation.Location = new System.Drawing.Point(10, 252);
            this.StartSimulation.Name = "StartSimulation";
            this.StartSimulation.Size = new System.Drawing.Size(226, 23);
            this.StartSimulation.TabIndex = 7;
            this.StartSimulation.Text = "Start Simulation";
            this.StartSimulation.UseVisualStyleBackColor = true;
            this.StartSimulation.Click += new System.EventHandler(this.StartSimulation_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(913, 487);
            this.Controls.Add(this.SettingsGroup);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.titleLabel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "Hotel Simulatie";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.SettingsGroup.ResumeLayout(false);
            this.SettingsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StaircaseTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HTEFactor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.GroupBox SettingsGroup;
        private System.Windows.Forms.NumericUpDown StaircaseTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label HTEFactorLabel;
        private System.Windows.Forms.NumericUpDown HTEFactor;
        private System.Windows.Forms.Button StartSimulation;
    }
}