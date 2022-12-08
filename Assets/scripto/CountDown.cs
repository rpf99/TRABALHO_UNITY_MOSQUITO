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
    private AudioSource sound;
    private PlayerMovement pl;
    
    private void Start() {
        minutos = 5;
        segundos = 0f;
        sound = GetComponent<AudioSource>();
        sound.Stop();
        pl = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    
    void FixedUpdate() {
        if (pl.Perdeu() == false) {
            timeText.text = String.Format("{0:00}:{1:00}", this.minutos, this.segundos);
            if (segundos <= 0.01) {
                if (minutos == 0) {
                    pl.GAMEOVER();
                }else {
                    minutos -= 1;
                    segundos = 59;
                }
            }
        
            segundos -= Time.deltaTime;
        }
    }
    
}
