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
            this.HteFactor = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.DebugGroup = new System.Windows.Forms.GroupBox();
            this.DebugCheckBox = new System.Windows.Forms.CheckBox();
            this.TimerHTE = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HteFactor)).BeginInit();
            this.DebugGroup.SuspendLayout();
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
            this.BackgroundLayer.Size = new System.Drawing.Size(1102, 639);
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
            // HteFactor
            // 
            this.HteFactor.Location = new System.Drawing.Point(74, 398);
            this.HteFactor.Name = "HteFactor";
            this.HteFactor.Size = new System.Drawing.Size(160, 20);
            this.HteFactor.TabIndex = 4;
            this.HteFactor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HteFactor.ValueChanged += new System.EventHandler(this.HteFactor_ValueChanged);
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
            this.DebugGroup.Controls.Add(this.HteFactor);
            this.DebugGroup.Location = new System.Drawing.Point(1120, 12);
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
            this.DebugCheckBox.Location = new System.Drawing.Point(1120, 454);
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
            // SimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 682);
            this.Controls.Add(this.DebugCheckBox);
            this.Controls.Add(this.BackgroundLayer);
            this.Controls.Add(this.DebugGroup);
            this.Name = "SimulationForm";
            this.Text = "SimulationForm";
            this.Load += new System.EventHandler(this.SimulationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HteFactor)).EndInit();
            this.DebugGroup.ResumeLayout(false);
            this.DebugGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox BackgroundLayer;
        private System.Windows.Forms.Button WireFrameButton;
        private System.Windows.Forms.RichTextBox EventDebug;
        private System.Windows.Forms.NumericUpDown HteFactor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox DebugGroup;
        private System.Windows.Forms.CheckBox DebugCheckBox;
        private System.Windows.Forms.Timer TimerHTE;
    }
}