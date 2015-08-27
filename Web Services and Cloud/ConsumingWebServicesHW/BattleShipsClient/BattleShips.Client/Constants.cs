namespace BattleShips.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Constants
    {
        public static class UrlEndPoints
        {
            public const string Register = "api/account/register";
            public const string Login = "token";
            public const string CreateGame = "api/games/create";
            public const string JoinGame = "api/games/join";
            public const string Play = "api/games/play";
        }
    }
}
