namespace BattleShips.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class BattleShipsMain
    {
        static string baseEndpoint = string.Empty;
        static string accessToken = string.Empty;

        static void Main(string[] args)
        {
            baseEndpoint = Console.ReadLine();

            string commandLine = Console.ReadLine();
            while (commandLine.ToLower() != "end")
            {
                if (string.IsNullOrEmpty(commandLine))
                {
                    commandLine = Console.ReadLine();
                    continue;
                }

                string[] arguments = commandLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Skip(2).ToArray();
                string command = commandLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Skip(1).Take(1).First();

                switch (command)
                {
                    case "register":
                        RegisterPlayerAsync(arguments);
                        break;
                    case "login":
                        LoginUser(arguments);
                        break;
                    case "create-game":
                        CreateGame();
                        break;
                    case "join-game":
                        JoinGame(arguments);
                        break;
                    case "play":
                        PlayGame(arguments);
                        break;
                    default:
                        break;
                }

                commandLine = Console.ReadLine();
            }
        }

        private static async Task PlayGame(string[] arguments)
        {
            using (var httpClient = new HttpClient())
            {
                var playGameUrlBuilder = new UriBuilder(string.Concat(baseEndpoint, Constants.UrlEndPoints.Play));
                httpClient.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", accessToken));
                var bodyData = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("GameId", arguments[0]),
                    new KeyValuePair<string, string>("PositionX", arguments[1]),
                    new KeyValuePair<string, string>("PositionY", arguments[2])
                });

                var response = await httpClient.PostAsync(playGameUrlBuilder.ToString(), bodyData);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("User successfully played;");
                }
                else
                {
                    var responseData = await response.Content.ReadAsAsync<ExceptionMessage>();
                    Console.WriteLine("User could not play: {0}", responseData.Message);
                }
            }
        }

        private static async Task JoinGame(string[] arguments)
        {
            using (var httpClient = new HttpClient())
            {

                var joinGameUrlBuilder = new UriBuilder(string.Concat(baseEndpoint, Constants.UrlEndPoints.JoinGame));
                httpClient.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", accessToken));
                var bodyData = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("GameId", arguments[0])
                });
                var response = await httpClient.PostAsync(joinGameUrlBuilder.ToString(), bodyData);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("The current user has successfully joined game with Id {0}", arguments[0]);
                }
                else
                {
                    string responseMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("The current user could not join game with Id {0}: {1}",
                        arguments[0],
                        responseMessage);
                }
            }
        }

        private static async Task CreateGame()
        {
            using (var httpClient = new HttpClient())
            {
                var createGameUrlBuilder = new UriBuilder(string.Concat(baseEndpoint, Constants.UrlEndPoints.CreateGame));

                httpClient.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", accessToken));
                var response = await httpClient.PostAsync(createGameUrlBuilder.ToString(), null);
                if (response.IsSuccessStatusCode)
                {
                    var createGameData = await response.Content.ReadAsAsync(typeof(string));
                    Console.WriteLine("Game with Id {0} was successfully created.", createGameData);
                }
                else
                {
                    string responseMessage = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("New game could not be created: {0}", responseMessage);
                }
            }
        }

        private static async Task LoginUser(string[] arguments)
        {
            using (var httpClient = new HttpClient())
            {
                var loginUrlBuilder = new UriBuilder(string.Concat(baseEndpoint, Constants.UrlEndPoints.Login));

                var bodyData = new FormUrlEncodedContent(new[] {
                        new KeyValuePair<string,string>("UserName", arguments[0]),
                        new KeyValuePair<string,string>("Password", arguments[1]),
                        new KeyValuePair<string,string>("grant_type", "password")
                });

                var response = await httpClient.PostAsync(loginUrlBuilder.ToString(), bodyData);

                if (response.IsSuccessStatusCode)
                {
                    var loginData = await response.Content.ReadAsAsync<LoginData>();
                    accessToken = loginData.Access_Token;
                    Console.WriteLine(string.Format("User {0} has successfuly logged in.", loginData.UserName));
                }
                else
                {
                    Console.WriteLine(string.Format("User {0} could not login.", arguments[0]));
                }
            }
        }

        private static async Task RegisterPlayerAsync(string[] arguments)
        {
            var httpClient = new HttpClient();

            using (httpClient)
            {
                var registrationUrlBuilder = new UriBuilder(string.Concat(baseEndpoint, Constants.UrlEndPoints.Register));

                var content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("Email", arguments[0]),
                new KeyValuePair<string, string>("Password", arguments[1]),
                new KeyValuePair<string, string>("ConfirmPassword", arguments[2])
            });

                var response = await httpClient.PostAsync(registrationUrlBuilder.ToString(), content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(string.Format("User {0} has successfuly registered.", arguments[0]));
                }
                else
                {
                    Console.WriteLine(string.Format("User {0} could not register.", arguments[0]));
                }
            }
        }
    }
}
