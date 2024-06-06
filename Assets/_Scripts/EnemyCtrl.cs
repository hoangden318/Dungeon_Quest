using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : HoangBehavior
{
    public EnemySO[] enemySO;
    
    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < enemySO.Length; i++)
        {
            enemySO[i].CaculateDropPercentage();
        }
    }
   
}
