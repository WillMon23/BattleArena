using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    // Test 

    /// <summary>
    /// Represents any entity that exists in game
    /// </summary>

    class Game
    {
        private string _playerName; 
        private bool _gameOver;
        private int _currentScene;
        private Entity _player;
        private Entity[] _enemies;
        private int _currentEnemyIndex = 0;
        private Entity _currentEnemy;

        /// <summary>
        /// Function that starts the main game loop
        /// </summary>
        public void Run()
        {
            Start();

            while(!_gameOver)
            {
                Update();

            }

            End();
        }

        /// <summary>
        /// Function used to initialize any starting values by default
        /// </summary>
        public void Start()
        {
            _gameOver = false;
            _currentScene = 0;
            InitilalzeEnemies();


        }

        public void InitilalzeEnemies()
        {
            _currentEnemyIndex = 0;
            //Slime Classification 
            Entity slimeEnemy = new Entity("Slime", 10f, 1f, 0f);

            //Zom=B Classidfication 
            Entity zombieEnemy = new Entity("Zom-B", 15f, 5f, 2f);

            //A Guy Name Kris Classification
            Entity unclePhil = new Entity("Uncle Phill", 40, 10, 5);

            _enemies = new Entity[] { slimeEnemy, zombieEnemy, unclePhil };
            
        }

        /// <summary>
        /// This function is called every time the game loops.
        /// </summary>
        public void Update()
        {
            DisplayCurrentScene();
        }

        /// <summary>
        /// This function is called before the applications closes
        /// </summary>
        public void End()
        {
            Console.WriteLine("FairWell Adventure");
            Console.ReadKey();
        }

        /// <summary>
        /// Gets an input from the player based on some given decision
        /// </summary>
        /// <param name="description">The context for the input</param>
        /// <param name="option1">The first option the player can choose</param>
        /// <param name="option2">The second option the player can choose</param>
        /// <returns></returns>
        int GetInput(string description, string option1, string option2)
        {
            string input = "";
            int inputReceived = 0;

            while (inputReceived != 1 && inputReceived != 2)
            {//Print options
                Console.WriteLine(description);
                Console.WriteLine("1. " + option1);
                Console.WriteLine("2. " + option2);
                Console.Write("> ");

                //Get input from player
                input = Console.ReadLine();

                //If player selected the first option...
                if (input == "1" || input == option1)
                {
                    //Set input received to be the first option
                    inputReceived = 1;
                }
                //Otherwise if the player selected the second option...
                else if (input == "2" || input == option2)
                {
                    //Set input received to be the second option
                    inputReceived = 2;
                }
                //If neither are true...
                else
                {
                    //...display error message
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey();
                }

                Console.Clear();
            }
            return inputReceived;
        }

        /// <summary>
        /// Calls the appropriate function(s) based on the current scene index
        /// </summary>
        void DisplayCurrentScene()
        {
            switch (_currentScene)
            {
                case 0:
                    GetPlayerName();
                    break;
                case 1:
                    CharacterSelection();
                    break;
                case 2:
                    Battle();
                    CheckBattleResults();
                    break;
                case 3:
                    DisplayMainMenu();
                    break;
            }
        }
        
        /// <summary>
        /// Displays the menu that allows the player to start or quit the game
        /// </summary>
        void DisplayMainMenu()
        {
            int choice = GetInput("Play Again", "Yes", "No?");

            switch (choice)
            {
                case 1:
                    _currentScene = 0;
                    _currentEnemyIndex = 0;
                    _currentEnemy = _enemies[_currentEnemyIndex];
                    InitilalzeEnemies();
                    break;
                case 2:
                    _gameOver = true;
                    break;
               
            }
        }

        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section
        /// until the player decides to keep the name.
        /// </summary>
        void GetPlayerName()
        {

                Console.WriteLine("Welcome! Please enter your name");
                Console.Write("> ");
               
                _playerName = Console.ReadLine();
                Console.Clear();

            if (GetInput("You've entered " + _playerName + " are you sure you want to keep this name?", "Yes", "No") == 1)
                _currentScene++;        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            int choice = GetInput("Nice To meet you, Please select a character.", "Wizard", "Knight");
            
            if ( choice == 1)
                //Wizard Classification 
                _player = new Entity(_playerName,50f, 25f, 0f);
            
            //Knight Classification
            else if(choice == 2)
                _player = new Entity(_playerName, 75f, 15f, 10f);

            _currentScene++;
        }


        public bool TryToEndSimulation()
        {
            bool simulation = _currentEnemyIndex >= _enemies.Length;
            if (simulation)
                _currentScene++;
            return simulation;

        }

        /// <summary>
        /// Prints a characters stats to the console
        /// </summary>
        /// <param name="character">The character that will have its stats shown</param>
        void DisplayStats(Entity character)
        {
            Console.Write("Name: " + character.Name +
                "\nHealth: " + character.Health +
                "\nAttack Power: " + character.AttackPower +
                "\nDefense Power: " + character.DefensePower + "\n>");
        }

        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        public void Battle()
        {
            Random rng = new Random();

            _currentEnemy = _enemies[_currentEnemyIndex];

            DisplayStats(_player);

            DisplayStats(_currentEnemy);

            int choice = GetInput(_currentEnemy + " stands in front of you! What will you do?", "Attack","Dodge");

            _currentEnemy.TakeDamage(_player.AttackPower);

            if (choice == 1)
            {
                float damageDealt = _player.Attack(_currentEnemy);
                Console.WriteLine("You delt " + damageDealt + " damage!");

            }
            else if (choice == 2)
            {
                Console.WriteLine("You dodged the enemy's attack!");
                
                Console.ReadKey();
                Console.Clear();
                return;
            }


             float damgeTaken = _currentEnemy.Attack(_player);
             Console.WriteLine(_currentEnemy.Name + " dealt " + damgeTaken);

            Console.ReadKey(true);
            Console.Clear();
        }

        /// <summary>
        /// Checks to see if either the player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        void CheckBattleResults()
        { 

            if (_player.Health <= 0)
            {
                Console.WriteLine("You Died");
                _currentScene++;
            }
            if (_currentEnemy.Health <= 0)
            {
                Console.WriteLine("You Slayed the " + _currentEnemy.Name);
                _currentEnemyIndex++;
                Console.ReadKey(true);
                Console.Clear();
            }

            if (_currentEnemyIndex >= _enemies.Length)
            {
                Console.WriteLine("They All Dead, Way To Goo");
                _currentScene++;

                Console.ReadKey(true);
                Console.Clear();

                return;
            }

        }

    }
}
