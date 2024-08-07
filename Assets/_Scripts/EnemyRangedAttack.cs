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
    //public Transform projectile;
    public Transform firePoint;

    private SpriteRenderer sp;
    private Transform target;

    public EnemyRangedAttack rangedAttack;
    public EnemySO enemySO;

    //heal bar
    public RectTransform healthBarEnemyRanger;
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
        OnHitPointsEnenmyRangerChange();
    }
    public void OnHitPointsEnenmyRangerChange()
    {
        float ratioBar = (float)rangedAttack.hitPoints / (float)rangedAttack.maxHitPoints;
        healthBarEnemyRanger.localScale = new Vector3(ratioBar, 1, 1);

    }
    protected void Update()
    {
        if(Vector3.Distance(transform.position, target.position) > attackRange)
        {
            anim.SetBool("isAttack", false);
            this.Patrol();
        }
        else
        {
            anim.SetBool("isAttack", true);
            //Shot(BulletPooling.Instance.prefab);
        }
        
    }

    private void Patrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPointTarget.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, wayPoint1.position) <= 0.01f)
        {
            wayPointTarget = wayPoint2;
            
            Vector3 localTemp = transform.localScale;
            localTemp.x *= -1;
            transform.localScale = localTemp;
        }

        if (Vector3.Distance(transform.position, wayPoint2.position) <= 0.01f)
        {
            wayPointTarget = wayPoint1;
            
            Vector3 localTemp = transform.localScale;
            localTemp.x *= -1;
            transform.localScale = localTemp;
        }
    }

    public void Shot()
    {
        Transform prefab = BulletPooling.Instance.prefab;
        //Instantiate(projectile, firePoint.position, Quaternion.identity);
        Transform bullet = BulletPooling.Instance.Spawn(prefab);
        bullet.transform.SetPositionAndRotation(firePoint.position, Quaternion.identity);
        bullet.gameObject.SetActive(true);
        //Debug.Log("ban!!");
    }

    protected override void Death()
    {
        Vector3 pos = transform.position;
        Quaternion rot = Quaternion.identity;
        Destroy(gameObject);
        GameManager.instance.GrantXp(expValue);
        GameManager.instance.ShowText("+" + expValue.ToString() + "xp", 20, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
        //DropManager.instance.Drop(rangedAttack.enemySO.dropList);
        ItemDropSpawner.Instance.Drop(rangedAttack.enemySO.dropList, pos, rot);
    }
}
