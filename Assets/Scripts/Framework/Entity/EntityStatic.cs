using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityStatic
{
    private static Dictionary<Type, object> compDic = new Dictionary<Type, object>();
    protected static List<IUpdate> updateList = new List<IUpdate>();
    protected static List<IFixedUpdate> fixedUpdateList = new List<IFixedUpdate>();
    protected static List<IApplicationQuit> applicationList = new List<IApplicationQuit>();

    protected static void AddComp(Type type) {
        Debug.Log(type);
        object t = Activator.CreateInstance(type);
        AddToComp(type, t);
    }

    private static void AddToComp(Type type, object t) {
        if (!compDic.ContainsKey(type)) {
            compDic.Add(type, t);

            if (t is IUpdate update) {
                updateList.Add(update);
            }

            if (t is IFixedUpdate fixedUpdate) {
                fixedUpdateList.Add(fixedUpdate);
            }

            if (t is IApplicationQuit applicationQuit) {
                applicationList.Add(applicationQuit);
            }

            if (t is Entity entity) {
                foreach (var one in entity.updateList) {
                    updateList.Add(one);
                }

                foreach (var one in entity.fixedUpdateList) {
                    fixedUpdateList.Add(one);
                }

                foreach (var one in entity.applicationList) {
                    applicationList.Add(one);
                }
            }
        } else {
            Debug.LogError("不能重复添加组件");
        }
    }

    /// <summary>
    /// 添加组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T AddComp<T>() where T : new()
    {
        Type type = typeof (T);
        T t = (T)Activator.CreateInstance(type);
        AddToComp(type, t);
        return t;
    }
    
    /// <summary>
    /// 组件刷新
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T AddNewComp<T>() where T : new()
    {
        Type type = typeof (T);
        T t = (T)Activator.CreateInstance(type);
        
        if (!compDic.ContainsKey(type))
        {
            compDic.Add(type, t);
        }
        else
        {
            compDic[type] = t;
        }
        
        if (t is IUpdate update)
        {
            updateList.Add(update);
        }
        if (t is IFixedUpdate fixedUpdate)
        {
            fixedUpdateList.Add(fixedUpdate);
        }
        if (t is IApplicationQuit applicationQuit)
        {
            applicationList.Add(applicationQuit);
        }

        return t;
    }
    
    /// <summary>
    /// 添加挂在场景中的MonoBehaviour组件脚本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T AddMonoComp<T>() where T : MonoBehaviour
    {
        Type type = typeof (T);
        T t = UnityEngine.Object.FindObjectOfType<T>();
        if (t == null)
        {
            GameObject go = new GameObject(typeof(T).ToString());
            t = go.AddComponent<T>();
        }
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
    
    public static T AddMonoComp<T>(GameObject go) where T : MonoBehaviour
    {
        Type type = typeof (T);
        T t = go.GetComponent<T>();
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
    public static T GetComp<T>()
    {
        Type type = typeof (T);
        if (compDic.ContainsKey(type))
        {
            return (T)compDic[type];
        }
        return default;
    }
}
