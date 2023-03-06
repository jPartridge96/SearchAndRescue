using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    public GameObject menuPanel; // The UI panel to be displayed when the Escape key is pressed

    void Start()
    {
        // Hide the menu panel when the game starts
        Time.timeScale = 0;
        //menuPanel.SetActive(false);
    }

    void Update()
    {
        // If the player presses the Escape key, toggle the menu panel on/off
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (menuPanel.activeSelf)
            {
                // If the menu panel is already active, hide it
                menuPanel.SetActive(false);
                //lock the cursor again
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1; // Resume the game
            }
            else
            {
                // If the menu panel is not active, show it
                menuPanel.SetActive(true);
                //Unlock the cursor
                Cursor.lockState = CursorLockMode.None;
                // Pause the game
                Time.timeScale = 0;
            }
        }
    }

    public void ResumeGame()
    {
        //hide the menu panel and resume the game
        menuPanel.SetActive(false);
        //lock the cursor again
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
}
