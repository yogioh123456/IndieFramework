using UnityEngine;

[GameComp]
public class MainLogic {
    
    /// <summary>
    /// 游戏主逻辑入口
    /// </summary>
    public void Init()
    {
        //流星测试项目
        Object.Instantiate(AssetManager.LoadAsset("Prefabs/TestObj"));
        //Game.UI.OpenUIPanel<UI_Menu>();
        
        //独立服务器测试项目
        Game.UI.OpenUIPanel<UI_Login>();
    }
}
