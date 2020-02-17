using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Windows.Speech;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoaderVoice : MonoBehaviour
{
    [Header("SpeechEssentials")]
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    [Header("BoolCaches")]
    bool SpeechStart = false;
    bool SpeechQuit = false;
    bool SpeechMenu = false;

    [Header("CachedReferences")]
    VoiceToText voicetotext;

    public AudioClip ClickSound;

    private void Start()
    {
        voicetotext = FindObjectOfType<VoiceToText>();

        actions.Add("Start", StartGame);
        actions.Add("Quit", QuitGame);
        actions.Add("MainMenu", MenuScreen);
        actions.Add("Menu", MenuScreen);

        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedVoice;
        keywordRecognizer.Start();
    }

    private void RecognizedVoice(PhraseRecognizedEventArgs speech)
    {
        Debug.Log(speech.text);
        actions[speech.text].Invoke();
    }

    private void StartGame()
    {
        SpeechStart = true;
        SpeechQuit = false;
        SpeechMenu = false;
    }

    private void QuitGame()
    {
        SpeechStart = false;
        SpeechQuit = true;
        SpeechMenu = false;
    }

    private void MenuScreen()
    {
        SpeechStart = false;
        SpeechQuit = false;
        SpeechMenu = true;
    }

    private void Update()
    {
        if (SpeechStart)
        {
            AudioSource.PlayClipAtPoint(ClickSound, Camera.main.transform.position, 0.4f);
            voicetotext.StartRecognised();
            StartCoroutine(StartDetected());    
        }

        if (SpeechMenu)
        {
            AudioSource.PlayClipAtPoint(ClickSound, Camera.main.transform.position, 0.4f);
            voicetotext.MenuRecognised();
            StartCoroutine(MenuDetected());       
        }

        if(SpeechQuit)
        {
            AudioSource.PlayClipAtPoint(ClickSound, Camera.main.transform.position, 0.4f);
            voicetotext.QuitRecognised();
            Application.Quit();
        }
    }

    IEnumerator StartDetected()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Level");
    }

    IEnumerator MenuDetected()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Start Screen");
    }

    
}
