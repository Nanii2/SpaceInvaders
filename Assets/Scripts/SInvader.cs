using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InvaderType {SQUID, CRAB, OCTOPUS };

public class SInvader : MonoBehaviour
{
    public InvaderType tipo = InvaderType.SQUID;

    public GameObject particulaMuerte;
    public SInvaderMovement padre;

    public GameObject EnemyBullet;
    public float bulletSpawnYOffset = -0.65f;

    public int puntosGanados = 10;

    public static int speed = 3;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "SBorder")//choca con borde de pantalla 
        {
            //Debug.Log("ha chocado");
            //llamar a SwitchDirection para que el padre gire
            padre.SwitchDirection();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("SGameOverBarrier"))
        {
            SGameManager.instance.PlayerGameOver();
        }
        else if (collision.tag == "SPlayerBullet")
        {
            SGameManager.instance.AlienDestroyed();
            //Debug.Log("Creada particula mmuerte");
            GameObject particula = Instantiate(particulaMuerte, transform.position, Quaternion.identity);
            // Destroy(particula, 0.3f);
            //Stun a los aliens
            padre.AlienDestroyedStun();

            //suma puntos
            SGameManager.instance.AddScore(puntosGanados);
            // destruir alien y bala
            Destroy(collision.gameObject);
            Destroy(gameObject);


        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        Vector3 aux = transform.position + new Vector3(0, bulletSpawnYOffset, 0); // Modificar posición spawn
        Instantiate(EnemyBullet, aux, Quaternion.identity); // Spawnear la bala

    }


    public void MovementAnimation()
    {
        /*if(tipo == InvaderType.SQUID)
        {

            animator.Play("alien_1_idle");

        }
        else if (tipo == InvaderType.CRAB)
        {

            animator.Play("alien_2_idle");

        }
        else if (tipo == InvaderType.OCTOPUS)
        {

            animator.Play("alien_3_idle");

        }*/

        animator.Play("Alien_" + ((int)tipo+1) + "_idle");
    }

    public void StunAnimation()
    {

        animator.Play("Alien_" + ((int)tipo + 1) + "_stun");

    }


}
