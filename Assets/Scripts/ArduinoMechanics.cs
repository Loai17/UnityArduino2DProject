using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class ArduinoMechanics : MonoBehaviour
{
    public float xVal, yVal;
    public bool fireToggle = false;
    public bool joystickToggle = false;
    public bool rotateToggle = false;
    public bool cardInserted = false;

    public bool keyboardControl = true;

    Animator anim;

    public string port = "COM5";
    public int baudrate = 9600;
    SerialPort arduinoPort;
    bool isStreaming = false;

    public float speed = 100;
    public GameObject player;
/*    Rigidbody ballBody;*/
    float tempTime;
    float sendRate = 0.1f;

    public bool blockY;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (!keyboardControl) OpenConnection();

    }

    private void OnDestroy()
    {
        if (!keyboardControl) CloseConnection();
    }

    // Update is called once per frame
    void Update()
    {

        if (!blockY)
        {
            player.transform.position = new Vector3(xVal, yVal, 0);
        }
       else
        {
            player.transform.position = new Vector3(xVal, this.transform.position.y, 0);
        }
        
        if (keyboardControl)
        {
            KeyboardMovement();
        }
        else
        {
            JoystickToMovement();
        }


        if (fireToggle) {
            anim.SetBool("onFire", true);
        } else
        {
            anim.SetBool("onFire", false);
        }
    }

    void JoystickToMovement()
    {
        ArduinoInfo info = ConvertArduinoInput();
        float x = info.xVal; float y = info.yVal; int jButton = info.jButton; int fire = info.fireSensor;
        
        // Movement Logic
        if (x <= 0.17 && y <= 0.17 && x >= (-0.17) && y >= (-0.17)) { /*Do nothing*/ }
        else if (x <= 0.17 && x >= (-0.17) && y > 1.7) { yVal -= (y * speed); Debug.Log("Down"); }
        else if (x <= 0.17 && x >= (-0.17) && y < (-1.7)) { yVal -= (y * speed); Debug.Log("Up"); }
        else if (y <= 0.17 && y >= (-0.17) && x > 1.7) { xVal += (x * speed); Debug.Log("Right"); }
        else if (y <= 0.17 && y >= (-0.17) && x < (-1.7)) { xVal += (x * speed); Debug.Log("Left"); }
        else if (x >= 1.7 && y <= (-1.7)) { xVal += (x * speed); yVal -= (y * speed); Debug.Log("Up-Right"); }
        else if (y >= 1.7 && x <= (-1.7)) { xVal += (x * speed); yVal -= (y * speed); Debug.Log("Down-Right"); }
        else if (x <= (1.7) && y <= (-1.7)) { xVal += (x * speed); yVal -= (y * speed); Debug.Log("Up-Left"); }
        else if (x >= 1.7 && y >= 1.7) { xVal += (x * speed); yVal -= (y * speed); Debug.Log("Down-Left"); }
        else { 
            
        }
    }

    void KeyboardMovement()
    {
        float constSpeed = 0.3f;
        if (Input.GetKey(KeyCode.W))
        {
            yVal -= (-constSpeed * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            yVal -= (constSpeed * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            xVal += (-constSpeed * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            xVal += (constSpeed * speed);
        }
    }

    ArduinoInfo ConvertArduinoInput()
    {
        if (!arduinoPort.IsOpen) OpenConnection();
        string line = ReadSerialPort();
        //Debug.Log(line);
        if (line == null) return new ArduinoInfo();

        string[] attributes = line.Split(',');
        string x = attributes[0];
        string y = attributes[1];
        int jButton = int.Parse(attributes[2]);
        if (jButton == 0) joystickToggle = true;
        else joystickToggle = false;
        
        int fire = int.Parse(attributes[3]);
        if (fire == 1) fireToggle = true;
        else fireToggle = false;

        int rotatingCounter = int.Parse(attributes[4]);
        int rButton = int.Parse(attributes[5]);
        if (rButton == 1) rotateToggle = true;
        else rotateToggle = false;

        int cardReader = int.Parse(attributes[6]);
        if (cardReader == 1) cardInserted = true;
        else cardInserted = false;

        float _x = float.Parse(x) - 2.5f;
        float _y = float.Parse(y) - 2.5f;

        ArduinoInfo info = new ArduinoInfo();
        info.xVal = _x;
        info.yVal = _y;
        info.jButton = jButton;
        info.fireSensor = fire;
        info.rotateCounter = rotatingCounter;
        info.rButton = rButton;
        info.cardInserted = cardReader; 

        return info;
    }

    private struct ArduinoInfo {
        public float xVal;
        public float yVal;
        public int jButton;
        public int fireSensor;
        public int rotateCounter;
        public int rButton;
        public int cardInserted;
    }

    void OpenConnection()
    {
        isStreaming = true;

        arduinoPort = new SerialPort(port, baudrate);
        arduinoPort.ReadTimeout = 100;
        arduinoPort.Open();
    }

    void CloseConnection()
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
}
