using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirJugador : MonoBehaviour {

    public GameObject jugador;
    public int desplazamiento;


    private void Start()
    {
        desplazamiento = 3;
    }

    // Update is called once per frame
    void LateUpdate () {


        transform.position = new Vector3(jugador.transform.position.x+desplazamiento, jugador.transform.position.y, transform.position.z);
    }
}
