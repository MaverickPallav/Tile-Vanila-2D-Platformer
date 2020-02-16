using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    public GameObject LoseLabel;
    public GameObject InstructionsLabel;

    // Start is called before the first frame update
    void Start()
    {
        LoseLabel.SetActive(false);
        InstructionsLabel.SetActive(true);
    }

    public void OnPlayerDeath()
    {
        LoseLabel.SetActive(true);
    }

    public void DisableInstructionsLabel()
    {
        InstructionsLabel.SetActive(false);
    }
}
