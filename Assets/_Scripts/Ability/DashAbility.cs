using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Dash")]
public class DashAbility : Ability
{
    //public float dashDistance = 0.4f;
    //public float minDistanceToWall = 0.01f;
    //public LayerMask wallLayer;
    //private Mover player;
    //public bool canDash = false;

    //public override void Activate()
    //{
    //    player = GameManager.instance.player;

    //    float dashDirection = player.transform.localScale.x;
    //    //float movDir = player.transform.position.x;
    //    float y = Input.GetAxis("Vertical");
    //    //Debug.Log(y);
    //    if (dashDirection > 0 && y==0)
    //    {
    //        canDash = true;
    //        if(canDash)
    //        {
    //            SoundManager.Instance.PlaySfx("Dash");
    //            Vector3 dashDir = new Vector3(dashDirection, 0f, 0f);
    //            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, dashDir, dashDistance, wallLayer);
    //            if (hit.collider != null)
    //            {
    //                float distanceToWall = Vector3.Distance(player.transform.position, hit.point);
    //                if (distanceToWall <= minDistanceToWall) return;

    //            }
    //            player.transform.Translate(dashDir * dashDistance);

    //        }


    //    } 
    //    else if(dashDirection < 0 && y==0)
    //    {
    //        canDash = true;
    //        if(canDash)
    //        {
    //            SoundManager.Instance.PlaySfx("Dash");
    //            Vector3 dashDir = new Vector3(-dashDirection, 0f, 0f);
    //            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, -dashDir, dashDistance, wallLayer);
    //            if (hit.collider != null)
    //            {
    //                float distanceToWall = Vector3.Distance(player.transform.position, hit.point);
    //                if (distanceToWall <= minDistanceToWall) return;

    //            }
    //            player.transform.Translate(-dashDir * dashDistance);

    //        }



    //    }
    //    else if(y >= -0.22f || y< 0.22f)
    //    {
    //       canDash = false;
    //        return;
    //    }

    //}

    private bool canDash;
    private bool isDashing;
    private float dashPower = 20f;
    private float dashTime = 0.2f;
    private float dashCooldown = 1f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Player player;
    public override void Activate()
    {
        player.GetComponent<Player>();
        CoroutineRunner.Instance.StartCoroutine(Dash());
       
    }
    
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float gravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(player.transform.localScale.x * dashPower, 0f);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = gravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
