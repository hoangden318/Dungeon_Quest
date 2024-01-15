using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoneMusic : MonoBehaviour
{
    public string triggerZoneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            SoundManager.Instance.PlayMusic("EnemyArea", triggerZoneName);
        }
    }
}
