using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectTreatment : MonoBehaviour {

    public AudioClip[] lt;
    private AudioSource As;

    void Start() {
        As = gameObject.GetComponent<AudioSource>();
    }

    public void PlayClip(string clip) {
        switch (clip) {
            case "EletricSwatter":
                As.PlayOneShot(lt[0]);
                break;
            case "SpawnerTreatment":
                As.PlayOneShot(lt[1]);
                break;
            case "Upgrade":
                As.PlayOneShot(lt[2]);
                break;
            case "Winner":
                As.PlayOneShot(lt[3]);
                break;
            case "GameOver":
                As.PlayOneShot(lt[4]);
                break;
            case "Walking":
                As.PlayOneShot(lt[5]);
                break;
        }
    }
}
