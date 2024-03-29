using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPlayer : MonoBehaviour
{
    [Tooltip("Prefab de la bala")]
    public GameObject prefabBullet; //prefab de la bala
    [Tooltip("Velocidad del jugador en unidades de unity / segundo")]
    public float speed = 2; // velocidad del jugador

    //teclas para input configurable
    public KeyCode shootKey = KeyCode.Space;
    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;

    public Transform posDisparo;

    public bool canShoot = true;
    private bool canMove = true;

    //animator del jugador
    public Animator pAnimator;

    private Vector3 posInicial;

    public float limiteHorizontal = 8f;

    // Start is called before the first frame update
    void Start()
    {
        posInicial = transform.position;
        pAnimator = GetComponent<Animator>();
    
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            InputPlayer();

        }
        
    }

    private void InputPlayer()
    {

        if (canShoot && Input.GetKeyDown(shootKey))
        {
            //dispara
            Shoot();
        }
        else if (Input.GetKey(moveLeftKey))
        {
            //voy a la izquierda
            transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
            if(transform.position.x < -limiteHorizontal)
            {

                Vector3 aux = transform.position;
                aux.x = -limiteHorizontal;
                transform.position = aux;
            }

        }
        else if (Input.GetKey(moveRightKey))
        {
            // voy a la derecha
            transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
            if (transform.position.x < -limiteHorizontal)
            {

                Vector3 aux = transform.position;
                aux.x = limiteHorizontal;
                transform.position = aux;
            }


        }

    }

    private void Shoot()
    {

        //Debug.Log("Disparo");
        SPlayerBullet bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity).GetComponent<SPlayerBullet>();
        bullet.player = this;     
        canShoot = false;
    }

    public void PlayerDamaged()
    {

        pAnimator.Play("Player");
        canMove = true;
    }

    public void PlayerReset()
    {
        pAnimator.Play("player_idle");
        canMove = true;
        transform.position = posInicial;

    }

    public bool GetCanMove()
    {
        return canMove;
    }

    public void SetCanMove(bool b)
    {
        canMove = b;

    }
}
