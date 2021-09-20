using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    // Test 

    /// <summary>
    /// Represents any entity that exists in game
    /// </summary>
    public enum ItemType
    {
        DEFENSE,
        ATTACK,
        NONE
    }
    public struct Item
    {
        public string Name;
        public float StatBoost;
        public ItemType Type;

    }
    class Game
    {
        private string _playerName; 
        private bool _gameOver;
        private int _currentScene;
        private Player _player;
        private Entity[] _enemies;
        private int _currentEnemyIndex = 0;
        private Entity _currentEnemy;

        private Item[] _wizardItems;
        private Item[] _knightItems;

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
            InitilizeItems();


        }

        public void InitilizeItems()
        {
            //WIzard Items 
            Item bigWand = new Item { Name = "Big Wand", StatBoost = 5, Type = ItemType.ATTACK };
            Item bigShield = new Item { Name = "Big Shield", StatBoost = 15 , Type = ItemType.DEFENSE};

            //Kight items
            Item wand = new Item { Name = "Wand", StatBoost = 1025f, Type = ItemType.ATTACK};
            Item shoes = new Item { Name = "Shoes", StatBoost = 9000.05f, Type = ItemType.DEFENSE};

            // Initilize Arry
            _wizardItems = new Item[] { bigWand, bigShield };
            _knightItems = new Item[] { wand, shoes };
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
        int GetInput(string description, params string[] options )
        {
            string input = "";
            int inputReceived = -1;

            while (inputReceived == -1)
            {
                //Print options
                Console.WriteLine(description);
                for (int i = 0; i < options.Length; i++)
                    Console.WriteLine((i + 1) + ". " + options[i]);

                Console.Write("> ");

                //Set Current Response to a Obtainable Variable
                input = Console.ReadLine();

                //If the player typed and int...
                if (int.TryParse(input, out inputReceived))
                {
                    //... decrement the input and check if it's within the bounds the array 
                    inputReceived--;
                    if (inputReceived < 0 || inputReceived >= options.Length)
                    {
                        //sets invalde input to default value
                        inputReceived = -1;
                        //Display error message 
                        Console.WriteLine("Invalid Input");
                        Console.ReadKey(true);
                    }
                    Console.Clear();
                }
                //If the player didn't type an int 
                else
                {
                    //set input recived to the default value 
                    inputReceived = -1;
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey(true);
                }
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
                case 0:
                    _currentScene = 0;
                    _currentEnemyIndex = 0;
                    _currentEnemy = _enemies[_currentEnemyIndex];
                    InitilalzeEnemies();
                    break;
                case 1:
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

            if (GetInput("You've entered " + _playerName + " are you sure you want to keep this name?", "Yes", "No") == 0)
                _currentScene++;        
        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            int choice = GetInput("Nice To meet you, Please select a character.", "Wizard", "Knight");

            if (choice == 0)
                //Wizard Classification 
                _player = new Player(_playerName, 50f, 25f, 0f, _wizardItems);

            //Knight Classification
            else if (choice == 1)
                _player = new Player(_playerName, 75f, 15f, 10f, _knightItems);

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

        public void DisplayEquipItemMenu()
        {
            //Get item index 
            int choice = GetInput("Select an item to equip.", _player.GetItemNames());
            //Equip item at the given index 
            if (!_player.TryEquipItem(choice))
                //Notifies if not possible to equip
                Console.WriteLine("You couldn't find that item in your bag.");
            //Print Feed Back 
            Console.WriteLine("You equipped " + _player.CurrentItem.Name + "!");

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

            int choice = GetInput(_currentEnemy.Name + " stands in front of you! What will you do?", "Attack","Equip Item","Remove Equiped Item");            

            if (choice == 0)
            {
                float damageDealt = _player.Attack(_currentEnemy);
                _currentEnemy.TakeDamage(_player.AttackPower);
                Console.WriteLine("You delt " + damageDealt + " damage!");
                
                return;

            }
            else if (choice == 1)
            {
                DisplayEquipItemMenu();
                Console.ReadKey(true);
                Console.Clear();
                return;
            }
            else if (choice == 2)
            {
                if (!_player.TryToRemoveCurrentItm())
                    Console.WriteLine("You Don't have anything equiped.");
                else
                    Console.WriteLine("You placed the item in your bag.");

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
            else if (_currentEnemy.Health <= 0)
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
