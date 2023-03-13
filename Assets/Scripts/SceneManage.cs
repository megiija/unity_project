using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public string nextScene;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by " + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
