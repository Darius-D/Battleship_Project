﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipLibrary.Models
{
    class PlayerInfoModel
    {
        public string UsersName { get; set; }
        public List<GridSpotModel> ShipLocations { get; set; }
        public List<GridSpotModel> ShotGrid { get; set; }
    }
}