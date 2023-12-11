using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : HoangBehavior
{
    //private float lifeTimer;
    //[SerializeField] private float maxLife = 2.0f;
    
    public GameObject attackEffect;
    public GameObject destroyEffect;

    protected virtual void Update()
    {
        //lifeTimer += Time.deltaTime;
        //if (lifeTimer >= maxLife)
        //{
        //    Instantiate(destroyEffect, transform.position, Quaternion.identity);
        //    Destroy(gameObject);
        //}
    }
    //chuyen Ontrigger sang incollision dc ko?
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
       

        if (other.gameObject.tag == "Fighter" && other.gameObject.name == "Player")
        {
            Instantiate(attackEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        

    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(gameObject, 0.1f);
            return;
        }

        if (collision.gameObject.tag != "Fighter")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            Destroy(gameObject, 0.1f);
            return;
        }

        Destroy(gameObject, 0.1f);
    }
}
