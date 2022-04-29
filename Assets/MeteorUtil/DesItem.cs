using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DesItem
{
    public string name;
    public Vector3 pos = new Vector3(0, 0, 0);
    public Quaternion quat = new Quaternion(0, 0, 0, 0);
    public bool useTextAnimation; //是否使用uv动画
    public Vector2 textAnimation = new Vector2(0, 0);//uv参数
    public List<string> custom = new List<string>();
    public bool ContainsKey(string key, out string value)
    {
        for (int i = 0; i < custom.Count; i++)
        {
            string []kv = custom[i].Split(new char[] { '='}, System.StringSplitOptions.RemoveEmptyEntries);
            if (kv.Length == 2)
            {
                string k = kv[0].Trim(new char[] { ' '});
                string v = kv[1].Trim(new char[] { ' ' });
                if (k == key)
                {
                    string[] varray = v.Split(new char[] { '\"' }, System.StringSplitOptions.RemoveEmptyEntries);
                    value = varray[0].Trim(new char[] { ' '});
                    return true;
                }
            }
        }
        value = "";
        return false;
    }
    public void ReadObjAttr(StreamReader asset)
    {
        bool readLeftToken = false;
        int leftTokenStack = 0;
        while (!asset.EndOfStream)
        {
            string obj = asset.ReadLine().Trim();
            string[] keyValue = obj.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (keyValue.Length == 0)
                continue;
            if (keyValue[0] == "#")
                continue;
            if (keyValue[0] == "{")
            {
                readLeftToken = true;
                leftTokenStack++;
                continue;
            }
            //Z UP TO Y UP x轴z轴取反
            if (keyValue[0] == "Position:" && readLeftToken && leftTokenStack == 1)
            {
                pos.x = Mathf.FloorToInt(10000 * float.Parse(keyValue[1])) / 10000.0f;
                pos.z = Mathf.FloorToInt(10000 * float.Parse(keyValue[2])) / 10000.0f;
                pos.y = Mathf.FloorToInt(10000 * float.Parse(keyValue[3])) / 10000.0f;
            }
            if (keyValue[0] == "Quaternion:" && readLeftToken && leftTokenStack == 1)
            {
                quat.w = Mathf.FloorToInt(10000 * float.Parse(keyValue[1])) / 10000.0f;
                quat.x = -Mathf.FloorToInt(10000 * float.Parse(keyValue[2])) / 10000.0f;
                quat.y = -Mathf.FloorToInt(10000 * float.Parse(keyValue[4])) / 10000.0f;
                quat.z = -Mathf.FloorToInt(10000 * float.Parse(keyValue[3])) / 10000.0f;
            }
            if (keyValue[0] == "TextureAnimation:" && readLeftToken && leftTokenStack == 1)
            {
                useTextAnimation = (int.Parse(keyValue[1]) == 1);
                textAnimation.x = float.Parse(keyValue[2]);
                textAnimation.y = float.Parse(keyValue[3]);
            }
            if (keyValue[0] == "Custom:" && readLeftToken && leftTokenStack == 1)
            {
                while (!asset.EndOfStream)
                {
                    obj = asset.ReadLine().Trim();
                    if (obj == "{")
                    {
                        leftTokenStack++;
                        continue;
                    }
                    if (obj == "}")
                    {
                        leftTokenStack--;
                        if (leftTokenStack == 1)
                            break;
                        continue;
                    }
                    if (obj == "#" || string.IsNullOrEmpty(obj))
                        continue;
                    custom.Add(obj);
                }
            }
            if (keyValue[0] == "}")
            {
                leftTokenStack--;
                if (leftTokenStack == 0)
                    break;
            }
        }
    }
}