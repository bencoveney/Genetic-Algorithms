using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmsUI
{
    class GenerationDataGridItem
    {
        public string GenerationName { get; set; }
        public float AverageFitness { get; set; }
        public float MaximumFitness { get; set; }
        public string MostSuccessfulChromosome { get; set; }
        public string LeastSuccessfulChromosome { get; set; }
    }
}
