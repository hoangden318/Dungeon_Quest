using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] protected Transform targetPlayer;
    [SerializeField] protected float boundX = 0.3f;
    [SerializeField] protected float boundY = 0.15f;

    private void Start()
    {
        targetPlayer = GameObject.Find("Player").transform;
    }
    private void LateUpdate()
    {
        this.Following();
    }

    protected void Following()
    {
        Vector3 delta = Vector3.zero;
        //caculate the distance targetPlayer vs player position
        float deltaX = targetPlayer.position.x - transform.position.x;

        // check if we inside the bound on the X axis
        if (deltaX > boundX || deltaX < -boundX)
        {
            if(transform.position.x < targetPlayer.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        float deltaY = targetPlayer.position.y - transform.position.y;
        // check if we inside the bound on the Y axis
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < targetPlayer.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        //update position player
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
