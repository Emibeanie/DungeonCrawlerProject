using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace DungeonCrawlerProject
{
    class CombatActor
    {
      
            public int x { get; set; }
            public int y { get; set; }
        
            public string Name { get; set; }

            public int MaxHP;
            public int CurrentHP { get; set; }
            public bool IsAlive { get { return CurrentHP > 0; } }


            public Weapon MainHandWeapon;
            public Shield SecondHand;
            public SpecialAbility Pocket;

        
        public CombatActor(string name, int maxHp, Weapon mainHand, Shield secondHand, SpecialAbility pocket)
            {
                Name = name;
                MaxHP = maxHp;
                MainHandWeapon = mainHand;
                SecondHand = secondHand;
                Pocket = pocket;
                CurrentHP = maxHp;
            }
        public CombatActor(string name, int maxHp, Weapon mainHand)
        {
            Name = name;
            MaxHP = maxHp;
            MainHandWeapon = mainHand;
            CurrentHP = maxHp;
        }
            public bool WeaponDefend(Weapon weapon)
            {
                if (weapon.TryAttack())
                {
                    CurrentHP -= weapon.Damage;
                    return false;
                }

                return true;
            }
            public bool ShieldDefend(Shield shield, Weapon weapon)
            {
                if (shield.Cover())
                {
                    shield.DamageTake -= weapon.Damage;
                    return false;
                }
                return true;
            }
        }

        class Weapon
        {
            public string DescriptiveName;
            public int Damage;
            public float HitChance;
            public WeaponType Type;

            public bool TryAttack()
            {
                var random = Random.Shared.Next(0, 100);
                var percentage = HitChance * 100;

                if (random <= percentage)
                    return true;
                else
                    return false;
            }

            public Weapon(string name, int damage, float hitChance)
            {
                DescriptiveName = name;
                Damage = damage;

                if (hitChance > 1)
                    hitChance = 1;
                else if (hitChance < 0)
                    hitChance = 0;

                HitChance = hitChance;
            }
        }

        class Shield
        {
            public string DescriptiveName;
            public int DamageTake;
            public ShieldType Type;
            public float ShieldChance;

            public Shield(string descriptiveName, int damageTake, float shieldChance)
            {
                DescriptiveName = descriptiveName;
                DamageTake = damageTake;
                if (shieldChance > 1)
                    shieldChance = 1;
                else if (shieldChance < 0)
                    shieldChance = 0;

                ShieldChance = shieldChance;
            }

            public bool Cover()
            {
                var random = Random.Shared.Next(0, 100);
                var percentage = ShieldChance * 100;

                if (random <= percentage)
                    return true;
                else
                    return false;
            }
        }

        class SpecialAbility
        {
            public float HitChance;
            
            public string DescriptiveName;
            public bool HitTry()
            {
                var random = Random.Shared.Next(0, 100);
                var percentage = HitChance * 100;

                if (random <= percentage)
                    return true;
                else
                    return false;
            }
            public SpecialAbility(string name, float hitChance)
            {
                DescriptiveName = name;

                if (hitChance > 1)
                    hitChance = 1;
                else if (hitChance < 0)
                    hitChance = 0;

                HitChance = hitChance;
            }
            public bool Wax(CombatActor enemy)
            {
                if (HitTry())
                {
                    enemy.CurrentHP -= 20;
                    return false;
                }
                return true;
            }

            public bool Trip(CombatActor enemy)
            {
                if (HitTry())
                {
                    enemy.CurrentHP -= 5;
                    return false;
                }
                return true;
            }
        }
        public enum WeaponType
        {
            Sword,
            Axe
        }

        public enum ShieldType
        {
            sun,
        }
    }
