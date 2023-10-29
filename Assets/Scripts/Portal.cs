using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    [SerializeField] protected string[] senceNames;
    
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            GameManager.instance.SaveState();
            //teleport player
            string scenceName = senceNames[Random.Range(0, senceNames.Length)];
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
