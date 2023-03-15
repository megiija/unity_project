using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveScene : MonoBehaviour
{
    [SerializeField] List<AdditiveScene> connected;
    public bool isLoaded { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log($"Entered {gameObject.name}");

            LoadScene();


            //GameController.instance.setCurrentScene(this);

            foreach(var scene in connected)
            {
                scene.LoadScene();
            }

            //if (GameController.instance.preScene != null)
            //{
            //    var prevLoaded = GameController.instance.preScene.connected;
            //    foreach (var scene in prevLoaded)
            //    {
            //        if (!connected.Contains(scene) && scene != this)
            //        {
            //            unLoadScene();
            //        }
            //    }
            //}

        }
    }

    public void LoadScene()
    {
        if (!isLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
        }   
    }

    public void unLoadScene()
    {
        if (isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }
}
