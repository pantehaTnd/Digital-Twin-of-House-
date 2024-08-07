//Written by : Panteha Taghavinezhad
//Creation date: Spring/Summer 2024


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingroomLampControl : MonoBehaviour
{
    public Light livingroomLamp;
    public GameObject livingButton;


    private bool isLivingroomLampOn = false;

    void Start()
    {
        
        
            livingroomLamp.intensity = 0f; // Initialize the lamp to be off
        

    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == livingButton.transform)
                {
                    ToggleLivingroomLamp();
                }
            }
        }
    }

    void ToggleLivingroomLamp()
    {
        isLivingroomLampOn = !isLivingroomLampOn;
        livingroomLamp.intensity = isLivingroomLampOn ? 3f : 0f;

        if (SerialPortManager.Instance != null)
        {
            string message = isLivingroomLampOn ? "3" : "2";
            SerialPortManager.Instance.WriteToPort(message);
        }

        Debug.Log("Bedroom lamp state:" + (isLivingroomLampOn ? "On" : "Off"));
    }
}
