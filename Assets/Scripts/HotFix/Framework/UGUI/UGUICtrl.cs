using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UGUICtrl
{
    public string panelName;
    public UGUIView mainView;

    protected Transform OnCreate<T>(ref T t,string path,string _panelName) where T: UGUIView, new() {
        GameObject go = Object.Instantiate(AssetManager.LoadAsset(path), Game.UI.UIRoot);
        T _ui = go.GetComponent<T>();
        mainView = _ui;
        t = _ui;
        Init();
        OnRegisterEvent();
        ButtonAddClick();
        panelName = _panelName;
        return go.transform;
    }

    protected virtual void Init()
    {
        
    }
    
    protected virtual void ButtonAddClick()
    {
        
    }

    protected virtual void OnRegisterEvent()
    {
        
    }
    
    protected void Back()
    {
        Game.UI.BackUIPanel();
    }
    
    public virtual void OpenPanel(object data)
    {
        //建议使用alpha开启和关闭
        mainView.gameObject.SetActive(true);
    }

    public virtual void ClosePanel()
    {
        //建议使用alpha开启和关闭
        mainView.gameObject.SetActive(false);
    }

    //用于一些情况的简单 显示和隐藏
    public void ViewActive(bool _b)
    {
        mainView.gameObject.SetActive(_b);
    }
    
    public virtual void Dispose()
    {
        GameObject.Destroy(mainView.gameObject);
    }
}
