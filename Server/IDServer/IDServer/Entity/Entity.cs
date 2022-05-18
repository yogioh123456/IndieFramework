using System;
using System.Collections;
using System.Collections.Generic;

public class Entity
{
    public Dictionary<Type, object> compDic = new Dictionary<Type, object>();
    
    /// <summary>
    /// 添加组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T AddComp<T>(params object[] datas)
    {
        Type type = typeof (T);
        T t = (T)Activator.CreateInstance(type, datas);
        
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

    public void RemoveComp<T>(T t)
    {
        Type type = typeof (T);
        if (compDic.ContainsKey(type))
        {
            if (compDic[type] is Entity entity)
            {
                if (entity.compDic.Count > 0)
                {
                    foreach (var one in entity.compDic)
                    {
                        Type c = one.Key;
                        RemoveComp(c);
                    }
                }
            }
            compDic.Remove(type);
        }
    }

    public virtual void Dispose() {
        foreach (var one in compDic) {
            if (one.Value is Entity entity) {
                entity.Dispose();
            }
        }
    }
}
