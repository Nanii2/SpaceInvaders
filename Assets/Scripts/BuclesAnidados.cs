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
        Debug.Log(listaNumeros.Length);
        Debug.Log(listaNumeros.GetLength(0));
        Debug.Log(listaNumeros.GetLength(1));


        for(int i = 0; i < listaNumeros.GetLength(0); i++)
        {
            string tablaDeI = "Esta es la tabla de " + (i+1).ToString() + " ";
            for(int j = 0; j < listaNumeros.GetLength(1); j++)
            {
                //listaNumeros[i, j] = (i+1) * (j+1);

                //listaNumeros[i, j] = n;
                tablaDeI += listaNumeros[i, j].ToString() + " ";
                //Debug.Log(listaNumeros[i, j]);
                //n++;
            }

            //Debug.Log(tablaDeI);
        }
        Debug.Log(listaNumeros);
    }

    public void DibujaMatriz(int[,] matriz)
    {
        string texto = " ";
        for (int j = 0; j < matriz.GetLength(1); j++)
        {
            
            for(int i = 0; i < matriz.GetLength(0); i++)
            {

                texto += matriz[j, i].ToString() + " ";

            }
            texto += '\n';
        }
        Debug.Log(texto);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
