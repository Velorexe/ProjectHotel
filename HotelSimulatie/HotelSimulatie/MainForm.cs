﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace HotelSimulatie
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = "Hotel.layout";
            openFileDialog.Filter = "Hotel Layout|*.layout";
            DialogResult layoutFile = openFileDialog.ShowDialog();

            if (layoutFile == DialogResult.OK)
            {
                SettingsGroup.Visible = true;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void StartSimulation_Click(object sender, EventArgs e)
        {
            Settings configSettings = new Settings() { HTEFactor = (int)HTEFactor.Value, StairCase = (int)StaircaseTime.Value };
            SimulationForm form = new SimulationForm(openFileDialog.FileName, configSettings);
            form.Show();
            this.Hide();
        }
    }
}
