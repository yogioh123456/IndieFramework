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
        //状态数据首先克隆一份还原，然后走追帧逻辑
    }

    // 新玩家登陆进行追帧
    public void ClientConnected(ushort id)
    {
        //克隆数据 追帧，追完后 通知玩家重连成功
        
        // 尝试直接执行所有网络命令
        for (int i = 0; i < cmdList.Count; i++)
        {
            Game.ServerNet.Send(cmdList[i].msg.Message, id);
        }
        
        //通知玩家重连成功
        Message messageToSend = Message.Create(MessageSendMode.reliable, Msg.connected);
        Game.ServerNet.Send(messageToSend, id);
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