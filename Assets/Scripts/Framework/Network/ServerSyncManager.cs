using System.Collections.Generic;
using RiptideNetworking;

/// <summary>
/// 服务器同步管理
/// </summary>
public class ServerSyncManager {
    public List<NetworkTimeMessage> cmdList = new List<NetworkTimeMessage>();

    // 接受发送的命令
    public void AddCmd(object data, ServerMessageReceivedEventArgs msg, uint nowTick) {
        NetworkTimeMessage networkTimeMessage = new NetworkTimeMessage(data, msg, nowTick);
        cmdList.Add(networkTimeMessage);
        ExcuteState();
    }

    // 根据命令改变游戏的状态
    public void ExcuteState() {
        //战报  建议通过反射获取Server代码标签生成字典然后执行
        
        //断线重连 采用 状态+追帧  的方式
        
    }
}

public class NetworkTimeMessage {
    public NetworkTimeMessage(object data, ServerMessageReceivedEventArgs msg, uint nowTick) {
        this.nowTick = nowTick;
        this.data = data;
        this.msg = msg;
    }

    public uint nowTick;
    public object data;
    public ServerMessageReceivedEventArgs msg;
}