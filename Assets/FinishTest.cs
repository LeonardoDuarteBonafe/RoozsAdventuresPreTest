using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishTest : MonoBehaviour
{
    public GameObject canvasForms;
    [DllImport("__Internal")]
    private static extern void OpenURLInExternalWindow(string url);
    private string formUrl = "https://forms.gle/ULX93VG3GvamB3ZZA";

    public void QuitGame(){

        Debug.Log("Quit");
        Application.Quit();
    }

    public void OpenURL(){

        //Application.OpenURL();
        OpenURLInExternalWindow(formUrl);

    }

    public void OpenCanvasFinishTest(){

        canvasForms.SetActive(true);


    }

    public void ContinueGame()
    {
        canvasForms.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
