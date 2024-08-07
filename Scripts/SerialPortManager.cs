//Written by : Panteha Taghavinezhad
//Creation date: Spring/Summer 2024

using UnityEngine;
using System.IO.Ports;

public class SerialPortManager : MonoBehaviour
{
    private static SerialPortManager _instance;
    public static SerialPortManager Instance => _instance;

    private SerialPort serialPort;

    public string portName = "COM3"; 
    public int baudRate = 9600;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object alive across scenes
            InitializeSerialPort();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSerialPort()
    {
        serialPort = new SerialPort(portName, baudRate);
        try
        {
            serialPort.Open();
            Debug.Log("Serial port opened successfully.");
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to open serial port: " + e.Message);
        }
    }

    public void WriteToPort(string message)
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                serialPort.Write(message);
                Debug.Log("Writing to port: " + message);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to write to serial port: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("Serial port is not open.");
        }
    }

    public string ReadFromPort()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                if (serialPort.BytesToRead > 0)
                {
                    string data = serialPort.ReadLine().Trim();
                    Debug.Log("Read from port: " + data);
                    return data;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error reading serial port: " + e.Message);
            }
        }
        return null;
    }

    private void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                serialPort.Close();
                Debug.Log("Serial port closed successfully.");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to close serial port: " + e.Message);
            }
        }
    }
}
