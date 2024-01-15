using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSpawner : Spawner
{
    public static ItemDropSpawner Instance;

    protected override void Awake()
    {
        base.Awake();
        if (ItemDropSpawner.Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public virtual void Drop(List<DropRate> dropList, Vector3 pos, Quaternion rot)
    {
        int totalDropRate = 1;
        foreach (DropRate dropRate in dropList)
        {
            totalDropRate += dropRate.dropRate;
            //Debug.Log(totalDropRate);
        }

        int randomValue = Random.Range(0, totalDropRate);
        //Debug.Log(randomValue);
        int activeDroprate = 0;
        foreach(DropRate dropRate in dropList)
        {
            activeDroprate += dropRate.dropRate;
            //Debug.Log(activeDroprate);
            if(randomValue < activeDroprate)
            {
                string itemCode = dropRate.itemSO.itemName;
                Transform itemDrop = this.Spawn(itemCode, pos, rot);
                itemDrop.gameObject.SetActive(true);
            }
        }

        //string itemCode = dropList[0].itemSO.itemName;
        //Transform itemDrop = this.Spawn(itemCode,pos, rot );
        //itemDrop.gameObject.SetActive(true);
    }
}
