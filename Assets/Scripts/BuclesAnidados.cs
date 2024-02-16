using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuclesAnidados : MonoBehaviour
{
    public int[,] listaNumeros;
    public List<int> listaDinamicNumeros;


    // Start is called before the first frame update
    void Start()
    {
        listaNumeros = new int[10, 10];

        for(int i = 0; i < listaNumeros.Length; i++)
        {
            for(int j = 0; j < listaNumeros.Length; j++)
            {

                listaNumeros[i, j] = (i+1) * (j+1);
                Debug.Log(listaNumeros[i, j]);
            }
        }
        Debug.Log(listaNumeros);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
