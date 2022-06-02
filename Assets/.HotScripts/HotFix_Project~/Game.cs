using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project
{
    public class Game
    {
        public void Start()
        {
            Debug.Log("开始");
        }

        public static void Update()
        {
            Debug.Log("Update");
        }

        public static void FixedUpdate()
        {
            Debug.Log("FixedUpdate");
        }

        public static void LateUpdate()
        {
            Debug.Log("LateUpdate");
        }

        public static void Close()
        {

        }
    }
}
