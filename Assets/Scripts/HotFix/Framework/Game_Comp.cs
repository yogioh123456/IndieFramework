using System;
using System.Collections.Generic;
using System.Reflection;

public partial class Game
{
    public static EventSystemManager Event => Get<EventSystemManager>();
    public static UGUIManager UI => Get<UGUIManager>();
    public static ServerNetwork ServerNet => Get<ServerNetwork>();
    public static ClientNetwork ClientNet => Get<ClientNetwork>();
    public static AssetManager Asset => Get<AssetManager>();
    public static ServerMain ServerMain => Get<ServerMain>();
}
