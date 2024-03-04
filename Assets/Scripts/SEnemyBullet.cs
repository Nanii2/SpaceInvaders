using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEnemyBullet : MonoBehaviour
{
    public float speed = 3;
    public GameObject bulletExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position += new Vector3(0, -speed, 0) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SBarrier")
        {
            Destroy(gameObject);

        }
        else if (collision.tag =="Player")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "SPlayerBullet")
        {
            Destroy(gameObject);
            

        }
    }
}
