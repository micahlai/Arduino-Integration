/*
By Anonymous Shrimp
https://youtube.com/channel/UCs2Sz1gPlWAdET5qcLcZCJw
https://github.com/Anonymous-Shrimp 
*/

using System;
using System.IO.Ports;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class ArduinoCom : MonoBehaviour
{

    public String[] ports;
    SerialPort port;
    bool isConnected = false;

    public Dropdown dropdownList;
    public Text buttonText;
    public Text errorText;

    public static ArduinoCom comm;

    public InputString inputString;

    public SerialSettings serialSettings;

    private void Awake()
    {
        comm = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ports = SerialPort.GetPortNames();
        errorText.text = "";
        GetCommOptions();
    }

    // Update is called once per frame
    void Update()
    {
        ports = SerialPort.GetPortNames();
        if (isConnected)
        {
            string s = port.ReadLine();
            print(s);
            inputString.Invoke(s);
        }
    }


    void ConnectToArduino()
    {
        try
        {
            isConnected = true;
            string selectedPort = ports[dropdownList.value];
            port = new SerialPort(selectedPort, serialSettings.baudRate, serialSettings.portParity, serialSettings.dataBits, serialSettings.stopBits);
            port.Open();
            errorText.text = "";
            buttonText.text = "Disconnect";
        }
        catch
        {
            Debug.LogError("Connection Error");
            errorText.text = "Connection Error";
        }
    }
    private void disconnectFromArduino()
    {
        try
        {
            isConnected = false;
            port.Write("#STOP~");
            Console.WriteLine("#STOP~");
            port.Close();
            buttonText.text = "Connect";
        }
        catch
        {
            Console.WriteLine("Connection Error");
        }
    }

    public void ConnectButton()
    {
        if (!isConnected)
        {
            ConnectToArduino();
        }
        else
        {
            disconnectFromArduino();
        }
    }

    public void GetCommOptions()
    {
        List<string> options = new List<string>();
        foreach (var option in ports)
        {
            options.Add(option);
        }
        dropdownList.ClearOptions();
        dropdownList.AddOptions(options);
    }

    public void SendSerialMessage(string message)
    {
        if (isConnected)
        {
            string mess = "#" + message;
            Debug.Log(mess + "~");
            port.Write(mess + "~");
        }
    }
    [Serializable]
    public class InputString : UnityEvent<string> { }

    [Serializable]
    public class SerialSettings
    {
        public string portName = "port";
        public int baudRate = 38400;
        public Parity portParity = Parity.None;
        public int dataBits = 8;
        public StopBits stopBits = StopBits.One;
    }
}
