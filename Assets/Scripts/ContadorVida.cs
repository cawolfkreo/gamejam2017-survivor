using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContadorVida : MonoBehaviour {

    public PlayerController jugador; // jugador.
    Text texto; //texto de juego.

	// Use this for initialization
	void Start () {
        texto = GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        int[] a= jugador.DarVida();
        texto.text = "HP: "+a[0] + " / " + a[1];
	}
}
