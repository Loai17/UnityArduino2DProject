using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO.Ports;
using System;

public class ArduinoTesting : MonoBehaviour
{
    public string port = "COM5";
    public int baudrate = 9600;
    SerialPort arduinoPort;
    bool isStreaming = false;

    public float speed = 100;
    public GameObject ball;
    Rigidbody ballBody;
    float tempTime;
    float sendRate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        OpenConnection();
        ballBody = ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log(tempTime);
        tempTime += Time.deltaTime;
        if (tempTime > sendRate)
        {
            tempTime -= sendRate;
            ArduinoData();
        }*/
        ArduinoData();
        SetLED(Input.GetKey(KeyCode.L));
    }

    void ArduinoData()
    {
        //Debug.Log(ReadSerialPort());
        (float x, float y, int b) = ConvertArduinoInput();
        Debug.Log((x, y, b));
        ballBody.AddForce(new Vector3(x * speed, 0, y * speed));
        

    }

    void OnApplicationQuit()
    {
        closeConnection();
    }

    void OpenConnection()
    {
        isStreaming = true;

        arduinoPort = new SerialPort(port, baudrate);
        arduinoPort.ReadTimeout = 100;
        arduinoPort.Open();
    }

    void closeConnection()
    {
        arduinoPort.Close();
    }

    string ReadSerialPort(int timeout = 100)
    {
        string message;
        arduinoPort.ReadTimeout = timeout;
        try
        {
            message = arduinoPort.ReadLine();
            return message;
        }
        catch (TimeoutException)
        {
            return null;
        }
    }

    (float, float, int) ConvertArduinoInput()
    {
        if (!arduinoPort.IsOpen) OpenConnection();
        string line = ReadSerialPort();
        if (line == null) return (0, 0, 0);
        string[] parts = line.Split(',');
        string a = parts[0].Split(':')[1];
        a = a.Remove(a.Length - 1);
        string b = parts[1].Split(':')[1];
        b = b.Remove(b.Length - 1);

        Debug.Log(parts[2]);
        int c = int.Parse(parts[2]);
        

        float _a = float.Parse(a);
        float _b = float.Parse(b);

        _a = _a - 2.5f;
        _b = _b - 2.5f;
        int _c = c;
        //bool _c = bool.Parse(c);


        return (_a, _b, _c);
    }    

    void SetRGBLight(float R, float G, float B)
    {
        arduinoPort.WriteLine("R" + R + "," + B + "," + G + "\0");
    }

    void SetLED(bool i)
    {
        if (i) arduinoPort.WriteLine("L1");
        else arduinoPort.WriteLine("L0");
    }
}
