using System;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
public class Weapon : MonoBehaviour
{
    public Transform FirePoint;
    public GameObject bulletPrefab;
    public GameMaster gm;
    public int bulletQuantity = 0;
    public CharacterController2D player;
    public GameObject bullet;

    public const float DISTANCE_PLAYER_BULLET = 20f;
    private float distance;

    void Start(){

        gm = gameObject.GetComponent<GameMaster>();
        bulletQuantity = GameMaster.instance.GetNumOfBullets();

    }

    // Update is called once per frame
    void Update()
    {
        bulletQuantity = GameMaster.instance.GetNumOfBullets();

        if((/*Input.GetButtonDown("Fire1") ||*/ Input.GetMouseButtonDown(1) || (Input.GetKeyDown("q")) || (Input.GetKeyDown(KeyCode.Keypad0)) || (Input.GetKeyDown(KeyCode.DownArrow)) || Input.GetKeyDown(KeyCode.LeftControl)) && bulletQuantity > 0){
            Shoot();
            GameMaster.instance.SetNumOfBullets(-1);
            GameMaster.instance.AttHud();
        }

        // distance = Vector3.Distance(player.transform.position, GameObject.FindGameObjectWithTag("Bullet").transform.position);
    }

    void Shoot(){

        bullet = Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        Destroy(bullet, 3.0f);
        
        // StartCoroutine(Destroy());

    }

    IEnumerator Destroy(){

        yield return new WaitForSeconds(5.0f);
        Destroy (bullet);

    }

}
