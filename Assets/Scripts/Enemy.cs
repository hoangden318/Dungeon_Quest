using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Enemy : Mover
{
    public Enemy enemy;
    //Experience
    public int expValue = 1;
    //public Vector3 originScale;
    //Logic
    public float triggerLenght = 0.3f;
    public float chaseLength = 1.0f;
    private bool chasing;
    private bool collisingWithPlayer;
    private Transform playerTranform;
    private Vector3 staringPosition;

    //Hit Box
    public ContactFilter2D contactFilter;
    private BoxCollider2D hitBox;
    private Collider2D[] hits = new Collider2D[10];

    //Patrol to WayPoints
    //public float speedToWaypoints;
    //public Transform waypoint1, waypoint2;
    //private Transform waypointTarget;
    private SpriteRenderer sr;
    //public bool isPatrol;

    //heal bar
    public RectTransform healthBarEnemy;

    public EnemySO enemySO;
    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
        //isPatrol = true;
        sr.flipX = true;
        //waypointTarget = waypoint1;
        
       
        playerTranform = GameManager.instance.player.transform;
        staringPosition = transform.position;
        hitBox = transform.GetChild(0).GetComponent<BoxCollider2D>();

       
    }
    
    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);
        OnHitPointsEnenmyChange();
    }
    public void OnHitPointsEnenmyChange()
    {
        float ratioBar = enemy.hitPoints / enemy.maxHitPoints;
        healthBarEnemy.localScale = new Vector3(ratioBar, 1, 1);

    }
    protected void FixedUpdate()
    {
        //this.Patrol();
        this.Chasing();
        
    }

    //protected virtual void Patrol()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, waypointTarget.position, speedToWaypoints * Time.fixedDeltaTime);
    //    isPatrol = true;
    //    if (Vector3.Distance(transform.position, waypoint1.position) <= 0.01f)
    //    {
    //        waypointTarget = waypoint2;
    //        sr.flipX = false;
    //    }

    //    if (Vector3.Distance(transform.position, waypoint2.position) <= 0.01f)
    //    {
    //        waypointTarget = waypoint1;
    //        sr.flipX = true;
    //    }
    //}
    protected virtual void Chasing()
    {
        //Is Player in range?
        if (Vector3.Distance(playerTranform.position, staringPosition) < chaseLength)
        {
            
            if (Vector3.Distance(playerTranform.position, staringPosition) < triggerLenght)
                chasing = true;
                //isPatrol = false;
                sr.flipX = true;

            if (chasing)
            {
                if (!collisingWithPlayer)
                {
                    UpdateMotor((playerTranform.position - transform.position).normalized);
                }

            }
            else
            {
                UpdateMotor(staringPosition - transform.position);
            }

        }
        else
        {
            UpdateMotor(staringPosition - transform.position);
            chasing = false;
            //isPatrol = true;
            sr.flipX = false;
        }

        //check For overlaps
        collisingWithPlayer = false;
        hitBox.OverlapCollider(contactFilter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                return;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
                collisingWithPlayer = true;
            //array is not cleaned up, we do it ourself
            hits[i] = null;
        }
    }
   
    protected override void Death()
    {
       
        Vector3 pos = transform.position;
        Quaternion rot = Quaternion.identity;
        Destroy(gameObject);
        GameManager.instance.GrantXp(expValue);
        GameManager.instance.ShowText("+" + expValue.ToString()+"xp", 20, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
        ItemDropSpawner.Instance.Drop(enemy.enemySO.dropList, pos, rot);
    }
}
