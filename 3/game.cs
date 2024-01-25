using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NUnit.Framework.Internal;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting;
using System.Threading;
namespace Prog3
{


    public class Map
    {
        private Matrix<Unit> unitMatrix;

        public Map(int rows, int cols)
        {
            unitMatrix = new Matrix<Unit>(rows, cols);
        }

        public void PlaceUnit(Unit unit, int row, int col)
        {
            if (unitMatrix[row, col] != null)
            {
                throw new InvalidOperationException($"Cell is Exist.");

            }
            unitMatrix[row, col] = unit;
            unit.Place = new Location(row, col);
        }

        public void MoveUnit(Unit unit, int newRow, int newCol, int i = 0)
        {

            if (newRow < 0 || newRow >= unitMatrix.rows || newCol < 0 || newCol >= unitMatrix.cols)
            {
                throw new InvalidOperationException($"Невозможно переместиться на клетку, так как она вне границ карты."); ;
            }

            if (unitMatrix[newRow, newCol] != null)
            {
                throw new InvalidOperationException($"Cell is Exist.");
            }

            Location currentLocation = unit.Place;
            Unit DownUnit = TraverseColumnDown(currentLocation.X + 1, currentLocation.Y);
            Unit UpUnit = TraverseColumnUp(currentLocation.X - 1, currentLocation.Y);

            if (i == 0)
            {
                if (DownUnit != null && newRow > DownUnit.Place.X)
                {
                    newRow = DownUnit.Place.X - 1;
                }
            }
            else if (UpUnit != null && newRow < UpUnit.Place.X)
            {
                newRow = UpUnit.Place.X + 1;
            }
            if (currentLocation != null)
            {
                unitMatrix[currentLocation.X, currentLocation.Y] = null;
            }
            unitMatrix[newRow, newCol] = unit;
            unit.Place = new Location(newRow, newCol);
        }

        public Unit TraverseColumnUp(int startRow, int col)
        {
            for (int row = startRow; row >= 0; row--)
            {
                if (IsWithinBounds(row, col) && unitMatrix[row, col] != null)
                {
                    return unitMatrix[row, col];
                }
            }

            return null;
        }

        public Unit TraverseColumnDown(int startRow, int col)
        {
            for (int row = startRow; row < unitMatrix.rows; row++)
            {
                if (IsWithinBounds(row, col) && unitMatrix[row, col] != null)
                {
                    return unitMatrix[row, col];
                }
            }

            return null;
        }

        private bool IsWithinBounds(int row, int col)
        {
            return row >= 0 && row < unitMatrix.rows && col >= 0 && col < unitMatrix.cols;
        }



        public bool IsRowEmpty(int row)
        {
            for (int col = 0; col < unitMatrix.cols; col++)
            {
                if (unitMatrix[row, col] != null)
                {
                    return false;
                }
            }
            return true;
        }


        public bool IsColumnEmpty(int col)
        {
            for (int row = 0; row < unitMatrix.rows; row++)
            {
                if (unitMatrix[row, col] != null)
                {
                    return false;
                }
            }
            return true;
        }

        public IEnumerable<Unit> GetUnits()
        {
            for (int row = 0; row < unitMatrix.rows; row++)
            {
                for (int col = 0; col < unitMatrix.cols; col++)
                {
                    Unit unit = unitMatrix[row, col];
                    if (unit != null)
                    {
                        yield return unit;
                    }
                }
            }
        }
        public int Rows
        {
            get { return unitMatrix.rows; }
        }
        public int Cols
        {
            get { return unitMatrix.cols; }
        }
        public Unit GetUnitFromInd(int row, int cow)
        {
            return unitMatrix[row, cow];
        }
        public Location FindFreeCellInUpperHalf()
        {
            int halfRows = unitMatrix.rows / 2;

            for (int row = 0; row < halfRows; row++)
            {
                for (int col = 0; col < unitMatrix.cols; col++)
                {
                    if (unitMatrix[row, col] == null)
                    {
                        return new Location(row, col);
                    }
                }
            }
            return null;
        }
        public Location FindFreeCellInLowerHalf()
        {
            int halfRows = unitMatrix.rows / 2;

            for (int row = halfRows; row < unitMatrix.rows; row++)
            {
                for (int col = 0; col < unitMatrix.cols; col++)
                {
                    if (unitMatrix[row, col] == null)
                    {
                        return new Location(row, col);
                    }
                }
            }
            return null;
        }

