public partial class Game {
    public static EventSystemManager Event => GetComp<EventSystemManager>();
    public static UGUIManager UI => GetComp<UGUIManager>();
    public static ServerNetwork Server => GetComp<ServerNetwork>();
    public static ClientNetwork Client => GetComp<ClientNetwork>();
}
