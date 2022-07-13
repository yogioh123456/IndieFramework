using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode {
    Develop,
    Release
}

public class Launch : MonoBehaviour {

    [SerializeField] 
    private GameMode gameMode;
    
    public static GameMode GameMode;
    public static AssetBundle assetBundle;
    
    void Start() {
#if !UNITY_EDITOR
        gameMode = GameMode.Release
#endif
        GameMode = gameMode;
        AssetManager.Init();
        
        if (gameMode == GameMode.Develop) {
            Instantiate(AssetManager.LoadAsset("Prefabs/Main"));
        } else {
            AssetBundle ab = assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/common");
            TextAsset hotFixDll = ab.LoadAsset<TextAsset>("HotFix.dll.bytes");
            System.Reflection.Assembly.Load(hotFixDll.bytes);
            Instantiate(ab.LoadAsset<GameObject>("Main.prefab"));
        }
    }
}
