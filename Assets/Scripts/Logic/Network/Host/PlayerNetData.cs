using System;
using UnityEngine;

[Serializable]
public class PlayerNetData
{
    public ushort id;
    public string name;
    public Vector3Serializable pos;
    public Vector3Serializable dir;
}
