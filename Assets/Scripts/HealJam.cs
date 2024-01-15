using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealJam : Collectable
{
    public Sprite emptySprite;
    private int healAmount = 1;
    

    protected override void OnCollect()
    {

        if (!Collected && GameManager.instance.player.hitPoints < GameManager.instance.player.maxHitPoints)
        {
            Collected = true;
           
            
                GameManager.instance.player.hitPoints += healAmount;
                SoundManager.Instance.PlaySfx("Treasure");
                
                GetComponent<SpriteRenderer>().sprite = emptySprite;
                boxCollider.enabled = false;
            
           
            GameManager.instance.ShowText("+" + healAmount + "HP", 23, Color.red, transform.position, Vector3.up * 60, 1.75f);
        }
    }

    protected override void OnNoCollect()
    {
        if(GameManager.instance.player.hitPoints >= GameManager.instance.player.maxHitPoints && Collected)
        {
            Collected = false;
            boxCollider.enabled = false;
            GameManager.instance.ShowText("Full HP", 23, Color.red, transform.position, Vector3.up * 60, 1.75f);
        }
    }
}
