using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class CreateUGUICScriptEditor : MonoBehaviour {
    private static string uiFieldStr;
    private static string uiMethodStr;
    private static Dictionary<string, string> uiNameDic = new Dictionary<string, string>() {
        {"text_","Text"},
        {"btn_","Button"},
        {"tog_","Toggle"},
        {"input_","InputField"},
        {"img_","Image"},
    };

    //panel
    [MenuItem("Assets/创建UGUI脚本Simple", priority = -1)]
    private static void CreateUIViewCtrl()
    {
        CreateUIView();
        CreateUIControl();
    }
    
    /// <summary>
    /// 自动创建View脚本并且绑定
    /// </summary>
    private static void CreateUIView() {
        var selectUI = Selection.gameObjects[0].transform;
        CheckUIObject(selectUI,"", true);
        CreateUIScript("_View.cs", AutoGetPanelComp);
    }

    private static void CheckUIObject(Transform selectUI, string findStr, bool b) {
        for (int i = 0; i < selectUI.childCount; i++) {
            if (b) {
                findStr = "";
            }
            //判断前缀，检测命名规则
            findStr += $".GetChild({i})";
            CheckHeadName(selectUI.GetChild(i), findStr);
            if (selectUI.GetChild(i).childCount > 0) {
                CheckUIObject(selectUI.GetChild(i), findStr, false);
            }
        }
    }

    private static void CheckHeadName(Transform ui, string findStr) {
        foreach (var one in uiNameDic) {
            if (ui.name.StartsWith(one.Key)) {
                uiFieldStr += $"    public {one.Value} {ui.name};\n";
                findStr = $"{ui.name} = trans{findStr}";
                uiMethodStr += $"        {findStr}.GetComponent<{one.Value}>();\n";
            }
        }
    }
    
    private static void CreateUIControl()
    {
        CreateUIScript(".cs", AutoGenControlScript);
    }
    
    
    private delegate string AutonGenScript(string selectName);
    private static void CreateUIScript(string csSuffix, AutonGenScript ac) {
        string[] guidArray = Selection.assetGUIDs;
        foreach (var item in guidArray)
        {
            //打印路径
            string selecetFloder = AssetDatabase.GUIDToAssetPath(item);
            //打印名字
            string selectName = Path.GetFileName(selecetFloder);
            
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
{1}
    public void Init(Transform trans) {{
{2}
    }}
}}", className+"_View", uiFieldStr, uiMethodStr);

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
        Transform uiTrans = OnCreate(ref selfView,""UI/Prefabs/{2}"",""{0}"");
        selfView.Init(uiTrans);
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
