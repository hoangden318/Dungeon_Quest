using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptySprite;
    public int goldAmount = 10;

    protected override void OnCollect()
    {
        
        if(!Collected)
        {
            Collected = true;
            GetComponent<SpriteRenderer>().sprite = emptySprite;
            GameManager.instance.gold += goldAmount;
            GameManager.instance.ShowText("+"+goldAmount+"Gold",23, Color.yellow, transform.position,Vector3.up * 50,1.5f);
        }
    }
}
