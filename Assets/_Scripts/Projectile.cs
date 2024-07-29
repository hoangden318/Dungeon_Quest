using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : BaseProjectile
{
    private float lifeTimer;
    [SerializeField] private float maxLife = 2.0f;

    private float addVelocity = 2.0f;
    public Vector3 lastPosPlayer;
    [SerializeField] private float moveSpeed;
    public bool isPass = false;

    private Rigidbody2D rb;
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPosPlayer = GameObject.Find("Player").transform.position;
    }

    protected virtual void FixedUpdate()
    {
        //base.Update();
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= maxLife)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        Vector3 direction = (lastPosPlayer - transform.position).normalized;
        if(!isPass)
        {
            transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);

            float distanceToLastPos = Vector3.Distance(transform.position, lastPosPlayer);
            if(distanceToLastPos <= 0.01f)
            {
                isPass = true;
               
                rb.AddForce(direction * addVelocity * Time.fixedDeltaTime, ForceMode2D.Impulse);
                
                
            }
           
        }
        else
        {
            rb.AddForce(direction * addVelocity * Time.fixedDeltaTime, ForceMode2D.Impulse);
            Destroy(gameObject, 0.1f);
            
        }
        

    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
