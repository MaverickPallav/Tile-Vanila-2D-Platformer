using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public AudioClip ClickSound;

   public void StartLevel()
    {
        AudioSource.PlayClipAtPoint(ClickSound, Camera.main.transform.position, 0.4f);
        SceneManager.LoadScene("Level");
    }

    public void QuitGame()
    {
        AudioSource.PlayClipAtPoint(ClickSound, Camera.main.transform.position, 0.4f);
        Application.Quit();
    }

    public void MainMenu()
    {
        AudioSource.PlayClipAtPoint(ClickSound, Camera.main.transform.position, 0.4f);
        SceneManager.LoadScene("Start Screen");
    }
}
