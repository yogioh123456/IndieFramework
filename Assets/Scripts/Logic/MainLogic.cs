﻿public class MainLogic {
    
    /// <summary>
    /// 游戏主逻辑入口
    /// </summary>
    public void Init()
    {
        Game.Asset.LoadAsset("Prefabs/TestObj");
        Game.UI.OpenUIPanel<UI_Menu>();
    }
}
