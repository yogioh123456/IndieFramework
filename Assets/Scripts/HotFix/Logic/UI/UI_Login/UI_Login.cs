using System.Collections;
using System.Collections.Generic;
using RiptideNetworking;
using UnityEngine;
using UnityEngine.UI;

//UI Ctrl层: UI_Login
public class UI_Login : UGUICtrl
{
    public UI_Login_View selfView;

    public UI_Login()
    {
        OnCreate(ref selfView,"UI/Prefabs/ui_login","UI_Login");
    }

    /// <summary>
    /// 按钮添加事件
    /// </summary>
    protected override void ButtonAddClick()
    {
        //------------------按钮添加事件-----------------
        selfView.btn_login.AddButtonEvent(ConnectServer);
        selfView.btn_chat.AddButtonEvent(Chat);
        selfView.btn_test.AddButtonEvent(Login);
    }

    /// <summary>
    /// 打开面板
    /// </summary>
    public override void OpenPanel(object data)
    {
        base.OpenPanel(data);
        
    }

    private void ConnectServer() {
        string ip = "127.0.0.1";
        Game.ClientNet.Connect(ip, 7778);
        Game.ClientNet.connectedAction = () => {
            Debug.Log("连接成功X");
        };
    }

    private void Chat() {
        Game.ClientNet.Send(Msg.Chat, "聊天信息KKKKKKKKKKKKKKK");
    }

    private void Login() {
        string name = selfView.input_userName.text;
        string password = selfView.input_password.text;
        Game.ClientNet.Send(Msg.Login, name, password);
    }
    
    //账号密码登录服务器返回结果
    [MessageHandler((ushort)Msg.Login)]
    private static void UserLogin(Message message)
    {
        bool info = message.GetBool();
        if (info) {
            Debug.Log("新账号，成功登录");
        } else {
            Debug.Log("账号存在，成功登录");
            //跳转大厅
            
        }
    }
}