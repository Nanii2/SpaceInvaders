using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SInvader : MonoBehaviour
{

    public GameObject particulaMuerte;
    public bool isQuitting = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnApplicationQuit()
    {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if(isQuitting == false)
        {
            GameObject particula = Instantiate(particulaMuerte, transform.position, Quaternion.identity);
           // Destroy(particula, 0.3f);
        }
    }
}
