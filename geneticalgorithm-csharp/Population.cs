using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace geneticalgorithm_csharp 
{
    public class Population 
    {
        // The size of the tournament
	    private const int TOURNAMENT_SIZE = 3;

	    // Convenience randomizer
	    private static Random rnd = new Random((int)DateTime.Now.Ticks);

	    private double _elitism;
	    private double _mutation;
	    private double _crossover;
	    private Chromosome[] _populace;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="size">The size of the population, where size > 0</param>
        /// <param name="crossoverRatio">The crossover ratio for the population during evolution, where 0.0 <= crossoverRatio <= 1.0</param>
        /// <param name="elitismRatio">The elitism ratio for the population during evolution, where 0.0 <= elitismRatio <= 1.0</param>
        /// <param name="mutationRatio">The mutation ratio for the population during evolution, where 0.0 <= mutationRatio <= 1.0</param>
	    public Population(int size, double crossoverRatio, double elitismRatio, double mutationRatio) 
        {
		    this._crossover = crossoverRatio;
		    this._elitism = elitismRatio;
		    this._mutation = mutationRatio;

		    // Generate an initial population
		    this._populace = new Chromosome[size];

		    for (int i = 0; i < size; i++) 
            {
			    this._populace[i] = Chromosome.GenerateRandom();
		    }

		    Array.Sort(this._populace);
	    }

	    /// <summary>
	    /// Evolve the population
	    /// </summary>
	    public void Evolve() 
        {
		    // Create a buffer for the new generation
		    Chromosome[] buffer = new Chromosome[_populace.Length];

		    // Copy over a portion of the population unchanged, based on 
		    // the elitism ratio.
		    int idx = (int)Math.Round(_populace.Length * _elitism);
		    Array.Copy(_populace, 0, buffer, 0, idx);

		    // Iterate over the remainder of the population and evolve as 
		    // appropriate.
		    while (idx < buffer.Length) 
            {
			    // Check to see if we should perform a crossover. 
			    if (rnd.NextDouble() <= _crossover) 
                {
				    // Select the parents and mate to get their children
				    Chromosome[] parents = SelectParents();
				    Chromosome[] children = parents[0].Mate(parents[1]);

				    // Check to see if the first child should be mutated.
				    if (rnd.NextDouble() <= _mutation) 
                    {
					    buffer[idx++] = children[0].Mutate();
				    } 
                    else 
                    {
					    buffer[idx++] = children[0];
				    }

				    // Repeat for the second child, if there is room.
				    if (idx < buffer.Length) 
                    {
					    if (rnd.NextDouble() <= _mutation) 
                        {
						    buffer[idx] = children[1].Mutate();
					    } 
                        else 
                        {
						    buffer[idx] = children[1];
					    }
				    }
			    } 
                else 
                { 
                    // No crossover, so copy verbatium.
				    // Determine if mutation should occur.
				    if (rnd.NextDouble() <= _mutation) 
                    {
					    buffer[idx] = _populace[idx].Mutate();
				    } 
                    else 
                    {
					    buffer[idx] = _populace[idx];
				    }
			    }

			    // Increase our counter
			    ++idx;
		    }

		    // Sort the buffer based on fitness.
		    Array.Sort(buffer);

		    // Reset the population
		    _populace = buffer;
	    }

	    /// <summary>
	    /// Retrieve a copy of the current population. This method returns a 
        /// copy of the population at the time the method was called.
	    /// </summary>
	    /// <returns>Array of Chromosomes representing current populace</returns>
	    public Chromosome[] GetPopulation() 
        {
		    Chromosome[] arr = new Chromosome[_populace.Length];
		    Array.Copy(_populace, 0, arr, 0, _populace.Length);

		    return arr;
	    }

	    /// <summary>
	    /// Retrieve the elitism ratio for the population.
	    /// </summary>
	    /// <returns>Elitism ratio</returns>
	    public double GetElitism() 
        {
		    return _elitism;
	    }

	    /// <summary>
	    /// Retrieve the crossover ratio for the population.
	    /// </summary>
	    /// <returns>Crossover ratio</returns>
	    public double GetCrossover() 
        {
		    return _crossover;
	    }

	    /// <summary>
	    /// Retrieve the mutation ratio for the population.
	    /// </summary>
	    /// <returns>Mutation ratio</returns>
	    public double GetMutation() 
        {
		    return _mutation;
	    }

        /// <summary>
        /// A helper method that can be used to select two random parents from the population to use in crossover during evolution.
        /// </summary>
        /// <returns>Two randomly selected Chromosomes for crossover.</returns>
	    private Chromosome[] SelectParents() 
        {
		    Chromosome[] parents = new Chromosome[2];

		    // Randomly select two parents via tournament selection.
		    for (int i = 0; i < 2; i++) 
            {
			    parents[i] = _populace[rnd.Next(_populace.Length)];

			    for (int j = 0; j < TOURNAMENT_SIZE; j++) 
                {
				    int idx = rnd.Next(_populace.Length);

				    if (_populace[idx].CompareTo(parents[i]) < 0)
					    parents[i] = _populace[idx];
			    }
		    }

		    return parents;
	    }
    }
}
