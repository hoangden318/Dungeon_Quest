using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptySprite;
    private int goldAmount;
    private int minGold = 7;
    private int maxGold = 31;

    protected override void OnCollect()
    {
        
        if(!Collected)
        {
            Collected = true;
            SoundManager.Instance.PlaySfx("Treasure");
            goldAmount = Random.Range(minGold, maxGold);
            GetComponent<SpriteRenderer>().sprite = emptySprite;
            boxCollider.enabled = false;
            GameManager.instance.gold += goldAmount;
            GameManager.instance.ShowText("+"+goldAmount+" Gold",23, Color.yellow, transform.position,Vector3.up * 50,1.5f);
        }
    }
}
