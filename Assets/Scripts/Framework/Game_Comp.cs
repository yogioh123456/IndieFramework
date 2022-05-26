using System;
using System.Collections.Generic;
using System.Reflection;

public partial class Game 
{
    public static EventSystemManager Event => GetComp<EventSystemManager>();
    public static UGUIManager UI => GetComp<UGUIManager>();
    public static ServerNetwork ServerNet => GetComp<ServerNetwork>();
    public static ClientNetwork ClientNet => GetComp<ClientNetwork>();
    public static AssetManager Asset => GetComp<AssetManager>();
    public static ServerMain ServerMain => GetComp<ServerMain>();
}
