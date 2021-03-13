using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.name.Contains("frog") || trig.name.Contains("eagle") || trig.name.Contains("opossum"))
        {
            Destroy(gameObject);
            Debug.Log("Bala bateu num inimigo fdm");
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "enemy")
        {
            Debug.Log("Derrubou algum inimigo na pedrada");
        }
    }
}
