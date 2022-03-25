public class ServerMain : Entity
{
    public ServerRoleManager serverRoleManager => GetComp<ServerRoleManager>();
    
    public ServerMain()
    {
        AddComp<ServerRoleManager>();
    }
}
