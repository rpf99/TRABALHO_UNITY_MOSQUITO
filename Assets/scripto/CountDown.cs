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
    private GameObject backgroundSong;
    
    private void Start() {
        minutos = 5;
        segundos = 0f;
        sound = GetComponent<AudioSource>();
        sound.Stop();
        backgroundSong = GameObject.Find("BackgroundSong");
    }
    
    void FixedUpdate() {
        timeText.text = String.Format("{0:00}:{1:00}", this.minutos, this.segundos);
        
        if (segundos <= 0.01) {
            if (minutos == 0) {
                backgroundSong.GetComponent<AudioSource>().Stop();
                sound.PlayOneShot(sound.clip);
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
