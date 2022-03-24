﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    public Dictionary<Type, object> compDic = new Dictionary<Type, object>();
    
    /// <summary>
    /// 添加组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T AddComp<T>() where T : new()
    {
        Type type = typeof (T);
        T t = (T)Activator.CreateInstance(type);
        
        if (!compDic.ContainsKey(type))
        {
            compDic.Add(type, t);
        }
        else
        {
            Debug.LogError("不能重复添加组件");
        }
        
        return t;
    }

    /// <summary>
    /// 获取组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T GetComp<T>()
    {
        Type type = typeof (T);
        if (compDic.ContainsKey(type))
        {
            return (T)compDic[type];
        }
        return default;
    }
}
