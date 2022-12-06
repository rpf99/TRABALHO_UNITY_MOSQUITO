using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthController : MonoBehaviour
{
    
    public static HealthController hController { get; private set; }

    public float health = 15;
    public float Health {
        get {
            return health;
        }
        set {
            health = Mathf.Clamp(value, 0, healthMax);
        }
    }
    public float healthMax = 15;
    public Image healthBar;

    private void Start()
    {
        hController = this;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = health / healthMax;
    }

    public void Damage()
    {   
        this.Health = Health - 5;
    }
}
