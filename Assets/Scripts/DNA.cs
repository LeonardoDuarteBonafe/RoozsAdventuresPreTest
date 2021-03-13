using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DNA
{
	public List<double[]> Genes { get; private set; }
	public double Fitness { get; private set; }
	public int size;

	private System.Random random;
	private Func<double[]> getRandomGene;
	private Func<int, double> fitnessFunction;
	private string[] objectList = { "spyke", "opossum", "eagle", "lifePoint" };


	public DNA(int size, System.Random random, Func<double[]> getRandomGene, Func<int, double> fitnessFunction, bool shouldInitGenes = true)
	{

		Genes =  new List<double[]>(size);
		//Debug.Log("Criando com tam: " + size);
		
		if (shouldInitGenes)
		{
			for (int i = 0; i < size; i++) //Genes.Count
			{
				/*int[] temp = getRandomGene();
				List<int> aux = new List<int>() ;
				int[] finalTemp = null;
                for (int j = 0; j < temp.Length-1; j++)
                {
					aux.Add(temp[j]);
					finalTemp[j] = temp[j];
					Debug.Log("AUX: " + aux[j] + " || Final temp: " + finalTemp[j]);
                }
				Debug.Log("Tipo do objeto e: " + objectList[temp[temp.Length - 1]]);
				Genes.Add(finalTemp); //GetElement()*/
				double[] temp = getRandomGene();
				/*if(temp[3] == 3)
                {
					Test.
                }*/
				//Debug.Log("Tipo do objeto: " + objectList[temp[temp.Length - 1]]);

				//Array.Resize(ref temp, temp.Length - 1);
               
				/*for (int j = 0; j < temp.Length; j++)
                {
					Debug.Log("Valores do objetos sao: " + temp[j]);
                }*/
				Genes.Add(temp); //GetElement()
			}
		}

		this.random = random;
		this.getRandomGene = getRandomGene;
		this.fitnessFunction = fitnessFunction;
	}

	public double CalculateFitness(int index)
	{
		Fitness = fitnessFunction(index);
		return Fitness;
	}

	public DNA Crossover(DNA otherParent)
	{
		DNA child = new DNA(Genes.Count, random, getRandomGene, fitnessFunction, shouldInitGenes: false);
		

	
		for(int i = 0; i < Genes.Count; i++){


			// Debug.Log("Pai: {" + Genes[i].GetValue(0) + ", " +  Genes[i].GetValue(1) + ", " + Genes[i].GetValue(2) + "}");
			// Debug.Log("Mãe: {" + otherParent.Genes[i].GetValue(0) + ", " +  otherParent.Genes[i].GetValue(1) + ", " + otherParent.Genes[i].GetValue(2) + "}");

			double rand = random.NextDouble();

    		// for(int j = 0; j < Genes[i].Length; j++){   

				// child.Genes[i].SetValue(random.NextDouble() < 0.5 ? Genes[i].GetValue(j):otherParent.Genes[i].GetValue(j),j);
        		//  child.Genes[i] = random.NextDouble() < 0.5 ? Genes[i]:otherParent.Genes[i];

				if(rand < 0.5){
				// child.Genes[i].SetValue(Genes[i].GetValue(j),j);
				// 	child.Genes[i] = Genes[i];
				if (Genes[i].Length > 3)
				{
					/*int[] temp = Genes[i];
					Array.Resize(ref temp, Genes[i].Length - 1);
					Genes[i] = temp;*/
					//Debug.Log("SIZE OF FATHER ERA MAIOR QUE 3, AGORA E: " + Genes[i].Length);
				}
				child.Genes.Add(Genes[i]);

				}else {
                // child.Genes[i].SetValue(otherParent.Genes[i].GetValue(j),j);
                // 	child.Genes[i] = otherParent.Genes[i];
                if (Genes[i].Length > 3)
                {
					/*int[] temp = Genes[i];
					Array.Resize(ref temp, Genes[i].Length - 1);
					Genes[i] = temp;*/
					//Debug.Log("SIZE OF MOTHER ERA MAIOR QUE 3, AGORA E: " + Genes[i].Length);
				}
				child.Genes.Add(otherParent.Genes[i]);
				}

				//Debug.Log("Filho: {" + child.Genes[i].GetValue(0) + ", " +  child.Genes[i].GetValue(1) + ", " + child.Genes[i].GetValue(2) + "}");
    		// }
		}
		return child;
	
	
	}

	public void Mutate(float mutationRate)
	{

		for (int i = 0; i < Genes.Count; i++)
		{
			if (random.NextDouble() < mutationRate)
			{
				double[] temp = getRandomGene();
				/*if(temp.Length > 3)
                {
					Array.Resize(ref temp, temp.Length - 1);
					//Debug.Log("VALOR DA MUTAÇÃO ERA 3, AGORA E: " + temp.Length);
                }
				if(temp.Length > 3)
                {
					//Array.Resize(ref temp, temp.Length - 1);
					Debug.Log("VALOR DA MUTACAO AINDA FOI MAIOR: " + temp.Length);
                }*/
				Genes[i] = temp; //GetElement() 
				//Genes[i] = getRandomGene(); //GetElement() 
				// Debug.Log( "valor de mutação de i " + i + " : {" + Genes[i].GetValue(0) + ", " +  Genes[i].GetValue(1) + ", " + Genes[i].GetValue(2) + "}");
			}
		}
	}


}