using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MeteorMapLoader))]
public class MapLoaderInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MeteorMapLoader myTarget = (MeteorMapLoader)target;
        if (GUILayout.Button("加载地图"))
        {
            myTarget.LoadDesMap();
        }
    }
}
