using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject enemy, warning;
    public float spawnRate;
    private float nextSpawn = 0;
    public SpriteTreatment st;
    private SpriteRenderer sr;
    private GameObject warningClone;
    private float distancia_maxima = 1.5f;
    
    private AudioSource AS;
    public AudioClip treated;
    
    private void Start() {
        sr = gameObject.GetComponent<SpriteRenderer>();
        var v3 = transform.position;
        v3.y += 0.6f;
        warningClone = Instantiate(warning, v3, warning.transform.rotation);
        AS = GetComponent<AudioSource>();
        AS.Stop();
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distancia_maxima);
    }

    void Update() {
        if(gameObject.CompareTag("Emissor")) {
            if(Time.time > nextSpawn) {
                nextSpawn = Time.time + spawnRate;
                float aleatorio = Random.Range(1f, this.distancia_maxima);
                Vector3 pos = (Random.onUnitSphere * aleatorio) + transform.position;
                AS.PlayOneShot(AS.clip);
                Instantiate(enemy, pos, enemy.transform.rotation);
            } 
        }
    }

    public void TrocarSprite() {
        AS.PlayOneShot(treated);
        warningClone.transform.parent = null;
        gameObject.tag = "Limpo";
        DestroyImmediate(warningClone);
        sr.sprite = st.ReturnSprite(sr.sprite);
    }
    
}