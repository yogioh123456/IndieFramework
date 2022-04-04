using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;
using UnityEngine.UI;

//UI Ctrl层: UI_Menu
public class UI_Menu : UGUICtrl
{
    public UI_Menu_View selfView;

    public UI_Menu()
    {
        OnCreate(ref selfView,"Prefabs/UI/ui_menu","UI_Menu");
    }

    /// <summary>
    /// 按钮添加事件
    /// </summary>
    protected override void ButtonAddClick()
    {
        //------------------按钮添加事件-----------------
        selfView.btnServer.AddButtonEvent(() => {
            Game.ServerNet.StartServer(7778, 10);
            Game.ClientNet.Connect("127.0.0.1", 7778);
            Game.ClientNet.connectedAction = () => {
                //CreatePlayer();
                ClosePanel();
            };
        });
        selfView.btnClient.AddButtonEvent(() => {
            Game.ClientNet.Connect("127.0.0.1", 7778);
            Game.ClientNet.connectedAction = () => {
                //CreatePlayer();
                ClosePanel();
            };
        });
        selfView.btnSend.AddButtonEvent(() => {
            string playerName = selfView.playerName.text;
            string chatContent = selfView.inputSend.text;
            
            /*
            //消息发送
            Message message = Message.Create(MessageSendMode.reliable, MessageTest.MessageTestId.spawnPlayer, shouldAutoRelay: true);
            message.AddUShort(Game.Client.client.Id);//client 的 id
            message.AddString(playerName + chatContent);
            //客户端发送消息
            Game.Client.client.Send(message);
            */

            Game.ClientNet.Send(Msg.Chat, playerName + chatContent);
        });
    }

    /// <summary>
    /// 打开面板
    /// </summary>
    public override void OpenPanel(object data)
    {
        base.OpenPanel(data);
        
    }

    private void CreatePlayer() {
        //通知服务器
        Game.ClientNet.Send(Msg.CreatePlayer);
    }
}