using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPooling : MonoBehaviour
{
    protected static BulletPooling instance;
    public static BulletPooling Instance => instance;

    //[SerializeField] protected Transform bulletPrefab;
    //[SerializeField] protected Transform Holder;
    //protected Queue<Transform> bulletPool;
    [SerializeField] public Transform prefab;
    [SerializeField] protected List<Transform> poolObjs;
    [SerializeField] protected Transform holder;
    private int poolCount = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
        //bulletPool = new Queue<Transform>();
        HidePrefab();
    }

    //public Transform CreateNewBullet()
    //{
    //    Transform bullet = Instantiate(bulletPrefab,Holder);

    //    Debug.Log("tao moi dan");
    //    bullet.gameObject.SetActive(true);
    //    return bullet;
    //}

    //public Transform GetBullet()
    //{
    //    if (bulletPool == null)
    //    {
    //        Debug.Log("het dan");
    //    }
    //    if(bulletPool.Count > 0)
    //    {
    //        Transform bullet = bulletPool.Dequeue();
    //        bullet.transform.SetParent(null);
    //        bullet.gameObject.SetActive(true);
    //        Debug.Log("lay tu pool");
    //        return bullet;
    //    } else
    //    {
    //        return CreateNewBullet();
            
    //    }
    //}

    //public void ReturnFromPool(Transform bullet)
    //{
    //    gameObject.SetActive(false);
    //    transform.SetParent(Holder);
    //    bulletPool.Enqueue(bullet);
    //}
    public void HidePrefab()
    {
        prefab.gameObject.SetActive(false);
        
    }

    public Transform Spawn(Transform prefab)
    {
        ClearBullet();
        Transform bullet = GetBuletFromPool(prefab);
        
        bullet.parent = holder;
        return bullet;
    }
    public void ClearBullet()
    {
        poolObjs.RemoveAll(bullet => bullet == null);
        poolCount = poolObjs.Count;
    }    
    public Transform GetBuletFromPool(Transform prefab)
    {
        
        foreach (Transform bullet in poolObjs)
        {
            poolObjs.Remove(bullet);
            return bullet;
        }
        
        if (poolCount == 0)
        {
            Transform newPrefab = Instantiate(prefab);
            newPrefab.name = prefab.gameObject.name;
            newPrefab.gameObject.SetActive(true);
            return newPrefab;
        }
        return null;
    }
    public void Despawn(Transform obj, float time)
    {
        if(obj == null) return;

        StartCoroutine(DespawnAfterTime(obj,time));
        
    }
    protected IEnumerator DespawnAfterTime(Transform obj,float time)
    {
        yield return new WaitForSeconds(time);
        if(!poolObjs.Contains(obj) && obj != null)
        {
            poolObjs.Add(obj);
            obj.gameObject.SetActive(false);
        }    
    }
}
