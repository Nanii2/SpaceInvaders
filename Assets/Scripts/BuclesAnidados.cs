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

        /*DibujaMatriz(CreaTablaPrimerosNumeros(8, 10));

        int[,] tabla = CreaTablaMultiplicar(5, 10);

         DibujaMatriz(tabla);

         DibujaMatriz(CreaTablaMultiplicar(2, 20));



         listaNumeros = new int[10, 10];
         Debug.Log(listaNumeros.Length);
         Debug.Log(listaNumeros.GetLength(0));
         Debug.Log(listaNumeros.GetLength(1));

         int n = 1;

         for(int i = 0; i < listaNumeros.GetLength(0); i++)
         {
             string tablaDeI = "Esta es la tabla de " + (i+1).ToString() + " ";
             for(int j = 0; j < listaNumeros.GetLength(1); j++)
             {
                 //listaNumeros[i, j] = (i+1) * (j+1);

                 listaNumeros[i, j] = n;
                 tablaDeI += listaNumeros[i, j].ToString() + " ";
                 //Debug.Log(listaNumeros[i, j]);
                 n++;
             }

             //Debug.Log(tablaDeI);
         }
         Debug.Log(listaNumeros);*/
    }
    // crea y devuelve un amatriz de col x row con los N primeros numeros natruales (N = COL*ROW)

    public bool ApareceNenTabla(int[,] tabla, int nBuscar)
    {
        bool encontrado = false;

        //recorer tabla
        for(int i = 0; i < tabla.GetLength(0); i++)
        {
            for(int j= 0; j<tabla.GetLength(1); j++)
            {
                if (tabla[i,j] == nBuscar)
                {
                    Debug.Log("El numero" + nBuscar + "esta en la tabla");
                    encontrado = true;
                    return true;
                }
            }

        }
        return encontrado;

    }
    public int[,] CreaTablaPrimerosNumeros(int ancho, int alto)
    {

        int[,] tabla = new int[ancho, alto];

        int contador = 0;

        for(int i = 0; i<ancho; i++)
        {

            for(int j = 0; j<alto; j++)
            {
                tabla[i, j] = 0; //asignar a la casilla valores 0, 1, 2, 3, 4... N
                contador++;

                tabla[i, j] = i * alto + j;
            }

        }

        return tabla;

    }
    //crea y devuelve una matriz de col x row con las tablas de multiplicar
    public int[,] CreaTablaMultiplicar(int col, int row)
    {
        int[,] tablaMultiplicar = new int[col, row]; //creo la matriz/tabla, de col x row

        
        for (int i = 0; i < tablaMultiplicar.GetLength(0); i++) //recorro en ancho (columnas)
        {
            //string tablaDeI = "Esta es la tabla de " + (i + 1).ToString() + " ";
            for (int j = 0; j < tablaMultiplicar.GetLength(1); j++) 
            {
                tablaMultiplicar[i, j] = (i+1) * (j+1);
                //tablaDeI += tablaMultiplicar[i, j].ToString() + " ";
                //Debug.Log(listaNumeros[i, j]);
                
            }

            //Debug.Log(tablaDeI);
        }
        return tablaMultiplicar;
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
