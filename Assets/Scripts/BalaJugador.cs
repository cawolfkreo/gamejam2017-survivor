using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaJugador : MonoBehaviour
{

    float speed;

    // Use this for initialization
    void Start() {
        speed = 8f;
    }

    // Update is called once per frame
    void Update(){

        Vector2 position = transform.position;

        position = new Vector2(position.x + speed * Time.deltaTime, position.y);

        transform.position = position;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.x > max.x){
            Destroy(gameObject);
        }

    }

    void Setspeed(bool posi){
        if (!posi){
            speed*= -1;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        bool cond = collision.gameObject.tag.Equals("mbala") || collision.gameObject.tag.Equals("laser") || collision.gameObject.tag.Equals("gbala") || collision.gameObject.tag.Equals("jugador");
        if (!cond)
        {
            Destroy(gameObject);
        }
    }
}