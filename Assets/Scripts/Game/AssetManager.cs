using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager {
    public static GameObject LoadAsset(string path) {
        if (Launch.GameMode == GameMode.Develop) {
            return UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Bundles/{path}.prefab");
        } else {
            GameObject go = Resources.Load<GameObject>(path);
            return Object.Instantiate(go);
        }
    }
}
