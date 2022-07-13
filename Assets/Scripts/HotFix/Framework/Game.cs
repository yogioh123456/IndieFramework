using System;
using System.Collections.Generic;
using System.Reflection;

public partial class Game : EntityStatic
{
    private static List<Type> gameCompList = new List<Type>();
    public static void Init(object instance) {
        gameCompList.Clear();
        //获得当前类的程序集
        Assembly assembly = instance.GetType().Assembly;
        //获取此程序集中的所有类
        Type[] allClass = assembly.GetTypes();
        foreach (Type oneClass in allClass) {
            var a = oneClass.GetCustomAttribute<GameComp>();
            if (a != null) {
                gameCompList.Add(oneClass);
            }
        }
        gameCompList.Sort(ComparaList);
        for (int i = 0; i < gameCompList.Count; i++) {
            AddComp(gameCompList[i]);
        }
    }
    private static int ComparaList(Type t1, Type t2) {
        return t1.GetCustomAttribute<GameComp>().priority.CompareTo(t2.GetCustomAttribute<GameComp>().priority);
    }
    
    public static void Update() {
        for (int i = 0; i < updateList.Count; i++)
        {
            updateList[i].Update();
        }
    }
    
    public static void FixedUpdate() {
        for (int i = 0; i < fixedUpdateList.Count; i++)
        {
            fixedUpdateList[i].FixedUpdate();
        }
    }
    
    public static void OnApplicationQuit() {
        for (int i = 0; i < applicationList.Count; i++)
        {
            applicationList[i].OnApplicationQuit();
        }
    }
}