        public Unit GetUnitAtLocation(int x, int y)
        {
            if (IsWithinBounds(x, y))
            {
                return unitMatrix[x, y];
            }
            else
            {
                throw new InvalidOperationException($"Coordinates ({x}, {y}) are outside the bounds of the map.");
            }
        }
        public void RemoveDeadUnits()
        {
            for (int row = 0; row < unitMatrix.rows; row++)
            {
                for (int col = 0; col < unitMatrix.cols; col++)
                {
                    Unit unit = unitMatrix[row, col];
                    if (unit != null && !unit.Alive)
                    {
                        unitMatrix[row, col] = null;
                    }
                }
            }
        }
    }
    public class Location
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Location(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class School
    {
        private string name;
        int level;
        int cost;
        private List<Skill> skills;

        public School(string name)
        {
            this.name = name;
            this.skills = new List<Skill>();
            this.level = 1;
            this.cost = 500;
        }
        public void UpLvl()
        {
            this.level++;
            foreach (Skill schoolSkill in this.skills)
            {
                schoolSkill.Coefficient = schoolSkill.Coefficient + 1;
            }

        }
        public int Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public void AddSkill(Skill skill)
        {
            this.skills.Add(skill); // Метод для добавления умения в список
            Console.WriteLine($"Добавлено умение {skill.UnitDescription} в школу {this.name}.");
        }

        public School GetUnitBySkill(Unit unit)
        {
            foreach (Skill schoolSkill in this.skills)
            {
                schoolSkill.Use(unit);
            }
            return this;
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }

    public class Skill
    {
        //    private string name;
        private string unitDescription;
        private int energyCost;
        private int coefficient;

        //   public Skill(string name, string description, int level, int cost, int coeff)
        public Skill(string description, int level, int cost, int coeff)
        {
            //        this.name = name;
            this.unitDescription = description;
            //            this.currentlvl = level;
            this.energyCost = cost;
            this.coefficient = coeff;
        }

        /*       public string Name
               {
                   get { return name; }
                   set { name = value; }
               }*/

        public string UnitDescription
        {
            get { return unitDescription; }
            set { unitDescription = value; }
        }

        /*       public int MinKnowledgeLevel
               {
                   get { return minKnowledgeLevel; }
                   set { minKnowledgeLevel = value; }
               }*/

        public int EnergyCost
        {
            get { return energyCost; }
            set { energyCost = value; }
        }

        public int Coefficient
        {
            get { return coefficient; }
            set { coefficient = value; }
        }
        public Skill Use(Unit target)
        {
            if (unitDescription == "damage")
            {
                target.Damage = target.Damage * coefficient;
            }
            if (unitDescription == "health")
            {
                if (target.CurrentHealth * coefficient > target.MaxHealth)
                {
                    target.MaxHealth = target.CurrentHealth * coefficient;
                    target.CurrentHealth = target.MaxHealth;

                }
                target.CurrentHealth = target.CurrentHealth * coefficient;
            }
            if (unitDescription == "speed")
            {
                target.Speed = target.Speed * coefficient;
            }
            Console.WriteLine($"Умение {this.UnitDescription} применено на {target}.");
            return this;
        }
    }

    public class Summoner
    {
        private string name;
        private int initiative;
        private int maxHealth;
        private int currentHealth;
        private int maxEnergy;
        private int currentEnergy;
        private int energyAccumulationRate;
        private int experience;
        private bool alive;
        private List<School> schoolsList = new List<School>();
        private List<Unit> units;
        private Dictionary<School, int> schoolsKnowledge;

        public Summoner(string name)
        {
            this.name = name;
            this.schoolsKnowledge = new Dictionary<School, int>();
            this.units = new List<Unit>();
            this.currentHealth = 1000;
            this.maxHealth = 1000;
            this.currentEnergy = 1000;
            this.maxEnergy = 1000;
            this.energyAccumulationRate = 250;

            School school1 = new School("Soldiers");
            Skill damageSkill1 = new Skill("damage", 1, 300, 2);
            school1.AddSkill(damageSkill1);

            School school2 = new School("Barbarians");
            Skill healthSkill1 = new Skill("health", 1, 300, 2);
            school2.AddSkill(healthSkill1);
            schoolsList.Add(school1);
            schoolsList.Add(school2);
        }
        public School GetSchoolByName(string schoolName)
        {
            return schoolsList.Find(school => school.Name.Equals(schoolName, StringComparison.OrdinalIgnoreCase));
        }
        public void AddSchool(School school)
        {
            schoolsList.Add(school);
        }

        public void RemoveSchool(School school)
        {
            schoolsList.Remove(school);
        }

        public List<School> GetAllSchools()
        {
            return schoolsList;
        }
        public void AddUnit(Unit unit)
        {
            if (this.currentEnergy > unit.School.Cost)
            {

                this.currentEnergy -= unit.School.Cost;
                if (unit != null)
                {
                    units.Add(unit);
                }
                else
                {
                    throw new ArgumentException("Empty Unit.");
                }
            }
            else
            {
                throw new ArgumentException("Not Enough Energy.");
            }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }



        public int Initiative
        {
            get { return initiative; }
            set { initiative = value; }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        public int CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public int MaxEnergy
        {
            get { return maxEnergy; }
            set { maxEnergy = value; }
        }

        public int CurrentEnergy
        {
            get { return currentEnergy; }
            set { currentEnergy = value; }
        }

        public int EnergyAccumulationRate
        {
            get { return energyAccumulationRate; }
            set { energyAccumulationRate = value; }
        }

        public int Experience
        {
            get { return experience; }
            set { experience = value; }
        }

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public List<Unit> GetUnits()
        {
            return units;
        }
        public int GetTotalNumberOfSchools()
        {
            return this.schoolsKnowledge.Count;
        }

        public void AddSchoolKnowledge(School school, int knowledgeLevel)
        {
            if (this.experience > school.Cost)
            {
                this.experience -= school.Cost;
                this.schoolsKnowledge[school] = knowledgeLevel;
            }
        }

        public int GetKnowledgeLevelForSchool(School school)
        {
            if (this.schoolsKnowledge.ContainsKey(school))
            {
                return this.schoolsKnowledge[school];
            }
            else
            {
                return 0;
            }
        }

        public void AccumulateEnergy()
        {
            if (this.currentEnergy < this.maxEnergy)
            {
                this.currentEnergy += this.energyAccumulationRate;
            }
        }

        public void ImproveSchool(School school)
        {

            if (this.schoolsKnowledge.ContainsKey(school))
            {
                this.schoolsKnowledge[school]++;
                school.UpLvl();
            }
            else
            {
                this.schoolsKnowledge.Add(school, 1);
            }
        }

        public void TakenDamage(Unit attacker)
        {
            int damageTaken = attacker.Damage;
            Console.WriteLine($"{this.name} получает урон в {damageTaken} от {attacker.Name}.");
            this.currentHealth -= damageTaken;
            if (this.currentHealth <= 0)
            {
                Alive = false;
                //             
            }
        }

        public void RemoveDeadUnits()
        {
            units.RemoveAll(unit => !unit.Alive);
        }
    }

    public class Unit
    {
        private string name;
        private School school;
        private int initiative;
        private int speed;
        private int damage;
        private double protection;

        private int maxHealth;
        private int currentHealth;
        private bool alive;
        private int experience;
        private Location place;

        public Unit(string name, School school)
        {
            this.name = name;
            if (school == null)
            {
                throw new InvalidOperationException($"School is NULL");
            }
            this.school = school;
            this.alive = true;
            this.initiative = 0;
            this.speed = 1;
            this.damage = 100;
            this.protection = 0.85;
            this.maxHealth = 20000;
            this.currentHealth = 20000;
            this.experience = 500;
            this.school.GetUnitBySkill(this);


        }


        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Experience
        {
            get { return experience; }
            set { experience = value; }
        }

        public School School
        {
            get { return school; }
            set { school = value; }
        }

        public int Initiative
        {
            get { return initiative; }
            set { initiative = value; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public double Protection
        {
            get { return protection; }
            set { protection = value; }
        }

        public void checkHP()
        {
            if (this.currentHealth <= 0)
            {
                this.alive = false;
            }
        }

        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }

        public int CurrentHealth
        {
            get { return currentHealth; }
            set { currentHealth = value; }
        }

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public Location Place
        {
            get { return place; }
            set { place = value; }
        }


        public virtual void AttackUnit(Unit target)
        {
            int damageGiven = this.damage;
            Console.WriteLine($"{target.name} получает урон в {damageGiven} от {this.name}.");
            target.currentHealth -= damageGiven;
            if (target.currentHealth <= 0)
            {
                target.alive = false;
                Console.WriteLine($"{target.name} был побежден!");
            }
        }
        public virtual void AttackSummoner(Summoner target)
        {
            int damageGiven = this.damage;
            Console.WriteLine($"{target.Name} получает урон в {damageGiven} от {this.name}.");
            target.CurrentHealth -= damageGiven;
            if (target.CurrentHealth <= 0)
            {
                target.Alive = false;
                Console.WriteLine($"{target.Name} был побежден!");
            }
        }
        public virtual void TakenDamage(Unit attacker)
        {
            int damageTaken = (int)Math.Round(attacker.Damage * protection);
            Console.WriteLine($"{this.name} получает урон в {damageTaken} от {attacker.Name}.");
            this.currentHealth -= damageTaken;
            if (this.currentHealth <= 0)
            {
                this.alive = false;
                Console.WriteLine($"{this.name} был побежден!");
            }
        }

        public void ResurrectUnits(int count)
        {
            if (currentHealth < maxHealth)
            {
                this.alive = true;
                this.currentHealth = 500;
                Console.WriteLine($"Отряд полностью возрожден!");
            }
        }


    }

    public class MoralUnit : Unit
    {
        private int morale;
#pragma warning disable CS0169 // Поле "MoralUnit.maxMorale" никогда не используется.
        private int maxMorale;
#pragma warning restore CS0169 // Поле "MoralUnit.maxMorale" никогда не используется.
        private bool resurent;
        public bool GetResurent()
        {
            return this.resurent;
        }
        public bool Resurrect
        {
            get { return resurent; }
            set { resurent = value; }
        }
        public MoralUnit(string name, School school) : base(name, school)
        {
            morale = 5;
        }
        public override void AttackUnit(Unit target)
        {
            if (morale < 0)
            {
                int damageGiven = this.Damage / 2;
                Console.WriteLine($"{target.Name} получает урон в {damageGiven} от {this.Name}.");
                target.CurrentHealth -= damageGiven;
                if (target.CurrentHealth <= 0)
                {
                    morale++;
                    target.Alive = false;
                    Console.WriteLine($"{this.Name} был побежден!");
                }
            }
            else
            {
                base.AttackUnit(target);
            }
            IncreaseMorale();
        }
        public override void AttackSummoner(Summoner target)
        {
            if (morale < 0)
            {
                int damageGiven = this.Damage / 2;
                Console.WriteLine($"{target.Name} получает урон в {damageGiven} от {this.Name}.");
                target.CurrentHealth -= damageGiven;
                if (target.CurrentHealth <= 0)
                {
                    morale++;
                    Console.WriteLine($"{this.Name} был побежден!");
                }
            }
            else
            {
                base.AttackSummoner(target);
            }
            IncreaseMorale();
        }
        public override void TakenDamage(Unit attacker)
        {
            base.TakenDamage(attacker);
            DecreaseMorale();
        }
        public void IncreaseMorale()
        {
            morale++;
        }

        public void DecreaseMorale()
        {
            morale--;
        }

        public int GetMorale()
        {
            return this.morale;
        }
    }

    public class AmoralUnit : Unit
    {
        public AmoralUnit(string name, School school) : base(name, school)
        {
        }
    }

    public class GameSetup
    {
        private Summoner first;
        private Summoner second;
        private Map gameMap;


        public GameSetup()
        {
            this.first = new Summoner("First player");
            this.second = new Summoner("AI player");
            this.gameMap = new Map(10, 10);
            CreateAndAssignSchools(first);
            CreateAndAssignSchools(second);
        }

        public Summoner First
        {
            get { return first; }
            set { first = value; }
        }
        public Summoner Second
        {
            get { return second; }
            set { second = value; }
        }
        public Map GameMap
        {
            get { return gameMap; }
            set { gameMap = value; }
        }

        public void CreateAndAssignSchools(Summoner player)
        {
            School[] schools = new School[5];

            schools[0] = new School("Necromancy");
            schools[0].AddSkill(new Skill("Animalism", 1, 50, 2));
            schools[0].AddSkill(new Skill("Здоровье", 1, 30, 2));

            schools[1] = new School("Animalism");
            schools[1].AddSkill(new Skill("Урон", 1, 40, 1));
            schools[1].AddSkill(new Skill("Здоровье", 1, 20, 1));


            schools[2] = new School("Demonology");
            schools[2].AddSkill(new Skill("damage", 1, 50, 2));
            schools[2].AddSkill(new Skill("health", 1, 30, 2));


            schools[2] = new School("High-speed.");
            schools[2].AddSkill(new Skill("damage", 1, 50, 2));
            schools[2].AddSkill(new Skill("health", 1, 30, 2));


            schools[3] = new School("Heavyweight");
            schools[3].AddSkill(new Skill("damage", 1, 50, 1));
            schools[3].AddSkill(new Skill("health", 1, 30, 2));
            for (int i = 0; i < 3; i++)
            {

                player.AddSchoolKnowledge(schools[i], 1);
                player.AddSchool(schools[i]);
            }
        }


        public Unit GetTargetFromUser(Summoner player, Location currloc)
        {
            foreach (var unit in player.GetUnits())
            {
                if ((0 <= unit.Place.X && unit.Place.X < gameMap.Cols) && (unit.Place.Y == currloc.Y))
                {
                    return unit;
                }
            }

            return null;
        }

        public enum AIAction
        {
            AccumulateEnergy,
            LearnNewSchool,
            SummonUnit,
            TakeAI
        }
        public AIAction ChooseAIAction()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, 101);

            try
            {
                if (randomNumber <= 30)
                {
                    return AIAction.AccumulateEnergy;
                }
                else if (randomNumber <= 60)
                {
                    return AIAction.LearnNewSchool;
                }
                else if (randomNumber <= 75)
                {
                    return AIAction.SummonUnit;
                }
                else
                {
                    return AIAction.TakeAI;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error choosing AI action: {ex.Message}");
                return AIAction.AccumulateEnergy;
            }
        }

        private static readonly object lockObject = new object();
        private static readonly Random random = new Random();

        public void PerformAIAction(AIAction action, Summoner player, Summoner AI)
        {
            try
            {
                switch (action)
                {
                    case AIAction.AccumulateEnergy:
                        lock (lockObject)
                        {
                            AI.AccumulateEnergy();
                            AI.Initiative = 0;
                        }
                        break;

                    case AIAction.LearnNewSchool:
                        lock (lockObject)
                        {
                            int randomNumber = random.Next(0, AI.GetAllSchools().Count() - 1);
                            if (AI.Experience > AI.GetAllSchools()[randomNumber].Cost)
                            {
                                AI.Experience -= AI.GetAllSchools()[randomNumber].Cost;
                                AI.GetAllSchools()[randomNumber].UpLvl();
                                AI.Initiative = 0;
                            }
                            else
                            {
                                throw new ArgumentException($"Not Enough Experience");
                            }
                        }
                        break;

                    case AIAction.SummonUnit:
                        lock (lockObject)
                        {
                            Unit tmp = CreateAIUnit(AI);
                            Location temp = gameMap.FindFreeCellInLowerHalf();
                            AI.AddUnit(tmp);
                            gameMap.PlaceUnit(tmp, temp.X, temp.Y);
                            AI.Initiative = 0;
                        }
                        break;

                    case AIAction.TakeAI:
                        lock (lockObject)
                        {
                            List<Unit> units = AI.GetUnits();
                            if (units.Count > 0)
                            {
                                Console.WriteLine("Список юнитов:");

                                foreach (var unit in units)
                                {
                                    if (player.Alive == false)
                                    {
                                        break;
                                    }

                                    Console.WriteLine($"Имя юнита: {unit.Name}");

                                    ThreadPool.QueueUserWorkItem((obj) =>
                                    {
                                        var unitInfo = (Tuple<Unit, int>)obj;
                                        Unit currentUnit = unitInfo.Item1;
                                        int currentDelay = unitInfo.Item2;

                                        Thread.Sleep(currentDelay);

                                        MoveOrAttack(currentUnit);
                                    }, new Tuple<Unit, int>(unit, 1000 / units.Count));
                                }
                                AI.Initiative = 0;
                            }
                            else
                            {
                                Console.WriteLine("Список юнитов пуст.");
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error performing AI action: {ex.Message}");

            }
        }

        public void MoveOrAttack(Unit unit)
        {
            try
            {
                lock (lockObject)
                {
                    int randomChoice = random.Next(2);

                    if (randomChoice == 0)
                    {
                        gameMap.MoveUnit(unit, unit.Place.X - 1 * unit.Speed, unit.Place.Y, 1);
                    }
                    else
                    {
                        var target = GetTargetFromUser(this.first, unit.Place);
                        if ((target == null) && (this.first.CurrentHealth > 0))
                        {
                            unit.AttackSummoner(this.first);
                            this.second.Initiative = 0;
                            return;
                        }

                        if ((target != null) && (target.CurrentHealth >= 0))
                        {
                            unit.AttackUnit(target);
                            if (target.Alive == false)
                            {
                                this.second.Experience += target.Experience;
                                this.second.Initiative = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void TURNAI(Summoner player, Summoner AI)
        {
            try
            {
                if (AI.CurrentEnergy < 300)
                {
                    PerformAIAction(AIAction.AccumulateEnergy, player, AI);
                    AI.Initiative = 0;
                }
                else
                {

                    AIAction chosenAction = ChooseAIAction();


                    PerformAIAction(chosenAction, player, AI);

                    AI.Initiative = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AI turn: {ex.Message}");

            }
        }
        public Unit CreateAIUnit(Summoner player)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, player.GetAllSchools().Count() - 1);
            return new Unit("AI", player.GetAllSchools()[randomNumber]);

        }


        public bool GameIsOver()
        {
            if (first.Alive == false || second.Alive == false)
            {
                return true;
            }




            return false;
        }

    }
}
