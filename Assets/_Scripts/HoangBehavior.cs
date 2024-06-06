using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoangBehavior : MonoBehaviour
{
    protected virtual void Reset()
    {
        this.LoadComponent();
        this.ResetValue();
    }

    protected virtual void LoadComponent()
    {
        // for Override
    }

    protected virtual void ResetValue()
    {
        // for Override
    }
    protected virtual void Awake()
    {
        this.LoadComponent();
    }

    protected virtual void Start()
    {
        // for override
    }

    protected virtual void OnEnable()
    {
        // for override
    }
}
