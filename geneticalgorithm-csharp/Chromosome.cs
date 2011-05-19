using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace geneticalgorithm_csharp 
{
    public class Chromosome : IComparable<Chromosome>
    {
        private string _gene;
	    private int _fitness;

	    // The target gene, converted to an array for convenience
	    private static char[] TARGET_GENE = "Hello, world!".ToCharArray();

	    // Convenience randomizer
	    private static Random rnd = new Random((int)DateTime.Now.Ticks);

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="gene">The gene representing this Chromosome</param>
	    public Chromosome(String gene) 
        {
		    this._gene = gene;
		    this._fitness = CalculateFitness(gene);
	    }

	    /// <summary>
	    /// Retrieve the gene for this Chromosome
	    /// </summary>
	    /// <returns>The gene for this Chromosome</returns>
	    public String GetGene() 
        {
		    return _gene;
	    }

	    /// <summary>
	    /// Method to retrieve the fitness of this Chromosome.  
        /// Note that a lower fitness indicates a better Chromosome for the solution.
	    /// </summary>
	    /// <returns>The fitness of this Chromosome</returns>
	    public int GetFitness() 
        {
		    return _fitness;
	    }

	    /// <summary>
	    /// Helper method used to calculate the fitness for a given gene. 
        /// The fitness is defined as being the sum of the absolute value of the difference 
        /// between the current gene and the target gene. 
	    /// </summary>
	    /// <param name="gene">The gene to calculate the fitness for</param>
	    /// <returns>The calculated fitness of the given gene</returns>
	    private static int CalculateFitness(String gene) 
        {
		    int fitness = 0;
		    char[] arr  = gene.ToCharArray();

		    for (int i = 0; i < arr.Length; i++) 
            {
			    fitness += Math.Abs(((int) arr[i]) - ((int) TARGET_GENE[i]));
		    }

		    return fitness;
	    }

	    /// <summary>
	    /// Method to generate a new Chromosome that is a random mutation of this Chromosome.  
        /// This method randomly selects one character in the Chromosomes gene, then replaces 
        /// it with another random (but valid) character. Note that this method returns a new Chromosome, 
        /// it does not modify the existing Chromosome.
	    /// </summary>
	    /// <returns>A mutation of this Chromosome</returns>
	    public Chromosome Mutate() 
        {
		    char[] arr  = _gene.ToCharArray();
		    int idx     = rnd.Next(arr.Length);
		    int delta   = (rnd.Next() % 90) + 32;
		    arr[idx]    = (char) ((arr[idx] + delta) % 122);

		    return new Chromosome(String.Join("", arr));
	    }

	    /// <summary>
	    /// Method used to mate this Chromosome with another. The resulting child Chromosomes are returned.
	    /// </summary>
	    /// <param name="mate">The Chromosome to mate with</param>
	    /// <returns>The resulting Chromosome children</returns>
	    public Chromosome[] Mate(Chromosome mate) 
        {
		    // Convert the genes to arrays to make thing easier.
		    char[] arr1  = _gene.ToCharArray();
		    char[] arr2  = mate._gene.ToCharArray();

		    // Select a rndom pivot point for the mating
		    int pivot = rnd.Next(arr1.Length);

		    // Provide a container for the child gene data
		    char[] child1 = new char[_gene.Length];
		    char[] child2 = new char[_gene.Length];

		    // Copy the data from each gene to the first child.
		    Array.Copy(arr1, 0, child1, 0, pivot);
		    Array.Copy(arr2, pivot, child1, pivot, (child1.Length - pivot));

		    // Repeat for the second child, but in reverse order.
		    Array.Copy(arr2, 0, child2, 0, pivot);
		    Array.Copy(arr1, pivot, child2, pivot, (child2.Length - pivot));

		    return new Chromosome[] { 
                new Chromosome(String.Join("", child1)), 
				new Chromosome(String.Join("", child2)) 
            }; 
	    }

        /// <summary>
        /// A convenience method to generate a random Chromosome.
        /// </summary>
        /// <returns>A randomly generated Chromosome</returns>
	    public static Chromosome GenerateRandom() 
        {
		    char[] arr = new char[TARGET_GENE.Length];

		    for (int i = 0; i < arr.Length; i++) 
            {
			    arr[i] = (char) (rnd.Next(90) + 32);
		    }

		    return new Chromosome(String.Join("", arr));
	    }

	    /// <summary>
	    /// Method to allow for comparing Chromosome objects with one another based on fitness. 
        /// Chromosome ordering is based on the natural ordering of the fitnesses of the Chromosomes.  
	    /// </summary>
	    /// <param name="c">Chromosome to compare against</param>
	    /// <returns> An integer value that indicates the relative order of the objects being compared. 
        /// Less than zero: This object is less than the other parameter. Zero: This object is equal to other. 
        /// Greater than zero: This object is greater than other.</returns>
	    public int CompareTo(Chromosome c) 
        {
		    if (_fitness < c._fitness) 
            {
			    return -1;
		    } 
            else if (_fitness > c._fitness) 
            {
			    return 1;
		    }

		    return 0;
	    }

	    /// <summary>
	    /// Override for Object.Equals
	    /// </summary>
	    /// <param name="o">Chromosome to compare against</param>
	    /// <returns>True if Chromosomes are equal</returns>
	    public override bool Equals(Object o) 
        {
		    if (!(o is Chromosome))
			    return false;

		    Chromosome c = (Chromosome) o;
		    return (_gene.Equals(c._gene) && _fitness == c._fitness);
	    }

	    /// <summary>
	    /// Override for Object.GetHashCode
	    /// </summary>
	    /// <returns>Hashcode for this Chromosome</returns>
	    public override int GetHashCode() 
        {		
		    return new StringBuilder().Append(_gene).Append(_fitness).ToString().GetHashCode();
	    }
    }
}
