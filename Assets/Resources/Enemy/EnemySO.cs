using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "SO/Enemy")]
public class EnemySO : ScriptableObject
{
    public string nameSO = "enemy_1";
    public int maxhp = 5;
    public List<DropRate> dropList;
    public EnemyType enemyType;

    public float GetDropPercentage()
    {
        switch (enemyType) 
        {
            case EnemyType.enemy :
                return 40f;

            case EnemyType.enemyRanger:
                return 30f;

            case EnemyType.boss:
                return 95f;

            default:
                return 1f;
        }
    } 
    
    public void CaculateDropPercentage()
    {
        float enemyDropPercentage = this.GetDropPercentage();

        int totalDropRate = 0;
        foreach (var dropRate in dropList)
        {
            totalDropRate += dropRate.dropRate;
        }

        foreach (var dropRate in dropList)
        {
            float percentage = (float)dropRate.dropRate / totalDropRate * enemyDropPercentage;
            //Debug.Log(dropRate.itemSO.name + ": "+ percentage.ToString("F2") + "%");
        }
    } 
    
    
}
