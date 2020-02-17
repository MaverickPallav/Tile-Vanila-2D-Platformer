using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTime : MonoBehaviour
{
    Text updateTime;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        updateTime = GetComponent<Text>();
        updateTime.text = "TIME : " + time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;   
        updateTime.text = "TIME : " + time.ToString("f0");
    }
}
