using UnityEngine;

namespace HotFix_Project
{
    [GameComp]
    public class MainLogic
    {

        /// <summary>
        /// 游戏主逻辑入口
        /// </summary>
        public void Init()
        {
            Debug.Log("启动");
            //流星测试项目
            //Game.Asset.LoadAsset("Prefabs/TestObj");
            //Game.UI.OpenUIPanel<UI_Menu>();

            //独立服务器测试项目
            //Game.UI.OpenUIPanel<UI_Login>();
        }
    }
}