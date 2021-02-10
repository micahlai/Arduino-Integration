using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceToLED : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ArduinoCom.comm.SendSerialMessage("ON");
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ArduinoCom.comm.SendSerialMessage("OF");
        }
    }
}
