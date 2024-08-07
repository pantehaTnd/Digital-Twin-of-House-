//Written by : Panteha Taghavinezhad
//Creation date: Spring/Summer 2024

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetLampController : MonoBehaviour
{
    private Light[] streetLamps;

    void Start()
    {
        // Find all Light components in the children of the StreetLamps GameObject
        streetLamps = GetComponentsInChildren<Light>();
    }

    public void SetStreetLampsActive(bool isActive)
    {
        foreach (Light lamp in streetLamps)
        {
            lamp.enabled = isActive;
        }
    }
}
