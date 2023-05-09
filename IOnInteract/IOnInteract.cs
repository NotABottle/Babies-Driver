using UnityEngine;
internal interface IOnInteract
{
    InteractableType GetInteractableType();
    float GetValue();
    void MoveHand();
    void Select();
    void DeSelect();
    void Update();
}