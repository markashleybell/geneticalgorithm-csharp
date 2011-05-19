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

	    /** The target gene, converted to an array for convenience. */
	    private static char[] TARGET_GENE = "Hello, world!".ToCharArray();

	    /** Convenience rndomizer. */
	    private static Random rnd = new Random((int)DateTime.Now.Ticks);

	    /**
	     * Default constructor.
	     *
	     * @param gene The gene representing this <code>Chromosome</code>.
	     */
	    public Chromosome(String gene) 
        {
		    this._gene    = gene;
		    this._fitness = CalculateFitness(gene);
	    }

	    /**
	     * Method to retrieve the gene for this <code>Chromosome</code>.
	     *
	     * @return The gene for this <code>Chromosome</code>.
	     */
	    public String GetGene() 
        {
		    return _gene;
	    }

	    /**
	     * Method to retrieve the fitness of this <code>Chromosome</code>.  Note
	     * that a lower fitness indicates a better <code>Chromosome</code> for the
	     * solution.
	     *
	     * @return The fitness of this <code>Chromosome</code>.
	     */
	    public int GetFitness() 
        {
		    return _fitness;
	    }

	    /**
	     * Helper method used to calculate the fitness for a given gene.  The
	     * fitness is defined as being the sum of the absolute value of the 
	     * difference between the current gene and the target gene.
	     * 
	     * @param gene The gene to calculate the fitness for.
	     * 
	     * @return The calculated fitness of the given gene.
	     */
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

	    /**
	     * Method to generate a new <code>Chromosome</code> that is a rndom
	     * mutation of this <code>Chromosome</code>.  This method rndomly
	     * selects one character in the <code>Chromosome</code>s gene, then
	     * replaces it with another rndom (but valid) character.  Note that
	     * this method returns a new <code>Chromosome</code>, it does not
	     * modify the existing <code>Chromosome</code>.
	     * 
	     * @return A mutated version of this <code>Chromosome</code>.
	     */
	    public Chromosome Mutate() 
        {
		    char[] arr  = _gene.ToCharArray();
		    int idx     = rnd.Next(arr.Length);
		    int delta   = (rnd.Next() % 90) + 32;
		    arr[idx]    = (char) ((arr[idx] + delta) % 122);

		    return new Chromosome(String.Join("", arr));
	    }

	    /**
	     * Method used to mate this <code>Chromosome</code> with another.  The
	     * resulting child <code>Chromosome</code>s are returned.
	     * 
	     * @param mate The <code>Chromosome</code> to mate with.
	     * 
	     * @return The resulting <code>Chromosome</code> children.
	     */
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

	    /**
	     * A convenience method to generate a rndome <code>Chromosome</code>.
	     * 
	     * @return A rndomly generated <code>Chromosome</code>.
	     */
	    public static Chromosome GenerateRandom() 
        {
		    char[] arr = new char[TARGET_GENE.Length];

		    for (int i = 0; i < arr.Length; i++) 
            {
			    arr[i] = (char) (rnd.Next(90) + 32);
		    }

		    return new Chromosome(String.Join("", arr));
	    }

	    /**
	     * Method to allow for comparing <code>Chromosome</code> objects with
	     * one another based on fitness.  <code>Chromosome</code> ordering is 
	     * based on the natural ordering of the fitnesses of the
	     * <code>Chromosome</code>s.  
	     */
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

	    /**
	     * @see Object#equals(Object)
	     */
	    public override bool Equals(Object o) 
        {
		    if (!(o is Chromosome))
			    return false;

		    Chromosome c = (Chromosome) o;
		    return (_gene.Equals(c._gene) && _fitness == c._fitness);
	    }

	    /**
	     * @see Object#hashCode()
	     */
	    public override int GetHashCode() 
        {		
		    return new StringBuilder().Append(_gene).Append(_fitness).ToString().GetHashCode();
	    }
    }
}
