using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AssetBundleHelp
{
    [MenuItem("工具/拷贝Dll和Excel")]
    public static void CopyHotFixDll() {
        CopyExcel("config.xlsx");
        CopyDll("HotFix.dll");
        Debug.Log("拷贝Dll和Excel 成功");
        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
    }

    [MenuItem("工具/BuildBundles/ActiveBuildTarget")]
    public static void BuildSeneAssetBundleActiveBuildTarget()
    {
        var target = EditorUserBuildSettings.activeBuildTarget;
        BuildAssetBundles(target);
    }
    
    private static void BuildAssetBundles(BuildTarget target)
    {
        List<string> notSceneAssets = new List<string>();
        
        notSceneAssets.Add($"{Application.dataPath}/Bundles/Dll/HotFix.dll.bytes");
        notSceneAssets.Add($"{Application.dataPath}/Bundles/Prefabs/Main.prefab");
        notSceneAssets.Add($"{Application.dataPath}/Bundles/Excel/config.xlsx.bytes");

        List<AssetBundleBuild> abs = new List<AssetBundleBuild>();
        AssetBundleBuild notSceneAb = new AssetBundleBuild
        {
            assetBundleName = "common",
            assetNames = notSceneAssets.Select(s => ToReleateAssetPath(s)).ToArray(),
        };
        abs.Add(notSceneAb);
        
        string outputDir = $"{Application.streamingAssetsPath}";
        BuildPipeline.BuildAssetBundles(outputDir, abs.ToArray(), BuildAssetBundleOptions.None, target);
        AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        Debug.Log("AB打包成功！");
    }

    private static void CopyExcel(string name) {
        string source = $"{Application.dataPath}/.Excel/{name}";
        string des = $"{Application.dataPath}/Bundles/Excel/{name}.bytes";
        File.Copy(source, des, true);
    }
    
    private static void CopyDll(string dll) {
        DirectoryInfo d = new DirectoryInfo(Application.dataPath + "\\..");
        string dllPath = $"{d.FullName}/Library/ScriptAssemblies/{dll}";
        string dllBytesPath = $"{Application.dataPath}/Bundles/Dll/{dll}.bytes";
        //将dll目录拷贝过去
        File.Copy(dllPath, dllBytesPath, true);
    }

    private static void CreateDirIfNotExists(string dirName)
    {
        if (!Directory.Exists(dirName))
        {
            Directory.CreateDirectory(dirName);
        }
    }
    
    public static string ToReleateAssetPath(string s) {
        // 路径只保留从Asset开头
        string x = s.Substring(s.IndexOf("Assets/"));
        return x;
    }
}
