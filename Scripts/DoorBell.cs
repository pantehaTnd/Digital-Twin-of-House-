//Written by : Panteha Taghavinezhad
//Creation date: Spring/Summer 2024



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBell : MonoBehaviour
{
    public AudioClip doorbellSound;
    private AudioSource audioSource;
    public GameObject doorbellButton;



    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = doorbellSound;
        

    }


    void Update()
    {
        // Check for mouse click on the doorbell button GameObject
        if (Input.GetMouseButtonDown(1))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits the doorbell button's collider
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == doorbellButton.transform)
                {
                    // Play the doorbell sound
                    //audioSource.Play();
                    Debug.Log("Doorbell button clicked");
                    // Send data to the Arduino to beep the buzzer
                    if (SerialPortManager.Instance != null)
                    {
                        SerialPortManager.Instance.WriteToPort("B");
                    }
                    else
                    {
                        Debug.LogError("SerialPortManager instance is null.");
                    }
                }
            }
        }

        // Check for incoming data from Arduino
        if (SerialPortManager.Instance != null)
        {
            string data = SerialPortManager.Instance.ReadFromPort();
            if (data == "ButtonPressed")
            {
                // Play the doorbell sound
                audioSource.Play();
            }
        }
        else
        {
            Debug.LogError("SerialPortManager instance is null.");
        }

    }
}
