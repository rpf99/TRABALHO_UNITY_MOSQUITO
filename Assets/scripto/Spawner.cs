using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy, warning;
    public float spawnRate;
    private float nextSpawn = 0;
    public SpriteTreatment st;
    private SpriteRenderer sr;
    private GameObject warningClone;
    
    private void Start() 
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        var v3 = transform.position;
        v3.y += 0.6f;
        warningClone = Instantiate(warning, v3, warning.transform.rotation);
    }

    void Update() {
        if(gameObject.CompareTag("Emissor")) {
            if(Time.time > nextSpawn) {
                nextSpawn = Time.time + spawnRate;
                Instantiate(enemy, transform.position, enemy.transform.rotation);
            } 
        }
    }

    public void TrocarSprite() {
        warningClone.transform.parent = null;
        DestroyImmediate(warningClone);
        sr.sprite = st.ReturnSprite(sr.sprite);
        gameObject.tag = "Limpo";
    }
    
}