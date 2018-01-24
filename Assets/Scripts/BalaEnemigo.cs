﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour {


    float speed;

    // Use this for initialization
    void Start()
    {
        speed = -6f;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 position = transform.position;

        position = new Vector2(position.x + speed * Time.deltaTime, position.y);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.x < min.x)
        {
            Destroy(gameObject);
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        bool cond = collision.gameObject.tag.Equals("mbala") || collision.gameObject.tag.Equals("laser") || collision.gameObject.tag.Equals("gbala");
        if (!cond)
        {
            Destroy(gameObject);
        }
    }
}
