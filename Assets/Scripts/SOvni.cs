using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOvni : MonoBehaviour
{
    public float speed = 3f; //velocidad a la que se mueve
    public int points = 100; // puntos que da al derrotarlo
    public int dir = 1; //direccion del ovni (1 -> derecha, -1 -> izquierda)
    public float deathAnimTime = 1f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "SBorder") //borde de la pantalla
        {

            Destroy(gameObject); // se destruye

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SPlayerBullet") //borde de la pantalla
        {

            SGameManager.instance.AddScore(points);
            Destroy(gameObject); // se destruye

        }
    }
    public void DerriboOVNI()
    {
        SGameManager.instance.AddScore(points);//Sumar puntos
        //animacion de destruirse
        animator.Play("OVNI_Death");
        speed = 0;
        Destroy(gameObject, deathAnimTime);

    }
}
