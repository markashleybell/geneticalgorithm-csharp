using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using geneticalgorithm_csharp;

namespace geneticalgorithm_csharp_tests 
{
    [TestFixture]
    public class PopulationTest 
    {
        [Test]
	    public void Test_GetCrossover() 
        {
		    Population pop = new Population(1024, 0.8d, 0.1d, 0.05d);
		    Assert.AreEqual(80, (int) (pop.GetCrossover() * 100));

		    pop = new Population(1024, 0.0d, 0.1d, 0.05d);
		    Assert.AreEqual(0, (int) (pop.GetCrossover() * 100));

		    pop = new Population(1024, 1.0d, 0.1d, 0.05d);
		    Assert.AreEqual(100, (int) (pop.GetCrossover() * 100));
	    }

	    [Test]
	    public void Test_GetElitism() 
        {
		    Population pop = new Population(1024, 0.8d, 0.1d, 0.05d);
		    Assert.AreEqual(10, (int) (pop.GetElitism() * 100));

		    pop = new Population(1024, 0.8d, 0.0d, 0.05d);
		    Assert.AreEqual(0, (int) (pop.GetElitism() * 100));

		    pop = new Population(1024, 0.8d, 0.99d, 0.05d);
		    Assert.AreEqual(99, (int) (pop.GetElitism() * 100));
	    }

	    [Test]
	    public void Test_GetMutation() 
        {
		    Population pop = new Population(1024, 0.8d, 0.1d, 0.05d);
		    Assert.AreEqual(5, (int) (pop.GetMutation() * 100));

		    pop = new Population(1024, 0.8d, 0.1d, 0.0d);
		    Assert.AreEqual(0, (int) (pop.GetMutation() * 100));

		    pop = new Population(1024, 0.8d, 0.1d, 1.0d);
		    Assert.AreEqual(100, (int) (pop.GetMutation() * 100));
	    }

	    [Test]
	    public void Test_GetPopulation() 
        {
		    Population pop   = new Population(1024, 0.8d, 0.1d, 0.05d);
		    Chromosome[] arr = pop.GetPopulation();

		    Assert.AreEqual(1024, arr.Length);

		    Chromosome[] newArr = new Chromosome[arr.Length];

		    arr.CopyTo(newArr, 0);
		    Array.Sort(newArr);

		    // Assert that the array is actually sorted.
		    CollectionAssert.AreEqual(arr, newArr);
	    }

	    [Test]
	    public void Test_Evolve() 
        {
		    Population pop = new Population(1024, 0.8d, 0.1d, 0.05d);
		    Chromosome[] oldArr = pop.GetPopulation();

		    // Evolve and get the new population
		    pop.Evolve();
		    Chromosome[] newArr = pop.GetPopulation();

		    // Check the details on the resulting evolved population.
		    Assert.AreEqual(80, (int) (pop.GetCrossover() * 100));
		    Assert.AreEqual(10, (int) (pop.GetElitism() * 100));
		    Assert.AreEqual(5, (int) (pop.GetMutation() * 100));

		    // Check to ensure that the elitism took.
		    int elitismCount = (int)Math.Round(1024 * 0.1d);

		    int counter = 0;

		    for (int i = 0; i < oldArr.Length; i++) 
            {
			    if (Array.BinarySearch(newArr, oldArr[i]) >= 0) 
                {
				    ++counter;
			    }
		    }

		    // Account for the fact that mating/mutation may cause more than
		    // just the fixed number of chromosomes to be identical.
		    Assert.IsTrue(counter >= elitismCount);
		    Assert.IsTrue(counter < oldArr.Length);
	    }
    }
}
