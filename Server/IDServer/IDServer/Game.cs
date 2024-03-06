using System;
using System.Collections.Generic;
using System.Text;

namespace IDServer
{
    public class Game : EntityStatic
    {
        public static void Init()
        {
            AddComp<DBManager>();
            AddComp<UserManager>();
            AddComp<RoomManager>();
        }
    }
}
