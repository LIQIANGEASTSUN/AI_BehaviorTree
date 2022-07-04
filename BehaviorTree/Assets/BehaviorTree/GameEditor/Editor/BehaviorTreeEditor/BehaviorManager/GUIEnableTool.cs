using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIEnableTool
{

    public static bool Enable
    {
        get { return GUI.enabled; }
        set {
            GUI.enabled = value;
        }
    }

}
