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

    public delegate void Del();

    private void MessageAdd<T>(Message message, T t) {
        
        //bool b1 = (() => { }) is Del;   // CS0837
        //bool b2 = delegate() { } is Del;// CS0837
        //Del d1 = () => { } as Del;      // CS0837  
        //Del d2 = delegate() { } as Del;
        
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
}
