using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
    // Test 

    /// <summary>
    /// Represents any entity that exists in game
    /// </summary>
    struct Character
    {
        public string name;
        public float health;
        public float attackPower;
        public float defensePower;
    }

    class Game
    {
        string playerName; 
        bool gameOver;
        int currentScene;
        Character player;
        Character[] enemies;
        private int currentEnemyIndex = 0;

        Character wizardClass;
        Character knightClass;

        Character slimeEnemy;
        Character zombieEnemy;
        Character krisEnemy;


        /// <summary>
        /// Function that starts the main game loop
        /// </summary>
        public void Run()
        {
            Start();

            while(!gameOver)
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
            //Wizard Classification 
            wizardClass.health = 50f;
            wizardClass.attackPower = 25f;
            wizardClass.defensePower = 0f;

            //Knight Classification 

            knightClass.health = 75f;
            knightClass.attackPower = 15f;
            knightClass.defensePower = 10f;

            //Slime Classification 
            slimeEnemy.name = "Slime";
            slimeEnemy.health = 10f;
            slimeEnemy.defensePower = 1f;
            slimeEnemy.defensePower = 0f;

            //Zom=B Classidfication 
            zombieEnemy.name = "Zom-B";
            zombieEnemy.health = 15f;
            zombieEnemy.attackPower = 5f;
            zombieEnemy.defensePower = 2f;

            //A Guy Name Kris Classification '
            krisEnemy.name = "Uncle Phill";
            krisEnemy.health = 25;
            krisEnemy.attackPower = 10;
            krisEnemy.defensePower = 5;

            enemies = new Character[] { slimeEnemy, zombieEnemy, krisEnemy };
            currentEnemyIndex = 0;

            
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
            switch (currentScene)
            {
                case 0:
                    GetPlayerName();
                    CharacterSelection();
                    break;
                case 1:
                    Battle();
                    UpdateCurrentEnemy();
                    break;
                case 2:
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
                    Start();
                    currentScene = 0;
                    break;
                case 2:
                    gameOver = true;
                    break;
               
            }
        }

        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section
        /// until the player decides to keep the name.
        /// </summary>
        void GetPlayerName()
        {
            bool okayWithName = false;
            while (!okayWithName) {
                Console.WriteLine("Welcome! Please enter your name");
                Console.Write("> ");
               
                playerName = Console.ReadLine();
                Console.Clear();

                if(GetInput("You've entered " + playerName + " are you sure you want to keep this name?", "Yes", "No") == 1)
                    okayWithName = true;
            }
             
        }

        /// <summary>
        /// Gets the players choice of character. Updates player stats based on
        /// the character chosen.
        /// </summary>
        public void CharacterSelection()
        {
            int choice = GetInput("Nice To meet you, Please select a character.", "Wizard", "Knight");
            
            if ( choice == 1)
            {
                player = wizardClass;
            }
            else if(choice == 2)
            {
                player = knightClass;
            }
            
            player.name = playerName;
            currentScene++;
        }

        /// <summary>
        /// Update the current monster When Monster health hasReached 0
        /// </summary>
        public void UpdateCurrentEnemy()
        {
            if(currentEnemyIndex >= enemies.Length)
            {
                currentScene = 2;
            }

            if (enemies[currentEnemyIndex].health <= 0)
            {
                Console.WriteLine("You Slayed the " + enemies[currentEnemyIndex].name);
                currentEnemyIndex++;
                Console.ReadKey();
                Console.Clear();

                if (TryToEndSimulation())
                    return;
            }
          
        }

        public bool TryToEndSimulation()
        {
            bool simulation = currentEnemyIndex >= enemies.Length;
            if (simulation)
                currentScene = 2;
            return simulation;

        }

        /// <summary>
        /// Prints a characters stats to the console
        /// </summary>
        /// <param name="character">The character that will have its stats shown</param>
        void DisplayStats(Character character)
        {
            Console.WriteLine("Name: " + character.name +
                "\nHealth: " + character.health +
                "\nAttack Power: " + character.attackPower +
                "\nDefense Power: " + character.defensePower + "\n");
        }

        /// <summary>
        /// Calculates the amount of damage that will be done to a character
        /// </summary>
        /// <param name="attackPower">The attacking character's attack power</param>
        /// <param name="defensePower">The defending character's defense power</param>
        /// <returns>The amount of damage done to the defender</returns>
        float CalculateDamage(float attackPower, float defensePower)
        {
            float damage = attackPower - defensePower;
            if (damage <= 0)
                return 0;
            return damage;
        }

        /// <summary>
        /// Deals damage to a character based on an attacker's attack power
        /// </summary>
        /// <param name="attacker">The character that initiated the attack</param>
        /// <param name="defender">The character that is being attacked</param>
        /// <returns>The amount of damage done to the defender</returns>
        public float Attack(ref Character attacker, ref Character defender)
        {
            float damageTaken = CalculateDamage(attacker.attackPower, defender.defensePower);
            defender.health -= damageTaken;
            return damageTaken;

        }

        /// <summary>
        /// Simulates one turn in the current monster fight
        /// </summary>
        public void Battle()
        {
            Random rng = new Random();

            

            DisplayStats(player);

            DisplayStats(enemies[currentEnemyIndex]);

            int choice = GetInput(enemies[currentEnemyIndex].name + " stands in front of you! What will you do?", "Attack","Dodge");

            

            if (choice == 1)
            {
                float damageDealt = Attack(ref player, ref enemies[currentEnemyIndex]);
                Console.WriteLine("You delt " + damageDealt + " damage!");

                float damgeTaken = Attack(ref enemies[currentEnemyIndex], ref player);
                Console.WriteLine(enemies[currentEnemyIndex].name + " dealt " + damgeTaken);

                Console.ReadKey();
                Console.Clear();

              
            }
            else if (choice == 2)
            {
                Console.WriteLine("You dodged the enemy's attack!");

                Console.ReadKey();
                Console.Clear();
            }

            CheckBattleResults();


        }

        /// <summary>
        /// Checks to see if either the player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        void CheckBattleResults()
        {

            if (player.health <= 0 && enemies[currentEnemyIndex].health <= 0)
                Console.WriteLine("Draw");

            else if (player.health <= 0)
            {
                Console.WriteLine("You Died");
                currentScene++;
            }


        }

    }
}
