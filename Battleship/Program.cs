using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipLibrary;
using BattleshipLibrary.Models;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();

            
            Console.ReadLine();
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship!");
            Console.WriteLine("Created by Darius Dubose \n");
            
        }

        private static PlayerInfoModel CreatPlayer()
        {
            //Ask the user for their name
            PlayerInfoModel player = new PlayerInfoModel();
            player.UsersName = AskForUsersName();

            //Load up the shot grid
            GameLogic.InitializeGrid(player);

            //Ask user for 5 ship placements.


            //clear

            return player;
        }

        private static string AskForUsersName()
        {
            Console.Write("Please input your name: ");
            string userName = Console.ReadLine();
            return userName;
        }
        private static void PlaceShips(PlayerInfoModel model)
        {
            do
            {
                Console.Write($"Where would you like to place ship number {model.ShipLocations.Count + 1}?: ");
                string location = Console.ReadLine();

            } while (model.ShipLocations.Count < 5);
        }
    }
}
