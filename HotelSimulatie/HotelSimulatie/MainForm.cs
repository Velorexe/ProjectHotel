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

            if(layoutFile == DialogResult.OK)
            {
                //Load in simulation
                SimulationForm form = new SimulationForm(openFileDialog.FileName);
                form.Show();
                this.Hide();
                //Open simulation form
                //Give the file name with the simulation
                //Perform the .layout to Hotel convertion
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
