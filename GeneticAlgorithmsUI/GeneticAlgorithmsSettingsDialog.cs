using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GeneticAlgorithms;

namespace GeneticAlgorithmsUI
{
    public partial class GeneticAlgorithmsSettingsDialog : Form
    {
        public GeneticAlgorithmsSettingsDialog()
        {
            InitializeComponent();

            // Get the genetic simulation's assembly
            Assembly geneticSimulation = typeof(GeneticSimulation<>).Assembly;
            
            // Find the interface's types
            Type selection = typeof(ISelectionProvider);
            Type crossover = typeof(IRecombinationProvider);

            // Find example classes in the namespace which match the
            Dictionary<string, Type> selectionClasses = new Dictionary<string, Type>();
            Dictionary<string, Type> crossoverClasses = new Dictionary<string, Type>();
            foreach (Type type in geneticSimulation.GetTypes())
            {
                // Only use classes from the exampleClasses namespace
                if (type.Namespace.Contains("ExampleClasses"))
                {
                    // If an ISelection provider
                    if (selection.IsAssignableFrom(type))
                        selectionClasses.Add(type.Name, type);

                    // If an IRecombination Provider
                    if (crossover.IsAssignableFrom(type))
                        crossoverClasses.Add(type.Name, type);
                }
            }

            // Assign the keys to the combo boxes
            selectionComboBox.DataSource = selectionClasses.Keys.ToList();
            crossoverComboBox.DataSource = crossoverClasses.Keys.ToList();
        }
    }
}
