using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCircle : BaseProjectile
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 1.0f;
    public int numberOfBullets = 10;
    public float speardAngle = 30f;
    public float timeBetweenShots = 1.0f;
    public bool canCreateBullet = true;
    //public Transform firePoint;
    protected override void Start()
    {
        if(canCreateBullet)
            InvokeRepeating("Shoot", 0.1f, timeBetweenShots);

    }

    protected override void Update()
    {
        base.Update();

        if (!canCreateBullet)
            CancelInvoke("Shoot");
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        Debug.Log("va cham vs" + gameObject.name);
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
   
    void Shoot()
    {
        
        float angleStep = 360f / numberOfBullets;
        float curentAngle = -speardAngle / 2f;

        for (int i = 0; i < numberOfBullets; i++)
        {
            Vector3 bulletDir = Quaternion.Euler(0, 0, curentAngle) * Vector3.right;
            Vector3 bulletPos = transform.position + bulletDir.normalized * 0.4f;
           

            GameObject bullet = Instantiate(bulletPrefab, bulletPos, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bulletDir * bulletSpeed * 1.0f, ForceMode2D.Impulse);
            //rb.velocity = bulletDir * bulletSpeed * 0.3f;
            Destroy(bullet, 3.0f);
            
            curentAngle += angleStep;


            //float angleStep = speardAngle / numberOfBullets;
            //float angle = -speardAngle / 2f;

            //float bulletDirX = transform.position.x + Mathf.Sin(Mathf.Deg2Rad * angle);
            //float bulletDirY = transform.position.y + Mathf.Cos(Mathf.Deg2Rad * angle);

            //Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY);
            //Vector2 bulletDirectionNomalized = (bulletDirection - (Vector2)transform.position).normalized;
        }
    }
}
