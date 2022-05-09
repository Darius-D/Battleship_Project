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

            PlayerInfoModel activePlayer = CreatePlayer("Player 1");
            PlayerInfoModel opponent = CreatePlayer("Player 2");
            PlayerInfoModel Winner = null;

            do
            {
                //Display grid from activePlayer on where they fired
                DisplayShotGrid(activePlayer);
                //Ask player 1 fire for a shot

                //determine is shot was valid

                //Determine shot results

                //determine is game is over

                //if over set player 1 as winner
                //else swap positions


            } while (Winner == null);


            Console.ReadLine();
        }

        private static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            foreach(var gridspot in activePlayer.ShotGrid)
            {
                if(gridspot.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = gridspot.SpotLetter;
                }

                if(gridspot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" {gridspot.SpotLetter}{gridspot.SpotNumber} ");
                }
            }
        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship!");
            Console.WriteLine("Created by Darius Dubose \n");
            
        }
        private static PlayerInfoModel CreatePlayer(string playerTitle)
        {
            //Ask the user for their name
            PlayerInfoModel player = new PlayerInfoModel();

            Console.WriteLine($"Player information for {playerTitle}");

            player.UsersName = AskForUsersName();

            //Load up the shot grid
            GameLogic.InitializeGrid(player);

            //Ask user for 5 ship placements.
            PlaceShips(player);

            //clear
            Console.Clear();

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

                bool isValidLocation = GameLogic.PlaceShip(model, location);

                if (!isValidLocation)
                {
                    Console.WriteLine("That was not a valid location, please try again");
                }

            } while (model.ShipLocations.Count < 5);
        }
    }
}
