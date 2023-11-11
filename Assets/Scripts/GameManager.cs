using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
        
    }

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //Reference
    public Player player;
    public Weapon weapon;
    
    public FloatingTextManager floatingTextManager;
    //Logic
    public int gold;
    public int experience;

    //floating Text
    public void ShowText(string msg, int fontSize, Color color, Vector3 pos, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, pos, motion, duration);
    }

    //Upgrade weapon
    public bool TryUpgradeWeapon()
    {
        //Is weapon max level?
        if(weaponPrices.Count <= weapon.weaponLevel)
            return false;

        if(gold >= weaponPrices[weapon.weaponLevel])
        {
            gold -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }
    
    //Experience System
    public int GetCurentLevel()
    {
        // r is a current level player
        int r = 0;
        // add is a experice to update level player
        int add = 0;

        while(experience >= add)
        {
            add += xpTable[r];
            r++;

            if(r == xpTable.Count) // max level
                return r;
        }

        return r;
    }    
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while(r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }
    public void GrantXp(int xp)
    {
        int currLevel  = GetCurentLevel();
        experience += xp;

        if(currLevel < GetCurentLevel())
        {
            this.OnLevelUp();
        }
    }
    public void OnLevelUp()
    {
        Debug.Log("Level up!");
        player.OnLevelUp();
    }    
    //save
    /* int prefered Skin
     * int gold
     * int experience
     * int weapon Level
     */
    public void SaveState()
    {
        string s = " ";
        s += "0" + "|";
        s += gold.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState",s);
        Debug.Log("SaveState");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState")) return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //change player skin
        //change player gold
        gold = int.Parse(data[1]);
        //change player exp
        experience = int.Parse(data[2]);
        //change player weaponLevel
        weapon.SetWeaponLevel(int.Parse(data[3]));

        Debug.Log("LoadState");
    }
}
