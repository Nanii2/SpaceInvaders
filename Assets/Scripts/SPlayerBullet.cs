using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayerBullet : MonoBehaviour
{
    public float speed = 3;
    [HideInInspector]
    public SPlayer player;
    public GameObject bulletExplosion;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, speed, 0) * Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SBorder")
        {
            Destroy(gameObject);
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);

        }
        else if (collision.tag == "Sinvader")
        {

            Destroy(gameObject);
            Destroy(collision.gameObject);
            SGameManager.instance.AlienDestroyed();


        }
        if (collision.tag == "SBarrier")
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);

        }
    }

    private void OnDestroy()
    {
        Debug.Log("Bala Destruida");
        player.canShoot = true;
    }
}