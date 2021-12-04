using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Game : EntityStatic
{
    public static void Init()
    {
        AddComp<EventSystemManager>();
    }

    public static EventSystemManager Event => GetComp<EventSystemManager>();
}
