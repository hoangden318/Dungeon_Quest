using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
   public ContactFilter2D contactFilter;
   private BoxCollider2D boxCollider;
   private Collider2D[] hits = new Collider2D[10];

   protected virtual void Start()
   {
        boxCollider = GetComponent<BoxCollider2D>();
   }

   protected virtual void Update()
    {
        this.Collider();
    }

    protected virtual void Collider()
    {
        //collision work
        boxCollider.OverlapCollider(contactFilter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);
            //array is not cleaned up, we do it ourself
            hits[i] = null;
        }
    } 
    
    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log(coll.name);
    }    
}
