using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [Header("CanvasReferences")]
    public GameObject LoseLabel;
    public GameObject InstructionsLabel;
    public GameObject ScoreLabel;
    public GameObject VoicetoTextLabel;
    public VoiceMovement voiceScript;
    public PlayerMovementRB keyboardscript;
    public VoiceToText voicetotext;
    public AudioClip ClickSound;
    /* [Header("PlayerInfo")]
     public int Playerlives = 3; 
     */

    /* private void Awake() //Appliying Singleton for future Use
     {
         int numGameSessions = FindObjectsOfType<GameSession>().Length;
         if(numGameSessions > 1)
         {
             Destroy(this.gameObject);
         }
         else
         {
             DontDestroyOnLoad(this.gameObject);
         }
     } */

    void Start()
    {
        voicetotext = FindObjectOfType<VoiceToText>();
        voiceScript = FindObjectOfType<VoiceMovement>();
        keyboardscript = FindObjectOfType<PlayerMovementRB>();
        LoseLabel.SetActive(false);
        InstructionsLabel.SetActive(true);
        ScoreLabel.SetActive(false);
        VoicetoTextLabel.SetActive(true);
        keyboardscript.enabled = true;
    }

    public void OnPlayerDeath()
    {
        LoseLabel.SetActive(true);
    }

    public void DisableInstructionsLabel()
    {
        InstructionsLabel.SetActive(false);
    }

    public void ShowScoreLabel()
    {
        ScoreLabel.SetActive(true);
    }

    public void EnableVoice()
    {
        AudioSource.PlayClipAtPoint(ClickSound, Camera.main.transform.position, 0.4f);
        voicetotext.VoiceScriptEnabled();
        voiceScript.enabled = true;
        keyboardscript.enabled = false;
    }

    public void EnableKeyboard()
    {
        AudioSource.PlayClipAtPoint(ClickSound, Camera.main.transform.position, 0.4f);
        StartCoroutine(DisableVoicetoTextforkeyboard());
        voicetotext.KeyboardEnabled();
        keyboardscript.enabled = true;
        voiceScript.enabled = false;
    }

    IEnumerator DisableVoicetoTextforkeyboard()
    {
        yield return new WaitForSeconds(3f);
        VoicetoTextLabel.SetActive(false);
    }

    /*public void ProcessPlayerDeath()
    {
        if(PlayerLives > 1)
        {
            TakeLife();
        }
        else
        {
            OnPlayerDeath();
        }
    }*/

    /* public void TakeLife()
     {
         PlayerLives --;
     }*/
}