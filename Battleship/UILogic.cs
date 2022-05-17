using BattleshipLibrary;
using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship
{
    public class UILogic
    {
        public static void IdentifyWinner(PlayerInfoModel winner)
        {
            Console.WriteLine($"Congratulations {winner}, You Won!!");
            Console.WriteLine($"{winner.UsersName} took {GameLogic.GetShotCount(winner)} shots.");
        }

        public static void RecordPlayerSHot(PlayerInfoModel activePlayer, PlayerInfoModel opponent)
        {
            bool isValidShot = false;
            string row = string.Empty;
            int column;

            do
            {
                string shot = AskForShot(activePlayer);
                (row, column) = GameLogic.splitShotIntoRowAndColumn(shot);
                isValidShot = GameLogic.ValidateShot(row, column, activePlayer);

                if (isValidShot == false)
                {
                    Console.WriteLine($"{activePlayer.UsersName} shot of {shot} was Invalid. Please try again.");
                }

            } while (isValidShot == false);

            bool isAHit = GameLogic.IdentifyShotResult(opponent, row, column);

            GameLogic.MarkShotResult(activePlayer, row, column, isAHit);
        }

        public static string AskForShot(PlayerInfoModel activePlayer)
        {
            Console.Write($"\n {activePlayer.UsersName} where would you like to shoot? ");
            var ShotPlacement = Console.ReadLine();
            return ShotPlacement;
        }

        public static void DisplayShotGrid(PlayerInfoModel activePlayer)
        {
            string currentRow = activePlayer.ShotGrid[0].SpotLetter;

            foreach (var gridspot in activePlayer.ShotGrid)
            {
                if (gridspot.SpotLetter != currentRow)
                {
                    Console.WriteLine();
                    currentRow = gridspot.SpotLetter;
                }

                if (gridspot.Status == GridSpotStatus.Empty)
                {
                    Console.Write($" {gridspot.SpotLetter}{gridspot.SpotNumber} ");
                }
                else if (gridspot.Status == GridSpotStatus.Hit)
                {
                    Console.Write(" X ");
                }
                else if (gridspot.Status == GridSpotStatus.Miss)
                {
                    Console.Write(" O ");
                }
                else
                {
                    Console.Write(" ? "); //Something went wrong if this populates
                }
            }
        }

        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Battleship!");
            Console.WriteLine("Created by Darius Dubose \n");

        }

        public static PlayerInfoModel CreatePlayer(string playerTitle)
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

        public static string AskForUsersName()
        {
            Console.Write("Please input your name: ");
            string userName = Console.ReadLine();
            return userName;
        }

        public static void PlaceShips(PlayerInfoModel model)
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
