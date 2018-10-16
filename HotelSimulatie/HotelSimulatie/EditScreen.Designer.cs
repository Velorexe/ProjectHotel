namespace HotelSimulatie
{
    partial class EditScreen
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
            this.CinemaGroup = new System.Windows.Forms.GroupBox();
            this.CinemaID = new System.Windows.Forms.TextBox();
            this.MovieTime = new System.Windows.Forms.NumericUpDown();
            this.MovieTimeLabel = new System.Windows.Forms.Label();
            this.CinemaIDLabel = new System.Windows.Forms.Label();
            this.RestaurantGroup = new System.Windows.Forms.GroupBox();
            this.RestaurantID = new System.Windows.Forms.TextBox();
            this.RestaurantTime = new System.Windows.Forms.NumericUpDown();
            this.EatingTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.RoomGroup = new System.Windows.Forms.GroupBox();
            this.RoomID = new System.Windows.Forms.TextBox();
            this.RoomOwnerLabel = new System.Windows.Forms.Label();
            this.RoomIDLabel = new System.Windows.Forms.Label();
            this.RoomOwner = new System.Windows.Forms.TextBox();
            this.CinemaGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MovieTime)).BeginInit();
            this.RestaurantGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RestaurantTime)).BeginInit();
            this.RoomGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // CinemaGroup
            // 
            this.CinemaGroup.Controls.Add(this.CinemaID);
            this.CinemaGroup.Controls.Add(this.MovieTime);
            this.CinemaGroup.Controls.Add(this.MovieTimeLabel);
            this.CinemaGroup.Controls.Add(this.CinemaIDLabel);
            this.CinemaGroup.Location = new System.Drawing.Point(12, 12);
            this.CinemaGroup.Name = "CinemaGroup";
            this.CinemaGroup.Size = new System.Drawing.Size(163, 106);
            this.CinemaGroup.TabIndex = 0;
            this.CinemaGroup.TabStop = false;
            this.CinemaGroup.Text = "Edit Cinema";
            this.CinemaGroup.Visible = false;
            // 
            // CinemaID
            // 
            this.CinemaID.Location = new System.Drawing.Point(10, 36);
            this.CinemaID.Name = "CinemaID";
            this.CinemaID.ReadOnly = true;
            this.CinemaID.Size = new System.Drawing.Size(147, 20);
            this.CinemaID.TabIndex = 4;
            // 
            // MovieTime
            // 
            this.MovieTime.Location = new System.Drawing.Point(10, 75);
            this.MovieTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MovieTime.Name = "MovieTime";
            this.MovieTime.Size = new System.Drawing.Size(147, 20);
            this.MovieTime.TabIndex = 3;
            this.MovieTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // MovieTimeLabel
            // 
            this.MovieTimeLabel.AutoSize = true;
            this.MovieTimeLabel.Location = new System.Drawing.Point(7, 59);
            this.MovieTimeLabel.Name = "MovieTimeLabel";
            this.MovieTimeLabel.Size = new System.Drawing.Size(65, 13);
            this.MovieTimeLabel.TabIndex = 2;
            this.MovieTimeLabel.Text = "Movie Time:";
            // 
            // CinemaIDLabel
            // 
            this.CinemaIDLabel.AutoSize = true;
            this.CinemaIDLabel.Location = new System.Drawing.Point(7, 20);
            this.CinemaIDLabel.Name = "CinemaIDLabel";
            this.CinemaIDLabel.Size = new System.Drawing.Size(21, 13);
            this.CinemaIDLabel.TabIndex = 1;
            this.CinemaIDLabel.Text = "ID:";
            // 
            // RestaurantGroup
            // 
            this.RestaurantGroup.Controls.Add(this.RestaurantID);
            this.RestaurantGroup.Controls.Add(this.RestaurantTime);
            this.RestaurantGroup.Controls.Add(this.EatingTimeLabel);
            this.RestaurantGroup.Controls.Add(this.label1);
            this.RestaurantGroup.Location = new System.Drawing.Point(181, 12);
            this.RestaurantGroup.Name = "RestaurantGroup";
            this.RestaurantGroup.Size = new System.Drawing.Size(163, 106);
            this.RestaurantGroup.TabIndex = 1;
            this.RestaurantGroup.TabStop = false;
            this.RestaurantGroup.Text = "Edit Restaurant";
            this.RestaurantGroup.Visible = false;
            // 
            // RestaurantID
            // 
            this.RestaurantID.Location = new System.Drawing.Point(9, 36);
            this.RestaurantID.Name = "RestaurantID";
            this.RestaurantID.ReadOnly = true;
            this.RestaurantID.Size = new System.Drawing.Size(148, 20);
            this.RestaurantID.TabIndex = 5;
            // 
            // RestaurantTime
            // 
            this.RestaurantTime.Location = new System.Drawing.Point(9, 75);
            this.RestaurantTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RestaurantTime.Name = "RestaurantTime";
            this.RestaurantTime.Size = new System.Drawing.Size(148, 20);
            this.RestaurantTime.TabIndex = 4;
            this.RestaurantTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // EatingTimeLabel
            // 
            this.EatingTimeLabel.AutoSize = true;
            this.EatingTimeLabel.Location = new System.Drawing.Point(6, 59);
            this.EatingTimeLabel.Name = "EatingTimeLabel";
            this.EatingTimeLabel.Size = new System.Drawing.Size(66, 13);
            this.EatingTimeLabel.TabIndex = 4;
            this.EatingTimeLabel.Text = "Eating Time:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "ID:";
            // 
            // ApplyButton
            // 
            this.ApplyButton.Location = new System.Drawing.Point(12, 124);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(163, 24);
            this.ApplyButton.TabIndex = 2;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // RoomGroup
            // 
            this.RoomGroup.Controls.Add(this.RoomOwner);
            this.RoomGroup.Controls.Add(this.RoomID);
            this.RoomGroup.Controls.Add(this.RoomOwnerLabel);
            this.RoomGroup.Controls.Add(this.RoomIDLabel);
            this.RoomGroup.Location = new System.Drawing.Point(350, 12);
            this.RoomGroup.Name = "RoomGroup";
            this.RoomGroup.Size = new System.Drawing.Size(163, 106);
            this.RoomGroup.TabIndex = 6;
            this.RoomGroup.TabStop = false;
            this.RoomGroup.Text = "View Room";
            this.RoomGroup.Visible = false;
            // 
            // RoomID
            // 
            this.RoomID.Location = new System.Drawing.Point(9, 36);
            this.RoomID.Name = "RoomID";
            this.RoomID.ReadOnly = true;
            this.RoomID.Size = new System.Drawing.Size(148, 20);
            this.RoomID.TabIndex = 5;
            // 
            // RoomOwnerLabel
            // 
            this.RoomOwnerLabel.AutoSize = true;
            this.RoomOwnerLabel.Location = new System.Drawing.Point(6, 59);
            this.RoomOwnerLabel.Name = "RoomOwnerLabel";
            this.RoomOwnerLabel.Size = new System.Drawing.Size(87, 13);
            this.RoomOwnerLabel.TabIndex = 4;
            this.RoomOwnerLabel.Text = "Assigned Owner:";
            // 
            // RoomIDLabel
            // 
            this.RoomIDLabel.AutoSize = true;
            this.RoomIDLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RoomIDLabel.Location = new System.Drawing.Point(6, 20);
            this.RoomIDLabel.Name = "RoomIDLabel";
            this.RoomIDLabel.Size = new System.Drawing.Size(23, 15);
            this.RoomIDLabel.TabIndex = 4;
            this.RoomIDLabel.Text = "ID:";
            // 
            // RoomOwner
            // 
            this.RoomOwner.Location = new System.Drawing.Point(9, 75);
            this.RoomOwner.Name = "RoomOwner";
            this.RoomOwner.ReadOnly = true;
            this.RoomOwner.Size = new System.Drawing.Size(148, 20);
            this.RoomOwner.TabIndex = 6;
            // 
            // EditScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 160);
            this.Controls.Add(this.RoomGroup);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.RestaurantGroup);
            this.Controls.Add(this.CinemaGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditScreen";
            this.Text = "Edit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditScreen_FormClosing);
            this.CinemaGroup.ResumeLayout(false);
            this.CinemaGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MovieTime)).EndInit();
            this.RestaurantGroup.ResumeLayout(false);
            this.RestaurantGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RestaurantTime)).EndInit();
            this.RoomGroup.ResumeLayout(false);
            this.RoomGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox CinemaGroup;
        private System.Windows.Forms.GroupBox RestaurantGroup;
        private System.Windows.Forms.Label CinemaIDLabel;
        private System.Windows.Forms.NumericUpDown MovieTime;
        private System.Windows.Forms.Label MovieTimeLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown RestaurantTime;
        private System.Windows.Forms.Label EatingTimeLabel;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.TextBox CinemaID;
        private System.Windows.Forms.TextBox RestaurantID;
        private System.Windows.Forms.GroupBox RoomGroup;
        private System.Windows.Forms.TextBox RoomID;
        private System.Windows.Forms.Label RoomOwnerLabel;
        private System.Windows.Forms.Label RoomIDLabel;
        private System.Windows.Forms.TextBox RoomOwner;
    }
}