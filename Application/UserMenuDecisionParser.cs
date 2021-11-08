using System;

namespace ElectricityBillMSIC.Application
{
    public static class UserMenuDecisionParser
    {
        public static UserMenuDecisionType GetUserMenuDecisionFromConsole()
        {
            while (true)
            {
                var consoleKeyInfo = Console.ReadKey();
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.Escape:
                        return UserMenuDecisionType.Exit;
                    case ConsoleKey.D1:
                        return UserMenuDecisionType.AddNewClient;
                    case ConsoleKey.D2:
                        return UserMenuDecisionType.ShowClient;
                    case ConsoleKey.D3:
                        return UserMenuDecisionType.ShowAllClients;
                    default:
                        break;
                }

                Console.WriteLine("Please input a valid operation.");
            }
        }
    }
}
