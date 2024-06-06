using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedBossArea : LockedArea
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (enemyCount == 0)
            StartCoroutine(StopSoundBoss());
    }

    protected override void OnCollide(Collider2D coll)
    {
        base.OnCollide(coll);
       
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player" && gameObject.name == "BossArea")
            StartCoroutine(PlaySoundBoss());
    }

    IEnumerator PlaySoundBoss()
    {
        yield return new WaitForSeconds(0.1f);
        SoundManager.Instance.PlayMusic("BossArea", triggerZoneName);
    }
    IEnumerator StopSoundBoss()
    {
        yield return new WaitForSeconds(0.1f);
        SoundManager.Instance.StopMusic("BossArea", triggerZoneName);
    }
}
