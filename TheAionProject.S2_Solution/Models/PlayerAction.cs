﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    /// <summary>
    /// enum of all possible player actions
    /// </summary>
    public enum PlayerAction
    {
        None,
        MissionSetup,
        LookAround,
        LookAt,
        PickUpItem,
        PickUp,
        PutDown,
        Inventory,
        PickUpTreasure,
        PutDownItem,
        PutDownTreasure,
        Travel,
        PlayerInfo,
        PlayerInventory,
        PlayerTreasure,
        PlayerLocationsVisited,
        ListGameObjects,
        ListSpaceTimeLocations,
        ListItems,
        ListTreasures,
        AdminMenu,
        ReturnToMainMenu,
        Exit
    }
}
