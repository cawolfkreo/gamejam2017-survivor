using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    int speed = 7; //Velocidad del jugador.
    float speedY = 8.5f;
    bool pistola = true; //si tiene o no la pistola activa.
    bool laser = false; //Si tiene o no la pistola laser desbloqueada.
    public GameObject spritePistola; //Los sprites de las armas del jugador.
    public GameObject spriteLaser;

    public GameObject bala;//bala de la pistola.
    public GameObject rayo;//rato del rsyo laser.
    public GameObject posBalas;//posicion de la que salen los disparos.

    float tiempoAccion = 0.5f;
    float tiempoLaser = 0.2f;
    float miTiempo = 0.0f;
    float tiempoSalto=0.3f;
    float tiempoActual = 0.0f;

    Vector3 restart; // valores para reiniciar la pos del jugador.
    Vector2 min; //posición minima de la camara.

    public int vida;
    public int vidaMax;//Vida y vida máxima del jugador
    public bool derecha = true; //si el jugador mira o no a la deecha.

    public int tiempoGolpe = 40;//Tiempo entre el golpe de vida anterior y el actual.

   
    private void Start(){
        restart = transform.position;
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        vida = 20;
        vidaMax=vida;

    }


    // Update is called once per frame
    void Update () {

        float inputH = Input.GetAxis("Horizontal");
        
        if (inputH < 0 && derecha)
        {
            Girar();
            derecha = !derecha;
        }
        else if (inputH > 0 && !derecha){
            Girar();
            derecha = !derecha;
        }

        var move = new Vector3(Input.GetAxis("Horizontal")*speed, Input.GetAxis("Vertical") *speedY, 0);
        transform.position += move * Time.deltaTime;//Mueve al jugador

        miTiempo = miTiempo + Time.deltaTime;

        if (Input.GetButton("Fire1")  && laser && miTiempo>tiempoAccion){
            tiempoAccion = miTiempo + 0.5f;
            pistola = !pistola;
            spritePistola.GetComponent<Renderer>().enabled = pistola;
            spriteLaser.GetComponent<Renderer>().enabled = !pistola;
        }

        if (Input.GetButton("Jump") && pistola && miTiempo > tiempoAccion){ //si dispara y tiene la pistola, que dispare balas
            tiempoAccion = miTiempo + 0.5f;
            GameObject Bala = (GameObject)Instantiate(bala);
            bala.transform.position = posBalas.transform.position;

            
        }

        if(Input.GetButton("Jump") && !pistola && miTiempo > tiempoLaser){ //si dispara y tiene el lazer, que dispare laseres
            tiempoLaser = miTiempo + 0.2f;
            GameObject Bala = (GameObject)Instantiate(rayo);
            rayo.transform.position = posBalas.transform.position;

        }

        if (transform.position.y < min.y-7 || vida<=0)        {
            Reinicio();
        }
    }

    void Reinicio(){
        transform.position = restart;
        vida = vidaMax;
    }


    public int[] DarVida(){
        int[] respuesta = new int[] { vida, vidaMax };

        return respuesta;
    }

    public void OnCollisionEnter2D(Collision2D collision){
     if (collision.gameObject.tag.Equals("radiacion")){
            tiempoGolpe--;
            if (tiempoGolpe==0){
                vida--;
                tiempoGolpe = 8;
            }
        }else if (collision.gameObject.tag.Equals("mbala")){
            vida--;
        }
    }

    public void OnCollisionStay2D(Collision2D collision){
        if (collision.gameObject.tag.Equals("radiacion")){
            tiempoGolpe--;
            if (tiempoGolpe == 0){
                vida--;
                tiempoGolpe = 10;
            }
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("radiacion"))
        {
            tiempoGolpe=20;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.tag.Equals("medikit")){
            vida += 4;
            Destroy(collision.gameObject); //Si toca un paquete de vida obtiene 4 puntos y el paquete desaparece.
            if (vida > vidaMax){
                vida = vidaMax;
            }
        }else if (collision.gameObject.tag.Equals("armaLaser")){
            pistola = !pistola;
            laser = true;
            spritePistola.GetComponent<Renderer>().enabled = pistola;
            spriteLaser.GetComponent<Renderer>().enabled = !pistola;

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag.Equals("Finish"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void Girar()
    {

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
}
