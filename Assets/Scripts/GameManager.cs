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
    
    //Logic
    public int gold;
    public int experience;

    //save
    /* int prefered Skin
     * int gold
     * int experience
     * int weapon Level
     */
    public void SaveState()
    {
        string s = "";
        s += "0" + "|";
        s += gold.ToString() + "|";
        s += experience.ToString() + "|";
        s += "0";

        PlayerPrefs.SetString("SaveState",s);
        Debug.Log("SaveState");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState")) return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //change player skin
        //change player gold
        gold = int.Parse(data[0]);
        //change player exp
        experience = int.Parse(data[1]);
        //change player weaponLevel

        Debug.Log("LoadState");
    }
}
