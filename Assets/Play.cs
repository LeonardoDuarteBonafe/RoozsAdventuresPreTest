using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class Play : MonoBehaviour
{
    public Button playButton;
    public InputField usernameInput;

    public Text nameAlert;

    private string url = "https://roozsadventures.000webhostapp.com/checkusername.php";
    //private string url = "'https://maps.googleapis.com/maps/api/http://roozsadventures.000webhostapp.com/checkusername.php";
    WWWForm form;

     private void Start() {
        
		Button btn = playButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
        nameAlert.GetComponent<Text>().enabled = false;
     }

    void TaskOnClick(){

        if(usernameInput.text != ""){
            if(usernameInput.text.Length >= 3)
            {
                //StartLoadMainScene();
                //nameAlert.GetComponent<Text>().enabled = true;
                //nameAlert.GetComponent<Text>().text = "VAMOS VER SE AGORA VAI";
                //StartCoroutine(CheckUsername());
                StartLoadMainScene();
                nameAlert.GetComponent<Text>().enabled = false;
            }
            else
            {
                nameAlert.GetComponent<Text>().text = "O nome deve ter pelo menos 3 caracteres";
                nameAlert.GetComponent<Text>().enabled = true;
            }
        }
        else {
            nameAlert.GetComponent<Text>().text = "Você precisa digitar seu nome para começar a partida";
            nameAlert.GetComponent<Text>().enabled = true;
        }
	}

    IEnumerator CheckUsername()
    {
        //nameAlert.GetComponent<Text>().text = "AO MENOS ENTROU NO CHECK USER";
        form = new WWWForm();
        //form.AddField("Jogador", usernameInput.text.ToLower());
        form.AddField("Jogador", usernameInput.text);
        //nameAlert.GetComponent<Text>().enabled = true;
        //nameAlert.GetComponent<Text>().text = "Esta comecando o cadastro";
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();
            
            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                //nameAlert.GetComponent<Text>().text = "Erro no banco de dados: " + www.error + " || DH: " + www.downloadHandler.text;
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text.Contains("failure"))
                {
                    nameAlert.GetComponent<Text>().text = "Estamos realizando seu cadastro";
                    StartLoadMainScene();
                }
                else
                {
                    nameAlert.GetComponent<Text>().text = "Usuário já está cadastrado, tente outro nome, por favor!";
                    nameAlert.GetComponent<Text>().enabled = true;
                    Debug.Log("Nome ja inserido no banco, use outro");
                }
            }
        }
    }
    
    private void StartLoadMainScene()
    {
        PlayerPrefs.SetString("playersName", usernameInput.text);
        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
