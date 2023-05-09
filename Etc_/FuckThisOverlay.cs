using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckThisOverlay : MonoBehaviour
{
    public GameObject SpriteOverlay;

    private void Start(){
        SpriteOverlay.SetActive(false);
        SpriteOverlay.SetActive(true);
    }
}
