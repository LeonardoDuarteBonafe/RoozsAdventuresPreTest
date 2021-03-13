using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Log : MonoBehaviour
{

    public GameHandler gh = new GameHandler();

    WWWForm form;
    private string url = "https://roozsadventures.000webhostapp.com/";

    private void Awake() {

        gh = FindObjectOfType<GameHandler>();
    }


    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void StartRegisterLog()
    {
        StartCoroutine(RegisterLog());
    }

    public IEnumerator RegisterLog()
    {
        // Create a form object 
        if(PlayerPrefs.GetInt("level") <= 10)
        {
            form = new WWWForm();
            form.AddField("Jogador", PlayerPrefs.GetString("playersName"));
            form.AddField("Fase", (PlayerPrefs.GetInt("level")));
            form.AddField("Fitness", (int)GameHandler.oldFitnessValue);
            form.AddField("PlayerScore", GameHandler.playerFitness);
            form.AddField("DistanciaPlayer", GameHandler.highscore);
            form.AddField("DistanciaTotal", GameHandler.maximumScore);
            form.AddField("DiamantesColetados", GameHandler.numberOfColectedCoins);
            form.AddField("DiamantesVistos", GameHandler.numberOfTotalCoins);
            form.AddField("VidasColetadas", GameHandler.numberOfBonusLife);
            form.AddField("VidasRestantes", GameHandler.numberOfRemainingLife);
            form.AddField("qtdAguia", GameHandler.quantityEagle);
            form.AddField("qtdEspinho", GameHandler.quantitySpyke);
            form.AddField("qtdGamba", GameHandler.quantityOpossum);
            form.AddField("qtdSapo", GameHandler.quantityFrog);
            form.AddField("qtdVida", GameHandler.quantityLife);

            Debug.Log("MESMO VINDO DO HANDLER, VEIO ATE AQUI");

            UnityWebRequest www = UnityWebRequest.Post(url, form);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
                Debug.Log("Deu merda nessa parte");
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                Debug.Log("Deu bom para enviar pro servidor");
            }
            /*using (UnityWebRequest www = UnityWebRequest.Post("https://roozadventure.000webhostapp.com/",form)){
                 yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError){

                    Debug.Log( "Error: " + www.error );
                    Debug.Log(www.url);
                } else {
                    Debug.Log("chegou a inserir");
                    Debug.Log(www.downloadHandler.text);
                }

            }*/
            Debug.Log("QUALQUER COISA AQUI DEPOIS");
        }
    }
}
