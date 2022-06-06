using System;
using System.Collections.Generic;
using System.Reflection;

namespace HotFix_Project
{
    public partial class Game : EntityStatic
    {
        public static List<Type> hotFixTypes = new List<Type>();
        private static List<Type> gameCompList = new List<Type>();
        public static void Init()
        {
            gameCompList.Clear();

            //获取此程序集中的所有类
            List<Type> allClass = hotFixTypes;
            foreach (Type oneClass in allClass)
            {
                object[] attrs = oneClass.GetCustomAttributes(typeof(GameComp), false);
                if (attrs.Length > 0)
                {
                    gameCompList.Add(oneClass);
                }
            }

            UnityEngine.Debug.Log("组件数量" + gameCompList.Count);
            if (gameCompList.Count > 0)
            {
                gameCompList.Sort(ComparaList);
                
                for (int i = 0; i < gameCompList.Count; i++)
                {
                    AddComp(gameCompList[i]);
                }
            }
        }
        private static int ComparaList(Type t1, Type t2)
        {
            object[] attrs1 = t1.GetCustomAttributes(typeof(GameComp), false);
            GameComp gameComp1 = attrs1[0] as GameComp;
            UnityEngine.Debug.Log("AAAAAAAAAAAAAAA");
            object[] attrs2 = t2.GetCustomAttributes(typeof(GameComp), false);
            GameComp gameComp2 = attrs2[0] as GameComp;
            UnityEngine.Debug.Log("BBBBBBBBBBBBBBBBBBBBB");
            return gameComp1.priority.CompareTo(gameComp2.priority);
        }

        public static void Update()
        {
            for (int i = 0; i < updateList.Count; i++)
            {
                updateList[i].Update();
            }
        }

        public static void FixedUpdate()
        {
            for (int i = 0; i < fixedUpdateList.Count; i++)
            {
                fixedUpdateList[i].FixedUpdate();
            }
        }

        public static void OnApplicationQuit()
        {
            for (int i = 0; i < applicationList.Count; i++)
            {
                applicationList[i].OnApplicationQuit();
            }
        }
    }
}