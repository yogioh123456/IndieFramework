using System;

//属于组件
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class GameComp : Attribute
{

}

//属于Mono组件
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public class GameMonoComp : Attribute
{

}
