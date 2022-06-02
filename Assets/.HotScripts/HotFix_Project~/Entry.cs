using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project
{
    public static class Entry
    {
        public static void Start()
        {
			try
			{
				Debug.Log("IDFramework");
				CodeLoader.Instance.Update += Game.Update;
				CodeLoader.Instance.FixedUpdate += Game.FixedUpdate;
				CodeLoader.Instance.LateUpdate += Game.LateUpdate;
				CodeLoader.Instance.OnApplicationQuit += Game.Close;
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
		}
    }
}
