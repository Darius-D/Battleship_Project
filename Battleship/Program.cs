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
            UILogic.WelcomeMessage();

            PlayerInfoModel activePlayer = UILogic.CreatePlayer("Player 1");
            PlayerInfoModel opponent = UILogic.CreatePlayer("Player 2");
            PlayerInfoModel Winner = null;

            do
            {

                UILogic.DisplayShotGrid(activePlayer);

                UILogic.RecordPlayerSHot(activePlayer, opponent);

                bool doesGameContinue = GameLogic.PLayerStillActive(opponent);

                if (doesGameContinue == true) 
                {
                    (activePlayer, opponent) = (opponent, activePlayer);
                }
                else
                {
                    Winner = activePlayer;
                }

            } while (Winner == null);

            UILogic.IdentifyWinner(Winner);

            Console.ReadLine();
        }

       
    }
}
