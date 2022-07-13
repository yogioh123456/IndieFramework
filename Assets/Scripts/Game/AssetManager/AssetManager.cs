using System.Collections.Generic;
using UnityEngine;

public class AssetManager 
{
    private static int maxCount = 50;
    private static GameObject assetPoolRoot;
    private static Dictionary<string, GameObject> loadDic = new Dictionary<string, GameObject>();
    private static Dictionary<string, Queue<GameObject>> assetsQueueDic = new Dictionary<string, Queue<GameObject>>();

    public static void Init()
    {
        assetPoolRoot = new GameObject
        {
            name = "_AssetPoolRoot"
        };
    }
    
    private static GameObject LoadAssetData(string path) {
        if (Launch.GameMode == GameMode.Develop) {
            // 将Prefab实例化加载出来
            return UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Bundles/{path}.prefab");
        } else {
            return Object.Instantiate(Launch.assetBundle.LoadAsset<GameObject>($"{path}.prefab"));
        }
    }
    
    public static GameObject LoadAsset(string path, Vector3 pos)
    {
        GameObject go = LoadAsset(path);
        go.transform.position = pos;
        return go;
    }

    /// <summary>
    /// 加载一个资源并且实例化，并且使用对象池
    /// </summary>
    public static GameObject LoadAsset(string path, bool isActive = true)
    {
        string key = path;
        Queue<GameObject> assetQueuePool;
        if (assetsQueueDic.ContainsKey(key))
        {
            assetQueuePool = assetsQueueDic[key];
        }
        else
        {
            assetQueuePool = new Queue<GameObject>();
            assetsQueueDic.Add(key, assetQueuePool);
        }

        GameObject go;
        if (assetQueuePool.Count > 0)
        {
            go = assetQueuePool.Dequeue();
            if (isActive)
            {
                go.SetActive(true);
            }
        }
        else 
        {
            GameObject loadGo;
            if (loadDic.ContainsKey(path)) {
                loadGo = loadDic[path];
            } else {
                loadGo = LoadAssetData(path);
                loadDic.Add(path, loadGo);
            }
            if (loadGo == null)
            {
                Debug.LogError("加载失败" + path);
            }
            //go = Object.Instantiate(loadGo);
            go = loadGo;
        }
        go.name = key;
        go.transform.parent = null;
        //调用资源重置接口
        if (go.GetComponent<IRes>() != null)
        {
            foreach (var one in go.GetComponents<IRes>())
            {
                one.ResReset();
            }
        }
        //放到活动场景中
        //SceneManager.MoveGameObjectToScene(go, SceneManager.GetActiveScene());
        //go.transform.SetZero();
        return go;
    }
    
    public static void UnLoadAsset(GameObject go)
    {
        string _key = go.name;
        if (!assetsQueueDic.ContainsKey(_key))
        {
            Debug.LogError("卸载了一个没有用资源管理器加载的物体,或者物体本身命名被修改,可以在这之前改回自己的名字");
        }

        if (assetsQueueDic[_key].Contains(go))
        {
            Debug.LogError("已经在池子里面了"+_key);
            return;
        }
        if (assetsQueueDic[_key].Count >= maxCount)
        {
            //直接销毁
            Object.Destroy(go);
        }
        else
        {
            //入对象池
            go.SetActive(false);
            go.transform.parent = assetPoolRoot.transform;
            assetsQueueDic[_key].Enqueue(go);
        }
    }
}

public static class AssetManagerHelper {
    public static void UnLoadAsset(this GameObject go) {
        UnLoadAsset(go);
    }
}