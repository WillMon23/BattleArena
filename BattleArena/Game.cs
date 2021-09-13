using System;
using System.Collections.Generic;
using System.Text;

namespace BattleArena
{
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
        private Character currentEnemy;

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
            krisEnemy.name = "A guy named Kris";
            krisEnemy.health = 25;
            krisEnemy.attackPower = 10;
            krisEnemy.defensePower = 5;

            enemies = new Character[] { slimeEnemy, zombieEnemy, krisEnemy };
            
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
                case 1:
                    DisplayMainMenu();
                    break;
                case 2:
                    Battle();
                    break;
                case 3:
                    CheckBattleResults();
                    break;
            }
        }

        /// <summary>
        /// Displays the menu that allows the player to start or quit the game
        /// </summary>
        void DisplayMainMenu()
        {
            int choice = GetInput("Welcome To Battle Arena", "Would You Like To Play?", "Would You Like to Quit-Out?");

            switch (choice)
            {
                case 1:
                    currentScene = 1;
                    break;
                case 2:
                    currentScene = 2;
                    break;
               
            }
        }

        /// <summary>
        /// Displays text asking for the players name. Doesn't transition to the next section
        /// until the player decides to keep the name.
        /// </summary>
        void GetPlayerName()
        {
           

            Console.WriteLine("Welcome! Please enter your name.");
            playerName = Console.ReadLine();
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
        }

        /// <summary>
        /// Update the current monster When Monster health hasReached 0
        /// </summary>
        public void UpdateCurrentEnemy()
        {
            if(currentEnemyIndex >= enemies.Length)
            {

            }
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
                "\nDefense Power: " + character.defensePower);
        }

        /// <summary>
        /// Calculates the amount of damage that will be done to a character
        /// </summary>
        /// <param name="attackPower">The attacking character's attack power</param>
        /// <param name="defensePower">The defending character's defense power</param>
        /// <returns>The amount of damage done to the defender</returns>
        float CalculateDamage(float attackPower, float defensePower)
        {
            float damage = 0;
            if(defensePower >= 0)
                damage = attackPower - defensePower;
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

            

            currentEnemy = enemies[currentEnemyIndex];
            DisplayStats(player);

            DisplayStats(currentEnemy);

            int choice = GetInput(currentEnemy.name + " stands in front of you! What will you do?", "Attack","Dodge");


            if(choice == 1)
            {
                float damageDealt = Attack(ref player, ref currentEnemy);
                Console.WriteLine("You delt " + damageDealt + " damage!");

                float damgeTaken = Attack(ref currentEnemy, ref player);
                Console.WriteLine(currentEnemy.name + " dealt " + damgeTaken);
            }
            if (choice == 2)
                Console.WriteLine("You dodged the enemy's attack!");
            


        }

        /// <summary>
        /// Checks to see if either the player or the enemy has won the current battle.
        /// Updates the game based on who won the battle..
        /// </summary>
        void CheckBattleResults()
        {
        }

    }
}
