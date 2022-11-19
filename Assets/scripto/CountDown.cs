using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {
    
    public Text timeText;
    private int minutos;
    private float segundos;

    //Princiapl: 5 minutos, 0 segundos
    private void Start() {
        minutos = 0;
        segundos = 10f;
    }
    
    void FixedUpdate() {
        timeText.text = String.Format("{0:00}:{1:00}", this.minutos, this.segundos);
        
        if (segundos <= 0.01) {
            if (minutos == 0) {
                timeText.text = "Game Over";
                Time.timeScale = 0;
            }else {
                minutos -= 1;
                segundos = 59;
            }
        }
        
        segundos -= Time.deltaTime;
    }
    
}
