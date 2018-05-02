﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    /// <summary>
    /// view class
    /// </summary>
    public class ConsoleView
    {
        #region ENUMS

        private enum ViewStatus
        {
            TravelerInitialization,
            PlayingGame
        }

        #endregion

        #region FIELDS

        //
        // declare game objects for the ConsoleView object to use
        //
        Player _gamePlayer;
        Universe _gameUniverse;

        ViewStatus _viewStatus;

        #endregion

        #region PROPERTIES

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// default constructor to create the console view objects
        /// </summary>
        public ConsoleView(Player gamePlayer, Universe gameUniverse)
        {
            _gamePlayer = gamePlayer;
            _gameUniverse = gameUniverse;

            _viewStatus = ViewStatus.TravelerInitialization;

            InitializeDisplay();
        }

        #endregion

        #region METHODS
        /// <summary>
        /// display all of the elements on the game play screen on the console
        /// </summary>
        /// <param name="messageBoxHeaderText">message box header title</param>
        /// <param name="messageBoxText">message box text</param>
        /// <param name="menu">menu to use</param>
        /// <param name="inputBoxPrompt">input box text</param>
        public void DisplayGamePlayScreen(string messageBoxHeaderText, string messageBoxText, Menu menu, string inputBoxPrompt)
        {
            //
            // reset screen to default window colors
            //
            Console.BackgroundColor = ConsoleTheme.WindowBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.WindowForegroundColor;
            Console.Clear();

            ConsoleWindowHelper.DisplayHeader(Text.HeaderText);
            ConsoleWindowHelper.DisplayFooter(Text.FooterText);

            DisplayMessageBox(messageBoxHeaderText, messageBoxText);
            DisplayMenuBox(menu);
            DisplayInputBox();
            DisplayStatusBox();
        }

        /// <summary>
        /// wait for any keystroke to continue
        /// </summary>
        public void GetContinueKey()
        {
            Console.ReadKey();
        }

        /// <summary>
        /// get a action menu choice from the user
        /// </summary>
        /// <returns>action menu choice</returns>
        public PlayerAction GetActionMenuChoice(Menu menu)
        {
            PlayerAction choosenAction = PlayerAction.None;
            Console.CursorVisible = false;

            //
            // create an array of valid keys from menu dictionary
            //
            char[] validKeys = menu.MenuChoices.Keys.ToArray();

            //
            // validate key pressed as in MenuChoices dictionary
            //
            char keyPressed;
            do
            {
                ConsoleKeyInfo keyPressedInfo = Console.ReadKey(true);
                keyPressed = keyPressedInfo.KeyChar;
            } while (!validKeys.Contains(keyPressed));

            choosenAction = menu.MenuChoices[keyPressed];
            Console.CursorVisible = true;

            return choosenAction;
        }

        /// <summary>
        /// get a string value from the user
        /// </summary>
        /// <returns>string value</returns>
        public string GetString()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// get an integer value from the user
        /// </summary>
        /// <returns>integer value</returns>
        private bool GetInteger(string prompt, int minimumValue, int maximumValue, out int integerChoice)
        {
            bool validResponse = false;
            integerChoice = 0;

            // Validate on range if either minimumValue and maximumValue are not 0
            bool validateRange = (minimumValue != 0 || maximumValue != 0);

            DisplayInputBoxPrompt(prompt);
            while (!validResponse)
            {
                if (int.TryParse(Console.ReadLine(), out integerChoice))
                {
                    if (validateRange)
                    {
                        if (integerChoice >= minimumValue && integerChoice <= maximumValue)
                        {
                            validResponse = true;
                        }
                        else
                        {
                            ClearInputBox();
                            DisplayInputErrorMessage($"You must enter an integer value between {minimumValue} and {maximumValue}. Please try again.");
                            DisplayInputBoxPrompt(prompt);
                        }
                    }
                }
                else
                {
                    ClearInputBox();
                    DisplayInputErrorMessage($"You must enter an integer value. Please try again.");
                    DisplayInputBoxPrompt(prompt);
                }
            }

            Console.CursorVisible = false;

            return true;
        }

        /// <summary>
        /// get an integer value from the user
        /// </summary>
        /// <returns>integer value</returns>
        public bool GetDouble(string prompt, double minimumValue, double maximumValue, out double doubleChoice)
        {
            bool validResponse = false;
            doubleChoice = 0;

            DisplayInputBoxPrompt(prompt);
            while (!validResponse)
            {
                if (double.TryParse(Console.ReadLine(), out doubleChoice))
                {
                    if (doubleChoice >= minimumValue && doubleChoice <= maximumValue)
                    {
                        validResponse = true;
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage($"You must enter an number value between {minimumValue} and {maximumValue}. Please try again.");
                        DisplayInputBoxPrompt(prompt);
                    }
                }
                else
                {
                    ClearInputBox();
                    DisplayInputErrorMessage($"You must enter an number value between {minimumValue} and {maximumValue}. Please try again.");
                    DisplayInputBoxPrompt(prompt);
                }
            }

            return true;
        }

        /// <summary>
        /// get a character race value from the user
        /// </summary>
        /// <returns>character race value</returns>
        public Character.RaceType GetRace()
        {
            Character.RaceType raceType;
            Enum.TryParse<Character.RaceType>(Console.ReadLine(), out raceType);

            return raceType;
        }

        /// <summary>
        /// get a character race value from the user
        /// </summary>
        /// <returns>character race value</returns>
        public Player.PlayerGender GetGender()
        {
            Player.PlayerGender gender;


            Enum.TryParse<Player.PlayerGender>(Console.ReadLine(), out gender);



            return gender;
        }

        /// <summary>
        /// display splash screen
        /// </summary>
        /// <returns>player chooses to play</returns>
        public bool DisplaySpashScreen()
        {
            bool playing = true;
            ConsoleKeyInfo keyPressed;

            Console.BackgroundColor = ConsoleTheme.SplashScreenBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.SplashScreenForegroundColor;
            Console.Clear();
            Console.CursorVisible = false;


            Console.SetCursorPosition(0, 10);
            string tabSpace = new String(' ', 15);
            Console.WriteLine(tabSpace + @".___________. __    __   _______      ______      __    __       ___       __  ___  _______ .__   __.  __  .__   __.   _______ ");
            Console.WriteLine(tabSpace + @"|           ||  |  |  | |   ____|    /  __  \    |  |  |  |     /   \     |  |/  / |   ____||  \ |  | |  | |  \ |  |  /  _____|");
            Console.WriteLine(tabSpace + @"`---|  |----`|  |__|  | |  |__      |  |  |  |   |  |  |  |    /  ^  \    |  '  /  |  |__   |   \|  | |  | |   \|  | |  |  __  ");
            Console.WriteLine(tabSpace + @"    |  |     |   __   | |   __|     |  |  |  |   |  |  |  |   /  /_\  \   |    <   |   __|  |  . `  | |  | |  . `  | |  | |_ | ");
            Console.WriteLine(tabSpace + @"    |  |     |  |  |  | |  |____    |  `--'  '--.|  `--'  |  /  _____  \  |  .  \  |  |____ |  |\   | |  | |  |\   | |  |__| | ");
            Console.WriteLine(tabSpace + @"    |__|     |__|  |__| |_______|    \_____\_____\\______/  /__/     \__\ |__|\__\ |_______||__| \__| |__| |__| \__|  \______| ");
            Console.WriteLine(tabSpace + @"                                                                                                                               ");
            Console.WriteLine(tabSpace + @"                                                                         ");

            Console.SetCursorPosition(80, 25);
            Console.Write("Press any key to continue or Esc to exit.");
            keyPressed = Console.ReadKey();
            if (keyPressed.Key == ConsoleKey.Escape)
            {
                playing = false;
            }

            return playing;
        }

        /// <summary>
        /// initialize the console window settings
        /// </summary>
        private static void InitializeDisplay()
        {
            //
            // control the console window properties
            //
            ConsoleWindowControl.DisableResize();
            ConsoleWindowControl.DisableMaximize();
            ConsoleWindowControl.DisableMinimize();
            Console.Title = "The Aion Project";

            //
            // set the default console window values
            //
            ConsoleWindowHelper.InitializeConsoleWindow();

            Console.CursorVisible = false;
        }

        /// <summary>
        /// display the correct menu in the menu box of the game screen
        /// </summary>
        /// <param name="menu">menu for current game state</param>
        private void DisplayMenuBox(Menu menu)
        {
            Console.BackgroundColor = ConsoleTheme.MenuBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MenuBorderColor;

            //
            // display menu box border
            //
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.MenuBoxPositionTop,
                ConsoleLayout.MenuBoxPositionLeft,
                ConsoleLayout.MenuBoxWidth,
                ConsoleLayout.MenuBoxHeight);

            //
            // display menu box header
            //
            Console.BackgroundColor = ConsoleTheme.MenuBorderColor;
            Console.ForegroundColor = ConsoleTheme.MenuForegroundColor;
            Console.SetCursorPosition(ConsoleLayout.MenuBoxPositionLeft + 2, ConsoleLayout.MenuBoxPositionTop + 1);
            Console.Write(ConsoleWindowHelper.Center(menu.MenuTitle, ConsoleLayout.MenuBoxWidth - 4));

            //
            // display menu choices
            //
            Console.BackgroundColor = ConsoleTheme.MenuBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MenuForegroundColor;
            int topRow = ConsoleLayout.MenuBoxPositionTop + 3;

            foreach (KeyValuePair<char, PlayerAction> menuChoice in menu.MenuChoices)
            {
                if (menuChoice.Value != PlayerAction.None)
                {
                    string formatedMenuChoice = ConsoleWindowHelper.ToLabelFormat(menuChoice.Value.ToString());
                    Console.SetCursorPosition(ConsoleLayout.MenuBoxPositionLeft + 3, topRow++);
                    Console.Write($"{menuChoice.Key}. {formatedMenuChoice}");
                }
            }
        }

        /// <summary>
        /// display the text in the message box of the game screen
        /// </summary>
        /// <param name="headerText"></param>
        /// <param name="messageText"></param>
        private void DisplayMessageBox(string headerText, string messageText)
        {
            //
            // display the outline for the message box
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MessageBoxBorderColor;
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.MessageBoxPositionTop,
                ConsoleLayout.MessageBoxPositionLeft,
                ConsoleLayout.MessageBoxWidth,
                ConsoleLayout.MessageBoxHeight);

            //
            // display message box header
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBorderColor;
            Console.ForegroundColor = ConsoleTheme.MessageBoxForegroundColor;
            Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, ConsoleLayout.MessageBoxPositionTop + 1);
            Console.Write(ConsoleWindowHelper.Center(headerText, ConsoleLayout.MessageBoxWidth - 4));

            //
            // display the text for the message box
            //
            Console.BackgroundColor = ConsoleTheme.MessageBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.MessageBoxForegroundColor;
            List<string> messageTextLines = new List<string>();
            messageTextLines = ConsoleWindowHelper.MessageBoxWordWrap(messageText, ConsoleLayout.MessageBoxWidth - 4);

            int startingRow = ConsoleLayout.MessageBoxPositionTop + 3;
            int endingRow = startingRow + messageTextLines.Count();
            int row = startingRow;
            foreach (string messageTextLine in messageTextLines)
            {
                Console.SetCursorPosition(ConsoleLayout.MessageBoxPositionLeft + 2, row);
                Console.Write(messageTextLine);
                row++;
            }

        }

        /// <summary>
        /// draw the status box on the game screen
        /// </summary>
        public void DisplayStatusBox()
        {
            Console.BackgroundColor = ConsoleTheme.InputBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.InputBoxBorderColor;

            //
            // display the outline for the status box
            //
            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.StatusBoxPositionTop,
                ConsoleLayout.StatusBoxPositionLeft,
                ConsoleLayout.StatusBoxWidth,
                ConsoleLayout.StatusBoxHeight);

            //
            // display the text for the status box if playing game
            //
            if (_viewStatus == ViewStatus.PlayingGame)
            {
                //
                // display status box header with title
                //
                Console.BackgroundColor = ConsoleTheme.StatusBoxBorderColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;
                Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 2, ConsoleLayout.StatusBoxPositionTop + 1);
                Console.Write(ConsoleWindowHelper.Center("Game Stats", ConsoleLayout.StatusBoxWidth - 4));
                Console.BackgroundColor = ConsoleTheme.StatusBoxBackgroundColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;

                //
                // display stats
                //
                int startingRow = ConsoleLayout.StatusBoxPositionTop + 3;
                int row = startingRow;
                foreach (string statusTextLine in Text.StatusBox(_gamePlayer, _gameUniverse))
                {
                    Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 3, row);
                    Console.Write(statusTextLine);
                    row++;
                }
            }
            else
            {
                //
                // display status box header without header
                //
                Console.BackgroundColor = ConsoleTheme.StatusBoxBorderColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;
                Console.SetCursorPosition(ConsoleLayout.StatusBoxPositionLeft + 2, ConsoleLayout.StatusBoxPositionTop + 1);
                Console.Write(ConsoleWindowHelper.Center("", ConsoleLayout.StatusBoxWidth - 4));
                Console.BackgroundColor = ConsoleTheme.StatusBoxBackgroundColor;
                Console.ForegroundColor = ConsoleTheme.StatusBoxForegroundColor;
            }
        }

        /// <summary>
        /// draw the input box on the game screen
        /// </summary>
        public void DisplayInputBox()
        {
            Console.BackgroundColor = ConsoleTheme.InputBoxBackgroundColor;
            Console.ForegroundColor = ConsoleTheme.InputBoxBorderColor;

            ConsoleWindowHelper.DisplayBoxOutline(
                ConsoleLayout.InputBoxPositionTop,
                ConsoleLayout.InputBoxPositionLeft,
                ConsoleLayout.InputBoxWidth,
                ConsoleLayout.InputBoxHeight);
        }

        /// <summary>
        /// display the prompt in the input box of the game screen
        /// </summary>
        /// <param name="prompt"></param>
        public void DisplayInputBoxPrompt(string prompt)
        {
            Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + 1);
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
            Console.Write(prompt);
            Console.CursorVisible = true;
        }

        /// <summary>
        /// display the error message in the input box of the game screen
        /// </summary>
        /// <param name="errorMessage">error message text</param>
        public void DisplayInputErrorMessage(string errorMessage)
        {
            Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + 2);
            Console.ForegroundColor = ConsoleTheme.InputBoxErrorMessageForegroundColor;
            Console.Write(errorMessage);
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
            Console.CursorVisible = true;
        }

        /// <summary>
        /// clear the input box
        /// </summary>
        private void ClearInputBox()
        {
            string backgroundColorString = new String(' ', ConsoleLayout.InputBoxWidth - 4);

            Console.ForegroundColor = ConsoleTheme.InputBoxBackgroundColor;
            for (int row = 1; row < ConsoleLayout.InputBoxHeight - 2; row++)
            {
                Console.SetCursorPosition(ConsoleLayout.InputBoxPositionLeft + 4, ConsoleLayout.InputBoxPositionTop + row);
                DisplayInputBoxPrompt(backgroundColorString);
            }
            Console.ForegroundColor = ConsoleTheme.InputBoxForegroundColor;
        }

        public void DisplayListOfAllGameObjects()
        {
            DisplayGamePlayScreen("List: Game Objects", Text.ListAllGameObjects(_gameUniverse.GameObjects), ActionMenu.AdminMenu, "");
        }

        public void DisplayInventory()
        {
            DisplayGamePlayScreen("Current Inventory", Text.CurrentInventory(_gamePlayer.Inventory), ActionMenu.MainMenu, "");
        }

        public int DisplayGetGameObjectToLookAt()
        {
            int gameObjectId = 0;
            bool validGamerObjectId = false;

            // get a list of game objects in the current space-time location
            List<GameObject> gameObjectsInSpaceTimeLocation = _gameUniverse.GetGameObjectsBySpaceTimeLocationId(_gamePlayer.SpaceTimeLocationID);

            if (gameObjectsInSpaceTimeLocation.Count > 0)
            {
                DisplayGamePlayScreen("Look at an Object", Text.GameObjectsChooseList(gameObjectsInSpaceTimeLocation), ActionMenu.MainMenu, "");

                while (!validGamerObjectId)
                {
                    // get an integer
                    GetInteger($"Enter the Id number of the object you wish to look at: ", 0, 0, out gameObjectId);

                    // validate integer as a valid game object id and in current location
                    if (_gameUniverse.IsValidGameObjectByLocation(gameObjectId, _gamePlayer.SpaceTimeLocationID))
                    {
                        validGamerObjectId = true;
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("It appears you entered an invalid game object id. Please try again.");
                    }
                }
            }
            else
            {
                DisplayGamePlayScreen("Look at an Object", "It appears there are no game objects here.", ActionMenu.MainMenu, "");
            }

            return gameObjectId;
        }

        public void DisplayGameObjectinfo(GameObject gameObject)
        {
            DisplayGamePlayScreen("Current Location", Text.LookAt(gameObject), ActionMenu.MainMenu, "");
        }

        /// <summary>
        /// get the player's initial information at the beginning of the game
        /// </summary>
        /// <returns>traveler object with all properties updated</returns>
        public Player GetInitialPlayerInfo()
        {
            Player player = new Player();

            //
            // intro
            //
            DisplayGamePlayScreen("Mission Initialization", Text.InitializeMissionIntro(), ActionMenu.MissionIntro, "");
            GetContinueKey();

            //
            // get traveler's name
            //
            DisplayGamePlayScreen("Mission Initialization - Name", Text.InitializeMissionGetPlayerName(), ActionMenu.MissionIntro, "");
            DisplayInputBoxPrompt("Enter your name: ");
            player.Name = GetString();

            //
            // get traveler's age
            //
            DisplayGamePlayScreen("Mission Initialization - Age", Text.InitializeMissionGetPlayerAge(player.Name), ActionMenu.MissionIntro, "");
            int gamePlayerAge;

            GetInteger($"Enter your age {player.Name}: ", 0, 125, out gamePlayerAge);
            player.Age = gamePlayerAge;

            //
            // get traveler's race
            //
            DisplayGamePlayScreen("Mission Initialization - Race", Text.InitializeMissionGetPlayerRace(player), ActionMenu.MissionIntro, "");
            DisplayInputBoxPrompt($"Enter your race {player.Name}: ");
            player.Race = GetRace();

            //
            // Get traveler's gender
            //
            DisplayGamePlayScreen("Mission Initialization - Gender", Text.InitializeMissionGetPlayerGender(player.Name), ActionMenu.MissionIntro, "");
            DisplayInputBoxPrompt("Enter your gender: ");

            Player.PlayerGender theirGender;
            theirGender = GetGender();


            if (theirGender == Player.PlayerGender.Male)
            {
                // Sets travelers gender to true for male
                player.Gender = true;
            }
            else if (theirGender == Player.PlayerGender.Female)
            {
                // Sets travelers gender to false for female
                player.Gender = false;
            }
            else
            {
                player.Gender = true;
            }

            //
            // echo the traveler's info
            //
            DisplayGamePlayScreen("Mission Initialization - Complete", Text.InitializeMissionEchoPlayerInfo(player), ActionMenu.MissionIntro, "");
            GetContinueKey();

            // 
            // change view status to playing game
            //
            _viewStatus = ViewStatus.PlayingGame;

            return player;
        }

        #region ----- display responses to menu action choices -----

        public void DisplayTravelerInfo()
        {
            SpaceTimeLocation currentSpaceTimeLocation = _gameUniverse.GetSpaceTimeLocationById(_gamePlayer.SpaceTimeLocationID);
            DisplayGamePlayScreen("Traveler Information", Text.PlayerInfo(_gamePlayer, currentSpaceTimeLocation), ActionMenu.MainMenu, "");
        }

        public void DisplayCurrentLocationInfo()
        {
            SpaceTimeLocation currentSpaceTimeLocation = _gameUniverse.GetSpaceTimeLocationById(_gamePlayer.SpaceTimeLocationID);
            DisplayGamePlayScreen("Current Location", Text.CurrentLocationInfo(currentSpaceTimeLocation), ActionMenu.MainMenu, "");
        }

        public void DisplayLookAround()
        {
            //get current space-time location
            SpaceTimeLocation currentSpaceTimeLocation = _gameUniverse.GetSpaceTimeLocationById(_gamePlayer.SpaceTimeLocationID);

            // get list of game objects in current space-time location
            List<GameObject> gameObjectsInCurrentSpaceTimeLocation = _gameUniverse.GetGameObjectsBySpaceTimeLocationId(_gamePlayer.SpaceTimeLocationID);

            string messageBoxText = Text.LookAround(currentSpaceTimeLocation) + Environment.NewLine + Environment.NewLine;
            messageBoxText += Text.GameObjectsChooseList(gameObjectsInCurrentSpaceTimeLocation);

            DisplayGamePlayScreen("Current Location", messageBoxText, ActionMenu.MainMenu, "");
        }

        public int DisplayGetNextSpaceTimeLocation()
        {
            int spaceTimeLocationId = 0;
            bool validSpaceTimeLocationId = false;

            DisplayGamePlayScreen("Travel to a New Space-Time Location", Text.Travel(_gamePlayer, _gameUniverse.SpaceTimeLocations), ActionMenu.AdminMenu, "");

            while (!validSpaceTimeLocationId)
            {
                //
                // get an integer from the player
                //
                GetInteger($"Enter your new location {_gamePlayer.Name}: ", 1, _gameUniverse.GetMaxSpaceTimeLocationId(), out spaceTimeLocationId);

                //
                // validate integer as a valid space-time location id and determine accessibility
                //
                if (_gameUniverse.IsValidSpaceTimeLocationId(spaceTimeLocationId))
                {
                    if (_gameUniverse.GetSpaceTimeLocationById(spaceTimeLocationId).Accessible)
                    {
                        validSpaceTimeLocationId = true;
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("It appears you attempting to travel to an inaccessible location. Please try again.");
                    }
                }
                else
                {
                    DisplayInputErrorMessage("It appears you entered an invalid Space-Time location id. Please try again.");
                }
            }

            return spaceTimeLocationId;
        }

        public void DisplayLocationsVisited()
        {
            //
            // generate a list of space time locations that have been visited
            //
            List<SpaceTimeLocation> visitedSpaceTimeLocations = new List<SpaceTimeLocation>();
            foreach (int spaceTimeLocationId in _gamePlayer.SpaceTimeLocationsVisited)
            {
                visitedSpaceTimeLocations.Add(_gameUniverse.GetSpaceTimeLocationById(spaceTimeLocationId));
            }

            DisplayGamePlayScreen("Space-Time Locations Visited", Text.VisitedLocations(visitedSpaceTimeLocations), ActionMenu.MainMenu, "");
        }

        public void DisplayListOfSpaceTimeLocations()
        {
            DisplayGamePlayScreen("List: Space-Time Locations", Text.ListSpaceTimeLocations(_gameUniverse.SpaceTimeLocations), ActionMenu.MainMenu, "");
        }

        public int DisplayGetPlayerObjectToPickUp()
        {
            int gameObjectId = 0;
            bool validGamerObjectId = false;

            // get a list of player objects in the current space-time location
            List<PlayerObject> playerObjectsInSpaceTimeLocation = _gameUniverse.GetPlayerObjectsBySpaceTimeLocation(_gamePlayer.SpaceTimeLocationID);

            if (playerObjectsInSpaceTimeLocation.Count > 0)
            {
                DisplayGamePlayScreen("Pick Up Game Object", Text.GameObjectsChooseList(playerObjectsInSpaceTimeLocation), ActionMenu.MainMenu, "");

                while (!validGamerObjectId)
                {
                    // get an integer from the player
                    GetInteger($"Enter the Id number of the object you wish to add to your inventory: ", 0, 0, out gameObjectId);

                    // validate integer as a valid game object id and in current location
                    if (_gameUniverse.IsValidPlayerObjectByLocationId(gameObjectId, _gamePlayer.SpaceTimeLocationID))
                    {
                        PlayerObject playerObject = _gameUniverse.GetGameObjectById(gameObjectId) as PlayerObject;
                        if (playerObject.CanInventory)
                        {
                            validGamerObjectId = true;
                        }
                        else
                        {
                            ClearInputBox();
                            DisplayInputErrorMessage("It appears you may not inventory that object. Please try again.");
                        }
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("It appears you entered an invalid game object id. Please try again.");
                    }
                }
            }
            else
            {
                DisplayGamePlayScreen("Pick Up Game Object", "It appears there are no game objects here.", ActionMenu.MainMenu, "");
            }

            return gameObjectId;
        }

        public void DisplayConfirmPlayerObjectAddedToInventory(PlayerObject objectAddedToInventory)
        {
            DisplayGamePlayScreen("Pick Up Game Object", $"The {objectAddedToInventory.Name} has been added to your inventory.", ActionMenu.MainMenu, "");
        }

        public int DisplayGetInventoryObjectToPutDown()
        {
            int playerObjectId = 0;
            bool validInventoryObjectId = false;

            if (_gamePlayer.Inventory.Count > 0)
            {
                DisplayGamePlayScreen("Put Down Game Object", Text.GameObjectsChooseList(_gamePlayer.Inventory), ActionMenu.MainMenu, "");

                while (!validInventoryObjectId)
                {
                    // get an integer from the player
                    GetInteger($"Enter the Id of the object you wish to remove from your inventory: ", 0, 0, out playerObjectId);

                    //find object in inventory
                    //note: LINQ used, but a foreach loop may also be used
                    PlayerObject objectToPutDown = _gamePlayer.Inventory.FirstOrDefault(o => o.Id == playerObjectId);

                    // validate object in inventory
                    if (objectToPutDown != null)
                    {
                        validInventoryObjectId = true;
                    }
                    else
                    {
                        ClearInputBox();
                        DisplayInputErrorMessage("It appears you entered the Id of an object not in your inventory. Please try again.");
                    }
                }
            }
            else
            {
                DisplayGamePlayScreen("Put Down Game Object", "It appears there are no objects currently in inventory.", ActionMenu.MainMenu, "");
            }

            return playerObjectId;
        }

        public void DisplayConfirmPlayerObjectRemovedFromInventory(PlayerObject objectRemovedFromInventory)
        {
            DisplayGamePlayScreen("Put Down Game Object", $"The {objectRemovedFromInventory.Name} has been removed from your inventory.", ActionMenu.MainMenu, "");
        }
        #endregion

        #endregion
    }
}
