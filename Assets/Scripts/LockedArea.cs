using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockedArea : Collidable
{
    [Header("Boss")]
    //public Boss boss;

    public GameObject[] spikes;
    public List<Transform> enemies = new List<Transform>();
    public int enemyCount = 0;

    //int count = 0;
    protected override void Start()
    {
        base.Start();
        
        
        for (int i = 0; i < spikes.Length; i++)
        {
            spikes[i].SetActive(false);
        }
    }
    protected override void Update()
    {
        base.Update();
        //for (int i = 0; i < enemies.Count; i++)
        //{
        //    if (enemies.Count>= 1  && enemies[i].activeSelf == true)
        //    {
        //        enemyCount += 1 ;
        //    }
        //    else
        //    {
        //        enemyCount--;
        //    }
        //}
        //boss.GetComponentInChildren<Boss>();
        foreach (Transform enemy in enemies)
        {

            if (enemy == null)
            {
                for (int i = 0; i < spikes.Length; i++)
                {
                    spikes[i].SetActive(false);
                }
                
            }

            //boss.bulletCircle.gameObject.SetActive(true);
        }

        
       if(enemies.Count == 0)
        {
            for (int i = 0; i < spikes.Length; i++)
            {
                spikes[i].SetActive(false);
            }
        }
        
        
    }

    protected override void OnCollide(Collider2D coll)
    {

        if (coll.gameObject.name == "Player" )
        {
            //boss.bulletCircle.gameObject.SetActive(true);
            for (int i = 0; i < spikes.Length; i++)
            {
                spikes[i].SetActive(true);
            }

        }

        
    }

    
}
