using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //damage struct
    public int[] damagePoint = {1, 2, 3, 4, 5, 6, 7};
    public float[] pushForce = { 2.0f ,2.2f, 2.5f, 3.0f, 3.2f, 3.5f, 4.0f};

    //upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    //swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                this.Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter")
        {
            if (coll.name == "Player") return;

            //create a new damage obj, then we'll to send it to the fighter when we hit
            Damage dmg = new Damage()
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };
            //Debug.Log(coll.name);
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }
    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
    
    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
