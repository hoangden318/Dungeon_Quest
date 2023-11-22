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
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud.gameObject);
            Destroy(menu.gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        
        
    }
    
    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //Reference
    public Player player;
    public Weapon weapon;
    public Enemy enemy;

    public FloatingTextManager floatingTextManager;
    public RectTransform healthBar;
    public RectTransform healthBarEnemy;

    public GameObject hud;
    public GameObject menu;
    public Animator deathMenuAnim;
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
        this.OnHitPointChange();
    }    
    public virtual void OnHitPointChange()
    {
        float ratioHeal = (float)player.hitPoints / (float)player.maxHitPoints;
        healthBar.localScale = new Vector3(ratioHeal, 1, 1);
    }
    public void OnHitPointsEnenmyChange()
    {
        float ratioBar = (float)enemy.hitPoints / (float)enemy.maxHitPoints;
        healthBarEnemy.localScale = new Vector3(ratioBar, 1, 1);

    }

    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        //load Spawn points player
        player.transform.position = GameObject.Find("SpawnPoints").transform.position;
    }
    //Respawn player
    public void Respawn()
    {
        GameManager.instance.deathMenuAnim.SetTrigger("hide");
        SceneManager.LoadScene("Main");
        player.Respawn();
    }
    public void SaveState()
    {
        string s = " ";
        s += "0" + "|";
        s += gold.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState",s);
       
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState")) return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //change player skin
        //change player gold
        gold = int.Parse(data[1]);

        //change player exp
        experience = int.Parse(data[2]);
        if(this.GetCurentLevel() != 1)
            player.SetLevel(GetCurentLevel());

        //change player weaponLevel
        weapon.SetWeaponLevel(int.Parse(data[3]));

       
        
    }
}
