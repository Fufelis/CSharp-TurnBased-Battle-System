using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG_Turn_Based_Battle_System.Spells;

namespace RPG_Turn_Based_Battle_System.Core
{
    public enum GameState
    {
        MainMenu,
        Battle,
        VictoryScreen,
        GameOver
    }

    internal class Game
    {
        private bool isRunning = true;
        private GameState currentGameState;

        public void Run()
        {
            currentGameState = GameState.MainMenu;

            while (isRunning)
            {
                switch (currentGameState)
                {
                    case GameState.MainMenu:
                        {
                            HandleMainMenu();
                            break;
                        }
                    case GameState.Battle:
                        {
                            HandleBattle();
                            break;
                        }

                    case GameState.VictoryScreen:
                        {
                            HandleVictoryScreen();
                            break;
                        }
                    case GameState.GameOver:
                        {
                            HandleGameOver();
                            break;
                        }
                }
            }
        }

        public void HandleMainMenu()
        {
            Console.Clear();
            Console.WriteLine("--- Console RPG ---");
            Console.WriteLine("1. Start New Game");
            Console.WriteLine("2. Load Game");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    {
                        currentGameState = GameState.Battle;
                        break;
                    }
                    case "2":
                    {
                        Console.WriteLine("NOT IMPLEMENTED");
                        break;
                    }
                    case "3":
                    {
                        isRunning = false;
                        break;
                    }
            }
        }

        public void HandleBattle()
        {
            Player player = new Player("Fufelis",100,25,10,100,15,20,true);
            Player player2 = new Player("Karote",50,10,10,50,10,10,true);
            Enemy enemy1 = new Enemy("Gobiln",50,5,10,50,0,15,false);
            Fireball fireball = new Fireball();
            Heal heal = new Heal();
            player.LearnAbility(fireball);
            player.LearnAbility(heal);
            enemy1.LearnAbility(fireball);
           // Enemy enemy2 = new Enemy("Big Gobiln", 55, 55, 10, 10, 5, 9, false);
            List<Character> players = new List<Character>();
            players.Add(player);
            players.Add(player2);
            List<Character> enemies = new List<Character>();
            enemies.Add(enemy1);
            //enemies.Add(enemy2);

            BattleSystem battle = new BattleSystem(players, enemies);
            bool battleResult = battle.StartBattleLoop();

            if (battleResult)
            {
                currentGameState = GameState.VictoryScreen;
            }
            else
            {
                currentGameState = GameState.GameOver;
            }
            



        }

        public void HandleVictoryScreen()
        {
            Console.WriteLine("=== VICTORY ===");
            Console.ReadKey(true);
            currentGameState = GameState.MainMenu;
        }

        public void HandleGameOver()
        {
            Console.WriteLine("=== DEFEAT ===");
            Console.ReadKey(true);
            currentGameState = GameState.MainMenu;
        }
    }
}