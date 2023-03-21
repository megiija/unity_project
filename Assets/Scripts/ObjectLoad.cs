using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLoad : MonoBehaviour
{
    //Load the always active objects if theyre not on screen

    [SerializeField] GameObject dontDestroyPrefab;

    private void Awake()
    {
        var existingObjects = FindObjectsOfType<DontDestroy>();
        if (existingObjects.Length == 0)
        {
            Instantiate(dontDestroyPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
