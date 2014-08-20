using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GeneticAlgorithms;

namespace GeneticAlgorithmsUI
{
    public partial class GeneticAlgorithmsUI : Form
    {
        private GeneticSimulation<IGene> _geneticSimulation;

        public GeneticAlgorithmsUI(GeneticSimulation<IGene> geneticSimulation)
        {
            InitializeComponent();

            // Assign local data members
            _geneticSimulation = geneticSimulation;
        }

        private void launchSettingsButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void resetSimulationButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void simulateSingleButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void simulateMultipleButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
