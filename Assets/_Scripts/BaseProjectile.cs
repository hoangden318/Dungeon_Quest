using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : HoangBehavior
{

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
            BulletPooling.Instance.Despawn(transform, 0.2f);
            
            return;
        }

        if (collision.gameObject.tag != "Fighter")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            BulletPooling.Instance.Despawn(transform, 0.1f);
            
            return;
        }

        BulletPooling.Instance.Despawn(transform, 0.1f);
        
    }
    //protected virtual void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.name == "Player")
    //    {
    //        Destroy(gameObject, 0.1f);
    //        //BulletPooling.Instance.Despawn(transform);
    //        return;
    //    }

    //    if (other.gameObject.tag != "Fighter")
    //    {
    //        Physics2D.IgnoreCollision(other, GetComponent<Collider2D>());
    //        Destroy(gameObject, 0.1f);
    //        //BulletPooling.Instance.Despawn(transform);
    //        return;
    //    }

    //    Destroy(gameObject, 0.1f);
    //    //BulletPooling.Instance.ReturnFromPool(transform);
    //}
}
