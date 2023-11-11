using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //Experience
    public int expValue = 1;

    //Logic
    public float triggerLenght = 0.3f;
    public float chaseLength = 1.0f;
    private bool chasing;
    private bool collisingWithPlayer;
    private Transform playerTranform;
    private Vector3 staringPosition;

    //Hit Box
    public ContactFilter2D contactFilter;
    private BoxCollider2D hitBox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTranform = GameManager.instance.player.transform;
        staringPosition = transform.position;
        hitBox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    protected void FixedUpdate()
    {
        this.Chasing();
    }

    protected void Chasing()
    {
        //Is Player in range?
        if (Vector3.Distance(playerTranform.position, staringPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTranform.position, staringPosition) < triggerLenght)
                chasing = true;

            if (chasing)
            {
                if (!collisingWithPlayer)
                {
                    UpdateMotor((playerTranform.position - transform.position).normalized);
                }

            }
            else
            {
                UpdateMotor(staringPosition - transform.position);
            }

        }
        else
        {
            UpdateMotor(staringPosition - transform.position);
            chasing = false;
        }

        //check For overlaps
        collisingWithPlayer = false;
        hitBox.OverlapCollider(contactFilter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                return;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
                collisingWithPlayer = true;
            //array is not cleaned up, we do it ourself
            hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(expValue);
        GameManager.instance.ShowText("+" + expValue.ToString()+"xp", 20, Color.magenta, transform.position, Vector3.up * 40, 1.0f);

    }
}
