using System.Collections.Generic;

/// <summary>
/// 服务器同步管理
/// </summary>
public class ServerSyncManager {
    public List<object> cmdList = new List<object>();

    // 接受发送的命令
    public void SendCmd() {

        ExcuteState();
    }

    // 根据命令改变游戏的状态
    public void ExcuteState() {
        
    }
}

