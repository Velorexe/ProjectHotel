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
            this.TimerHTE = new System.Windows.Forms.Timer(this.components);
            this.EventDebug = new System.Windows.Forms.RichTextBox();
            this.HteFactor = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HteFactor)).BeginInit();
            this.SuspendLayout();
            // 
            // WireFrameButton
            // 
            this.WireFrameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WireFrameButton.Location = new System.Drawing.Point(1352, 12);
            this.WireFrameButton.Margin = new System.Windows.Forms.Padding(2);
            this.WireFrameButton.Name = "WireFrameButton";
            this.WireFrameButton.Size = new System.Drawing.Size(113, 25);
            this.WireFrameButton.TabIndex = 2;
            this.WireFrameButton.Text = "Enable Wireframe";
            this.WireFrameButton.UseVisualStyleBackColor = true;
            this.WireFrameButton.Click += new System.EventHandler(this.WireFrameButton_Click);
            // 
            // BackgroundLayer
            // 
            this.BackgroundLayer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.BackgroundLayer.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundLayer.Location = new System.Drawing.Point(12, 12);
            this.BackgroundLayer.Name = "BackgroundLayer";
            this.BackgroundLayer.Size = new System.Drawing.Size(1255, 658);
            this.BackgroundLayer.TabIndex = 0;
            this.BackgroundLayer.TabStop = false;
            // 
            // EventDebug
            // 
            this.EventDebug.Location = new System.Drawing.Point(1273, 42);
            this.EventDebug.Name = "EventDebug";
            this.EventDebug.Size = new System.Drawing.Size(247, 338);
            this.EventDebug.TabIndex = 3;
            this.EventDebug.Text = "";
            // 
            // HteFactor
            // 
            this.HteFactor.Location = new System.Drawing.Point(1274, 387);
            this.HteFactor.Name = "HteFactor";
            this.HteFactor.Size = new System.Drawing.Size(103, 20);
            this.HteFactor.TabIndex = 4;
            this.HteFactor.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.HteFactor.ValueChanged += new System.EventHandler(this.HteFactor_ValueChanged);
            // 
            // SimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1532, 682);
            this.Controls.Add(this.HteFactor);
            this.Controls.Add(this.EventDebug);
            this.Controls.Add(this.WireFrameButton);
            this.Controls.Add(this.BackgroundLayer);
            this.Name = "SimulationForm";
            this.Text = "SimulationForm";
            this.Load += new System.EventHandler(this.SimulationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HteFactor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BackgroundLayer;
        private System.Windows.Forms.Button WireFrameButton;
        private System.Windows.Forms.Timer TimerHTE;
        private System.Windows.Forms.RichTextBox EventDebug;
        private System.Windows.Forms.NumericUpDown HteFactor;
    }
}