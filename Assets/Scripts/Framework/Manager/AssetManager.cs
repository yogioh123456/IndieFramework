using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[GameComp(-1)]
public class AssetManager {
    public GameObject LoadAsset(string path) {
        GameObject go = Resources.Load<GameObject>(path);
        return Object.Instantiate(go);
    }
}
