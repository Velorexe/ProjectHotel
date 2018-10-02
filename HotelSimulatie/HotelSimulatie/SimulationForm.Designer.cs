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
            this.BackgroundLayer = new System.Windows.Forms.PictureBox();
            this.ForegroundLayer = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundLayer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ForegroundLayer)).BeginInit();
            this.SuspendLayout();
            // 
            // BackgroundLayer
            // 
            this.BackgroundLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BackgroundLayer.Location = new System.Drawing.Point(12, 12);
            this.BackgroundLayer.Name = "BackgroundLayer";
            this.BackgroundLayer.Size = new System.Drawing.Size(776, 426);
            this.BackgroundLayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BackgroundLayer.TabIndex = 0;
            this.BackgroundLayer.TabStop = false;
            // 
            // ForegroundLayer
            // 
            this.ForegroundLayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ForegroundLayer.Location = new System.Drawing.Point(12, 12);
            this.ForegroundLayer.Name = "ForegroundLayer";
            this.ForegroundLayer.Size = new System.Drawing.Size(776, 426);
            this.ForegroundLayer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ForegroundLayer.TabIndex = 1;
            this.ForegroundLayer.TabStop = false;
            // 
            // SimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ForegroundLayer);
            this.Controls.Add(this.BackgroundLayer);
            this.Name = "SimulationForm";
            this.Text = "SimulationForm";
            this.Load += new System.EventHandler(this.SimulationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BackgroundLayer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ForegroundLayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox BackgroundLayer;
        private System.Windows.Forms.PictureBox ForegroundLayer;
    }
}