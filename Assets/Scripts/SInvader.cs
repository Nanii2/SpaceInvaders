using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SInvader : MonoBehaviour
{

    public GameObject particulaMuerte;
    public bool isQuitting = false;
    public SInvaderMovement parent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2d(Collider2D collision)
    {
        if(collision.tag == "SBorder")//choca con borde de pantalla 
        {
            //llamar a SwitchDirection para que el padre gire
            parent.SwitchDirection();
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if(isQuitting == false)
        {
            GameObject particula = Instantiate(particulaMuerte, transform.position, Quaternion.identity);
           // Destroy(particula, 0.3f);
        }
    }
}
