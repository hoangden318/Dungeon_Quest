using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public fields
    public int hitPoints = 10;
    public int maxHitPoints = 10;
    public float pushRecoverySpeed = 0.2f;

    //Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;

    //push
    protected Vector3 pushDirection;

    //All fighters can receive damage and Die
    protected virtual void ReceiveDamage(Damage dmg) 
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPoints -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 20, Color.red,transform.position, Vector3.zero, 0.5f);

            if(hitPoints <= 0)
            {
                hitPoints = 0;
                this.Death();
            }
        }
    }

    protected virtual void Death()
    {

    }
}
