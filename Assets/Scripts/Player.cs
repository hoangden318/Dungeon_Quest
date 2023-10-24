using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;
    [SerializeField] protected Vector3 moveDelta = Vector3.zero;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        this.MoveMent();
    }

    protected void MoveMent()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        this.moveDelta = new Vector3(x, y, 0);
        
        //swap sprite direction
        if(moveDelta.x >= 0)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        //if the box collider is null, can free move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0,moveDelta.y),Mathf.Abs(moveDelta.y * Time.deltaTime),LayerMask.GetMask("Actor","Blocking"));
        
        if(hit.collider == null)
        {
            //movement player
            transform.Translate(0,this.moveDelta.y * Time.deltaTime,0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            //movement player
            transform.Translate(this.moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
