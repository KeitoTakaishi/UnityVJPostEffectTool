using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameRate : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        var t = this.GetComponent<Text>();
        t.text =  "FPS : " + (1f/Time.deltaTime).ToString();
    }
}
