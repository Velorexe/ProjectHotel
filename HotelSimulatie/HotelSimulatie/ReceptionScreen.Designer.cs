namespace HotelSimulatie
{
    partial class ReceptionScreen
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
            this.SettingsGroup = new System.Windows.Forms.GroupBox();
            this.ZoomLevel = new System.Windows.Forms.ComboBox();
            this.ZoomLevelsLabel = new System.Windows.Forms.Label();
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.SimulationSpeed = new System.Windows.Forms.ComboBox();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.DefaultValuesButton = new System.Windows.Forms.Button();
            this.TimeBasedGroup = new System.Windows.Forms.GroupBox();
            this.TimeBeforeDeath = new System.Windows.Forms.NumericUpDown();
            this.TimeBeforeDeathLabel = new System.Windows.Forms.Label();
            this.StairTime = new System.Windows.Forms.NumericUpDown();
            this.CleaningTime = new System.Windows.Forms.NumericUpDown();
            this.StairTimeLabel = new System.Windows.Forms.Label();
            this.CleaningTimeLabel = new System.Windows.Forms.Label();
            this.FacilityGroup = new System.Windows.Forms.GroupBox();
            this.RoomViewButton = new System.Windows.Forms.Button();
            this.RoomsBox = new System.Windows.Forms.ComboBox();
            this.RoomsLabel = new System.Windows.Forms.Label();
            this.CinemaEditButton = new System.Windows.Forms.Button();
            this.RestaurantEditButton = new System.Windows.Forms.Button();
            this.CinemasBox = new System.Windows.Forms.ComboBox();
            this.CinemaLabel = new System.Windows.Forms.Label();
            this.RestaurantBox = new System.Windows.Forms.ComboBox();
            this.RestaurantLabel = new System.Windows.Forms.Label();
            this.CustomerBox = new System.Windows.Forms.ComboBox();
            this.CustomerLabel = new System.Windows.Forms.Label();
            this.AssignedRoom = new System.Windows.Forms.TextBox();
            this.AssignedRoomLabel = new System.Windows.Forms.Label();
            this.CustomerNameLabel = new System.Windows.Forms.Label();
            this.CustomerName = new System.Windows.Forms.TextBox();
            this.CustomerApplyButton = new System.Windows.Forms.Button();
            this.CurrentActivity = new System.Windows.Forms.TextBox();
            this.CurrentActivityLabel = new System.Windows.Forms.Label();
            this.CustomerGroup = new System.Windows.Forms.GroupBox();
            this.LiveStatisticsButton = new System.Windows.Forms.Button();
            this.SettingsGroup.SuspendLayout();
            this.TimeBasedGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeBeforeDeath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StairTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CleaningTime)).BeginInit();
            this.FacilityGroup.SuspendLayout();
            this.CustomerGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // SettingsGroup
            // 
            this.SettingsGroup.Controls.Add(this.ZoomLevel);
            this.SettingsGroup.Controls.Add(this.ZoomLevelsLabel);
            this.SettingsGroup.Controls.Add(this.SpeedLabel);
            this.SettingsGroup.Controls.Add(this.SimulationSpeed);
            this.SettingsGroup.Location = new System.Drawing.Point(12, 12);
            this.SettingsGroup.Name = "SettingsGroup";
            this.SettingsGroup.Size = new System.Drawing.Size(149, 199);
            this.SettingsGroup.TabIndex = 0;
            this.SettingsGroup.TabStop = false;
            this.SettingsGroup.Text = "Settings";
            // 
            // ZoomLevel
            // 
            this.ZoomLevel.AutoCompleteCustomSource.AddRange(new string[] {
            "x 1.0",
            "x 1.25",
            "x 1.5",
            "x 2.0"});
            this.ZoomLevel.FormattingEnabled = true;
            this.ZoomLevel.Items.AddRange(new object[] {
            "x 1.0",
            "x 1.25",
            "x 1.5",
            "x 2.0"});
            this.ZoomLevel.Location = new System.Drawing.Point(10, 76);
            this.ZoomLevel.Name = "ZoomLevel";
            this.ZoomLevel.Size = new System.Drawing.Size(133, 21);
            this.ZoomLevel.TabIndex = 3;
            this.ZoomLevel.Text = "x 1.0";
            // 
            // ZoomLevelsLabel
            // 
            this.ZoomLevelsLabel.AutoSize = true;
            this.ZoomLevelsLabel.Location = new System.Drawing.Point(7, 60);
            this.ZoomLevelsLabel.Name = "ZoomLevelsLabel";
            this.ZoomLevelsLabel.Size = new System.Drawing.Size(68, 13);
            this.ZoomLevelsLabel.TabIndex = 2;
            this.ZoomLevelsLabel.Text = "Zoom Levels";
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.AutoSize = true;
            this.SpeedLabel.Location = new System.Drawing.Point(7, 20);
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(89, 13);
            this.SpeedLabel.TabIndex = 1;
            this.SpeedLabel.Text = "Simulation Speed";
            // 
            // SimulationSpeed
            // 
            this.SimulationSpeed.FormattingEnabled = true;
            this.SimulationSpeed.Items.AddRange(new object[] {
            "x 0.5",
            "x 0.75",
            "x 1.0",
            "x 1.25",
            "x 1.5"});
            this.SimulationSpeed.Location = new System.Drawing.Point(10, 36);
            this.SimulationSpeed.Name = "SimulationSpeed";
            this.SimulationSpeed.Size = new System.Drawing.Size(133, 21);
            this.SimulationSpeed.TabIndex = 0;
            this.SimulationSpeed.Text = "x 1.0";
            // 
            // ApplyButton
            // 
            this.ApplyButton.Location = new System.Drawing.Point(12, 275);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(698, 43);
            this.ApplyButton.TabIndex = 1;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // DefaultValuesButton
            // 
            this.DefaultValuesButton.Location = new System.Drawing.Point(12, 246);
            this.DefaultValuesButton.Name = "DefaultValuesButton";
            this.DefaultValuesButton.Size = new System.Drawing.Size(698, 23);
            this.DefaultValuesButton.TabIndex = 2;
            this.DefaultValuesButton.Text = "Revert To Default Values";
            this.DefaultValuesButton.UseVisualStyleBackColor = true;
            this.DefaultValuesButton.Click += new System.EventHandler(this.DefaultValuesButton_Click);
            // 
            // TimeBasedGroup
            // 
            this.TimeBasedGroup.Controls.Add(this.TimeBeforeDeath);
            this.TimeBasedGroup.Controls.Add(this.TimeBeforeDeathLabel);
            this.TimeBasedGroup.Controls.Add(this.StairTime);
            this.TimeBasedGroup.Controls.Add(this.CleaningTime);
            this.TimeBasedGroup.Controls.Add(this.StairTimeLabel);
            this.TimeBasedGroup.Controls.Add(this.CleaningTimeLabel);
            this.TimeBasedGroup.Location = new System.Drawing.Point(167, 12);
            this.TimeBasedGroup.Name = "TimeBasedGroup";
            this.TimeBasedGroup.Size = new System.Drawing.Size(149, 199);
            this.TimeBasedGroup.TabIndex = 3;
            this.TimeBasedGroup.TabStop = false;
            this.TimeBasedGroup.Text = "Time Based Settings";
            // 
            // TimeBeforeDeath
            // 
            this.TimeBeforeDeath.Location = new System.Drawing.Point(9, 116);
            this.TimeBeforeDeath.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TimeBeforeDeath.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TimeBeforeDeath.Name = "TimeBeforeDeath";
            this.TimeBeforeDeath.Size = new System.Drawing.Size(122, 20);
            this.TimeBeforeDeath.TabIndex = 9;
            this.TimeBeforeDeath.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // TimeBeforeDeathLabel
            // 
            this.TimeBeforeDeathLabel.AutoSize = true;
            this.TimeBeforeDeathLabel.Location = new System.Drawing.Point(6, 100);
            this.TimeBeforeDeathLabel.Name = "TimeBeforeDeathLabel";
            this.TimeBeforeDeathLabel.Size = new System.Drawing.Size(96, 13);
            this.TimeBeforeDeathLabel.TabIndex = 8;
            this.TimeBeforeDeathLabel.Text = "Time Before Death";
            // 
            // StairTime
            // 
            this.StairTime.Location = new System.Drawing.Point(9, 77);
            this.StairTime.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.StairTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.StairTime.Name = "StairTime";
            this.StairTime.Size = new System.Drawing.Size(122, 20);
            this.StairTime.TabIndex = 7;
            this.StairTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CleaningTime
            // 
            this.CleaningTime.Location = new System.Drawing.Point(9, 36);
            this.CleaningTime.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.CleaningTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CleaningTime.Name = "CleaningTime";
            this.CleaningTime.Size = new System.Drawing.Size(122, 20);
            this.CleaningTime.TabIndex = 6;
            this.CleaningTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // StairTimeLabel
            // 
            this.StairTimeLabel.AutoSize = true;
            this.StairTimeLabel.Location = new System.Drawing.Point(6, 60);
            this.StairTimeLabel.Name = "StairTimeLabel";
            this.StairTimeLabel.Size = new System.Drawing.Size(54, 13);
            this.StairTimeLabel.TabIndex = 5;
            this.StairTimeLabel.Text = "Stair Time";
            // 
            // CleaningTimeLabel
            // 
            this.CleaningTimeLabel.AutoSize = true;
            this.CleaningTimeLabel.Location = new System.Drawing.Point(6, 20);
            this.CleaningTimeLabel.Name = "CleaningTimeLabel";
            this.CleaningTimeLabel.Size = new System.Drawing.Size(74, 13);
            this.CleaningTimeLabel.TabIndex = 4;
            this.CleaningTimeLabel.Text = "Cleaning Time";
            // 
            // FacilityGroup
            // 
            this.FacilityGroup.Controls.Add(this.RoomViewButton);
            this.FacilityGroup.Controls.Add(this.RoomsBox);
            this.FacilityGroup.Controls.Add(this.RoomsLabel);
            this.FacilityGroup.Controls.Add(this.CinemaEditButton);
            this.FacilityGroup.Controls.Add(this.RestaurantEditButton);
            this.FacilityGroup.Controls.Add(this.CinemasBox);
            this.FacilityGroup.Controls.Add(this.CinemaLabel);
            this.FacilityGroup.Controls.Add(this.RestaurantBox);
            this.FacilityGroup.Controls.Add(this.RestaurantLabel);
            this.FacilityGroup.Location = new System.Drawing.Point(323, 13);
            this.FacilityGroup.Name = "FacilityGroup";
            this.FacilityGroup.Size = new System.Drawing.Size(190, 227);
            this.FacilityGroup.TabIndex = 4;
            this.FacilityGroup.TabStop = false;
            this.FacilityGroup.Text = "Facilities";
            // 
            // RoomViewButton
            // 
            this.RoomViewButton.Location = new System.Drawing.Point(10, 195);
            this.RoomViewButton.Name = "RoomViewButton";
            this.RoomViewButton.Size = new System.Drawing.Size(174, 23);
            this.RoomViewButton.TabIndex = 8;
            this.RoomViewButton.Text = "View";
            this.RoomViewButton.UseVisualStyleBackColor = true;
            this.RoomViewButton.Click += new System.EventHandler(this.RoomViewButton_Click);
            // 
            // RoomsBox
            // 
            this.RoomsBox.FormattingEnabled = true;
            this.RoomsBox.Location = new System.Drawing.Point(10, 168);
            this.RoomsBox.Name = "RoomsBox";
            this.RoomsBox.Size = new System.Drawing.Size(174, 21);
            this.RoomsBox.TabIndex = 7;
            this.RoomsBox.SelectedIndexChanged += new System.EventHandler(this.RoomsBox_SelectedIndexChanged);
            // 
            // RoomsLabel
            // 
            this.RoomsLabel.AutoSize = true;
            this.RoomsLabel.Location = new System.Drawing.Point(7, 152);
            this.RoomsLabel.Name = "RoomsLabel";
            this.RoomsLabel.Size = new System.Drawing.Size(35, 13);
            this.RoomsLabel.TabIndex = 6;
            this.RoomsLabel.Text = "Room";
            // 
            // CinemaEditButton
            // 
            this.CinemaEditButton.Location = new System.Drawing.Point(10, 126);
            this.CinemaEditButton.Name = "CinemaEditButton";
            this.CinemaEditButton.Size = new System.Drawing.Size(174, 23);
            this.CinemaEditButton.TabIndex = 5;
            this.CinemaEditButton.Text = "Edit";
            this.CinemaEditButton.UseVisualStyleBackColor = true;
            this.CinemaEditButton.Click += new System.EventHandler(this.CinemaEditButton_Click);
            // 
            // RestaurantEditButton
            // 
            this.RestaurantEditButton.Location = new System.Drawing.Point(10, 59);
            this.RestaurantEditButton.Name = "RestaurantEditButton";
            this.RestaurantEditButton.Size = new System.Drawing.Size(174, 23);
            this.RestaurantEditButton.TabIndex = 4;
            this.RestaurantEditButton.Text = "Edit";
            this.RestaurantEditButton.UseVisualStyleBackColor = true;
            this.RestaurantEditButton.Click += new System.EventHandler(this.RestaurantEditButton_Click);
            // 
            // CinemasBox
            // 
            this.CinemasBox.FormattingEnabled = true;
            this.CinemasBox.Location = new System.Drawing.Point(10, 99);
            this.CinemasBox.Name = "CinemasBox";
            this.CinemasBox.Size = new System.Drawing.Size(174, 21);
            this.CinemasBox.TabIndex = 3;
            this.CinemasBox.SelectedIndexChanged += new System.EventHandler(this.CinemasBox_SelectedIndexChanged);
            // 
            // CinemaLabel
            // 
            this.CinemaLabel.AutoSize = true;
            this.CinemaLabel.Location = new System.Drawing.Point(7, 85);
            this.CinemaLabel.Name = "CinemaLabel";
            this.CinemaLabel.Size = new System.Drawing.Size(42, 13);
            this.CinemaLabel.TabIndex = 2;
            this.CinemaLabel.Text = "Cinema";
            // 
            // RestaurantBox
            // 
            this.RestaurantBox.FormattingEnabled = true;
            this.RestaurantBox.Location = new System.Drawing.Point(10, 35);
            this.RestaurantBox.Name = "RestaurantBox";
            this.RestaurantBox.Size = new System.Drawing.Size(174, 21);
            this.RestaurantBox.TabIndex = 1;
            this.RestaurantBox.SelectedIndexChanged += new System.EventHandler(this.RestaurantBox_SelectedIndexChanged);
            // 
            // RestaurantLabel
            // 
            this.RestaurantLabel.AutoSize = true;
            this.RestaurantLabel.Location = new System.Drawing.Point(7, 18);
            this.RestaurantLabel.Name = "RestaurantLabel";
            this.RestaurantLabel.Size = new System.Drawing.Size(59, 13);
            this.RestaurantLabel.TabIndex = 0;
            this.RestaurantLabel.Text = "Restaurant";
            // 
            // CustomerBox
            // 
            this.CustomerBox.FormattingEnabled = true;
            this.CustomerBox.Location = new System.Drawing.Point(6, 34);
            this.CustomerBox.Name = "CustomerBox";
            this.CustomerBox.Size = new System.Drawing.Size(179, 21);
            this.CustomerBox.TabIndex = 0;
            this.CustomerBox.SelectedIndexChanged += new System.EventHandler(this.CustomerBox_SelectedIndexChanged);
            // 
            // CustomerLabel
            // 
            this.CustomerLabel.AutoSize = true;
            this.CustomerLabel.Location = new System.Drawing.Point(6, 18);
            this.CustomerLabel.Name = "CustomerLabel";
            this.CustomerLabel.Size = new System.Drawing.Size(65, 13);
            this.CustomerLabel.TabIndex = 9;
            this.CustomerLabel.Text = "Customer ID";
            // 
            // AssignedRoom
            // 
            this.AssignedRoom.Location = new System.Drawing.Point(6, 115);
            this.AssignedRoom.Name = "AssignedRoom";
            this.AssignedRoom.ReadOnly = true;
            this.AssignedRoom.Size = new System.Drawing.Size(179, 20);
            this.AssignedRoom.TabIndex = 10;
            // 
            // AssignedRoomLabel
            // 
            this.AssignedRoomLabel.AutoSize = true;
            this.AssignedRoomLabel.Location = new System.Drawing.Point(6, 99);
            this.AssignedRoomLabel.Name = "AssignedRoomLabel";
            this.AssignedRoomLabel.Size = new System.Drawing.Size(81, 13);
            this.AssignedRoomLabel.TabIndex = 11;
            this.AssignedRoomLabel.Text = "Assigned Room";
            // 
            // CustomerNameLabel
            // 
            this.CustomerNameLabel.AutoSize = true;
            this.CustomerNameLabel.Location = new System.Drawing.Point(6, 59);
            this.CustomerNameLabel.Name = "CustomerNameLabel";
            this.CustomerNameLabel.Size = new System.Drawing.Size(35, 13);
            this.CustomerNameLabel.TabIndex = 12;
            this.CustomerNameLabel.Text = "Name";
            // 
            // CustomerName
            // 
            this.CustomerName.Location = new System.Drawing.Point(6, 76);
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.Size = new System.Drawing.Size(179, 20);
            this.CustomerName.TabIndex = 13;
            // 
            // CustomerApplyButton
            // 
            this.CustomerApplyButton.Location = new System.Drawing.Point(6, 195);
            this.CustomerApplyButton.Name = "CustomerApplyButton";
            this.CustomerApplyButton.Size = new System.Drawing.Size(179, 23);
            this.CustomerApplyButton.TabIndex = 14;
            this.CustomerApplyButton.Text = "Apply";
            this.CustomerApplyButton.UseVisualStyleBackColor = true;
            this.CustomerApplyButton.Click += new System.EventHandler(this.CustomerApplyButton_Click);
            // 
            // CurrentActivity
            // 
            this.CurrentActivity.Location = new System.Drawing.Point(6, 154);
            this.CurrentActivity.Name = "CurrentActivity";
            this.CurrentActivity.ReadOnly = true;
            this.CurrentActivity.Size = new System.Drawing.Size(179, 20);
            this.CurrentActivity.TabIndex = 15;
            // 
            // CurrentActivityLabel
            // 
            this.CurrentActivityLabel.AutoSize = true;
            this.CurrentActivityLabel.Location = new System.Drawing.Point(6, 138);
            this.CurrentActivityLabel.Name = "CurrentActivityLabel";
            this.CurrentActivityLabel.Size = new System.Drawing.Size(78, 13);
            this.CurrentActivityLabel.TabIndex = 16;
            this.CurrentActivityLabel.Text = "Current Activity";
            // 
            // CustomerGroup
            // 
            this.CustomerGroup.Controls.Add(this.CurrentActivityLabel);
            this.CustomerGroup.Controls.Add(this.CurrentActivity);
            this.CustomerGroup.Controls.Add(this.CustomerApplyButton);
            this.CustomerGroup.Controls.Add(this.CustomerName);
            this.CustomerGroup.Controls.Add(this.CustomerNameLabel);
            this.CustomerGroup.Controls.Add(this.AssignedRoomLabel);
            this.CustomerGroup.Controls.Add(this.AssignedRoom);
            this.CustomerGroup.Controls.Add(this.CustomerLabel);
            this.CustomerGroup.Controls.Add(this.CustomerBox);
            this.CustomerGroup.Location = new System.Drawing.Point(519, 13);
            this.CustomerGroup.Name = "CustomerGroup";
            this.CustomerGroup.Size = new System.Drawing.Size(191, 227);
            this.CustomerGroup.TabIndex = 5;
            this.CustomerGroup.TabStop = false;
            this.CustomerGroup.Text = "Customers";
            // 
            // LiveStatisticsButton
            // 
            this.LiveStatisticsButton.Location = new System.Drawing.Point(12, 217);
            this.LiveStatisticsButton.Name = "LiveStatisticsButton";
            this.LiveStatisticsButton.Size = new System.Drawing.Size(304, 23);
            this.LiveStatisticsButton.TabIndex = 6;
            this.LiveStatisticsButton.Text = "Show Live Statistics";
            this.LiveStatisticsButton.UseVisualStyleBackColor = true;
            this.LiveStatisticsButton.Click += new System.EventHandler(this.LiveStatisticsButton_Click);
            // 
            // ReceptionScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 330);
            this.Controls.Add(this.LiveStatisticsButton);
            this.Controls.Add(this.CustomerGroup);
            this.Controls.Add(this.FacilityGroup);
            this.Controls.Add(this.TimeBasedGroup);
            this.Controls.Add(this.DefaultValuesButton);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.SettingsGroup);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReceptionScreen";
            this.Text = "ReceptionScreen";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReceptionScreen_FormClosed);
            this.SettingsGroup.ResumeLayout(false);
            this.SettingsGroup.PerformLayout();
            this.TimeBasedGroup.ResumeLayout(false);
            this.TimeBasedGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimeBeforeDeath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StairTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.CleaningTime)).EndInit();
            this.FacilityGroup.ResumeLayout(false);
            this.FacilityGroup.PerformLayout();
            this.CustomerGroup.ResumeLayout(false);
            this.CustomerGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SettingsGroup;
        private System.Windows.Forms.Label SpeedLabel;
        private System.Windows.Forms.ComboBox SimulationSpeed;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Label ZoomLevelsLabel;
        private System.Windows.Forms.ComboBox ZoomLevel;
        private System.Windows.Forms.Button DefaultValuesButton;
        private System.Windows.Forms.GroupBox TimeBasedGroup;
        private System.Windows.Forms.Label StairTimeLabel;
        private System.Windows.Forms.Label CleaningTimeLabel;
        private System.Windows.Forms.NumericUpDown CleaningTime;
        private System.Windows.Forms.NumericUpDown StairTime;
        private System.Windows.Forms.Label TimeBeforeDeathLabel;
        private System.Windows.Forms.NumericUpDown TimeBeforeDeath;
        private System.Windows.Forms.GroupBox FacilityGroup;
        private System.Windows.Forms.Label RestaurantLabel;
        private System.Windows.Forms.ComboBox RestaurantBox;
        private System.Windows.Forms.Label CinemaLabel;
        private System.Windows.Forms.ComboBox CinemasBox;
        private System.Windows.Forms.Button RestaurantEditButton;
        private System.Windows.Forms.Button CinemaEditButton;
        private System.Windows.Forms.Label RoomsLabel;
        private System.Windows.Forms.Button RoomViewButton;
        private System.Windows.Forms.ComboBox RoomsBox;
        private System.Windows.Forms.ComboBox CustomerBox;
        private System.Windows.Forms.Label CustomerLabel;
        private System.Windows.Forms.TextBox AssignedRoom;
        private System.Windows.Forms.Label AssignedRoomLabel;
        private System.Windows.Forms.Label CustomerNameLabel;
        private System.Windows.Forms.TextBox CustomerName;
        private System.Windows.Forms.Button CustomerApplyButton;
        private System.Windows.Forms.TextBox CurrentActivity;
        private System.Windows.Forms.Label CurrentActivityLabel;
        private System.Windows.Forms.GroupBox CustomerGroup;
        private System.Windows.Forms.Button LiveStatisticsButton;
    }
}