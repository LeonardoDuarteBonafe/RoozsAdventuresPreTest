using System;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlgorithm
{
	public List<DNA> Population { get; private set; }
	public int Generation { get; private set; }
	public double BestFitness { get; private set; }
	public List<int[]> BestGenes { get; private set; }

	public int Elitism;
	public float MutationRate;
	
	private List<DNA> newPopulation;
	private System.Random random;
	private double fitnessSum;
	private int dnaSize;
	private Func<double[]> getRandomGene;
	private Func<int, double> fitnessFunction;

	private string[] enemyList = { "spyke", "opossum", "eagle", "lifePoint" };

	private Test test = new Test();

	public GeneticAlgorithm(int populationSize, int dnaSize, System.Random random, Func<double[]> getRandomGene, Func<int, double> fitnessFunction,
		int elitism, float mutationRate = 0.01f)
	{
		Generation = 1;
		Elitism = elitism;
		MutationRate = mutationRate;
		Population = new List<DNA>(populationSize);
		newPopulation = new List<DNA>(populationSize);
		this.random = random;
		this.dnaSize = dnaSize;
		this.getRandomGene = getRandomGene;
		this.fitnessFunction = fitnessFunction;

		BestGenes = new List<int[]>(dnaSize);

		for (int i = 0; i < populationSize; i++)
		{
			Population.Add(new DNA(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));
		}
	}

	//public void NewGeneration(int dnaTargetSize, int numNewDNA = 0, bool crossoverNewDNA = false)
	public void NewGeneration(int numNewDNA = 0, bool crossoverNewDNA = false)
	{
		
		int finalCount = Population.Count + numNewDNA;

		if (finalCount <= 0) {
			return;
		}

		if (Population.Count > 0) {
			CalculateFitness();
			Population.Sort(CompareDNA);

            //testando fitness
            /*for (int k = 0; k < 5; k++)
            {
				DNA test = Population[k];
				int spykeCount = 0;
				int opossumCount = 0;
				int eagleCount = 0;
				int lifeCount = 0;

				for (int j = 0; j < test.Genes.Count; j++)
				{
					switch ((int)test.Genes[j].GetValue(test.Genes[j].Length - 1))
					{
						case 0:
							spykeCount++;
							break;
						case 1:
							opossumCount++;
							break;
						case 2:
							eagleCount++;
							break;
						case 3:
							lifeCount++;
							break;
					}
				}
				Debug.Log("Valores do K = " + k + " com FITNESS = " + test.Fitness + " : -> SPYKE: " + spykeCount + " || OPOSSUM: " + opossumCount + " || EAGLE: " + eagleCount + " || LIFE: " + lifeCount);

			}*/
		}
		newPopulation.Clear();
	
		for (int i = 0; i < finalCount; i++)
		{

			// Debug.Log("NOVO INDIVIDUO");
			if (i < Elitism && i < Population.Count)
			{
				newPopulation.Add(Population[i]);
			}
			else if (i < Population.Count || crossoverNewDNA)
			{
				DNA parent1 = ChooseParent();
				DNA parent2 = ChooseParent();
				DNA child = parent1.Crossover(parent2);
				child.Mutate(MutationRate);

				// for (int j = 0; j < child.Genes.Count; j++)
				// {
				// 	Debug.Log("Filho Mutado: {" + child.Genes[j].GetValue(0) + ", " +  child.Genes[j].GetValue(1) + ", " + child.Genes[j].GetValue(2) + "}");
				// }
				
				newPopulation.Add(child);
			}
			else
			{
			/*if (dnaTargetSize > 50)
			{
				dnaTargetSize = 50;
			}*/
			//Debug.Log("DNA SIZE AQUI: " + dnaTargetSize);
			
			newPopulation.Add(new DNA(dnaSize, random, getRandomGene, fitnessFunction, shouldInitGenes: true));

			}
		}

		List<DNA> tmpList = Population;
        /*for (int i = 0; i < tmpList.Count; i++)
        {
			Debug.Log("SIZE NO FOR: " + tmpList[i].size);
        }*/
		Population = newPopulation;
		newPopulation = tmpList;

		Generation++;
	}
	
	private int CompareDNA(DNA a, DNA b)
	{
		if (a.Fitness > b.Fitness) {
			return -1;
		} else if (a.Fitness < b.Fitness) {
			return 1;
		} else {
			return 0;
		}
	}

	private void CalculateFitness()
	{
		fitnessSum = 0;
		DNA firstBest = Population[0];
		//DNA secondBest = Population[0];
		//DNA thirdBest = Population[0];

		// Debug.Log("População:" + Population.Count);

		for (int i = 0; i < Population.Count; i++)
		{
			fitnessSum += Population[i].CalculateFitness(i);
			// Debug.Log("Somatório Fitness:" + fitnessSum);

			if (Population[i].Fitness > firstBest.Fitness)
			{
				//thirdBest = secondBest;
				//secondBest = firstBest;
				firstBest = Population[i];
			}
		}
		//Debug.Log("Printar o melhor individu, tendo fitness de: " + firstBest.Fitness);
		//Debug.Log("Printar o SEGUNDO melhor individu, tendo fitness de: " + secondBest.Fitness);
		//Debug.Log("Printar o TERCEIRO melhor individu, tendo fitness de: " + thirdBest.Fitness);
		/*if(firstBest.Fitness >= 120)
        {
			*//*int spykeCount = 0;
			int opossumCount = 0;
			int eagleCount = 0;
			int lifeCount = 0;

            for (int j = 0; j < firstBest.Genes.Count; j++)
            {
				switch((int)firstBest.Genes[j].GetValue(firstBest.Genes[j].Length - 1))
                {
					case 0:
						spykeCount++;
						break;
					case 1:
						opossumCount++;
						break;
					case 2:
						eagleCount++;
						break;
					case 3:
						lifeCount++;
						break;
                }
			}
			Debug.Log("Valores do FIRST: FITNESS = " + firstBest.Fitness + " -> SPYKE: " + spykeCount + " || OPOSSUM: " + opossumCount + " || EAGLE: " + eagleCount + " || LIFE: " + lifeCount);

			spykeCount = 0;
			opossumCount = 0;
			eagleCount = 0;
			lifeCount = 0;

			for (int j = 0; j < secondBest.Genes.Count; j++)
			{
				switch ((int)secondBest.Genes[j].GetValue(secondBest.Genes[j].Length - 1))
				{
					case 0:
						spykeCount++;
						break;
					case 1:
						opossumCount++;
						break;
					case 2:
						eagleCount++;
						break;
					case 3:
						lifeCount++;
						break;
				}
			}
			Debug.Log("Valores do SECOND: FITNESS = " + secondBest.Fitness + " -> SPYKE: " + spykeCount + " || OPOSSUM: " + opossumCount + " || EAGLE: " + eagleCount + " || LIFE: " + lifeCount);

			spykeCount = 0;
			opossumCount = 0;
			eagleCount = 0;
			lifeCount = 0;

			for (int j = 0; j < thirdBest.Genes.Count; j++)
			{
				switch ((int)thirdBest.Genes[j].GetValue(thirdBest.Genes[j].Length - 1))
				{
					case 0:
						spykeCount++;
						break;
					case 1:
						opossumCount++;
						break;
					case 2:
						eagleCount++;
						break;
					case 3:
						lifeCount++;
						break;
				}
			}
			Debug.Log("Valores do THIRD: FITNESS = " + thirdBest.Fitness + " -> SPYKE: " + spykeCount + " || OPOSSUM: " + opossumCount + " || EAGLE: " + eagleCount + " || LIFE: " + lifeCount);*/

			/*if ((int)secondBest.Genes[i].GetValue(secondBest.Genes[i].Length - 1) != 0)
			{
				Debug.Log("Segundo Elemento: " + enemyList[(int)secondBest.Genes[i].GetValue(firstBest.Genes[i].Length - 1)]);
			}
			if ((int)thirdBest.Genes[i].GetValue(thirdBest.Genes[i].Length - 1) != 0)
			{
				Debug.Log("Terceiro Elemento: " + enemyList[(int)thirdBest.Genes[i].GetValue(thirdBest.Genes[i].Length - 1)]);
			}*//*
			//Debug.Log("ELEMENTO: " + enemyList[(int)firstBest.Genes[i].GetValue(firstBest.Genes[i].Length - 1)]);
		}*/


		BestFitness = firstBest.Fitness;
		// Debug.Log("Best Gene: " + best.Genes.Count);
	

		// best.Genes.CopyTo(BestGenes, 0);
		for(int i = 0; i < BestGenes.Count; i++){ 
			
    		for(int j = 0; j < BestGenes[i].Length; j++){
				firstBest.Genes[i].SetValue(BestGenes[i].GetValue(j),j);  

    		}
		}

	}

	private DNA ChooseParent()
	{
		double randomNumber = random.NextDouble() * fitnessSum; 
		
		for (int i = 0; i < Population.Count; i++)
		{
			if (randomNumber < Population[i].Fitness) 
			{
				return Population[i];
			}

			randomNumber -= Population[i].Fitness;
		}

		return null;
	}
}
