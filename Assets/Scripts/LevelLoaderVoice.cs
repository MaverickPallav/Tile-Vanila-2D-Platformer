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
            voicetotext.StartRecognised();
            StartCoroutine(StartDetected());    
        }

        if (SpeechMenu)
        {
            voicetotext.MenuRecognised();
            StartCoroutine(MenuDetected());       
        }

        if(SpeechQuit)
        {
            voicetotext.QuitRecognised();
            Application.Quit();
        }
    }

    IEnumerator StartDetected()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level");
    }

    IEnumerator MenuDetected()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("StartScreen");
    }

    
}
