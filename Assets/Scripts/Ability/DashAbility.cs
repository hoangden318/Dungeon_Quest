using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Dash")]
public class DashAbility : Ability
{
    public float dashDistance = 0.4f;
    public float minDistanceToWall = 0.01f;
    public LayerMask wallLayer;
    private Mover player;
    //Vector3 targetPos;

    public override void Activate(GameObject parent)
    {
        player = parent.GetComponent<Mover>();
        
        float dashDirection = player.transform.localScale.x;
       
        if (dashDirection > 0)
        {
            Vector3 dashDir = new Vector3(dashDirection, 0f, 0f);
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, dashDir, dashDistance, wallLayer);
            if(hit.collider != null)
            {
                float distanceToWall = Vector3.Distance(player.transform.position, hit.point);
                if (distanceToWall <= minDistanceToWall) return;
               
            }
            parent.transform.Translate(dashDir * dashDistance);
            //targetPos = new Vector3(0.4f, player.transform.position.y, 0);
            //player.transform.position = new Vector3((player.transform.position.x + targetPos.x), player.transform.position.y, 0f);
        } 
        else if(dashDirection < 0)
        {
            Vector3 dashDir = new Vector3(-dashDirection, 0f, 0f);
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, -dashDir, dashDistance, wallLayer);
            if (hit.collider != null)
            {
                float distanceToWall = Vector3.Distance(player.transform.position, hit.point);
                if (distanceToWall <= minDistanceToWall) return;
               
            }
            parent.transform.Translate(-dashDir * dashDistance);

            //targetPos = new Vector3(-0.4f, player.transform.position.y, 0);
            //player.transform.position = new Vector3((player.transform.position.x + targetPos.x), player.transform.position.y, 0f);
        }
        
    }
}
