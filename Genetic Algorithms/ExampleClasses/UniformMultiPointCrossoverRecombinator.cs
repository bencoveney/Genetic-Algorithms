using System;
using System.Collections;
using System.Collections.Generic;

namespace GeneticAlgorithms.ExampleClasses
{
    /// <summary>
    /// Produces a single gene list by recombining two parents 
    /// </summary>
    public class UniformMultiPointCrossoverRecombinator : IRecombinationProvider
    {
        /// <summary>
        /// The amount of genes to take from each chromosome at a time before switching to take from the other
        /// </summary>
        public static float GenesPerCrossover = 5;

        /// <summary>
        /// See interface documentation
        /// </summary>
        public List<Gene> Recombine<Gene>(List<Gene> maleGenes, List<Gene> femaleGenes) where Gene: IGene, new()
        {
            // Copy the male genes to the child
            List<Gene> Child = new List<Gene>(maleGenes);

            // For each gene copy from the female gene if necessary
            bool usingMale = false;
            for (int i = 0; i < maleGenes.Count; i++)
            {
                // If we're on the boundary which gene to take from
                if (i % GenesPerCrossover == 0) usingMale = !usingMale;

                // Take from the female
                if(!usingMale) Child[i] = (Gene)femaleGenes[i].Clone();
            }

            return Child;
        }
    }
}
