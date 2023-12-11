using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttack : Mover
{
    public int expValue = 2;

    public Transform wayPoint1, wayPoint2;
    private Transform wayPointTarget;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackRange;

    private Animator anim;
    public GameObject projectile;
    public Transform firePoint;

    private SpriteRenderer sp;
    private Transform target;

    protected EnemyRangedAttack rangedAttack;
    public EnemySO enemySO;
    protected override void Start()
    {
        base.Start();
        wayPointTarget = wayPoint1;
        sp = GetComponent<SpriteRenderer>();
        target = GameObject.Find("Player").transform.GetComponent<Transform>();
        anim = GetComponent<Animator>();

        rangedAttack = GetComponent<EnemyRangedAttack>();
       
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitPointsEnenmyRangerChange();
    }
    protected virtual void Update()
    {
        if(Vector3.Distance(transform.position, target.position) > attackRange)
        {
            anim.SetBool("isAttack", false);
            this.Patrol();
        }
        else
        {
            anim.SetBool("isAttack", true);
            
        }
        
    }

    private void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPointTarget.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, wayPoint1.position) <= 0.01f)
        {
            wayPointTarget = wayPoint2;
            //sp.flipX = true;
            Vector3 localTemp = transform.localScale;
            localTemp.x *= -1;
            transform.localScale = localTemp;
        }

        if (Vector3.Distance(transform.position, wayPoint2.position) <= 0.01f)
        {
            wayPointTarget = wayPoint1;
            //sp.flipX = false;
            Vector3 localTemp = transform.localScale;
            localTemp.x *= -1;
            transform.localScale = localTemp;
        }
    }

    public void Shot()
    {
        Instantiate(projectile, firePoint.position, Quaternion.identity);
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(expValue);
        GameManager.instance.ShowText("+" + expValue.ToString() + "xp", 20, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
        DropManager.instance.Drop(rangedAttack.enemySO.dropList);
    }
}
