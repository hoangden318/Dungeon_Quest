using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    private bool isLive = true;


    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

       
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isLive) return;

        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitPointChange();
    }

    protected override void Death()
    {
        isLive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("show");
    }
    protected void FixedUpdate()
    {
        //this.MoveMent();
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if(isLive)
            UpdateMotor(new Vector3(x, y, 0));
    }
    
    public void SwapSprites(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp()
    {
        maxHitPoints++;
        hitPoints = maxHitPoints;
    }
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }
    public void Heal(int healingAmount)
    {
        if (hitPoints == maxHitPoints)
            return;

        hitPoints += healingAmount;

        if(hitPoints > maxHitPoints)
            hitPoints = maxHitPoints;

        GameManager.instance.ShowText("+ " + healingAmount.ToString()+"hp",20, Color.red, transform.position, Vector3.up * 40, 1.0f);
        GameManager.instance.OnHitPointChange();
    }
    public void Respawn()
    {
        isLive = true;
        this.Heal(maxHitPoints);
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }
}
