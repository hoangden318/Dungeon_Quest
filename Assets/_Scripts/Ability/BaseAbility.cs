using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : MonoBehaviour
{
    protected string abilityName;
    protected float activeTime;
    protected float cooldownTime;

    public virtual void Active()
    {

    }
}
