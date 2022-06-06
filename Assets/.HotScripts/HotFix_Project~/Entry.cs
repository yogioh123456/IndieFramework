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
				Game.hotFixTypes = CodeLoader.Instance.hotfixTypes;
				CodeLoader.Instance.Update += Main.Update;
				CodeLoader.Instance.FixedUpdate += Main.FixedUpdate;
				CodeLoader.Instance.LateUpdate += Main.LateUpdate;
				CodeLoader.Instance.OnApplicationQuit += Main.Close;
				Main.Start();
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
		}
    }
}
