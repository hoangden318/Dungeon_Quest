using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_Ability : BaseAbility
{
    public Player player;
    public KeyCode key;
    
    protected float dashPower = 3.05f;

    protected bool canDash = true;
    protected bool isDashing = false;
    protected float dashTime = 0.2f;
    public float dashCooldown = 3.5f;

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected TrailRenderer trail;
    private void Start()
    {
        
        activeTime = dashTime;
        cooldownTime = dashCooldown;

    }
    private void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float playerDir = player.transform.localScale.x;

        if (isDashing) return;

        if(Input.GetKeyDown(key) && canDash)
        {
            if ((playerDir > 0 || playerDir < 0) && vertical == 0)
                StartCoroutine(Dash());
            else if (vertical >= -0.22f || vertical < 0.22f)
                return;
                
        }
    }

    protected IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        Vector2 originVelocity = rb.velocity;
        rb.velocity = new Vector2(player.transform.localScale.x * dashPower, 0f);
        trail.emitting = true;

        yield return new WaitForSeconds(dashTime);

        trail.emitting = false;
        rb.velocity = originVelocity;
        rb.gravityScale = originGravity;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
