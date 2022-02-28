using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeController : MonoBehaviour
{   
    public float current_time = 0f;  // Seconds
    public Text timer_text;
    public bool is_timer_active = true;

    void Update()
    {   
        if(is_timer_active == true) current_time += Time.deltaTime;
        TimeSpan time = TimeSpan.FromSeconds(current_time);
        timer_text.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }
}
