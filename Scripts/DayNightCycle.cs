//Written by : Panteha Taghavinezhad
//Creation date: Spring/Summer 2024

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight; // The directional light representing the sun
    public float dayIntensity = 1.0f; // Intensity of the light during the day
    public float nightIntensity = 0.3f; // Intensity of the light during the night
    public GameObject streetLampsGameObject;

    private StreetLampController streetLampController;
    private bool isDay = true; // Track whether it is currently day or night
    

    void Start()
    {
        if (streetLampsGameObject != null)
        {
            streetLampController = streetLampsGameObject.GetComponent<StreetLampController>();
        }

        // Initialize the street lamps based on the current time of day
        UpdateStreetLamps();
        //SetDay();
        //streetLampController.SetStreetLampsActive(false);
    }

    void Update()
    {
        // Check for incoming data from Arduino
        if (SerialPortManager.Instance != null)
        {
            string data = SerialPortManager.Instance.ReadFromPort();
            if (data != null)
            {
                Debug.Log("Data from Arduino: " + data); 

                if (data == "Day" && !isDay)
                {
                    SetDay();
                }
                else if (data == "Night" && isDay)
                {
                    SetNight();
                }
            }
        }
        else
        {
            Debug.LogError("SerialPortManager instance is null.");
        }
    }

    void SetDay()
    {
        directionalLight.intensity = dayIntensity;
        isDay = true;

        UpdateStreetLamps();
        Debug.Log("Switched to Day");
    }

    void SetNight()
    {
        directionalLight.intensity = nightIntensity;
        isDay = false;
        UpdateStreetLamps();

        Debug.Log("Switched to Night");
    }

    void UpdateStreetLamps()
    {
        if (streetLampController != null)
        {
            streetLampController.SetStreetLampsActive(!isDay);
        }
    }
}
