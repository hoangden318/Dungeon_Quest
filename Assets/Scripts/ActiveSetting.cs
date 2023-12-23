using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSetting : HoangBehavior
{
    public GameObject activeSetting;
    public bool isActive;

    protected override void Start()
    {
        base.Start();
        activeSetting.SetActive(false);
        //isActive = false;
    }

    public void Update()
    {
        ActiveSettingPanel();
    }

    public void ActiveSettingPanel()
    {
            if (Input.GetKey(KeyCode.Escape))
            {
                activeSetting.SetActive(true);
            }
    }
}
