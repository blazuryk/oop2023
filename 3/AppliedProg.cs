using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NUnit.Framework.Internal;
namespace Prog3
{

    class AppliedProg
    {
        private static string GetValidInput(string prompt)
        {
            string userInput;

            do
            {
                Console.Write(prompt);
                userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Invalid input. Please enter a non-empty name.");
                }

            } while (string.IsNullOrWhiteSpace(userInput));

            return userInput;
        }
        void ChangeYourTurn(Unit unit, Map gameMap, GameSetup gameSetup, Summoner player, Summoner AI)
        {
            Console.WriteLine("1. Передвинуть:");
            Console.WriteLine("2. Аттаковать");
            ConsoleKeyInfo key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    int x = 1;
                    Location temp = unit.Place;
                    gameMap.MoveUnit(unit, unit.Place.X + x * unit.Speed, unit.Place.Y);
                    break;

                case ConsoleKey.D2:
                    Random random = new Random();
                    int randomNumber;
                    randomNumber = random.Next(0, AI.GetUnits().Count() - 1);
                    var target = gameSetup.GetTargetFromUser(AI, unit.Place);
                    Console.WriteLine($"{target} {randomNumber}");
                    if (target == null)
                    {
                        unit.AttackSummoner(AI);
                        player.Initiative = 0;
                        break;
                    }
                    unit.AttackUnit(target);
                    if (target.Alive == false)
                    {
                        player.Experience += target.Experience;
                        player.Initiative = 0;
                    }
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Пожалуйста, выберите еще раз.");
                    break;
            }
        }
        Unit CreateAIUnit(Summoner player)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, player.GetAllSchools().Count() - 1);
            return new Unit("AI", player.GetAllSchools()[randomNumber]);

        }


        static void Main(string[] args)
        {
            School[] schools = new School[5];

            schools[0] = new School("Necromancy");
            schools[0].AddSkill(new Skill("Animalism", 1, 50, 2));
            schools[0].AddSkill(new Skill("Здоровье", 1, 30, 2));

            schools[1] = new School("Animalism");
            schools[1].AddSkill(new Skill("Урон", 1, 40, 1));
            schools[1].AddSkill(new Skill("Здоровье", 1, 20, 1));


            schools[0] = new School("Demonology");
            schools[0].AddSkill(new Skill("damage", 1, 50, 2));
            schools[0].AddSkill(new Skill("health", 1, 30, 2));


            schools[0] = new School("High-speed.");
            schools[0].AddSkill(new Skill("damage", 1, 50, 2));
            schools[0].AddSkill(new Skill("health", 1, 30, 2));


            schools[0] = new School("Heavyweight");
            schools[0].AddSkill(new Skill("damage", 1, 50, 1));
            schools[0].AddSkill(new Skill("health", 1, 30, 2));

            GameSetup gameSetup = new GameSetup();
            Summoner player = gameSetup.First;
            Summoner computer = gameSetup.Second;
            player.Alive = true;
            computer.Alive = true;
            Map gameMap = gameSetup.GameMap;

            Console.WriteLine("Добро пожаловать в игру!");

            while (!gameSetup.GameIsOver())
            {
                Console.Clear(); 


                DisplayChessboard(gameMap, player, computer);

                Console.WriteLine("Ход игрока:");
                Console.WriteLine("1. Выбрать отряд");
                Console.WriteLine("2. Аккамулировать энергию");
                Console.WriteLine("3. Воскресить отряд");
                Console.WriteLine("4. Призвать отряд");
                Console.WriteLine("5. Выучить новую школу");
                ConsoleKeyInfo key = Console.ReadKey();
                try
                {
                    switch (key.Key)
                    {
                        case ConsoleKey.D1:
                            List<Unit> units = player.GetUnits();


                            if (units.Count > 0)
                            {
                                var flag = false;
                                Console.WriteLine("Список юнитов:");
                                foreach (var unit in units)
                                {
                                    Console.WriteLine($"Имя: {unit.Name}, Здоровье: {unit.CurrentHealth}");

                                }
                                while (!flag)
                                {
                                    Console.WriteLine("Введите имя:");
                                    string userInput = Console.ReadLine();
                                    if (userInput == null)
                                    {
                                        break;
                                    }
                                    foreach (var unit in units)
                                    {
                                        if (userInput == unit.Name)
                                        {
                                            flag = true;
                                            player.RemoveDeadUnits();
                                            AppliedProg appliedProg = new AppliedProg();
                                            appliedProg.ChangeYourTurn(unit, gameMap, gameSetup, player, computer);
                                            break;
                                        }

                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Список юнитов пуст.");
                            }
                            break;
                        case ConsoleKey.D2:
                            player.AccumulateEnergy();
                            //                          player.Initiative = 0;
                            break;
                        case ConsoleKey.D3:

                            foreach (var trying in player.GetUnits())
                            {

                                Unit deadUnit = player.GetUnits().FirstOrDefault(unit => !unit.Alive);

                                if (deadUnit != null)
                                {

                                    deadUnit.Alive = true;
                                    deadUnit.CurrentHealth = deadUnit.MaxHealth;

                                    gameMap.PlaceUnit(deadUnit, deadUnit.Place.X, deadUnit.Place.Y);
                                }
                                else
                                {

                                    Console.WriteLine("Нет юнитов для возраждения.");
                                    break;
                                }
                            }

                            break;
                        case ConsoleKey.D4:

                            List<School> Schools = player.GetAllSchools();
                            foreach (School school in Schools)
                            {
                                Console.WriteLine($"- {school.Name}");
                            }
                            string selectedSchoolName;
                            School selectedSchool;

                            do
                            {
                                Console.Write("Choose a school: ");
                                selectedSchoolName = Console.ReadLine();
                                selectedSchool = player.GetSchoolByName(selectedSchoolName);
                                if (selectedSchool == null)
                                {
                                    Console.WriteLine("Школа с таким именем не найдена. Пожалуйста, введите корректное название.");
                                }

                            } while (selectedSchool == null);
                            Console.Write("Choose name of unit: ");
                            string UnitName = GetValidInput("Enter your name: ");
                            Console.Write("Choose Moral or Amoral: ");
                            string UnitType = GetValidInput("Enter your type: ");
                            Unit tmp;
                            if (UnitType == "Moral" || UnitType == "moral")
                            {
                                tmp = new MoralUnit(UnitName, selectedSchool);
                            }
                            else
                            {
                                tmp = new AmoralUnit(UnitName, selectedSchool);

                            }
                            Location temp = gameMap.FindFreeCellInUpperHalf();
                            player.AddUnit(tmp);
                            gameMap.FindFreeCellInLowerHalf();
                            gameMap.PlaceUnit(tmp, temp.X, temp.Y);
                            Console.WriteLine($"X={temp.X}, Y={temp.Y}:");
                            player.Initiative = 0;
                            break;
                        case ConsoleKey.D5:

                            break;
                        default:
                            Console.WriteLine("Некорректный выбор. Пожалуйста, выберите еще раз.");
                            break;
                    }


                    Console.WriteLine("Ход компьютера:");
                    gameSetup.TURNAI(player, computer);

                    if (gameSetup.GameIsOver())
                    {
                        Console.WriteLine("Игра окончена!");
                        break;
                    }
                    gameMap.RemoveDeadUnits();



                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in PLayer turn: {ex.Message}");

                }
            }

            Console.ReadLine();
        }

        static void DisplayChessboard(Map gameMap, Summoner player, Summoner computer)
        {
            int cellSize = 3;
            DisplaySummonerSymbol(player);
            for (int y = 0; y < gameMap.Rows; y++)
            {
                for (int x = 0; x < gameMap.Cols; x++)
                {
                    Unit unit = gameMap.GetUnitAtLocation(y, x);
                    string unitName = unit != null ? unit.Name[0].ToString() : " ";
                    ConsoleColor backgroundColor = (x + y) % 2 == 0 ? ConsoleColor.White : ConsoleColor.Gray;
                    ConsoleColor foregroundColor = ConsoleColor.Black;

                    if (unit != null)
                    {
                        foregroundColor = (player.GetUnits().Contains(unit)) ? ConsoleColor.Blue : ConsoleColor.Red;
                    }

                    Console.BackgroundColor = backgroundColor;
                    Console.ForegroundColor = foregroundColor;

                    Console.Write(new string(' ', cellSize - 1) + unitName); 
                }

                Console.WriteLine();
            }
            DisplaySummonerSymbol(computer);
        }
        static void DisplaySummonerSymbol(Summoner summoner)
        {

            Console.WriteLine($"  {summoner.Name}");
            Console.WriteLine("  _________");
            Console.WriteLine($" | Health: {summoner.CurrentHealth}");
            Console.WriteLine($" | Energy:   {summoner.CurrentEnergy}");
            /*           Console.WriteLine(" | Units:  ");

                       foreach (Unit unit in summoner.GetUnits())
                       {
                           Console.WriteLine($" |   - {unit.Name}");
                       }
            */
            Console.WriteLine(" |_________");
            Console.WriteLine(); 
        }


    }

}