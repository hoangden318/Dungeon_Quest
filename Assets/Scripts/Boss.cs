using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float[] fireBallSpeed = { 1.2f, -1.2f };
    public float distance = 0.4f;
    public Transform[] fireBalls;

    private void Update()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            fireBalls[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * fireBallSpeed[i]) * distance, Mathf.Sin(Time.time * fireBallSpeed[i]) * distance,0);
        }
    }
}
