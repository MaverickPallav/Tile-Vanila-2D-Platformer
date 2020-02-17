using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    Text updatescore;
    private int score;
    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        updatescore = GetComponent<Text>();
        updatescore.text = "SCORE : " + score.ToString();
    }

    public void UpdateScoreonPickup()
    {
        score = score + 10;
        updatescore.text = "SCORE : " + score.ToString();
    }
}
