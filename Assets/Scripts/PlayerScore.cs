using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{

     void OnTriggerEnter2D(Collider2D other){

	if(other.CompareTag("gem")){
            GameHandler.numberOfColectedCoins += 1;
            GameMaster.instance.SetNumOfPoints(1);
        	GameMaster.instance.AttHud();
    }

    else if(other.CompareTag("life")){

            GameMaster.instance.SetNumOfHearts(1);
            GameHandler.numberOfBonusLife += 1;
            GameMaster.instance.AttHud();
      }
        else if (other.CompareTag("gemBackground"))
        {
            GameHandler.numberOfTotalCoins++;
            Debug.Log("Quantas GEMAS JA FORAM ATE AGORA? " + GameHandler.numberOfTotalCoins);
        }
 
    }

}
