using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;


namespace DungeonCrawlerProject
{
    internal class Combat
    {
        static public void CombatAlgorithm(CombatActor player, CombatActor enemy) //CLEAR?!?!?
        {
            enemy.CurrentHP = 10;
            WriteLine($"You've encountered a {enemy.Name}!");

            while (player.IsAlive && enemy.IsAlive)
            {
               
                string playerWeaponName = player.MainHandWeapon.DescriptiveName;
                string playerShieldName = player.SecondHand.DescriptiveName;
                string playerAbility = player.Pocket.DescriptiveName;
                

                WriteLine("Choose your action!");
                WriteLine($"1. Use your {playerWeaponName} ({player.MainHandWeapon.HitChance * 100}%)");
                if(player.SecondHand.DamageTake > 0)
                WriteLine($"2. Use your {playerShieldName} ({player.SecondHand.ShieldChance * 100}%) and try to blind the guard and escape!");
                WriteLine($"3. Use {playerAbility} to burn the Guard!({player.Pocket.HitChance * 100}%)");
                WriteLine($"4. Try to trip {enemy.Name} ({player.Pocket.HitChance * 100}%)");
                try
                {
                    Write("You choose: ");
                    var selection = int.Parse(ReadLine());

                    switch (selection)
                    {
                        case 1:
                            ActorAttackActor(player, enemy);
                            ActorAttackActor(enemy, player);
                            Thread.Sleep(1000);

                            break;
                        case 2:
                            BlindingAttack(player, enemy);
                            Thread.Sleep(1000);
                            break;
                        case 3:
                            WaxDamage(player, enemy);
                            Thread.Sleep(1000);
                            break;
                        case 4:
                            TripAttack(player, enemy);
                            Thread.Sleep(1000);
                            break;
                        default:
                            break;
                    }
                }
                catch (FormatException)
                {
                    WriteLine("Error! please do as instructed :(");
                }
                WriteLine();

                if(player.CurrentHP <= 0)
                {
                    SpecialEvents.EndingDead();
                }
                if(enemy.CurrentHP <= 0)
                {
                    break;
                }
            }

        }

        static private void ActorAttackActor(CombatActor attacker, CombatActor defender)
        {
            bool isHit = defender.WeaponDefend(attacker.MainHandWeapon);

            if (!isHit)
            {
                WriteLine($"{attacker.Name} hit {defender.Name} for {attacker.MainHandWeapon.Damage} damage.");
                if (defender.CurrentHP < 0)
                    defender.CurrentHP = 0;

                    Map.featherCount += 2;
            }
            else
                WriteLine($"{attacker.Name} missed!");

            WriteLine($"The {defender.Name} has {defender.CurrentHP} HP remaining.");
        }

        static private void BlindingAttack(CombatActor defender, CombatActor attacker)
        {
            
            bool isHit = defender.ShieldDefend(defender.SecondHand, attacker.MainHandWeapon);
            if (!isHit)
            {
                WriteLine("Successes! the dirty tactic worked and the guard got distracted!");
                Thread.Sleep(1500);
                attacker.CurrentHP = 0;
            }
            else
            {
                WriteLine("Move failed!");
                ActorAttackActor(attacker, defender);
            }

        }
        static private void WaxDamage(CombatActor attacker, CombatActor defender)
        {
            bool hit = attacker.Pocket.Wax(defender);
            if (!hit)
            {
                WriteLine($"{attacker.Name} splashed {defender.Name} with Hot Wax!");
                if (defender.CurrentHP < 0)
                    defender.CurrentHP = 0;
                WriteLine($"The {defender.Name} has {defender.CurrentHP} HP remaining.");
                Thread.Sleep(1500);
                Map.featherCount += 2;
            }
            else
                WriteLine($"{attacker.Name} missed!");
            ActorAttackActor(defender, attacker);
        }

        static private void TripAttack(CombatActor attacker, CombatActor defender)
        {
            bool hit = attacker.Pocket.Trip(defender);
            if (!hit)
            {
                WriteLine($"{attacker.Name} tripped {defender.Name}!");
                WriteLine($"The {defender.Name} is super embarassed and lost 5 HP from the shame.");
                if (defender.CurrentHP < 0)
                    defender.CurrentHP = 0;
                WriteLine($"The {defender.Name} has {defender.CurrentHP} HP remaining.");
                Map.featherCount++;
            }
            else
                WriteLine($"{attacker.Name} missed!");
            ActorAttackActor(defender, attacker);
        }
        
    }
}
