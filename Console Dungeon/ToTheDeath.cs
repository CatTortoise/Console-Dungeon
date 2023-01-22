using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static My_IO.My_IO;

namespace Class_07___Workshop_Combat_System
{
    static class ToTheDeath
    {
        public static void Fight()
        {
            GameLoop();
        }

        private static void GameLoop()
        {
            bool isNewGameLoop = true;
            bool gameOver = false;
            int numberOfUnits = 4;
            do
            {
                Unit[] units = new Unit[numberOfUnits];
                Unit[] minotaurs = new Unit[2];
                gameOver = false;
                isNewGameLoop = true;
                while (!gameOver)
                {
                    if (isNewGameLoop)
                    {
                        InstantiateUnits(units, "Unit", 10, ConsoleColor.Blue);
                        InstantiateUnits(minotaurs, "Minotaur", 5, ConsoleColor.Red);
                        isNewGameLoop = false;
                        Console.Clear();
                    }
                    foreach (Unit unit in units)
                    {
                        Renderer.RenderArena(units, minotaurs);
                        if (unit.UnitAction != Action.Actions.Dead)
                        {
                            Act(Renderer.FightMenu(unit), unit, minotaurs,false);
                        }
                        Console.Clear();
                    }

                    AutomaticActionSelection(minotaurs,units);
                    Renderer.RenderArena(units, minotaurs);
                    foreach (Unit unit in units)
                    {
                        gameOver = true;
                        if (unit.UnitAction != Action.Actions.Dead)
                        {
                            gameOver = false;
                            break;
                        }
                    }
                    if (!gameOver)
                    {
                        foreach (Unit unit in minotaurs)
                        {
                            if (unit.UnitAction != Action.Actions.Dead)
                            {
                                gameOver = false;
                                break;
                            }
                        }
                    }
                    if(!gameOver)
                    {
                        PrintColoredMessage($"Press any key to continue\n", ConsoleColor.Yellow);
                        Console.ReadKey();
                        Console.Clear();
                        Renderer.ResetUnitsAction(units);
                        Renderer.ResetUnitsAction(minotaurs);
                    }
                }
                if (gameOver)
                {
                    Console.Clear();
                    Renderer.Winner(units[0]);
                    Renderer.Winner(minotaurs[0]);
                }
                Array.Clear(units);
                Array.Clear(minotaurs);

            } while (!ExitCheck(""));
        }
        private static void AutomaticActionSelection(Unit[] acters, Unit[] targets)
        {
            foreach (Unit unit in acters)
            {
                Action.Actions action = (Action.Actions)Random.Shared.Next(Action.numberOfActions);
                Act(action, unit, targets,true);
            }   
        }

        private static Unit[] InstantiateUnits(Unit[] units, string name, int maxHP, ConsoleColor color)
        {

            for (int i = 0; i < units.Length; i++)
            {
                int maxEvasion = Random.Shared.Next(1, 5);
                int minEvasion = Random.Shared.Next(maxEvasion);
                Shield shield = new Shield();
                Weapon weapon = new Weapon();
                units[i] = new($"ID:({i + 1}) {name}_{i + 1}", maxHP, shield, weapon, minEvasion, maxEvasion, color);
            }
            return units;
        }

        private static int AutomaticTarget(Unit[] target)
        {
            return Random.Shared.Next(target.Length);
        }

        private static void Act(Action.Actions action, Unit acter, Unit[] targets, bool isAutomatic)
        {
            int temp = 0;
            switch (action)
            {
                case Action.Actions.DealDamage:
                    if (isAutomatic)
                    {
                        temp = AutomaticTarget(targets);
                    }
                    else
                    {
                        temp = Renderer.ChooseAction($"Choose target to attack by ID:", targets[0].UnitColor);
                    }
                    if (temp >= targets.Length)
                    {
                        Renderer.InputErrer($"{temp+1} is an Invalid target Attacking unit will go idle \n");
                        Action.GoIdol(acter);
                    }
                    else
                    { 
                        Action.DealDamage(targets[temp], acter); 
                    }
                    break;
                case Action.Actions.ShieldYourself:
                    Action.ShieldYourself(acter);
                    break;
                case Action.Actions.Heal:
                    Action.UseHeal(acter, 10);
                    break;
                case Action.Actions.UpgradEquipment:
                    Action.UpgradEquipment(acter, 2, Unit.Equipment.Weapon);
                    break;
                default:
                    Action.GoIdol(acter);                
                    break;
            }
        }


    }
}
