using System;

namespace HotFix_Project
{
    //属于组件
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class GameComp : Attribute
    {
        public int priority;
        public GameComp(int priority = 0)
        {
            this.priority = priority;
        }
    }
}