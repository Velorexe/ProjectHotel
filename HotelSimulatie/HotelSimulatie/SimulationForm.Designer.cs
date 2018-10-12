namespace HotelSimulatie
{
    partial class SimulationForm
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
            this.components = new System.ComponentModel.Container();
            this.WireFrameButton = new System.Windows.Forms.Button();
            this.BackgroundLayer = new System.Windows.Forms.PictureBox();
            this.EventDebug = new System.Windows.Forms.RichTextBox();
            this.HTEFactor = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.DebugGroup = new System.Windows.Forms.GroupBox();
            this.DebugCheckBox = new System.Windows.Forms.CheckBox();
            this.TimerHTE = new System.Windows.Forms.Timer(this.components);
            this.SettingsGroupbox = new System.Windows.Forms.GroupBox();
            this.HTEFactorLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.StaircaseTime = new System.Windows.Forms.NumericUpDown();
            this.PauseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HTEFactor)).BeginInit();
            this.DebugGroup.SuspendLayout();
            this.SettingsGroupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StaircaseTime)).BeginInit();
            this.SuspendLayout();
            // 
            // WireFrameButton
            // 
            this.WireFrameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WireFrameButton.Location = new System.Drawing.Point(6, 18);
            this.WireFrameButton.Margin = new System.Windows.Forms.Padding(2);
            this.WireFrameButton.Name = "WireFrameButton";
            this.WireFrameButton.Size = new System.Drawing.Size(228, 25);
            this.WireFrameButton.TabIndex = 2;
            this.WireFrameButton.Text = "Enable Wireframe";
            this.WireFrameButton.UseVisualStyleBackColor = true;
            this.WireFrameButton.Click += new System.EventHandler(this.WireFrameButton_Click);
            // 
            // BackgroundLayer
            // 
            this.BackgroundLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BackgroundLayer.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundLayer.Location = new System.Drawing.Point(12, 12);
            this.BackgroundLayer.Name = "BackgroundLayer";
            this.BackgroundLayer.Size = new System.Drawing.Size(354, 349);
            this.BackgroundLayer.TabIndex = 0;
            this.BackgroundLayer.TabStop = false;
            // 
            // EventDebug
            // 
            this.EventDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EventDebug.Location = new System.Drawing.Point(6, 54);
            this.EventDebug.Name = "EventDebug";
            this.EventDebug.ReadOnly = true;
            this.EventDebug.Size = new System.Drawing.Size(228, 338);
            this.EventDebug.TabIndex = 3;
            this.EventDebug.Text = "";
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
            this.HTEFactor.ValueChanged += new System.EventHandler(this.HteFactor_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 400);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "HTE Factor:";
            // 
            // DebugGroup
            // 
            this.DebugGroup.Controls.Add(this.WireFrameButton);
            this.DebugGroup.Controls.Add(this.EventDebug);
            this.DebugGroup.Controls.Add(this.label1);
            this.DebugGroup.Location = new System.Drawing.Point(868, 13);
            this.DebugGroup.Name = "DebugGroup";
            this.DebugGroup.Size = new System.Drawing.Size(246, 436);
            this.DebugGroup.TabIndex = 6;
            this.DebugGroup.TabStop = false;
            this.DebugGroup.Text = "Debug";
            this.DebugGroup.Visible = false;
            // 
            // DebugCheckBox
            // 
            this.DebugCheckBox.AutoSize = true;
            this.DebugCheckBox.Location = new System.Drawing.Point(868, 455);
            this.DebugCheckBox.Name = "DebugCheckBox";
            this.DebugCheckBox.Size = new System.Drawing.Size(94, 17);
            this.DebugCheckBox.TabIndex = 7;
            this.DebugCheckBox.Text = "Enable Debug";
            this.DebugCheckBox.UseVisualStyleBackColor = true;
            this.DebugCheckBox.Visible = false;
            this.DebugCheckBox.CheckedChanged += new System.EventHandler(this.DebugCheckBox_CheckedChanged);
            // 
            // TimerHTE
            // 
            this.TimerHTE.Tick += new System.EventHandler(this.TimerHTE_Tick);
            // 
            // SettingsGroupbox
            // 
            this.SettingsGroupbox.Controls.Add(this.PauseButton);
            this.SettingsGroupbox.Controls.Add(this.StaircaseTime);
            this.SettingsGroupbox.Controls.Add(this.label2);
            this.SettingsGroupbox.Controls.Add(this.HTEFactorLabel);
            this.SettingsGroupbox.Controls.Add(this.HTEFactor);
            this.SettingsGroupbox.Location = new System.Drawing.Point(619, 13);
            this.SettingsGroupbox.Name = "SettingsGroupbox";
            this.SettingsGroupbox.Size = new System.Drawing.Size(242, 368);
            this.SettingsGroupbox.TabIndex = 8;
            this.SettingsGroupbox.TabStop = false;
            this.SettingsGroupbox.Text = "Settings";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Staircase Time";
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
            this.StaircaseTime.ValueChanged += new System.EventHandler(this.StaircaseTime_ValueChanged);
            // 
            // PauseButton
            // 
            this.PauseButton.Location = new System.Drawing.Point(13, 339);
            this.PauseButton.Name = "PauseButton";
            this.PauseButton.Size = new System.Drawing.Size(75, 23);
            this.PauseButton.TabIndex = 9;
            this.PauseButton.Text = "Pause";
            this.PauseButton.UseVisualStyleBackColor = true;
            this.PauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // SimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1124, 481);
            this.Controls.Add(this.SettingsGroupbox);
            this.Controls.Add(this.DebugCheckBox);
            this.Controls.Add(this.BackgroundLayer);
            this.Controls.Add(this.DebugGroup);
            this.Name = "SimulationForm";
            this.Text = "SimulationForm";
            this.Load += new System.EventHandler(this.SimulationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HTEFactor)).EndInit();
            this.DebugGroup.ResumeLayout(false);
            this.DebugGroup.PerformLayout();
            this.SettingsGroupbox.ResumeLayout(false);
            this.SettingsGroupbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StaircaseTime)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox BackgroundLayer;
        private System.Windows.Forms.Button WireFrameButton;
        private System.Windows.Forms.RichTextBox EventDebug;
        private System.Windows.Forms.NumericUpDown HTEFactor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox DebugGroup;
        private System.Windows.Forms.CheckBox DebugCheckBox;
        private System.Windows.Forms.Timer TimerHTE;
        private System.Windows.Forms.GroupBox SettingsGroupbox;
        private System.Windows.Forms.Label HTEFactorLabel;
        private System.Windows.Forms.NumericUpDown StaircaseTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button PauseButton;
    }
}