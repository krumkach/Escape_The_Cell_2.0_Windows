using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolButtonScript
{
    public int[] pinChanges = new int[3];
    public int messageIndx = 0;

    public LocalizationManager localization;

    public ToolButtonScript()
    {
        pinChanges[0] = 0;
        pinChanges[1] = 0;
        pinChanges[2] = 0;
    }

    public ToolButtonScript(int pin1Change, int pin2Change, int pin3Change)
    {
        pinChanges[0] = pin1Change;
        pinChanges[1] = pin2Change;
        pinChanges[2] = pin3Change;
    }

    public bool ChangePins(int[] pins)
    {
        bool isInRange = true;
        for (int indx = 0; indx < pins.Length; indx++)
        {
            int result = pins[indx] + pinChanges[indx];
            if (result > 10 || result < 0)
            {
                isInRange = false;
                messageIndx = 1;
                break;
            }
            else
            {
                pins[indx] = result;
                messageIndx = 2;
            }
        }

        return isInRange;
    }
}
