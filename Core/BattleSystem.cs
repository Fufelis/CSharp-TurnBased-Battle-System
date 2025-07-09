using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RPG_Turn_Based_Battle_System.Spells;

namespace RPG_Turn_Based_Battle_System.Core
{
    internal class BattleSystem
    {
        private List<Character> allCombatants;
        private Queue<Character> turnOrderQueue;
        private Random _rand = new Random();

        private int currentTurnNumber;

        private bool battleIsOver = false;
        private bool isVictory = false;

        public bool IsVictory { get; private set; }
        public bool BattleIsOver { get; private set; }

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

            while (!BattleIsOver)
            {
                if (turnOrderQueue.Count == 0)
                {
                    RebuildTurnOrder();
                    if (turnOrderQueue.Count == 0)
                    {
                        CheckBattleEndConditions();
                        if (BattleIsOver) break;
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
            return IsVictory;
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
            BattleIsOver = false;
            IsVictory = false;
            RebuildTurnOrder();

            Console.WriteLine($"Round {currentTurnNumber} begins");
        }

        private void DisplayBattleStatus()
        {
            Console.WriteLine("\n===Current Battle Status===");
            foreach (var c in allCombatants.OrderBy(c => c.IsPlayer).ThenBy(c => c.Name))
            {
                string status = c.IsAlive ? $"HP: {c.Health}/{c.MaxHealth}" : "DEFEATED";
                Console.WriteLine($"{c.Name} ({(c.IsPlayer ? "Player" : "Enemy")}): {status}  MP : {c.Mana}/{c.MaxMana}");
            }
            Console.WriteLine("=========================");
        }

        private void CheckBattleEndConditions()
        {
            bool allPlayersDefeated = allCombatants.Where(c => c.IsPlayer).All(c => !c.IsAlive);
            bool allEnemiesDefeated = allCombatants.Where(c => !c.IsPlayer).All(c => !c.IsAlive);

            if (allEnemiesDefeated)
            {
                BattleIsOver = true;
                IsVictory = true;
            }
            else if (allPlayersDefeated)
            {
                BattleIsOver = true;
                IsVictory = false;
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
                Console.WriteLine("4. SKIP (testing purpose)");

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
                            actionTaken = HandlePlayerAbility(player);
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("NOT IMPLEMENTED");
                            break;
                        }
                    case "4":
                        {
                            actionTaken = true;
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
               livingPlayers= livingPlayers.OrderByDescending(c=> c.Health).ToList();
                Character target = livingPlayers.Last();
                if (enemy.Abilities.Any()&& _rand.Next(100)<=30) { 
                    var randomAbility = enemy.Abilities[_rand.Next(enemy.Abilities.Count())];
                    Console.WriteLine($"{enemy.Name} casts {randomAbility.Name} on {target.Name}");
                    TryUseAbility(enemy, randomAbility,target);
                    enemy.Mana=enemy.MaxMana;
                }
                else
                {
                    int damage = enemy.CalculateAttackDamage(target);
                    target.TakeDamage(damage);

                    Console.WriteLine($"{enemy.Name} attacked {target.Name} for {damage} damage!");

                }
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

            Character target = SelectTarget(false);

            if (target == null)
            {
                Console.WriteLine("Invalid target");
                return false;
            }
            else
            {
                int damage = player.CalculateAttackDamage(target);
                target.TakeDamage(damage);
                Console.WriteLine($"{player.Name} attacked {target.Name} for {damage}");
                return true;
            }
        }

        private bool HandlePlayerAbility(Character player)
        {
            if (!player.Abilities.Any())
            {
                Console.WriteLine("No spells to cast");
                return false;
            }
            for (int i = 0; i < player.Abilities.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {player.Abilities[i].Name} costs {player.Abilities[i].ManaCost}");
            }
            Console.WriteLine("Enter Ability number");
            if (int.TryParse(Console.ReadLine(), out int abilityIndex) && (abilityIndex > 0 && abilityIndex <= player.Abilities.Count))
            {
                Character target = SelectTarget(player.Abilities[abilityIndex - 1].CastOnAlly);
                if (target == null)
                {
                    Console.WriteLine("invalid target");
                    return false;
                }
                else
                {
                    Console.WriteLine($"{player.Name} casts {player.Abilities[abilityIndex - 1].Name} on {target.Name}");
                    return TryUseAbility(player, player.Abilities[abilityIndex - 1], target);
                }
            }
            else
            {
                Console.WriteLine("Invalid ability");
                return false;
            }
        }

        private void RemoveDefeatedCombatants()
        {
            allCombatants.RemoveAll(c => !c.IsAlive);
        }

        private bool TryUseAbility(Character caster, Ability ability, Character target)
        {
            if (caster.Mana < ability.ManaCost)
            {
                Console.WriteLine($"{caster.Name} Doesnt have enough mana for {ability.Name}");
                return false;
            }

            ability.Use(caster, target);
            return true;
        }

        private Character SelectTarget(bool isAlly)
        {
            var livingEnemies = allCombatants.Where(c => !c.IsPlayer && c.IsAlive).ToList();
            var livingAllies = allCombatants.Where(c => c.IsPlayer && c.IsAlive).ToList();

            if (isAlly)
            {
                for (int i = 0; i < livingAllies.Count; i++)
                {
                    Console.WriteLine($"{i + 1} . {livingAllies[i].Name} (HP: {livingAllies[i].Health}/{livingAllies[i].MaxHealth})");
                }
                Console.WriteLine("Enter target number");
                if (int.TryParse(Console.ReadLine(), out int targetIndex) && (targetIndex > 0 && targetIndex <= livingAllies.Count))
                {
                    Character target = livingAllies[targetIndex - 1];
                    return target;
                }
                else
                {
                    return null;
                }
            }
            else
            { 
                if (!livingEnemies.Any())
                {
                    Console.WriteLine("No enemies to attack");
                    return null;
                }
                for (int i = 0; i < livingEnemies.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {livingEnemies[i].Name} (HP: {livingEnemies[i].Health}/{livingEnemies[i].MaxHealth}");
                }
                Console.WriteLine("Enter target number");

                if (int.TryParse(Console.ReadLine(), out int targetIndex) && targetIndex > 0 && targetIndex <= livingEnemies.Count)
                {
                    Character target = livingEnemies[targetIndex - 1];
                    return target;
                }
                else
                {
                    
                    return null;
                }
            }
        }

        private void DisplayBattleEndResult()
        {
            Console.Clear();
            if (IsVictory)
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