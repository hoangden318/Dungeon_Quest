using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySystem : MonoBehaviour
{
    protected static AbilitySystem instance;
    public static AbilitySystem Instance => instance;

    [SerializeField] protected Canvas abilityCanvas;
    [Header("Dash Ability")]
    [SerializeField] protected Dash_Ability dashAbility;

    [SerializeField] protected Image dashAbilityImage;
    private float cooldown;
    [SerializeField] protected bool isCooldown = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        SetInActive();
        cooldown = dashAbility.dashCooldown;
        dashAbilityImage.fillAmount = 1f;
    }

    private void Update()
    {
        DashAbility();
        CooldownAbility();
        
    }

    protected void DashAbility()
    {
        if(Input.GetKey(dashAbility.key) && GameManager.instance.player.isLive)
        {
            if (isCooldown) return;
            else
            {
                isCooldown = true;
                dashAbilityImage.fillAmount = 0f;
            }
        }

    }
    protected void CooldownAbility()
    {
        if (isCooldown)
        {
            dashAbilityImage.fillAmount += 1 / cooldown * Time.deltaTime;
        }

        if (dashAbilityImage.fillAmount == 1)
            isCooldown = false;

    }

    public void SetActive()
    {
        abilityCanvas.gameObject.SetActive(true);
    }

    public void SetInActive()
    {
        abilityCanvas.gameObject.SetActive(false);
    }
}
