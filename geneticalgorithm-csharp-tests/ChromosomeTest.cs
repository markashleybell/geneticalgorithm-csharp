using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using geneticalgorithm_csharp;

namespace geneticalgorithm_csharp_tests 
{
    [TestFixture]
    public class ChromosomeTest 
    {
        [Test]
	    public void Test_GetFitness() 
        {
		    Chromosome c1 = new Chromosome("Hello, world!");
		    Assert.AreEqual(0, c1.GetFitness());

		    Chromosome c2 = new Chromosome("H5p&J;!l<X\\7l");
		    Assert.AreEqual(399, c2.GetFitness());

		    Chromosome c3 = new Chromosome("Vc;fx#QRP8V\\$");
		    Assert.AreEqual(297, c3.GetFitness());

		    Chromosome c4 = new Chromosome("t\\O`E_Jx$n=NF");
		    Assert.AreEqual(415, c4.GetFitness());
	    }

	    /**
	     * Method used to test <code>Chromosome.generateRandom()</code>.
	     * 
	     * @see net.auxesia.Chromosome#generateRandom()
	     */
	    [Test]
	    public void Test_RandomGene() 
        {
		    Chromosome c;

		    for (int i = 0; i < 1000; i++) 
            {
			    c = Chromosome.GenerateRandom();

			    Assert.IsTrue(c.GetFitness() >= 0);

			    Assert.AreEqual(13, c.GetGene().Length);

			    foreach(char ch in c.GetGene().ToCharArray()) 
                {
				    Assert.IsTrue(ch >= 32);
				    Assert.IsTrue(ch <= 121);
			    }
		    }
	    }

	    /**
	     * Method to test <code>Chromosome.mutate()</code>.
	     * 
	     * @see net.auxesia.Chromosome#mutate()
	     */
	    [Test]
	    public void Test_Mutate() 
        {
		    Chromosome c1, c2;

		    char[] arr1, arr2;

		    for (int i = 0; i < 1000; i++) 
            {
			    c1 = Chromosome.GenerateRandom();
			    arr1 = c1.GetGene().ToCharArray();

			    c2 = c1.Mutate();
			    arr2 = c2.GetGene().ToCharArray();

			    Assert.AreEqual(arr1.Length, arr2.Length);

			    int diff = 0;

			    for (int j = 0; j < arr1.Length; j++) 
                {
				    if (arr1[j] != arr2[j]) 
                    {
					    ++diff;
				    }
			    }

			    Assert.IsTrue(diff <= 1);
		    }
	    }

	    /**
	     * Method to test <code>Chromosome.mate(Chromosome)</code>.
	     * 
	     * @see net.auxesia.Chromosome#mate(Chromosome)
	     */
	    [Test]
	    public void Test_Mate() 
        {
		    Chromosome c1 = Chromosome.GenerateRandom();
		    Chromosome c2 = Chromosome.GenerateRandom();

		    char[] arr1   = c1.GetGene().ToCharArray();
		    char[] arr2   = c2.GetGene().ToCharArray();

		    // Check to ensure the right number of children are returned.
		    Chromosome[] children = c1.Mate(c2);		
		    Assert.AreEqual(2, children.Length);

		    // Check the resulting child gene lengths
		    Assert.AreEqual(13, children[0].GetGene().Length);
		    Assert.AreEqual(13, children[1].GetGene().Length);

		    // Determine the pivot point for the mating
		    char[] tmpArr = children[0].GetGene().ToCharArray();
		    int pivot;
		    for (pivot = 0; pivot < arr1.Length; pivot++) 
            {
			    if (arr1[pivot] != tmpArr[pivot]) 
                {
				    break;
			    }
		    }

		    // Check the first child.
		    tmpArr = children[0].GetGene().ToCharArray();

		    for (int i = 0; i < tmpArr.Length; i++) 
            {
			    if (i < pivot) 
                {
				    Assert.AreEqual(arr1[i], tmpArr[i]);
			    } 
                else 
                {
				    Assert.AreEqual(arr2[i], tmpArr[i]);
			    }
		    }

		    // Check the second child.
		    tmpArr = children[1].GetGene().ToCharArray();

		    for (int i = 0; i < tmpArr.Length; i++) 
            {
			    if (i < pivot) 
                {
				    Assert.AreEqual(arr2[i], tmpArr[i]);
			    } 
                else 
                {
				    Assert.AreEqual(arr1[i], tmpArr[i]);
			    }
		    }
	    }

	    /**
	     * Method to test <code>Chromosome.CompareTo(Chromosome)</code>.
	     * 
	     * @see net.auxesia.Chromosome#CompareTo(Chromosome)
	     */
	    [Test]
	    public void Test_CompareTo() 
        {
		    Chromosome c1 = new Chromosome("Hello, world!");
		    Assert.AreEqual(0, c1.GetFitness());

		    Chromosome c2 = new Chromosome("H5p&J;!l<X\\7l");
		    Assert.AreEqual(399, c2.GetFitness());

		    Chromosome c3 = new Chromosome("Vc;fx#QRP8V\\$");
		    Assert.AreEqual(297, c3.GetFitness());

		    Chromosome c4 = new Chromosome("t\\O`E_Jx$n=NF");
		    Assert.AreEqual(415, c4.GetFitness());

		    Assert.AreEqual(0, c1.CompareTo(c1));
		    Assert.IsTrue(c1.CompareTo(c2) < 0);
		    Assert.IsTrue(c1.CompareTo(c3) < 0);
		    Assert.IsTrue(c1.CompareTo(c4) < 0);

		    Assert.AreEqual(0, c2.CompareTo(c2));
		    Assert.IsTrue(c2.CompareTo(c1) > 0);
		    Assert.IsTrue(c2.CompareTo(c3) > 0);
		    Assert.IsTrue(c2.CompareTo(c4) < 0);

		    Assert.AreEqual(0, c3.CompareTo(c3));
		    Assert.IsTrue(c3.CompareTo(c1) > 0);
		    Assert.IsTrue(c3.CompareTo(c2) < 0);
		    Assert.IsTrue(c3.CompareTo(c4) < 0);

		    Assert.AreEqual(0, c4.CompareTo(c4));
		    Assert.IsTrue(c4.CompareTo(c1) > 0);
		    Assert.IsTrue(c4.CompareTo(c2) > 0);
		    Assert.IsTrue(c4.CompareTo(c3) > 0);
	    }

	    /**
	     * Method to test <code>Chromosome.Equals(Object)</code>.
	     * 
	     * @see net.auxesia.Chromosome#Equals(Object)
	     */
	    [Test]
	    public void Test_Equals() 
        {
		    Chromosome c1 = new Chromosome("Hello, world!");
		    Chromosome c2 = new Chromosome("H5p&J;!l<X\\7l");

		    Assert.IsTrue(c1.Equals(c1));
		    Assert.IsTrue(c2.Equals(c2));

		    Assert.IsFalse(c1.Equals(c2));
		    Assert.IsFalse(c1.Equals(null));
		    Assert.IsFalse(c1.Equals(new Object()));

		    Assert.IsFalse(c2.Equals(c1));
		    Assert.IsFalse(c2.Equals(null));
		    Assert.IsFalse(c2.Equals(new Object()));
	    }
    }
}
