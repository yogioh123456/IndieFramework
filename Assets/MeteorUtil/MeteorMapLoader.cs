using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MeteorMapLoader : MonoBehaviour {
    
    public string mapName;
    SortedDictionary<string, DesFile> DesFile = new SortedDictionary<string, global::DesFile>();
    SortedDictionary<string, GMBFile> GmbFile = new SortedDictionary<string, GMBFile>();
    
    //despath 参数 sn04
    public void LoadDesMap()
    {
        string despath = mapName;
        DesFile des = DesLoad(despath);
        GMBFile gmb = GMBLoad(despath);
        if (des == null)
        {
            Debug.LogError("缺少des文件");
            return;
        }
        
        if (gmb == null)
        {
            Debug.LogError("缺少gmb文件");
            return;
        }
        
        bool generateFile = true;
        if (!System.IO.Directory.Exists("Assets/Materials/" + despath + "/Resources/"))
            System.IO.Directory.CreateDirectory("Assets/Materials/" + despath + "/Resources/");

        //Material[] mat = new Material[gmb.ShaderCount];
        //for (int x = 0; x < gmb.ShaderCount; x++)
        //{
        //    //mat[x] = new Material
        //    //gmb.shader[x].
        //}
        string shaders = "Custom/MeteorVertexL";
        Material[] mat = new Material[gmb.TexturesCount];
        bool exist;
        for (int x = 0; x < gmb.TexturesCount; x++)
        {
            exist = false;
            string materialName = string.Format("{0:D2}_{1:D2}", despath, x);
            mat[x] = Resources.Load<Material>(materialName);
            if (mat[x] == null)
                mat[x] = new Material(Shader.Find(shaders));
            else
                exist = true;
            string tex = gmb.TexturesNames[x];
            int del = tex.LastIndexOf('.');
            if (del != -1)
                tex = tex.Substring(0, del);
            Texture texture = Resources.Load<Texture>(tex);
            //这里的贴图可能是一个ifl文件，这种先不考虑，手动修改
            if (texture == null)
                Debug.LogError("texture miss:" + tex);
            mat[x].SetTexture("_MainTex", texture);
            mat[x].name = materialName;
            if (generateFile && !exist)
            {
                //AssetDatabase.CreateAsset(mat[x], "Assets/Materials/" + despath + "/Resources/" + mat[x].name + ".mat");
                //AssetDatabase.Refresh();
            }
        }

        while (true)
        {
            for (int i = 0; i < Mathf.Min(des.SceneItems.Count, gmb.SceneObjectsCount); i++)
            {
                //if (des.SceneItems[i].name != "Object23")
                //    continue;

                GameObject objMesh = new GameObject();
                objMesh.name = des.SceneItems[i].name;
                objMesh.transform.localRotation = Quaternion.identity;
                objMesh.transform.localPosition = Vector3.zero;
                objMesh.transform.localScale = Vector3.one;
                //bool realObject = false;//是不是正常物体，虚拟体无需设置材质球之类
                for (int j = 0; j < gmb.SceneObjectsCount; j++)
                {
                    if (gmb.mesh[j].name == objMesh.name && j == i)
                    {

                        //realObject = true;
                        Mesh w = new Mesh();
                        //前者子网格编号，后者 索引缓冲区
                        SortedDictionary<int, List<int>> tr = new SortedDictionary<int, List<int>>();
                        List<Vector3> ve = new List<Vector3>();
                        List<Vector2> uv = new List<Vector2>();
                        List<Vector3> nor = new List<Vector3>();
                        List<Color> col = new List<Color>();
                        for (int k = 0; k < gmb.mesh[j].faces.Count; k++)
                        {
                            int key = gmb.mesh[j].faces[k].material;
                            if (tr.ContainsKey(key))
                            {
                                tr[key].Add(gmb.mesh[j].faces[k].triangle[0]);
                                tr[key].Add(gmb.mesh[j].faces[k].triangle[1]);
                                tr[key].Add(gmb.mesh[j].faces[k].triangle[2]);
                            }
                            else
                            {
                                tr.Add(key, new List<int>());
                                tr[key].Add(gmb.mesh[j].faces[k].triangle[0]);
                                tr[key].Add(gmb.mesh[j].faces[k].triangle[1]);
                                tr[key].Add(gmb.mesh[j].faces[k].triangle[2]);
                            }

                        }
                        for (int k = 0; k < gmb.mesh[j].vertices.Count; k++)
                        {
                            Vector3 vecLocalToWorld = des.SceneItems[i].pos;
                            Quaternion quatToWorld = des.SceneItems[i].quat;

                            //                            Object ian1633
                            //{
                            //                                Position: -894.278 - 2506.653 51.167
                            //  Quaternion: 0.000 0.000 0.000 1.000
                            //  TextureAnimation: 0 0.000 0.000
                            //  Custom:
                            //                                {
                            //                                }
                            //                            }
                            //                            */
                            //                           Vector3 vecLocalToWorld = new Vector3(-894.278f, -2506.653f, 51.167f);
                            //                            Quaternion quatToWorld = new Quaternion(0f, 0f, 0f, 1f);

                            //                            Vector3 vecWorldToLocal = new Vector3(-vecLocalToWorld.x, -vecLocalToWorld.z, -vecLocalToWorld.y);
                            //                            Quaternion quat = new Quaternion(-quatToWorld.y, -quatToWorld.w, -quatToWorld.z, quatToWorld.x);
                            Vector3 vecWorldToLocal = new Vector3(-vecLocalToWorld.x, -vecLocalToWorld.y, -vecLocalToWorld.z);
                            Vector3 vec = gmb.mesh[j].vertices[k].pos + vecWorldToLocal;
                            vec = Quaternion.Inverse(quatToWorld) * vec;
                            //这个是世界坐标，要变换到自身坐标系来。
                            //ve.Add(gmb.mesh[j].vertices[k].pos);
                            ve.Add(vec);
                            uv.Add(gmb.mesh[j].vertices[k].uv);
                            col.Add(gmb.mesh[j].vertices[k].color);
                            nor.Add(gmb.mesh[j].vertices[k].normal);
                        }
                        w.SetVertices(ve);
                        w.uv = uv.ToArray();
                        w.subMeshCount = tr.Count;
                        int ss = 0;
                        //查看这个网格使用了哪些材质，然后把这几个材质取出来
                        List<Material> targetMat = new List<Material>();
                        foreach (var each in tr)
                        {
                            w.SetIndices(each.Value.ToArray(), MeshTopology.Triangles, ss++);
                            if (each.Key >= 0 && each.Key < gmb.shader.Length)
                            {
                                int materialIndex = gmb.shader[each.Key].TextureArg0;
                                if (materialIndex >= 0 && materialIndex < mat.Length)
                                    targetMat.Add(mat[materialIndex]);
                                else
                                {
                                    //即使没有贴图，也存在材质
                                    string materialName = string.Format("{0:D2}_{1:D2}", despath, materialIndex);
                                    Material defaults = Resources.Load<Material>(materialName);
                                    if (defaults == null)
                                    {
                                        defaults = new Material(Shader.Find("Custom/MeteorVertexL"));
                                        defaults.name = materialName;
                                        if (generateFile)
                                        {
                                            //AssetDatabase.CreateAsset(defaults, "Assets/Materials/" + despath + "/Resources/" + defaults.name + ".mat");
                                            //AssetDatabase.Refresh();
                                        }
                                    }
                                    targetMat.Add(defaults);
                                }
                            }
                        }
                        MeshRenderer mr = objMesh.AddComponent<MeshRenderer>();
                        MeshFilter mf = objMesh.AddComponent<MeshFilter>();
                        mf.sharedMesh = w;
                        mf.sharedMesh.colors = col.ToArray();
                        mf.sharedMesh.normals = nor.ToArray();
                        mf.sharedMesh.RecalculateBounds();
                        mf.sharedMesh.RecalculateNormals();
                        mr.materials = targetMat.ToArray();
                        string vis = "";
                        if (des.SceneItems[i].ContainsKey("visible", out vis))
                        {
                            if (vis == "0")
                            {
                                mr.enabled = false;
                                mr.gameObject.AddComponent<MeshCollider>();
                            }
                        }

                        /*
                         * animation=sin
    deformsize=5
    deformfreq=0.25
    deformrange=5.0
                         */
                        string block = "";
                        if (des.SceneItems[i].ContainsKey("blockplayer", out block))
                        {
                            if (block == "no")
                            {
                            }
                            else
                                mr.gameObject.AddComponent<MeshCollider>();
                        }
                        else
                        {
                            mr.gameObject.AddComponent<MeshCollider>();
                        }
                        break;
                    }
                }

                objMesh.transform.SetParent(transform);
                objMesh.transform.localRotation = des.SceneItems[i].quat;
                objMesh.transform.localPosition = des.SceneItems[i].pos;
                //if (objMesh.GetComponent<MeshFilter>() != null && generateFile)
                //{
                //    AssetDatabase.CreateAsset(objMesh.GetComponent<MeshFilter>().sharedMesh, "Assets/Materials/" + despath + "/" + des.SceneItems[i].name + ".asset");
                //    AssetDatabase.Refresh();
                //}
                //yield return 0;
            }
            return;
            //yield break;
        }
    }
    
    public DesFile DesLoad(string file)
    {
        string file_no_ext = file;
        file += ".des";
        if (DesFile.ContainsKey(file))
            return DesFile[file];

        DesFile f = null;
        /*
        if (CombatData.Ins.Chapter != null) {
            string path = CombatData.Ins.Chapter.GetResPath(FileExt.Des, file_no_ext);
            if (!string.IsNullOrEmpty(path)) {
                f = new DesFile();
                f.Load(path);
                DesFile[file] = f;
                return f;
            }
        }
        */

        f = new DesFile();
        f.Load(file);
        DesFile[file] = f;
        return f;
    }
    
    public GMBFile GMBLoad(string file)
    {
        string file_no_ext = file;
        file += ".gmb";
        if (GmbFile.ContainsKey(file))
            return GmbFile[file];
        GMBFile gmb = null;
        /*
        if (CombatData.Ins.Chapter != null) {
            string path = CombatData.Ins.Chapter.GetResPath(FileExt.Gmc, file_no_ext);
            if (!string.IsNullOrEmpty(path)) {
                gmb = new GMBFile();
                gmb = gmb.Load(path);
                if (gmb != null) {
                    GmbFile.Add(file, gmb);
                    return gmb;
                }
            }
        }
        */
        gmb = new GMBFile();
        GMBFile gmbOK = gmb.Load(file);
        if (gmbOK != null)
            GmbFile.Add(file, gmbOK);
        return gmbOK;
    }
}
