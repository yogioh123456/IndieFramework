using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager {
    public GameObject LoadAsset(string path) {
        GameObject go = Resources.Load<GameObject>(path);
        return Object.Instantiate(go);
    }
}
