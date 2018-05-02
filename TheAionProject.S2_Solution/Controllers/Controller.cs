using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    /// <summary>
    /// controller for the MVC pattern in the application
    /// </summary>
    public class Controller
    {
        #region FIELDS

        private ConsoleView _gameConsoleView;
        private Player _gamePlayer;
        private Universe _gameUniverse;
        private SpaceTimeLocation _currentLocation;
        private bool _playingGame;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            //
            // setup all of the objects in the game
            //
            InitializeGame();

            //
            // begins running the application UI
            //
            ManageGameLoop();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// initialize the major game objects
        /// </summary>
        private void InitializeGame()
        {
            _gamePlayer = new Player();
            _gameUniverse = new Universe();
            _gameConsoleView = new ConsoleView(_gamePlayer, _gameUniverse);
            _playingGame = true;

            //add initial item(s) to the player's inventory
            _gamePlayer.Inventory.Add(_gameUniverse.GetGameObjectById(100) as PlayerObject);

            Console.CursorVisible = false;
        }

        /// <summary>
        /// method to manage the application setup and game loop
        /// </summary>
        private void ManageGameLoop()
        {
            PlayerAction travelerActionChoice = PlayerAction.None;

            //
            // display splash screen
            //
            _playingGame = _gameConsoleView.DisplaySpashScreen();

            //
            // player chooses to quit
            //
            if (!_playingGame)
            {
                Environment.Exit(1);
            }

            //
            // display introductory message
            //
            _gameConsoleView.DisplayGamePlayScreen("Mission Intro", Text.MissionIntro(), ActionMenu.MissionIntro, "");
            _gameConsoleView.GetContinueKey();

            //
            // initialize the mission traveler
            // 
            InitializeMission();

            //
            // prepare game play screen
            //
            _currentLocation = _gameUniverse.GetSpaceTimeLocationById(_gamePlayer.SpaceTimeLocationID);
            _gameConsoleView.DisplayGamePlayScreen("Current Location", Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");

            //
            // game loop
            //
            while (_playingGame)
            {
                //
                // process all flags, events, and stats
                //
                UpdateGameStatus();

                //
                // get next game action from player
                //
                // get next game action from player
                if (ActionMenu.currentMenu == ActionMenu.CurrentMenu.MainMenu)
                {
                    travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.MainMenu);
                }
                else if (ActionMenu.currentMenu == ActionMenu.CurrentMenu.AdminMenu)
                {
                    travelerActionChoice = _gameConsoleView.GetActionMenuChoice(ActionMenu.AdminMenu);
                }

                //
                // choose an action based on the player's menu choice
                //
                switch (travelerActionChoice)
                {
                    case PlayerAction.None:
                        break;

                    case PlayerAction.AdminMenu:
                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.AdminMenu;
                        _gameConsoleView.DisplayGamePlayScreen("Admin Menu", "Select an operation from the menu.", ActionMenu.AdminMenu, "");
                        break;

                    case PlayerAction.ReturnToMainMenu:
                        ActionMenu.currentMenu = ActionMenu.CurrentMenu.MainMenu;
                        _gameConsoleView.DisplayGamePlayScreen("Current Location", Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                        break;

                    case PlayerAction.PlayerInfo:
                        _gameConsoleView.DisplayTravelerInfo();
                        break;

                    case PlayerAction.LookAround:
                        _gameConsoleView.DisplayLookAround();
                        break;

                    case PlayerAction.LookAt:
                        LookAtAction();
                        break;

                    case PlayerAction.Inventory:
                        _gameConsoleView.DisplayInventory();
                        break;

                    case PlayerAction.PickUp:
                        PickUpAction();
                        break;

                    case PlayerAction.Travel:
                        // Update accessibility
                        UpdateAccessibility();

                        //
                        // get new location choice and update the current location property
                        //                        
                        _gamePlayer.SpaceTimeLocationID = _gameConsoleView.DisplayGetNextSpaceTimeLocation();
                        _currentLocation = _gameUniverse.GetSpaceTimeLocationById(_gamePlayer.SpaceTimeLocationID);

                        //
                        // set the game play screen to the current location info format
                        //
                        _gameConsoleView.DisplayGamePlayScreen("Current Location", Text.CurrentLocationInfo(_currentLocation), ActionMenu.MainMenu, "");
                        break;

                    case PlayerAction.PlayerLocationsVisited:
                        _gameConsoleView.DisplayLocationsVisited();
                          break;

                    case PlayerAction.ListSpaceTimeLocations:
                        _gameConsoleView.DisplayListOfSpaceTimeLocations();
                        break;

                    case PlayerAction.ListGameObjects:
                        _gameConsoleView.DisplayListOfAllGameObjects();
                        break;

                    case PlayerAction.Exit:
                        _playingGame = false;
                        break;

                    default:
                        break;
                }
            }

            //
            // close the application
            //
            Environment.Exit(0);
        }

        /// <summary>
        /// initialize the player info
        /// </summary>
        private void InitializeMission()
        {
            Player player = _gameConsoleView.GetInitialPlayerInfo();

            _gamePlayer.Name = player.Name;
            _gamePlayer.Age = player.Age;
            _gamePlayer.Race = player.Race;
            _gamePlayer.Gender = player.Gender;
            _gamePlayer.SpaceTimeLocationID = 1;

            _gamePlayer.ExperiencePoints = 0;
            _gamePlayer.Health = 100;
            _gamePlayer.Lives = 3;
        }

        private void UpdateGameStatus()
        {
            if (!_gamePlayer.HasVisited(_currentLocation.SpaceTimeLocationID))
            {
                //
                // add new location to the list of visited locations if this is a first visit
                //
                _gamePlayer.SpaceTimeLocationsVisited.Add(_currentLocation.SpaceTimeLocationID);

                //
                // update experience points for visiting locations
                //
                _gamePlayer.ExperiencePoints += _currentLocation.ExperiencePoints;

                //
                // update health for visiting locations
                //
                _gamePlayer.Health += _currentLocation.Health;
            }
        }

        /// <summary>
        /// Change Room Accessibility based on User's Current Location
        /// </summary>
        /// <param name="currentLocation"></param>
        public void UpdateAccessibility()
        {
            _gameUniverse.UpdateLocation(_gamePlayer);
        }

        private void LookAtAction()
        {
            // display a list of traveler objects in space-time location and get a player choice
            int gameObjectToLookAtId = _gameConsoleView.DisplayGetGameObjectToLookAt();

            // display game object info
            if (gameObjectToLookAtId != 0)
            {
                // get game object from the universe
                GameObject gameObject = _gameUniverse.GetGameObjectById(gameObjectToLookAtId);

                // display information for the chosen object
                _gameConsoleView.DisplayGameObjectinfo(gameObject);
            }
        }

        private void PickUpAction()
        {
            //display a list of player objects in space-time location and get a player choice
            int playerObjectToPickUpId = _gameConsoleView.DisplayGetPlayerObjectToPickUp();

            //add the player object to player's inventory
            if (playerObjectToPickUpId != 0)
            {
                // get the game object from the universe
                PlayerObject playerObject = _gameUniverse.GetGameObjectById(playerObjectToPickUpId) as PlayerObject;

                // note: player object is added to list and the space-time location is set to 0
                _gamePlayer.Inventory.Add(playerObject);
                playerObject.SpaceTimeLocationId = 0;

                // display confirmation message
                _gameConsoleView.DisplayConfirmPlayerObjectAddedToInventory(playerObject);
            }
        }

        #endregion
    }
}
