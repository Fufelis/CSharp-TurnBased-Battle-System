using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Turn_Based_Battle_System.Core
{
    public class BattleSystem
    {
        private List<Character> allCombatants;
        private Queue<Character> turnOrderQueue;

        private int currentTurnNumber;

        private bool battleIsOver = false;
        private bool isVictory = false;

        public bool IsVictory { get; }
        public bool BattleIsOver { get; }

        public BattleSystem(List<Character> players, List<Character> enemies)
        {
            allCombatants = new List<Character>();
            allCombatants.AddRange(players);
            allCombatants.AddRange(enemies);
        }

        public bool StartBattleLoop()
        {
            Console.Clear();
            Console.WriteLine("===BATTLE START===");
            InitializeBattle();
            DisplayBattleStatus();

            while (!battleIsOver)
            {
                if (turnOrderQueue.Count == 0)
                {
                    RebuildTurnOrder();
                    if (turnOrderQueue.Count == 0)
                    {
                        CheckBattleEndConditions();
                        if (battleIsOver) break;
                        currentTurnNumber++;
                        Console.WriteLine($"\n=== Round {currentTurnNumber} ===");
                    }
                }

                Character currentActor = turnOrderQueue.Dequeue();

                if (!currentActor.IsAlive)
                {
                    Console.WriteLine($"{currentActor.Name} is defeated and cannot act");
                    CheckBattleEndConditions();
                    continue;
                }
                Console.WriteLine($"\n=== {currentActor.Name}`s Turn (Turn {currentTurnNumber}) ===");
                Thread.Sleep(500);

                PerformTurn(currentActor);

                ApplyEndOfTurnEffects(currentActor);
                RemoveDefeatedCombatants();

                DisplayBattleStatus();
                Thread.Sleep(1000);

                CheckBattleEndConditions();
            }

            DisplayBattleEndResult();
            return isVictory;
        }

        private void RebuildTurnOrder()
        {
            List<Character> activeCombatants = allCombatants.Where(c => c.IsAlive).ToList();

            activeCombatants = activeCombatants.OrderByDescending(c => c.Speed).ToList();

            turnOrderQueue = new Queue<Character>(activeCombatants);
        }

        private void InitializeBattle()
        {
            currentTurnNumber = 1;
            battleIsOver = false;
            isVictory = false;
            RebuildTurnOrder();

            Console.WriteLine($"Round {currentTurnNumber} begins");
        }

        private void DisplayBattleStatus()
        {
            Console.WriteLine("\n===Current Battle Status===");
            foreach (var c in allCombatants.OrderBy(c => c.IsPlayer).ThenBy(c => c.Name))
            {
                string status = c.IsAlive ? $"HP: {c.Health}/{c.MaxHealth}" : "DEFEATED";
                Console.WriteLine($"{c.Name} ({(c.IsPlayer ? "Player" : "Enemy")}): {status}");
            }
            Console.WriteLine("=========================");
        }

        private void CheckBattleEndConditions()
        {
            bool allPlayersDefeated = allCombatants.Where(c => c.IsPlayer).All(c => !c.IsAlive);
            bool allEnemiesDefeated = allCombatants.Where(c => !c.IsPlayer).All(c => !c.IsAlive);

            if (allEnemiesDefeated)
            {
                battleIsOver = true;
                isVictory = true;
            }
            else if (allPlayersDefeated)
            {
                battleIsOver = true;
                isVictory = false;
            }
        }

        private void PerformTurn(Character actor)
        {
            actor.ApplyStartOfTurnEffects();
            if (!actor.IsAlive) return;

            if (actor.IsPlayer)
            {
                HandlePlayerTurn(actor);
            }
            else
            {
                HandleEnemyTurn(actor);
            }
        }

        private void HandlePlayerTurn(Character player)
        {
            bool actionTaken = false;
            while (!actionTaken)
            {
                Console.WriteLine($"\nWhat will {player.Name} do?");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Use Ability");
                Console.WriteLine("3. Use Item");

                Console.Write("Enter choice: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        {
                            actionTaken = HandlePlayerAttack(player);
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("NOT IMPLEMENTED");
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("NOT IMPLEMENTED");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong choice ");
                            break;
                        }
                }
            }
        }

        private void HandleEnemyTurn(Character enemy)
        {
            Console.WriteLine($"{enemy.Name} is thinking");
            Thread.Sleep(1000);

            var livingPlayers = allCombatants.Where(c => c.IsPlayer && c.IsAlive).ToList();
            if (livingPlayers.Any())
            {
                Random rand = new Random();
                Character target = livingPlayers[rand.Next(livingPlayers.Count)];
                int damage = enemy.CalculateAttackDamage(target);
                target.TakeDamage(damage);
                Console.WriteLine($"{enemy.Name} attacked {target.Name} for {damage} damage!");
            }
            else
            {
                Console.WriteLine($"{enemy.Name} has no targets to attack");
            }
        }

        private void ApplyEndOfTurnEffects(Character actor)
        {
            actor.ApplyEndOfTurnEffects();
        }

        private bool HandlePlayerAttack(Character player)
        {
            var livingEnemies = allCombatants.Where(c => !c.IsPlayer && c.IsAlive).ToList();
            if (!livingEnemies.Any())
            {
                Console.WriteLine("No enemies to attack");
                return false;
            }
            for (int i = 0; i < livingEnemies.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {livingEnemies[i].Name} (HP: {livingEnemies[i].Health}/{livingEnemies[i].MaxHealth}");
            }
            Console.WriteLine("Enter target number");
            if (int.TryParse(Console.ReadLine(), out int targetIndex) && targetIndex > 0 && targetIndex <= livingEnemies.Count)
            {
                Character target = livingEnemies[targetIndex - 1];
                int damage = player.CalculateAttackDamage(target);
                target.TakeDamage(damage);
                Console.WriteLine($"{player.Name} attacked {target.Name} for {damage}");
                return true;
            }
            else
            {
                Console.WriteLine("Invalid Target");
                return false;
            }
        }


        private void RemoveDefeatedCombatants()
        {
            allCombatants.RemoveAll(c => !c.IsAlive);
        }

        private void DisplayBattleEndResult()
        {
            Console.Clear();
            if (isVictory)
            {
                Console.WriteLine("=== VICTORY! ===");
                Console.WriteLine("You have defeated all enemies!");
                //victory logic
            }
            else
            {
                Console.WriteLine("=== DEFEAT! ===");
                Console.WriteLine("You have been defeated.");
                //defeat logic
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}