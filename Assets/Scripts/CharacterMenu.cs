using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    //text filed
    public Text levelText, hitpointText, goldText, upgradeConstText, xpText;
    //logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprites;
    public Image weaponSprites;
    public RectTransform xpBar;

    //protected void Start()
    //{
    //    //weaponSprites.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
    //}
    //character selection
    public void OnArrowClick(bool right)
    {
        if(right)
        {
            currentCharacterSelection++;

            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            this.OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;

            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            this.OnSelectionChanged();
        }
    }

    private void OnSelectionChanged()
    {
        characterSelectionSprites.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprites(currentCharacterSelection);
    }

    //upgrade Weapon
    public void OnUpgradeWeapon()
    {
        if(GameManager.instance.TryUpgradeWeapon())
            this.UpdateMenu();
    }
    //update the charcater information
    public void UpdateMenu()
    {
        //weapon
        weaponSprites.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLevel];
        if (GameManager.instance.weapon.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeConstText.text = "MAX";
        else
            upgradeConstText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLevel].ToString();

        //meta
        levelText.text = GameManager.instance.GetCurentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitPoints.ToString();
        goldText.text = GameManager.instance.gold.ToString();

        //xpBar
        int currLevel = GameManager.instance.GetCurentLevel();

        if(currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.experience.ToString() + "Total experiences points";// display level
            xpBar.localScale = Vector3.one;
        } 
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currXpToLevel = GameManager.instance.experience - prevLevelXp;

            float completionRatio = (float)currXpToLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio,1,1);
            xpText.text = currXpToLevel.ToString() + " / " + diff;

        }    

    }
}
