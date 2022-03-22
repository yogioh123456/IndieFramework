using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class CreateUGUICScriptEditor : MonoBehaviour
{
    //panel
    [MenuItem("Assets/创建UGUI脚本Simple", priority = -1)]
    static void CreateUIViewCtrl()
    {
        CreateUIView();
        CreateUIControl();
    }
    
    static void CreateUIView()
    {
        CreateUIScript("_View.cs", AutoGetPanelComp);
    }
    
    static void CreateUIControl()
    {
        CreateUIScript(".cs", AutoGenControlScript);
    }
    
    
    delegate string AutonGenScript(string selectName);
    static void CreateUIScript(string csSuffix, AutonGenScript ac){

        
        string[] guidArray = Selection.assetGUIDs;
        foreach (var item in guidArray)
        {
            //打印路径
            string selecetFloder = AssetDatabase.GUIDToAssetPath(item);
            //Debug.Log(selecetFloder);
            //打印名字
            string selectName = Path.GetFileName(selecetFloder);
            //Debug.Log(selectName);
            
            {
                //------------------------写文件View-------------------------//
                File.WriteAllText(selecetFloder + "/" + selectName + csSuffix, ac(selectName), new UTF8Encoding(false));
                //刷新
                AssetDatabase.Refresh();
            }
        }
        Debug.Log("UI脚本创建完成");
    }

    
    //-------------------生成Panel-View层脚本-----------------------
    static string AutoGetPanelComp(string className){
        string content = "";
        string packName = "";

        string head = string.Format(
            @"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI View层 {0}
/// 类型 Panel
/// 注意：本段代码由系统自动生成
/// </summary>
public class {0} : UGUIView
{{
    //---------------字段---------------
    
}}", className+"_View");

        content += head;
        return content;
    }



    //-------------------生成Panel-ctrl层脚本------------------------
    static string AutoGenControlScript(string className)
    {
        string content = "";
        string head = string.Format(@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI Ctrl层: {0}
public class {0} : UGUICtrl
{{
    public {1} selfView;

    public {0}()
    {{
        OnCreate(ref selfView,""UGUI/{2}"",""{0}"");
    }}

    /// <summary>
    /// 按钮添加事件
    /// </summary>
    protected override void ButtonAddClick()
    {{
        //------------------按钮添加事件-----------------
        
    }}

    /// <summary>
    /// 打开面板
    /// </summary>
    public override void OpenPanel(object data)
    {{
        base.OpenPanel(data);
        
    }}
}}", className, className+"_View", GetPrefabName(className));
        content += head;
        return content;
    }
    
    //自动生成按钮事件
    static string GetPrefabName(string _name)
    {
        return _name.Substring(0,4).ToLower() + _name.Substring(4,_name.Length - 4);
    }

}
