using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SInvaderMovement : MonoBehaviour
{

    public float speed = 3f; //velocidad de movimiento en eje x
    public float despAbajo = 1f; // distancia que baja al cambiar la direccion
    private int dir = 1;// +1 derecha, -1 izquierda

    public bool canSwitch = true; //bool que indica si puede girarse
    public float switchDelay = 0.5f; // tiempo que debe pasar despues de girar, para poder volver a hacerlo

    /*
     * 1- despues de girar, pongo canswitch a false
     * 2 - Crear una funcion que ponga canSwitch a true
     * 3 - A la vez que pongo canSwitch a false, hago Invoke del método que cree antes, con tiempo switchDelay
     */

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        transform.position += new Vector3(speed, 0, 0) * dir * Time.deltaTime;

    }

    public void SwitchDirection()
    {
        if (canSwitch == true)
        {
            dir *= -1; //invierto la direccion (1 y -1)
            transform.position += new Vector3(0, -despAbajo, 0);//desplazaarme hacia abajo

            canSwitch = false;
            Invoke("ActivaGiro", switchDelay);
        
       
            
        }

    }

    private void ActivaGiro()
    {

        canSwitch = true;

    }
}
