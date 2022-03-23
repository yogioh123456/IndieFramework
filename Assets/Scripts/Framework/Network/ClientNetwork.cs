using System;
using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;

public class ClientNetwork : Mono
{
    public Client client;
    
    public ClientNetwork() {
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError,false);
        client = new Client();
    }

    public override void FixedUpdate() {
        client?.Tick();
    }

    public override void OnApplicationQuit() {
        client.Disconnect();
    }

    public void Connect(string ip, ushort port) {
        client.Connect($"{ip}:{port}");
    }

    public delegate void MessageDelegate<Message, T>(Message msg, T t);

    public void Send<T>(Enum id, T t) {
        //消息发送
        Message message = Message.Create(MessageSendMode.reliable, id, shouldAutoRelay: true);
        message.AddUShort(Game.Client.client.Id);

        MessageAdd(message, t);

        //客户端发送消息
        Game.Client.client.Send(message);
    }

    public void Send<T,K>(Enum id, T t, K k) {
        Message message = Message.Create(MessageSendMode.reliable, id, shouldAutoRelay: true);
        message.AddUShort(Game.Client.client.Id);
        MessageAdd(message, t);
        MessageAdd(message, k);
        Game.Client.client.Send(message);
    }
    
    public void Send<T,K,V>(Enum id, T t, K k, V v) {
        Message message = Message.Create(MessageSendMode.reliable, id, shouldAutoRelay: true);
        message.AddUShort(Game.Client.client.Id);
        MessageAdd(message, t);
        MessageAdd(message, k);
        MessageAdd(message, v);
        Game.Client.client.Send(message);
    }
    
    private void MessageAdd<T>(Message message, T t) {
        if (typeof(T) == typeof(int)) {
            (AddInt as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(float)) {
            (AddFloat as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(bool)) {
            message.AddBool(Convert.ToBoolean(t));
        } else if (typeof(T) == typeof(string)) {
            (AddString as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(double)) {
            message.AddDouble(Convert.ToDouble(t));
        } else if (typeof(T) == typeof(byte)) {
            message.AddByte(Convert.ToByte(t));
        } else if (typeof(T) == typeof(byte)) {
            message.AddLong(Convert.ToInt64(t));
        } else if (typeof(T) == typeof(byte)) {
            message.AddShort(Convert.ToInt16(t));
        } else if (typeof(T) == typeof(Quaternion)) {
            (AddQuaternion as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(Vector3)) {
            (AddVector3 as MessageDelegate<Message, T>)?.Invoke(message, t);
        } else if (typeof(T) == typeof(Vector2)) {
            (AddVector2 as MessageDelegate<Message, T>)?.Invoke(message, t);
        }
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
}
