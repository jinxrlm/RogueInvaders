using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/motionVars")]
public class motionVars : ScriptableObject
{
    public Vector3 curDirection = Vector3.left;
    public bool switchedRow = false;
    public bool isGoingLeft = true;
    public bool lockRowShift = false;
}
