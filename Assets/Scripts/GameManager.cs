using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
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
    //distanciaentre aliens al spawnearlos

    // Start is called before the first frame update
    void Start()
    {
        //Decimos que matrizAliens es una nueva matriz de SInvaders de nColumnas x nFilas
        // - INICIALIZACION
        matrizAliens = new SInvader[nColumnas, nFilas];
        /*Vector3 v;
        v = new Vector3(0, 0, 0);*/
        SpawnAliens();
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
                auxAlien.parent = padreAliens;

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
    private void SpawnInvaders()
    {


    }
}
