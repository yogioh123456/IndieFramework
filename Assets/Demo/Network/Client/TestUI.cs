using System.Collections;
using System.Collections.Generic;
using GameServer;
using RiptideNetworking;
using UnityEngine;
using UnityEngine.UI;

internal enum MessageId : ushort
{
    spawnPlayer = 1,
    playerMovement
}

public class TestUI : MonoBehaviour {
    public ClientNetworkManager clientNetworkManager;
    public ServerNetworkManager serverNetworkManager;
    public static Server MyServer;
    public Button button_host;
    public Button button_client;
    public Button button_send;
    public InputField input_send;
    public Text text_info;
    
    // Start is called before the first frame update
    void Start()
    {
        button_host.onClick.AddListener(()=>
        {
            serverNetworkManager.StartServer();
            clientNetworkManager.Client();
            MyServer = serverNetworkManager.server;
        });
        button_client.onClick.AddListener(()=>
        {
            clientNetworkManager.Client();
        });
        button_send.onClick.AddListener(()=> {
            text_info.text = input_send.text;
            Debug.Log(input_send.text);
            //消息发送
            Message message = Message.Create(MessageSendMode.reliable, MessageId.spawnPlayer, shouldAutoRelay: true);
            message.AddUShort(clientNetworkManager.client.Id);//client 的 id
            message.AddString(input_send.text);
            //客户端发送消息
            clientNetworkManager.client.Send(message);
        });
    }

    //服务端处理消息
    [MessageHandler((ushort)MessageId.spawnPlayer)]
    private static void SpawnPlayer(ushort fromClientId, Message message) {
        ushort playerId = message.GetUShort();
        string str = message.GetString();
        Debug.Log("服务端生成玩家" + playerId + str);
        Message messageToSend = Message.Create(MessageSendMode.reliable, MessageId.spawnPlayer);
        messageToSend.AddUShort(playerId);
        messageToSend.AddString(str);
        MyServer.SendToAll(messageToSend);
    }
    
    //客户端接收消息
    [MessageHandler((ushort)MessageId.spawnPlayer)]
    private static void SpawnPlayer(Message message)
    {
        ushort playerId = message.GetUShort();
        string str = message.GetString();
        
        Debug.Log("生成玩家" + playerId + str);
        
    }
    
    
}
