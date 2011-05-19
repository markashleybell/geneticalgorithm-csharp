using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using geneticalgorithm_csharp;

namespace geneticalgorithm_csharp_driver 
{
    class Program 
    {
        static void Main(string[] args) 
        {
            // The size of the simulation population
		    const int populationSize = 2048;

		    // The maximum number of generations for the simulation.
		    const int maxGenerations = 50000;

		    // The probability of crossover for any member of the population,
		    // where 0.0 <= crossoverRatio <= 1.0
		    const double crossoverRatio = 0.8d;

		    // The portion of the population that will be retained without change
		    // between evolutions, where 0.0 <= elitismRatio < 1.0
		    const double elitismRatio = 0.1d;

		    // The probability of mutation for any member of the population,
		    // where 0.0 <= mutationRatio <= 1.0
		    const double mutationRatio = 0.20d;

		    // Get the current run time.  Not very accurate, but useful for 
		    // some simple reporting.
		    long startTime = DateTime.Now.Ticks;

		    // Create the initial population
		    Population population = new Population(populationSize, crossoverRatio, elitismRatio, mutationRatio);

		    // Start evolving the population, stopping when the maximum number of
		    // generations is reached, or when we find a solution.
		    int i = 0;
		    Chromosome best = population.GetPopulation()[0];

		    while ((i++ <= maxGenerations) && (best.GetFitness() != 0)) 
            {
			    Console.WriteLine("Generation " + i + ": " + best.GetGene());
			    population.Evolve();
			    best = population.GetPopulation()[0];
		    }

		    // Get the end time for the simulation.
		    long endTime = DateTime.Now.Ticks;

		    // Print out some information to the console.
		    Console.WriteLine("Generation " + i + ": " + best.GetGene());
		    Console.WriteLine("Total execution time: " + (endTime - startTime) + "ms");

            Console.ReadLine();
        }
    }
}
