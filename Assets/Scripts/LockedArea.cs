using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockedArea : Collidable
{
    [Header("Area Enemy")]

    public GameObject[] spikes;
    public List<Transform> enemies = new List<Transform>();
    public int enemyCount = 0;

    //private bool isPlayingSound;
    public string triggerZoneName;
    protected override void Start()
    {
        base.Start();
        DeactiveSpikes();
    }
    protected override void Update()
    {
        base.Update();
        
       enemies.RemoveAll(enemy => enemy == null);

        enemyCount = enemies.Count;

        if (enemyCount == 0)
        {
            DeactiveSpikes();
            StartCoroutine(StopSound());
        }

    }

    protected override void OnCollide(Collider2D coll)
    {

        if (coll.gameObject.name == "Player" )
        {
            ActiveSpikes();
        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.gameObject.name == "Player")
    //        StartCoroutine(PlaySound());
    //}
    //IEnumerator PlaySound()
    //{
    //    if(isPlayingSound == false)
    //    {
    //        isPlayingSound = true;
    //        yield return new WaitForSeconds(0.1f);
    //        SoundManager.Instance.PlayMusic("EnemyArea");
    //    }
        
    //}

    IEnumerator StopSound()
    {
        //isPlayingSound = false;
        yield return new WaitForSeconds(0.1f);
        SoundManager.Instance.StopMusic("EnemyArea",triggerZoneName);
    }
    void DeactiveSpikes()
    {
        foreach(GameObject spike in spikes)
        {
            spike.SetActive(false);
        }
    }

    void ActiveSpikes()
    {
        foreach (GameObject spike in spikes)
        {
            spike.SetActive(true);
        }
    }

    //public void AddEnemy(Transform enemy)
    //{
    //    enemies.Add(enemy);
    //    enemyCount = enemies.Count;
    //}

    //public void RemoveEnemy(Transform enemy)
    //{
    //    enemies.Remove(enemy);
    //    enemyCount = enemies.Count;

    //    if(enemyCount == 0)
    //    {
    //        DeactiveSpikes();
    //    }
    //}
}
