using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseProjectile
{
    private float lifeTimer;
    [SerializeField] private float maxLife = 2.0f;

    private Transform target;
    [SerializeField] private float moveSpeed;

    
    protected override void Start()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    protected override void Update()
    {
        base.Update();
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= maxLife)
        {
            Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
}
