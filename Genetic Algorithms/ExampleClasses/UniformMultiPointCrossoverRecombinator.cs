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
        public PairedChromosomes<Gene> Recombine<Gene>(PairedChromosomes<Gene> couple) where Gene: IGene, new()
        {
            // Copy the genes to the children
            List<Gene> maleChild = new List<Gene>(couple.Male.Genes);
            List<Gene> femaleChild = new List<Gene>(couple.Male.Genes);

            // Calculate the length of the shortest gene
            int shorterGene = Math.Min(maleChild.Count, femaleChild.Count);

            // For each gene copy across if necessary
            bool crossoverGenes = false;
            for (int i = 0; i < shorterGene; i++)
            {
                // If we're on the boundary which gene to take from
                if (i % GenesPerCrossover == 0) crossoverGenes = !crossoverGenes;

                // Take from the other chromosome
                if (!crossoverGenes) maleChild[i] = (Gene)couple.Female.Genes[i].Clone();
                if (!crossoverGenes) femaleChild[i] = (Gene)couple.Male.Genes[i].Clone();
            }

            return new PairedChromosomes<Gene>(
                new Chromosome<Gene>(maleChild),
                new Chromosome<Gene>(femaleChild));
        }
    }
}
