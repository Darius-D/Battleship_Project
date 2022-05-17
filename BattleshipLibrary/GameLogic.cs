using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary
{
    public static class GameLogic
    {
        public static void InitializeGrid(PlayerInfoModel model)
        {
            List<string> letters = new List<string>
            {
                "A","B","C","D","E"
            };
            List<int> numbers = new List<int>
            {
                1,2,3,4,5
            };
            foreach (var letter in letters)
            {
                foreach(var number in numbers)
                {
                    AddGridSpot(model, letter, number);
                }
            }
            
        }

        private static void AddGridSpot(PlayerInfoModel model, string letter, int number)
        {
            GridSpotModel spot = new GridSpotModel
            {
                SpotLetter = letter,
                SpotNumber = number,
                Status = GridSpotStatus.Empty
            };
            model.ShotGrid.Add(spot);
        }

        public static bool PLayerStillActive(PlayerInfoModel player)
        {
            bool isActive = false;

            foreach(var ship in player.ShipLocations)
            {
                if (ship.Status != GridSpotStatus.Sunk)
                {
                    isActive = true;
                }
            }
            return isActive;
        }

        public static bool PlaceShip(PlayerInfoModel player, string shotlocation)
        {
            (string row, int column) = splitShotIntoRowAndColumn(shotlocation);

            bool output = false;
            bool isValidLocation = ValidateGridLocation(player, row, column);
            bool isSpotOpen = ValidateShipLocation(player, row, column);

            if (isValidLocation && isSpotOpen )
            {
                player.ShipLocations.Add(new GridSpotModel
                {
                    SpotLetter = row.ToUpper(),
                    SpotNumber = column,
                    Status = GridSpotStatus.Ship

                });
                output = true;
            }
            return output;
        }

        private static bool ValidateShipLocation(PlayerInfoModel player, string row, int column)
        {
            bool isValidLocation = true;
            foreach(var ship in player.ShipLocations)
            {
                if(ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isValidLocation = false; 
                }
            }
            return isValidLocation;
        }

        private static bool ValidateGridLocation(PlayerInfoModel player, string row, int column)
        {
            bool isValidLocation = false;
            foreach (var ship in player.ShotGrid)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isValidLocation = true;
                }
            }
            return isValidLocation;
        }

        public static int GetShotCount(PlayerInfoModel player)
        {
            int shotCount = 0;

            foreach(var shot in player.ShotGrid)
            {
                if(shot.Status != GridSpotStatus.Empty)
                {
                    shotCount++;
                }
            }
            return shotCount;
        }

        public static (string row, int column) splitShotIntoRowAndColumn(string shot)
        {


            if (shot.Length != 2 || shot[1].GetType().Equals(typeof(int)))
            {
                throw new ArgumentException("Invalid shot format");
            }

            string row = shot[0].ToString();
            int column = int.Parse(shot[1].ToString());
           
            return (row, column);
        }

        public static bool ValidateShot(string row, int column, PlayerInfoModel Player)
        {
            bool isValidShot = false;
            foreach (var gridSpot in Player.ShotGrid)
            {
                if (gridSpot.SpotLetter == row.ToUpper() && gridSpot.SpotNumber == column)
                {
                    if(gridSpot.Status == GridSpotStatus.Empty)
                    {
                        isValidShot = true;
                    }
                }
            }
            return isValidShot; ;
        }

        public static bool IdentifyShotResult(PlayerInfoModel opponentPlayer, string row, int column)
        {
            bool isAHit = false;
            
            foreach (var ship in opponentPlayer.ShipLocations)
            {
                if (ship.SpotLetter == row.ToUpper() && ship.SpotNumber == column)
                {
                    isAHit = false;
                }
            }
            return isAHit;
        }

        public static void MarkShotResult(PlayerInfoModel player, string row, int column, bool isAHit)
        {
            
            foreach(var gridspot in player.ShotGrid)
            {
                if(gridspot.SpotLetter == row.ToUpper() && gridspot.SpotNumber == column)
                {
                    if (isAHit)
                    {
                        gridspot.Status = GridSpotStatus.Hit;
                    }
                    else
                    {
                        gridspot.Status = GridSpotStatus.Miss;
                    }
                }
            }
        }
    }
}
