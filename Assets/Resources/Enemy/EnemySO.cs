using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "SO/Enemy")]
public class EnemySO : ScriptableObject
{
    public string nameSO = "enemy_1";
    public int maxhp = 5;
    public List<DropRate> dropList;
}
