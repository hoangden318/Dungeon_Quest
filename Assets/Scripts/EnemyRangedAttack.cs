using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : Mover
{
    public Transform wayPoint1, wayPoint2;
    private Transform wayPointTarget;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRange;

    private SpriteRenderer sp;
    private Transform target;
    protected override void Start()
    {
        base.Start();
        wayPointTarget = wayPoint1;
        sp = GetComponent<SpriteRenderer>();
        target = GameObject.Find("Player").transform.GetComponent<Transform>();
    }

    protected virtual void Update()
    {
        if(Vector3.Distance(transform.position, target.position) > attackRange)
        {
            this.Patrol();
        }
        
    }

    private void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPointTarget.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, wayPoint1.position) <= 0.01f)
        {
            wayPointTarget = wayPoint2;
            sp.flipX = true;
        }

        if (Vector3.Distance(transform.position, wayPoint2.position) <= 0.01f)
        {
            wayPointTarget = wayPoint1;
            sp.flipX = false;
        }
    }
}
