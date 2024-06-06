using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor_boss : MonoBehaviour
{
    public Sprite openDoor;

    public void OnBossDefeated()
    {
        GetComponent<SpriteRenderer>().sprite = openDoor;
    }
}
