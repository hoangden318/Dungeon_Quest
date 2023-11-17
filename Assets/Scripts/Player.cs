using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

       
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitPointChange();
    }
    protected void FixedUpdate()
    {
        //this.MoveMent();
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        UpdateMotor(new Vector3(x,y,0));
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
}
