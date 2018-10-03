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
            this.WireFrameButton = new System.Windows.Forms.Button();
            this.BackgroundLayer = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundLayer)).BeginInit();
            this.SuspendLayout();
            // 
            // WireFrameButton
            // 
            this.WireFrameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WireFrameButton.Location = new System.Drawing.Point(1189, 18);
            this.WireFrameButton.Name = "WireFrameButton";
            this.WireFrameButton.Size = new System.Drawing.Size(169, 38);
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
            this.BackgroundLayer.Location = new System.Drawing.Point(18, 18);
            this.BackgroundLayer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BackgroundLayer.Name = "BackgroundLayer";
            this.BackgroundLayer.Size = new System.Drawing.Size(1057, 655);
            this.BackgroundLayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BackgroundLayer.TabIndex = 0;
            this.BackgroundLayer.TabStop = false;
            // 
            // SimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1459, 692);
            this.Controls.Add(this.WireFrameButton);
            this.Controls.Add(this.BackgroundLayer);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SimulationForm";
            this.Text = "SimulationForm";
            this.Load += new System.EventHandler(this.SimulationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundLayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BackgroundLayer;
        private System.Windows.Forms.Button WireFrameButton;
    }
}