using UnityEngine;

public class Door : Interactable
{
    public float openAngle = 90f;
    public float closeAngle = 0f;
    private bool isOpen = false;
    private Quaternion openRotation;
    private Quaternion closeRotation;
    public float smooth = 2.0f;
    public Transform door;

    private void Start()
    {
        openRotation = door.rotation * Quaternion.Euler(0.0f, openAngle, 0.0f);
        closeRotation = door.rotation * Quaternion.Euler(0.0f, closeAngle, 0.0f);
    }

    private void Update()
    {
        if(isOpen)
        {
            door.rotation = Quaternion.Slerp(door.rotation, openRotation, Time.deltaTime * smooth);
            this.message = "Close the door";
        }
        else
        {
            door.rotation = Quaternion.Slerp(door.rotation, closeRotation, Time.deltaTime * smooth);
            this.message = "Open the door"; 
        }
    }

    public override void Interact()
    {
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
