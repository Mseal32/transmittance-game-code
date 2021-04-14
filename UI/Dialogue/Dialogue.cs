using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

    //this class is to be used with the other "Dialogue" scripts

    //takes in an array of strings that are typed in the inspector
    [TextArea(3, 20)]
    public string[] sentences;
}
