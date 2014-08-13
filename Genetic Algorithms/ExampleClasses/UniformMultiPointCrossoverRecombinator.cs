using System;
using System.Collections;
using System.Collections.Generic;

namespace GeneticAlgorithms.Example_Classes
{
    public class UniformMultiPointCrossoverRecombinator : IRecombinationProvider
    {
        public static float GenesPerCrossover = 5;

        Random random = new Random();
        #region IRecombinationProvider Member

        public List<Gene> Recombine<Gene>(List<Gene> maleGenes, List<Gene> femaleGenes) where Gene: IGene, new()
        {
            List<Gene> Child = new List<Gene>(maleGenes);

            bool usingMale = true;
            for (int i = 0; i < maleGenes.Count; i++)
            {
                Child[i] = usingMale ? maleGenes[i] : Child[i] = femaleGenes[i];
                if (i % GenesPerCrossover == 0) usingMale = !usingMale;
            }

            return Child;
        }

        #endregion
    }
}
