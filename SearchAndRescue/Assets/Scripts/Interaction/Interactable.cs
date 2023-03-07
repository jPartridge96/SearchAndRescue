using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string message = "Interacting with object.";
    public abstract void Interact();
}