using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VoiceToText : MonoBehaviour
{
    Text voiceToText;

    private void Start()
    {
        voiceToText = GetComponent<Text>();
    }

    public void StartRecognised()
    {
        voiceToText.text = "Start";
    }

    public void MenuRecognised()
    {
        voiceToText.text = "Menu";
    }

    public void QuitRecognised()
    {
        voiceToText.text = "Quit";
    }

    public void RightMovementRecognised()
    {
        voiceToText.text = "Right";
    }

    public void LeftMovementRecognised()
    {
        voiceToText.text = "Left";
    }

    public void ShootRecognised()
    {
        voiceToText.text = "Shoot";
    }

    public void StopRecognised()
    {
        voiceToText.text = "Stop";
    }

   /* public void JumpPlusRightRecognised()
    {
        voiceToText.text = "Right" + "Jump";
    }

    public void JumpPlusLeftRecognised()
    {
        voiceToText.text = "Left" + "Jump";
    } */

    public void ClimbingUpRecognised()
    {
        voiceToText.text = "Up";
    }

    public void ClimbingDownRecognised()
    {
        voiceToText.text = "Down";
    }

    public void JumpRecognised()
    {
        voiceToText.text = "Jump";
    }

}
