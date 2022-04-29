using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DesFile
{
    public List<DesItem> SceneItems = new List<DesItem>();
    public int DummyCount;
    public int ObjectCount;
    public void Load(string strFile)
    {
        SceneItems.Clear();
        if (!string.IsNullOrEmpty(strFile))
        {
            byte[] body = null;
            if (File.Exists(strFile))
                body = File.ReadAllBytes(strFile);
            else 
            {
                TextAsset assetDes = Resources.Load<TextAsset>(strFile);
                body = assetDes != null ? assetDes.bytes: null;
            }

            if (body == null)
                return;
            MemoryStream ms = new MemoryStream(body);
            StreamReader asset = new StreamReader(ms);
            while (!asset.EndOfStream)
            {
                string obj = asset.ReadLine();
                string[] keyValue = obj.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                if (keyValue.Length == 0)
                {
                    Debug.LogError("err：" + obj);
                    continue;
                }
                if (keyValue[0] == "#")
                    continue;
                if (keyValue[0] == "SceneObjects" && keyValue[2] == "DummeyObjects")
                {
                    ObjectCount = int.Parse(keyValue[1]);
                    DummyCount = int.Parse(keyValue[3]);
                    continue;
                }

                if (keyValue[0] == "Object")
                {
                    DesItem attr = new DesItem();
                    attr.name = keyValue[1];
                    attr.ReadObjAttr(asset);
                    SceneItems.Add(attr);
                }
            }
            //Debug.LogFormat("共有{0}个对象{1}个虚拟物", ObjectCount, DummyCount);
            if (SceneItems.Count != ObjectCount + DummyCount)
            {
                Debug.Log(string.Format("物品数量不对, 场景物件数量{0}-对象{1}-拷贝对象{2}", SceneItems.Count, ObjectCount, DummyCount));
                return;
            }
        }
    }
}