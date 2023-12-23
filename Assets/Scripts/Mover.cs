using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected Vector3 originalSize;

    protected BoxCollider2D boxCollider;
    protected RaycastHit2D hit;
    protected Vector3 moveDelta = Vector3.zero;
    public float xSpeed = 1.0f;
    public float ySpeed = 0.8f;

    public float doubleTapTime = 0.19f;
    private float lastTapTime;
    private Vector3 targetPos;
   
    protected override void Start()
    {
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }
    protected virtual void Update()
    {
        this.DoubleTap();
    }
    protected virtual void UpdateMotor(Vector3 input)
    {
        //reset moveDelta
        this.moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        //this.DoubleTap();
        //swap sprite direction
        if (moveDelta.x > 0)
            transform.localScale = originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x *-1, originalSize.y, originalSize.z);

        //add push vector if any
        moveDelta += pushDirection;

        //reduce push force every frame, based off recovery speed
        pushDirection = Vector3.Lerp(pushDirection,Vector3.zero,pushRecoverySpeed);

        //if the box collider is null, can free move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            //movement player
            transform.Translate(0, this.moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            //movement player
            transform.Translate(this.moveDelta.x * Time.deltaTime, 0, 0);
        }

       
    }

    public void DoubleTap()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (Time.time - lastTapTime < doubleTapTime)
            {
                targetPos = new Vector3(0.3f * xSpeed , transform.position.y * ySpeed, 0);
                transform.position = new Vector3((transform.position.x + targetPos.x), transform.position.y,0f);
                
            }
            lastTapTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (Time.time - lastTapTime < doubleTapTime)
            {
                targetPos = new Vector3(-0.3f * xSpeed , transform.position.y * ySpeed, 0);
                transform.position = new Vector3((transform.position.x + targetPos.x), transform.position.y, 0f);
                
            }
            lastTapTime = Time.time;
        }

       
    }
}
