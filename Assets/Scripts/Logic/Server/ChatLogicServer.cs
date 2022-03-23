using RiptideNetworking;
using UnityEngine;

public class ChatLogicServer {
    //服务端处理消息
    [MessageHandler((ushort)MessageTest.MessageTestId.spawnPlayer)]
    private static void SpawnPlayer(ushort fromClientId, Message message) {
        ushort playerId = message.GetUShort();
        string str = message.GetString();
        Debug.Log("服务端生成玩家" + playerId + str);
        Message messageToSend = Message.Create(MessageSendMode.reliable, MessageTest.MessageTestId.spawnPlayer);
        messageToSend.AddUShort(playerId);
        messageToSend.AddString(str);
        Game.Server.SendToAll(messageToSend);
    }
}