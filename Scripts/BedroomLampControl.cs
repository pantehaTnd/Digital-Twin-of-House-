//Written by : Panteha Taghavinezhad
//Creation date: Spring/Summer 2024


using UnityEngine;

public class BedroomLampControl : MonoBehaviour
{
    public Light bedroomLamp; // Reference to the bedroom lamp light
    public GameObject bedroomButton; // Reference to the bedroom button object

    private bool isBedroomLampOn = false; // Track the state of the bedroom lamp

    void Start()
    {
        // Ensure the lamp light and button are assigned
        if (bedroomLamp == null)
        {
            Debug.LogError("Bedroom lamp is not assigned.");
        }
        else
        {
            bedroomLamp.intensity = 0f; // Initialize the lamp to be off
        }
        if (bedroomButton == null)
        {
            Debug.LogError("Bedroom button is not assigned.");
        }
    }

    void Update()
    {
        // Check for mouse click on the bedroom button
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the camera to the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits the bedroom button's collider
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == bedroomButton.transform)
                {
                    // Toggle the bedroom lamp on/off
                    ToggleBedroomLamp();
                }
            }
        }
    }

    void ToggleBedroomLamp()
    {
        isBedroomLampOn = !isBedroomLampOn;
        bedroomLamp.intensity = isBedroomLampOn ? 1f : 0f;

        // Optionally, send data to the Arduino if needed
        if (SerialPortManager.Instance != null)
        {
            //string message = isBedroomLampOn ? "1" : "0";
            Debug.Log("Sending buzz command to Arduino");
            SerialPortManager.Instance.WriteToPort(isBedroomLampOn ? "1" : "0");
        }
        else
        {
            Debug.LogError("SerialPortManager instance is null.");
        }

        //Debug.Log("Bedroom lamp state: " + (isBedroomLampOn ? "On" : "Off"));
    }
}
