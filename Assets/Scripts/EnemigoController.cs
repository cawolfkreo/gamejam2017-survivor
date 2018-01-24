using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoController : MonoBehaviour {

    int speed = 5; //Velocidad del enemigo.
    public GameObject bala;//bala de la pistola.
    public GameObject posBala;//posicion de la que salen los disparos.
    public PlayerController jugador; //el jugador.

    float tiempoAccion = 1.9f; //tiempo antes de poder volver a disparar.
    float miTiempo = 0.0f; //Tiempo del enemigo para disparar
    public int vida; //vida del enemigo.

    bool derecha = false; //si mira o no a la derecha.

    // Use this for initialization
    void Start () {
        vida = 3; //vida inicial del enemigo.
		
	}
	
	// Update is called once per frame
	void Update () {
        float distancia = Dist();
        if ((distancia < 0 )&& derecha){
            derecha = !derecha;
            Girar();
        }else if((distancia>=0 && !derecha)){
            derecha = !derecha;
            Girar();
        }

        miTiempo = miTiempo + Time.deltaTime;

        if ((distancia<0 || distancia>(-2)) && miTiempo > tiempoAccion)
        {
            Disparar();
        }


    }

    void Girar(){

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    void Disparar()
    {
        tiempoAccion = miTiempo + 0.9f;
        GameObject Bala = (GameObject)Instantiate(bala);
        bala.transform.position = posBala.transform.position;
    }

    float Dist(){
        float yo = transform.position.x;

        return jugador.transform.position.x - yo;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("laser")){
            Destroy(gameObject);
        }else if (collision.gameObject.tag.Equals("gbala")){
            vida--;
        }
    }

    private void LateUpdate(){
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}
