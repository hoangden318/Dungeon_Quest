using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    [SerializeField] protected string[] sceneNames;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            GameManager.instance.SaveState();
            //teleport player
            string scenceName = sceneNames[Random.Range(0, sceneNames.Length)];
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    //public string SceneName;
    //public string loadingSceneName = "SceneLoad";


    //protected override void OnCollide(Collider2D coll)
    //{
    //    if (coll.name == "Player")
    //    {
    //        GameManager.instance.SaveState();
    //        StartCoroutine(LoadingScene());

    //    }
    //}
    //IEnumerator LoadingScene()
    //{
    //    //SceneManager.LoadScene(loadingSceneName);

    //    //yield return new WaitForSeconds(2f);

    //    //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneName);
    //    //while (!asyncLoad.isDone)
    //    //{
    //    //    yield return null;
    //    //}

    //    //SceneManager.LoadScene(SceneName);

    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadingSceneName);

    //    yield return new WaitForSeconds(1f);
    //    SceneManager.LoadScene(SceneName);
    //}
}
