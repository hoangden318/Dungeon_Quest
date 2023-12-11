using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : HoangBehavior
{
    public static DropManager instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null ) Destroy( instance );
        instance = this;
    }

    public void Drop(List<DropRate> dropList)
    {
        Debug.Log(dropList[0].itemSO.itemName);
    }
}
