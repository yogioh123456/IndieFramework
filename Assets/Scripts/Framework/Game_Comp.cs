public partial class Game {
    public static void Init() {
        AddComp<MainLogic>();
        AddComp<UGUIManager>();
        AddComp<EventSystemManager>();
        AddComp<ServerMain>();
        AddComp<ServerNetwork>();
        AddComp<ServerSyncManager>();
        AddComp<ClientNetwork>();
        AddComp<InputManager>();
        AddComp<AssetManager>();
        AddComp<PlayerManager>();
    }
    
    public static EventSystemManager Event => GetComp<EventSystemManager>();
    public static UGUIManager UI => GetComp<UGUIManager>();
    public static ServerNetwork ServerNet => GetComp<ServerNetwork>();
    public static ClientNetwork ClientNet => GetComp<ClientNetwork>();
    public static AssetManager Asset => GetComp<AssetManager>();
    public static ServerMain ServerMain => GetComp<ServerMain>();
}
