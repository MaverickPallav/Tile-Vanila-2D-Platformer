using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    // make sure to make a Gameobject in Heirarchy and Attach the Script to it And Drag does items under which you want to remember after player loses one life eg , coin pickups
    // or Check Course Lecture to Again Get Some Idea About it

    int startingSceneIndex;

    private void Awake()
    {
        int numScenePersist = FindObjectsOfType<ScenePersist>().Length;

        if (numScenePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
         startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(currentSceneIndex != startingSceneIndex )
        {
            Destroy(gameObject);
        }
    }
}
