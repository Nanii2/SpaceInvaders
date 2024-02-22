using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SInvaderMovement : MonoBehaviour
{

    public float speed = 3f; //velocidad de movimiento en eje x
    public float despAbajo = 1f; // distancia que baja al cambiar la direccion
    private int dir = 1;

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
        dir *= -1; //invierto la direccion (1 y -1)
        transform.position += new Vector3(0, -despAbajo, 0);//desplazaarme hacia abajo

    }
}
