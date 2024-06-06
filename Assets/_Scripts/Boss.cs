using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Mover
{
    public Boss boss;
    public OpenDoor_boss openDoor;
    //exp
    public int expValue = 8;
   
    public float[] fireBallSpeed = { 1.2f, -1.2f };
    public float distance = 0.4f;
    public Transform[] fireBalls;

    [SerializeField] protected bool isLive;

    //Logic
    public float triggerLenght = 0.3f;
    public float chaseLength = 1.0f;
    private bool chasing;
    private bool collisingWithPlayer;
    private Transform playerTranform;
    private Vector3 staringPosition;
    private SpriteRenderer sr;

    //heal bar
    public RectTransform healthBarBoss;
    public Text bossName;
    public Image healBoss;
    public BulletCircle bulletCircle;

    public Animator winMenuAnim;
    protected override void Start()
    {
       base.Start();
        isLive = true;
        sr = GetComponent<SpriteRenderer>();
        sr.flipX = true;
        bulletCircle.enabled = false;
        playerTranform = GameManager.instance.player.transform;
        staringPosition = transform.position;
        DeactiveSmallEnemy();
        
        
        bossName.gameObject.SetActive(false);
        healBoss.gameObject.SetActive(false);
       
    }
    void DeactiveSmallEnemy()
    {
        for (int i = 0; i < fireBalls.Length; i++)
            fireBalls[i].gameObject.SetActive(false);
    }
    void ActiveSmallEnemy()
    {
        for (int i = 0; i < fireBalls.Length; i++)
            fireBalls[i].gameObject.SetActive(true);
    }

    public void OnDestroy()
    {
        if(openDoor != null && gameObject.name == "boss_Tentacle")
        {
            openDoor.OnBossDefeated();
            SoundManager.Instance.PlaySfx("WinGame");
        }
            
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);
        OnHitPointsBossChange();
    }

    public void OnHitPointsBossChange()
    {
        float ratioBar = (float)boss.hitPoints / (float)boss.maxHitPoints;
        healthBarBoss.localScale = new Vector3(ratioBar, 1, 1);

    }
    private void FixedUpdate()
    {
        if (isLive && hitPoints <= 6)
        {
            bulletCircle.enabled = false;
            bulletCircle.canCreateBullet = false;
            ActiveSmallEnemy();
            
        }
        AroundEntity();
        this.UpdateChasing();
    }

    protected void UpdateChasing()
    {
        //Is Player in range?
        if (Vector3.Distance(playerTranform.position, staringPosition) < chaseLength)
        {
            bossName.gameObject.SetActive(true);
            healBoss.gameObject.SetActive(true);
            bulletCircle.enabled = true;
            
            if (Vector3.Distance(playerTranform.position, staringPosition) < triggerLenght)
                chasing = true;
           
            sr.flipX = true;

            if (chasing)
            {
                if (!collisingWithPlayer)
                {
                    UpdateMotor((playerTranform.position - transform.position).normalized * 0.6f);
                }

            }
            else
            {
                UpdateMotor((staringPosition - transform.position) * 0.6f);
            }

        }
        else
        {
            UpdateMotor((staringPosition - transform.position) * 0.6f);
            chasing = false;
           
            sr.flipX = false;
        }
    }
    public virtual void AroundEntity()
    {
        if (isLive == true)
        {
            for (int i = 0; i < fireBalls.Length; i++)
            {
                fireBalls[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireBallSpeed[i]) * distance, Mathf.Sin(Time.time * fireBallSpeed[i]) * distance, 0);
            }
        }
       
    }

    
    protected override void Death()
    {
        isLive = false;
        //GameManager.instance.winMenuAnim.SetTrigger("showMenu");
        winMenuAnim.SetTrigger("showMenu");
        Destroy(gameObject);
        bulletCircle.gameObject.SetActive(false);
        GameManager.instance.GrantXp(expValue);
        GameManager.instance.ShowText("+" + expValue.ToString() + "xp", 20, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }
}
