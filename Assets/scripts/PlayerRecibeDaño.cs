using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecibeDaño : MonoBehaviour
{

    bool recibiendoDanio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecibeDanio(vector2 direccion,int cantDanio)
    {
        if (!recibiendoDanio) //hacemos esto para darle un calldown del jugador al recibir daño y no recibirlo infinitamente
        {
            recibiendoDanio = true;
            // creamos direccion opuesta donde atacamos
            Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
            rb.AddForce(rebote, ForceMode2D.Impulse);
        }
    }

    public void DesactivarDanio()
    {
        recibiendoDanio = false;
    }

}
