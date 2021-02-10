using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    [Range(-512,512)]
    public int xVal = 0;
    [Range(-512, 512)]
    public int yVal = 0;
    // Start is called before the first frame update
    void Start()
    {
        ArduinoCom.comm.inputString.AddListener(changeValues);
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void changeValues(string InString)
    {
        string[] outputString = InString.Split('|');
        int.TryParse(outputString[0], out xVal);
        int.TryParse(outputString[1], out yVal);

    }
}
