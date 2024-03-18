using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SGameManager : MonoBehaviour
{
    //Lista doble (matriz) de SInvaders - DECLARACIÓN
    public SInvader[,] matrizAliens;

    //N de filas de invaders, alto
    public int nFilas = 5;
    //N columnas de invaders, ancho
    public int nColumnas = 11;
    //prefab dde alien
    public GameObject alien1Prefab;
    public GameObject alien2Prefab;
    public GameObject alien3Prefab;

    public SInvaderMovement padreAliens;

    public float distanciaAliens = 1;

    public float tiempoEntreDisparos = 2f;

    public bool gameOver = false;

    public int vidas = 3;

    public int score = 0;

    private int defeatedAliens = 0;
    public float playerDamageDelay = 1.5f;

    public float incrVel = 3;

    public static SGameManager instance = null;

    //INTERFAZ
    public TextMeshPro scoreText;
    public TextMeshPro hiScore;
    public TextMeshPro lifesText;
    public GameObject spriteVida3;
    public GameObject spriteVida2;
    public GameObject gameOverText;
    public GameObject ui;
    public GameObject Pwin;

    //OVNI
    public GameObject prefabOVNI;
    public Transform spawnIzqOVNI;
    public Transform spawnDerOVNI;
    public float spawnOvniTime = 15f;

    public int highScore = 0;
 

    private SPlayer player;

    //distanciaentre aliens al spawnearlos
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

        player = FindAnyObjectByType<SPlayer>();
        //Decimos que matrizAliens es una nueva matriz de SInvaders de nColumnas x nFilas
        // - INICIALIZACION
        matrizAliens = new SInvader[nColumnas, nFilas];
        /*Vector3 v;
        v = new Vector3(0, 0, 0);*/
        SpawnAliens();
        InvokeRepeating("SelectAlienShoot", tiempoEntreDisparos, tiempoEntreDisparos);
        InvokeRepeating("SpawnOVNI", spawnOvniTime, spawnOvniTime);

        highScore = PlayerPrefs.GetInt("HIGH-SCORE");
        hiScore.text = "HI-SCORE '\n' " + highScore.ToString();

        gameOverText.SetActive(false);
        Pwin.SetActive(false);
        ui.SetActive(true);

    }

    void SpawnAliens()
    {
        for (int i = 0; i < nColumnas; i++)
        {

            for (int j = 0; j < nFilas; j++)
            {

                GameObject prefab;
                if (j == 4) prefab = alien1Prefab;
                else if (j < 2) prefab = alien3Prefab;
                else prefab = alien2Prefab;

                SInvader auxAlien = Instantiate(prefab, padreAliens.transform).GetComponent<SInvader>();
                matrizAliens[i,j] = auxAlien;
                auxAlien.transform.position += new Vector3(i-nColumnas/2,j-nFilas/2,0) * distanciaAliens;
                //if (matrizAliens[i, j] == null);
                //Debug.Log("alien no creado")
                auxAlien.padre = padreAliens;

            }

        }
      
    }

    void SpawnOVNI()
    {
        int random = Random.Range(0, 2); //entre 0 y 1

        //Si sale 0, lo colocamos a la izquierda
        if (random == 0)
        {
            //crearlo y ponerle la direccion apropiada
            Instantiate(prefabOVNI, spawnIzqOVNI).GetComponent<SOvni>().dir = 1;

        }
        else if (random == 1)
        {
            Instantiate(prefabOVNI, spawnDerOVNI).GetComponent<SOvni>().dir = -1;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectAlienShoot()
    {
        // Variable de control de la busqueda. Cuando esta a true ya he encontrado al alien y paro.
        bool encontrado = false;

        while (!encontrado) // Se repite con columnas aleaorias hasta encontrar un alien
        {
            // Elegir una columna aleatoria que no esté vacia
            int randomCol = Random.Range(0, nColumnas); // Columna aleatoria

            // Buscar al alie mas cercano al jugador en esa columna (el que este más abajo)
            // En este for tenemos dos condiciones: Que j > -1 y que encontrado == false
            // Como usamos && entre ellas (AND), deben cumplirse las dos, o salimos del bucle for
            for (int j = 0; j < nFilas && !encontrado; j++) // Recorrer la columna aleatoria
            {
                // Compruebo si el alien existe (no se ha destruido)
                if (matrizAliens[randomCol, j] != null) // Si la casilla no está vacia (null) el alien sigue vivo
                {
                    // Si encuentro un alien vivo, es el mas cercano de la columna al jugador porque la estoy recorriendo de abajo a arriba
                    matrizAliens[randomCol, j].Shoot(); // El alien dispara
                    encontrado = true; // He acabado la busqueda
                }
            }
        }
    }

    public void PlayerGameOver()
    {
        gameOver = true;
        //Debug.Log("el jugador ha perdido");
        CancelInvoke();

        
        Invoke("ResetGame", 2);

        gameOverText.SetActive(true);
        ui.SetActive(false);



    }

    public void PlayerWin()
    {
        gameOver = true;
        //Debug.Log("el jugador ha ganado");
        CancelInvoke();
        Invoke("ResetGame", 2);

        Pwin.SetActive(true);
        ui.SetActive(false);


    }

    public void DamagePlayer()
    {
        if (!gameOver && player.GetCanMove())
        {
            vidas--;
            UpdateLifeUI();
            //animacion de daño del jugador
            player.PlayerDamaged();
            padreAliens.canMove = false; //bloqueo los aliens
            SetInvadersAnim(false);
            Invoke("UnlockDamagedPlayer", 1.5f);
            if (vidas <= 0)
            {
                PlayerGameOver();
            }
        }

    }
    private void UnlockDamagedPlayer()
    {

        player.PlayerReset(); //desbloqueo al jugador
        padreAliens.canMove = true; // desbloqueo los aliens
        SetInvadersAnim(true);
    }

    private void UpdateLifeUI()
    {
        lifesText.text = vidas.ToString();

        /*if (vidas > 2) spriteVida2.SetActive(true);
        else spriteVida2.SetActive(false);
        if (vidas >= 3) spriteVida3.SetActive(true);
        else spriteVida3.SetActive(false);*/

        spriteVida2.SetActive(vidas >= 2); //se activa si vidas >= 2
        spriteVida3.SetActive(vidas >= 3); //se activa si vidas >= 3

    }
    public void AlienDestroyed()
    {
        defeatedAliens++; //aumento la cuenta de aliens derrotados
        //actualizar velocidad de los aliens segun cuantos quedan 

        //(1 + (aliensDerrotados / alienTotales) * incVelocidad) * speedAliens
        // 1 + (0/55) * 3) * 2 = 2
        // 1 + (55/55) * 3) * 2 = 8

        //suma incrVelocidad / aliensTotales
        padreAliens.speed += (1f / (float)(nFilas * nColumnas)) * incrVel;

        if(defeatedAliens >= nFilas * nColumnas)
        {
            PlayerWin();
        }

    }

    public void ResetGame()
    {

        UpdateHighScore();
        SceneManager.LoadScene("SpaceInvaders");
        

    }
   
    public void UpdateHighScore()
    {
        if (score > highScore) //si mi puntuacion es mayor que la maxima
        {
            PlayerPrefs.SetInt("HIGH-SCORE", score); //la guardo
        }

    }

    private void OnApplicationQuit()
    {
        UpdateHighScore(); //si cerramos la aplicacion los puntos se quedan
    }
    public void AddScore(int points)
    {

        score += points;
        scoreText.text = "score\n" + score.ToString();
        

    }

    public void SetInvadersAnim(bool movement)
    {

        for ( int i = 0; i < nColumnas; i++)
        {
            for (int j = 0; j < nFilas; j++)
            {
                if(matrizAliens[i,j] != null)
                {
                    if (movement)
                    {
                        matrizAliens[i, j].MovementAnimation();
                    }
                    else
                    {
                        matrizAliens[i, j].StunAnimation();
                    }
                }
            }
        }

    }
   
}
