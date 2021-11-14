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
                    case ConsoleKey.C:
                        return UserMenuDecisionType.ClearConsole;
                    case ConsoleKey.D1:
                        return UserMenuDecisionType.AddNewClient;
                    case ConsoleKey.D2:
                        return UserMenuDecisionType.ShowClientInfo;
                    case ConsoleKey.D3:
                        return UserMenuDecisionType.ShowClientBills;
                    case ConsoleKey.D4:
                        return UserMenuDecisionType.ShowClientBillsByAddress;
                    case ConsoleKey.D5:
                        return UserMenuDecisionType.ShowClientsGroupedByOverdueness;
                    case ConsoleKey.D6:
                        return UserMenuDecisionType.ShowClientsThatHaveToPayMoreThan;
                    case ConsoleKey.Escape:
                        return UserMenuDecisionType.Exit;
                    default:
                        break;
                }

                Console.WriteLine("Please input a valid operation.");
            }
        }
    }
}
