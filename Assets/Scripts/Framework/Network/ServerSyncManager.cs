using System;
using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;

/// <summary>
/// 服务器同步管理
/// </summary>
public class ServerSyncManager : IFixedUpdate {
    public List<NetworkTimeMessage> cmdList = new List<NetworkTimeMessage>();
    private Queue<NetworkTimeMessage> copyCmd = new Queue<NetworkTimeMessage>();
    

    // 接受发送的命令
    public void AddCmd(object data, ServerMessageReceivedEventArgs msg, uint nowTick) {
        //NetworkTimeMessage networkTimeMessage = new NetworkTimeMessage(data, msg, nowTick);
        //cmdList.Add(networkTimeMessage);
        //ExcuteState();
    }

    public void AddCmdMsg(uint nowTick, Enum id) {
        Message message = Message.Create(MessageSendMode.reliable, id, shouldAutoRelay: true);
        NetworkTimeMessage networkTimeMessage = new NetworkTimeMessage(message, nowTick);
        cmdList.Add(networkTimeMessage);
    }
    
    public void AddCmdMsg<T>(uint nowTick, Enum id, T t) {
        Message message = Message.Create(MessageSendMode.reliable, id, shouldAutoRelay: true);
        MessageAdd(message, t);
        NetworkTimeMessage networkTimeMessage = new NetworkTimeMessage(message, nowTick);
        cmdList.Add(networkTimeMessage);
    }
    
    public void AddCmdMsg<T,K,V>(uint nowTick, Enum id, T t, K k, V v) {
        Message message = Message.Create(MessageSendMode.reliable, id, shouldAutoRelay: true);
        MessageAdd(message, t);
        MessageAdd(message, k);
        MessageAdd(message, v);
        NetworkTimeMessage networkTimeMessage = new NetworkTimeMessage(message, nowTick);
        cmdList.Add(networkTimeMessage);
    }

    private delegate void MessageDelegate<Message, T>(Message msg, T t);
    private void MessageAdd<T>(Message message, T t) {
        if (typeof(T) == typeof(int)) {
            MessageDelegate<Message, int> add = AddInt;
            (add as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(float)) {
            MessageDelegate<Message, float> add = AddFloat;
            (add as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(bool)) {
            MessageDelegate<Message, bool> add = AddBool;
            (add as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(string)) {
            MessageDelegate<Message, string> add = AddString;
            (add as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(double)) {
            MessageDelegate<Message, double> add = AddDouble;
            (add as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(long)) {
            MessageDelegate<Message, long> add = AddLong;
            (add as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(short)) {
            MessageDelegate<Message, short> add = AddShort;
            (add as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(ushort)) {
            MessageDelegate<Message, ushort> add = AddUShort;
            (add as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(byte)) {
            MessageDelegate<Message, byte> add = AddByte;
            (add as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(Quaternion)) {
            MessageDelegate<Message, Quaternion> add = AddQuaternion;
            (add as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(Vector3)) {
            MessageDelegate<Message, Vector3> add = AddVector3;
            (add as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(Vector2)) {
            MessageDelegate<Message, Vector2> add = AddVector2;
            (add as MessageDelegate<Message, T>)?.Invoke(message, t);
        }
    }
    #region AddMsg
    private void AddUShort(Message message, ushort data) {
        message.AddUShort(data);
    }
    private void AddShort(Message message, short data) {
        message.AddShort(data);
    }
    private void AddLong(Message message, long data) {
        message.AddLong(data);
    }
    private void AddByte(Message message, byte data) {
        message.AddByte(data);
    }
    private void AddBool(Message message, bool data) {
        message.AddBool(data);
    }
    private void AddDouble(Message message, double data) {
        message.AddDouble(data);
    }
    private void AddInt(Message message, int data) {
        message.AddInt(data);
    }
    private void AddFloat(Message message, float data) {
        message.AddFloat(data);
    }
    private void AddString(Message message, string data) {
        message.AddString(data);
    }
    private void AddQuaternion(Message message, Quaternion quaternion) {
        message.AddQuaternion(quaternion);
    }
    private void AddVector3(Message message, Vector3 vector3) {
        message.AddVector3(vector3);
    }
    private void AddVector2(Message message, Vector2 vector2) {
        message.AddVector2(vector2);
    }
    #endregion
    
    
    // 根据命令改变游戏的状态
    public void ExcuteState() {
        //战报  建议通过反射获取Server代码标签生成字典然后执行
        
        //断线重连 采用 状态+追帧  的方式
        //状态数据首先克隆一份还原，然后走追帧逻辑
    }

    private int lisInt = 0;
    
    public void FixedUpdate()
    {
        /*
        if (copyCmd.Count > 0)
        {
            Debug.Log("CMD长度" + copyCmd.Count);
            NetworkTimeMessage data = copyCmd.Dequeue();

            lisInt++;
            Debug.Log(lisInt);
            if (lisInt == 3)
            {
                //return;
            }
            
            Debug.Log("重连消息队列" + data.msg);
            Game.ServerNet.Send(data.msg, tempId);
        }
        */
    }
    
    // 新玩家登陆进行追帧
    public void ClientConnected(ushort id)
    {
        //克隆数据 追帧，追完后 通知玩家重连成功
        
        // 尝试直接执行所有网络命令
        //copyCmd = cmdList.Clone();

        NetworkTimeMessage[] array = new NetworkTimeMessage[cmdList.Count];
        cmdList.CopyTo(array, 0);
        
        Debug.Log("数据长度" + array.Length);
        
        for (int i = 0; i < array.Length; i++)
        {
            //copyCmd.Enqueue(array[i]);
        }
        
        
        for (int i = 0; i < cmdList.Count; i++)
        {
            var message = cmdList[i];
            Debug.Log("重连执行消息" + message.msg);
            Game.ServerNet.Send(message.msg, id);
            //break;
        }
        
        
        //通知玩家连接成功
        Message messageToSend = Message.Create(MessageSendMode.reliable, Msg.connected);
        Game.ServerNet.Send(messageToSend, id);
        //copyCmd.Enqueue(new NetworkTimeMessage(messageToSend, 1));
        //AddCmdMsg(0, Msg.connected);

        tempId = id;
    }

    private ushort tempId;
    public void SendData()
    {
        //Game.ServerNet.Send(cmdList[0].msg, tempId);
    }
}

public class NetworkTimeMessage {
    public NetworkTimeMessage(Message msg, uint nowTick) {
        this.nowTick = nowTick;
        this.msg = msg;
        
        
    }

    public uint nowTick;
    public Message msg;
}