using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTreatment : MonoBehaviour
{
    public Sprite[] spriteArray;
    private Dictionary<Sprite, Sprite> Sprite_Dict;
    
    private void Start() {
        Sprite_Dict = new Dictionary<Sprite, Sprite>();
        for (int x = 0; x < spriteArray.Length; x+=2) {
            Sprite_Dict.Add(spriteArray[x], spriteArray[x+1]);
        }
    }

    public Sprite ReturnSprite(Sprite s) {
        return Sprite_Dict[s];
    }
    
}
