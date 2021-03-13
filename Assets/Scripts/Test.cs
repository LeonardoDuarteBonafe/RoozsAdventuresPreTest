using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Test : MonoBehaviour
{
	[Header("Genetic Algorithm")]
	[SerializeField] public double fitnessTarget = 100;
	[SerializeField] public int sizeTarget;

	private List<double[]> GenesList = new List<double[]>();
	private string[] enemyList = {"spyke", "opossum", "eagle", "lifePoint", "frog"};

	//Bonificação, Num Ações e Complexidade
	/*private int[] spyke = {1,1, 4, 0};
	private int[] opossum = {1,1, 4, 1};
	private int[] eagle = {1, 1, 4, 2};
	private int[] lifePoint = {1,1, 4, 3};
	private int[] frog = {1,1,2, 4};
	private int[] element;*/
	
	public double[] spyke = {1, 1, 1.8, 0};
	public double[] opossum = {1, 1, 3, 1};
	public double[] eagle = {1, 1, 3, 2};
	public double[] lifePoint = {1, 1, 1, 3};
	public double[] frog = {1, 1, 2.3, 4};
	public double[] element;

	[SerializeField] int populationSize = 200;
	[SerializeField] float mutationRate = 0.01f;
	[SerializeField] int elitism = 5;

	[Header("Other")]

	//[SerializeField] Text target;
	//[SerializeField] Text bestText;

	//[SerializeField] Text bestFitnessText;
	private float bestFitness = 0;
	//[SerializeField] Text numGenerationsText;

	private int numGenerations = 0; 
	//[SerializeField] Text textPrefab;

	private GeneticAlgorithm ga;
	private System.Random random;

	public GameObject enemyEagle;
	public GameObject enemySpyke;
	public GameObject enemyFrog;
	public GameObject enemyOpossum;
	public GameObject enemyLife;

	public List<GameObject> listOfPrefabs = new List<GameObject>();

	public GameHandler gh = new GameHandler();



	void Start()
	{

		
		int weightCounter = 0;
        for (int i = 0; i < GameHandler.weightOfElements.Length; i++)
        {
			if(GameHandler.weightOfElements[i] == 0)
            {
				weightCounter++;
            }
        }
		if(weightCounter >= 5)
        {
            //values for fitness between 40 and 60
            GameHandler.weightOfElements[0] = 2.0f; //spyke
            GameHandler.weightOfElements[1] = 2.0f; //opossum
            GameHandler.weightOfElements[2] = 2.0f; //eagle
            GameHandler.weightOfElements[3] = 1.85f; //life
            GameHandler.weightOfElements[4] = 2.0f; //frog

            /*GameHandler.weightOfElements[0] = 1.7f; //spyke
			GameHandler.weightOfElements[1] = 2.0f; //opossum
			GameHandler.weightOfElements[2] = 2.0f; //eagle
			GameHandler.weightOfElements[3] = 1.6f; //life
			GameHandler.weightOfElements[4] = 1.9f; //frog*/

            GameHandler.FitnessValue = 40;
			GameHandler.sizeTargetValue = Mathf.RoundToInt((float)GameHandler.FitnessValue / 2)+1;

			GameHandler.isCoinGenerated = false;
			
		}
		spyke[2] = GameHandler.weightOfElements[0];
		opossum[2] = GameHandler.weightOfElements[1];
		eagle[2] = GameHandler.weightOfElements[2];
		lifePoint[2] = GameHandler.weightOfElements[3];
		frog[2] = GameHandler.weightOfElements[4];

		GenesList.Add(spyke);
		GenesList.Add(opossum);
		GenesList.Add(eagle);
		GenesList.Add(lifePoint);
		GenesList.Add(frog);


		//target.text = (fitnessTarget).ToString();
		//numGenerationsText.text = numGenerations.ToString();

		random = new System.Random();
		//ga = new GeneticAlgorithm(populationSize, sizeTarget, random, GetElement, FitnessFunction, elitism, mutationRate);
		//StartGA(80, 45);
		StartGA();

		GameMaster gm = FindObjectOfType<GameMaster>();

	}

	public void StartGA()
    {
		//fitnessTarget = 100;
		//populationSize = 32;
		//sizeTarget = 50;
		//fitnessTarget = gh.getFitness();
		//sizeTarget = gh.getSize();
		fitnessTarget = GameHandler.FitnessValue;
		sizeTarget = GameHandler.sizeTargetValue;
		
		ga = new GeneticAlgorithm(populationSize, sizeTarget, random, GetElement, FitnessFunction, elitism, mutationRate);
	}

	void Update()
	{
		//sizeTarget = (int)(sizeTarget * 1.2);
		//ga.NewGeneration(sizeTarget);
		ga.NewGeneration();
		//Debug.Log("TARGET SIZE: " + sizeTarget);

		Debug.Log("Nova Geração");
		numGenerations += 1;

		//numGenerationsText.text = numGenerations.ToString();
		//bestFitnessText.text = ga.BestFitness.ToString();

		//if (ga.BestFitness >= fitnessTarget*1) {
		//Debug.Log("Best fitness: " + ga.BestFitness);
		if (ga.Population[0].Fitness >= fitnessTarget * 1) /*&&
			ga.Population[1].Fitness >= fitnessTarget * 1 &&
			ga.Population[2].Fitness >= fitnessTarget * 1  
			)*/
		{
			int contador = 0;
			GameHandler.quantityEagle = 0;
			GameHandler.quantityFrog = 0;
			GameHandler.quantityOpossum = 0;
			GameHandler.quantitySpyke = 0;
			GameHandler.quantityLife = 0;
			for (int i = 2; i >= 0; i--)
			{
				DNA aux = ga.Population[i];
				int spykeCount = 0;
				int opossumCount = 0;
				int eagleCount = 0;
				int lifeCount = 0;
				int frogCount = 0;

				for (int j = 0; j < aux.Genes.Count; j++)
				{
					//Debug.Log("Valor de DOUBLE: " + (double) aux.Genes[j].GetValue(aux.Genes[j].Length - 1));
					contador++;
					//Debug.Log("Contador: " + contador);
					switch ((double)aux.Genes[j].GetValue(aux.Genes[j].Length - 1))
					{
						case 0:
							spykeCount++;
							// Instantiate(enemySpyke, new Vector3(j * 1f, i * 1.5f, 0), Quaternion.identity);
							listOfPrefabs.Add(enemySpyke);
							break;
						case 1:
							opossumCount++;
							// Instantiate(enemyOpossum, new Vector3(j * 1.0f, i * 1.5f, 0), Quaternion.identity);
							listOfPrefabs.Add(enemyOpossum);
							break;
						case 2:
							eagleCount++;
							listOfPrefabs.Add(enemyEagle);
							// Instantiate(enemyEagle, new Vector3(j * 1.0f, i * 1.5f, 0), Quaternion.identity);
							break;
						case 3:
							lifeCount++;
							listOfPrefabs.Add(enemyLife);
							// Instantiate(enemyLife, new Vector3(j * 1.0f, i * 1.5f, 0), Quaternion.identity);
							break;
						case 4:
							frogCount++;
							listOfPrefabs.Add(enemyFrog);
							// Instantiate(enemyLife, new Vector3(j * 1.0f, i * 1.5f, 0), Quaternion.identity);
							break;
					}
				}
				Debug.Log("Valores do I = " + i + " com FITNESS = " + aux.Fitness + " : -> SPYKE: " + spykeCount + " || OPOSSUM: " + opossumCount + " || EAGLE: " + eagleCount + " || LIFE: " + lifeCount + " || FROG: " + frogCount);
				GameHandler.quantityEagle += eagleCount;
				GameHandler.quantityFrog += frogCount;
				GameHandler.quantityOpossum += opossumCount;
				GameHandler.quantitySpyke += spykeCount;
				GameHandler.quantityLife += lifeCount;
				// Debug.Log(listOfPrefabs[1].ToString());
				// Instantiate(listOfPrefabs[1], new Vector3(1 * 1.0f, i * 1.5f, 0), Quaternion.identity);
			}
			this.enabled = false;
		}
	}

	public List<GameObject> GetElementsPrefab(){

		return listOfPrefabs;
	}

	private double[] GetElement()
	{
		int i = random.Next(GenesList.Count);
		/*List<int> temp = new List<int>();
		element = null;
        for (int j = 0; j <= GenesList[i].Length; j++)
        {
			if(j == GenesList[i].Length)
            {
				temp.Add(i);
            }
            else
            {
				temp.Add((int)GenesList[i].GetValue(j));
            }
        }*/
        element  = GenesList[i];
		
			//SetElementWeight(i, 6);
			//SetElementWeight(i);
        /*for (int k = 0; k < temp.Count; k++)
        {
			element[k] = temp[k];
			Debug.Log("Elemento adicionado aqui: " + element[k]);
        }*/
		//Debug.Log("Valor ANTES DE IR: " + enemyList[i]);

		return element;

	}

	private double FitnessFunction(int index)
	{
		double score = 0;
		DNA individuo = ga.Population[index];

		// Debug.Log("numero de genes de um individuo:" + individuo.Genes.Count);

		// Calcula o score de um elemento
		for(int i = 0; i < individuo.Genes.Count; i++){
			double temp = 1;

    		//for(int j = 0; j < individuo.Genes[i].Length; j++){ 
    		for(int j = 0; j < 3; j++){ 

				 //Debug.Log("Tamanho da lista de genes: " + individuo.Genes[i].Length);
				// temp *= (int) GenesList[i].GetValue(j); 
				 temp *= (double) individuo.Genes[i].GetValue(j);
				/*if(temp > 4)
                {
					Debug.Log("Valor do temp da fitness: " + temp + " | TAM DO IND: " + individuo.Genes[i].Length + " | VALOR DO I: " + i);
				}*/

			}
			
			// Debug.Log("Score dentro do loop: " + score);
			score += temp;	
		}

		// Debug.Log("Score: " + score);
		return score;
	}

	//public void SetElementWeight(int index, int weight)
	public void SetElementWeight(int index)//, int weight)
    {
		double weight;
		double randomValue;
		int divisor = 10;
		for (int i = 0; i < GenesList.Count; i++)
        {
			if(i == index)
            {
				//Debug.Log("Valor do peso antes: " + GenesList[index].GetValue(2));
				//GenesList[index].SetValue(weight, 2);
				
				randomValue = random.NextDouble() / divisor;
				weight = (double)GenesList[index].GetValue(2) - randomValue;
				//fitnessTarget -= randomValue;
				GenesList[index].SetValue(weight, 2);
				//Debug.Log("Valor do peso depois: " + GenesList[index].GetValue(2));
			}
            else
            {
				//Debug.Log("Valor do peso antes: " + GenesList[index].GetValue(2));
				//GenesList[index].SetValue(weight, 2);
				randomValue = random.NextDouble() / (divisor * 3);
				weight = (double)GenesList[i].GetValue(2) + randomValue;
				GenesList[i].SetValue(weight, 2);
				//Debug.Log("Valor do peso depois: " + GenesList[index].GetValue(2));
			}
        }

		/*Debug.Log("Valor do peso antes: " + GenesList[index].GetValue(2));
		//GenesList[index].SetValue(weight, 2);
		randomValue = random.NextDouble() / 5;
		weight = (double) GenesList[index].GetValue(2) - randomValue;
		fitnessTarget -= randomValue;
		GenesList[index].SetValue(weight, 2);
		Debug.Log("Valor do peso depois: " + GenesList[index].GetValue(2));*/
	}

}
