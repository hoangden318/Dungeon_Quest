using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : HoangBehavior
{
    public Transform holder;
    public List<Transform> poolObjs;
    public List<Transform> prefabs;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPrefabs();
        this.LoadHolder();
    }

    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;

        Transform prefabObj = transform.Find("Prefab");
        foreach (Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }
        this.HidePrefabs();
    }

    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    public virtual Transform Spawn(string prefabName, Vector3 SpawnPos, Quaternion rotation)
    {
        Transform prefab = this.getPrefabName(prefabName);
        if (prefab == null)
        {
            Debug.LogWarning("Prefab not found " + prefabName);
            return null;
        }


        return Spawn(prefab, SpawnPos, rotation);
    }

    public virtual Transform Spawn(Transform prefab, Vector3 SpawnPos, Quaternion rotation)
    {
        Transform newPrefab = this.getObjectFromPool(prefab);
        newPrefab.SetPositionAndRotation(SpawnPos, rotation);

        newPrefab.parent = this.holder;
       
        return newPrefab;
    }
    protected virtual Transform getObjectFromPool(Transform prefab)
    {
        foreach (Transform poolObj in poolObjs)
        {
            if (poolObj.gameObject.name == prefab.name)
            {
                this.poolObjs.Remove(poolObj);
                return poolObj;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }
    public virtual void Despawn(Transform obj)
    {
        this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
       
    }
    public virtual Transform getPrefabName(string prefabName)
    {
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.name == prefabName) return prefab;
        }
        return null;
    }
}
