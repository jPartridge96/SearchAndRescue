using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    //angles for door
    public float openAngle = 90f;
    public float closeAngle = 0f;
    //check if door is open
    private bool isOpen = false;
    //quaternion for slerping door rotations
    private Quaternion openRotation;
    private Quaternion closeRotation;
    //speed at which the door opens/closes
    public float smooth = 2.0f;
    //the transform for the door game object (drag and drop)
    public Transform door;

    //Changes the message for the string from the IInteractable interface
    public string Message
    {
        get { return isOpen ? "Close the door" : "Open the door"; }
    }
    
    
    private void Start()
    {
	//Set the starting rotation of the door to the predefined rotation angles
        openRotation = door.rotation * Quaternion.Euler(0.0f, openAngle, 0.0f);
        closeRotation = door.rotation * Quaternion.Euler(0.0f, closeAngle, 0.0f);
    }

    private void Update()
    {
        if (isOpen)
        {
            door.rotation = Quaternion.Slerp(door.rotation, openRotation, Time.deltaTime * smooth);
        }
        else
        {
            door.rotation = Quaternion.Slerp(door.rotation, closeRotation, Time.deltaTime * smooth);
        }
    }

    public void Interact()
    {
	//call the interact from the IInteractable, method is setup here to add sound or whatever in the Open/Close methods
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    private void Open()
    {
        isOpen = true;
    }

    private void Close()
    {
        isOpen = false;
    }
}
