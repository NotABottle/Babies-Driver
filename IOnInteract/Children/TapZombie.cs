using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapZombie : MonoBehaviour, IOnInteract
{
    private bool isSelected = false;
    private bool wasTapped = false;

    public void DeSelect()
    {
        isSelected= false;
    }

    public InteractableType GetInteractableType()
    {
        throw new System.NotImplementedException();
    }

    public float GetValue()
    {
        if(wasTapped == true){
            return 1f;
        }else{
            return 0f;
        }
    }

    public void MoveHand()
    {
        
    }

    public void Select()
    {
        isSelected = true;
        wasTapped = true;
    }

    public void Update()
    {

    }
}
