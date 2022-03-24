public partial class Game {
    public static void Init() {
        AddComp<MainLogic>();
        AddComp<UGUIManager>();
        AddComp<EventSystemManager>();
        AddComp<ServerNetwork>();
        AddComp<ServerSyncManager>();
        AddComp<ClientNetwork>();
        AddComp<InputManager>();
        AddComp<AssetManager>();
        AddComp<PlayerManager>();
    }
    
    public static EventSystemManager Event => GetComp<EventSystemManager>();
    public static UGUIManager UI => GetComp<UGUIManager>();
    public static ServerNetwork Server => GetComp<ServerNetwork>();
    public static ClientNetwork Client => GetComp<ClientNetwork>();
    public static AssetManager Asset => GetComp<AssetManager>();
}
