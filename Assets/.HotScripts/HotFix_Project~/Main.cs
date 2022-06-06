using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project
{
    public class Main
    {
        public static void Start()
        {
            Debug.Log("开始");
            Game.Init();
            //Game.GetComp<MainLogic>().Init();
        }

        public static void Update()
        {
            Game.Update();
        }

        public static void FixedUpdate()
        {
            Game.FixedUpdate();
        }

        public static void LateUpdate()
        {
            
        }

        public static void Close()
        {
            Game.OnApplicationQuit();
        }
    }
}
