using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launch : MonoBehaviour {
    
    public static AssetBundle assetBundle;
    
    void Start() {
        AssetBundle ab = assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/common");
        TextAsset hotFixDll = ab.LoadAsset<TextAsset>("HotFix.dll.bytes");
        System.Reflection.Assembly.Load(hotFixDll.bytes);
        Instantiate(ab.LoadAsset<GameObject>("Main.prefab"));
    }
}
