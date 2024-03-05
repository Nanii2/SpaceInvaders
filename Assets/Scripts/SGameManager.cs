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

    public static SGameManager instance = null;

    public TextMeshPro scoreText;
    public TextMeshPro lifesText;
    public GameObject spriteVida3;
    public GameObject spriteVida2;

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
                //if (matrizAliens[i, j] == null) Debug.Log("alien no creado");
                auxAlien.padre = padreAliens;

            }

        }
        
        //doble bucle (anidado) que recorra la matriz (de 11 x 5, rangos 0.10 y 0-4)

        //dentro de los dos bucles, intanciamos un alien
        // lo guardamos en la posicion de la matriz apropiada
        //colocaos el alien
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
        Debug.Log("el jugador ha perdido");
        CancelInvoke();
        

    }

    public void PlayerWin()
    {
        gameOver = true;
        Debug.Log("el jugador ha ganado");
        CancelInvoke();


    }

    public void DamagePlayer()
    {
        vidas--;
        UpdateLifeUI();
        player.PlayerDamaged();
        Invoke("UnlockDamagedPlayer", 1.5f);
        if(vidas <= 0)
        {
            PlayerGameOver();
        }

    }
    private void UnlockDamagedPlayer()
    {

        player.PlayerReset();

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
        if(defeatedAliens >= nFilas * nColumnas)
        {
            PlayerWin();
        }

    }

    public void ResetGame()
    {

        SceneManager.LoadScene("DemoInvaders");
        

    }
   
    //suma puntos
    public void AddScore(int points)
    {

        score += points;
        scoreText.text = "score\n" + score.ToString();

    }
}
