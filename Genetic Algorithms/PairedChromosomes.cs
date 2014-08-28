using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgorithms
{
    public class PairedChromosomes<Gene> where Gene: IGene, new()
    {
        private Chromosome<Gene> _male;
        public Chromosome<Gene> Male { get { return _male; } }
        private Chromosome<Gene> _female;
        public Chromosome<Gene> Female { get { return _female; } }
        public IEnumerable<Chromosome<Gene>> Chromosomes
        {
            get
            {
                List<Chromosome<Gene>> result = new List<Chromosome<Gene>>();
                result.Add(_male);
                result.Add(_female);
                return result;
            }
        }

        public PairedChromosomes(Chromosome<Gene> male, Chromosome<Gene> female)
        {
            _male = male;
            _female = female;
        }

        public PairedChromosomes<Gene> Reproduce(IRecombinationProvider recombinator)
        {
            return recombinator.Recombine(this);
        }
    }
}
