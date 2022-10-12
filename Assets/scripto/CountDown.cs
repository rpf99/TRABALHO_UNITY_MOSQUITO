using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text minutesText;
    public Text secondsText;
    
    public int minutos;
    public float segundos;

    void FixedUpdate() {
        
        minutesText.text = minutos.ToString("00");
        secondsText.text = segundos.ToString("00");
        if (segundos <= 0.01) {
            if (minutos == 0)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
            else
            {
                minutos -= 1;
                segundos = 59;
            }
        }
        
        segundos -= Time.deltaTime;
    }
    
}
