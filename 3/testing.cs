using NUnit.Framework;
using Prog3;
using System;

[TestFixture]
public class MapTests
{
    [Test]
    public void PlaceUnit_WhenCellIsFree_ShouldPlaceUnit()
    {
        // Arrange
        Map map = new Map(5, 5);
        Assert.Throws<InvalidOperationException>(() => new Unit("Unit1", null));

    }

    [Test]
    public void PlaceUnit_WhenCellIsOccupied_ShouldThrowException()
    {

        Map map = new Map(5, 5);
        School testing = new School("Test");
        Unit unit1 = new Unit("Unit1", testing);
        Unit unit2 = new Unit("Unit2", testing);

        map.PlaceUnit(unit1, 2, 2);

        Assert.Throws<InvalidOperationException>(() => map.PlaceUnit(unit2, 2, 2));
    }

    [Test]
    public void MoveUnit_WhenCellIsFree_ShouldMoveUnit()
    {

        Map map = new Map(5, 5);
        Assert.Throws<InvalidOperationException>(() => new Unit("TestUnit", null));

    }

    [Test]
    public void MoveUnit_WhenCellIsOccupied_ShouldThrowException()
    {

        Map map = new Map(5, 5);
        Assert.Throws<InvalidOperationException>(() => new Unit("Unit1", null));
        Assert.Throws<InvalidOperationException>(() => new Unit("Unit2", null));


    }

    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void AttackSummoner_ShouldDealDamageToTarget()
        {
            School school = new School("TestSchool");
            Unit attacker = new Unit("Attacker", school);
            Summoner target = new Summoner("Target");
            target.CurrentHealth = 10000;


            attacker.AttackSummoner(target);


            Assert.AreEqual(9900, target.CurrentHealth);
            Assert.IsFalse(target.Alive);
        }

        [Test]
        public void GetResurent_ShouldReturnResurrectStatus()
        {

            School school = new School("TestSchool");
            MoralUnit unit = new MoralUnit("Unit", school);
            unit.Resurrect = true;

            bool result = unit.GetResurent();

            Assert.IsTrue(result);
        }

    }

    [TestFixture]
    public class MoralUnitTests
    {
        [Test]
        public void AttackUnit_WithLowMorale_ShouldDealReducedDamageAndIncreaseMorale()
        {

            School school = new School("TestSchool");
            MoralUnit moralUnit = new MoralUnit("MoralUnit", school);
            Unit target = new Unit("Target", school);
            moralUnit.DecreaseMorale();

            moralUnit.AttackUnit(target);

            Assert.AreEqual(19900, target.CurrentHealth);
            Assert.AreEqual(5, moralUnit.GetMorale());
        }

        [Test]
        public void AttackSummoner_WithLowMorale_ShouldDealReducedDamageAndIncreaseMorale()
        {
            School school = new School("TestSchool");
            MoralUnit moralUnit = new MoralUnit("MoralUnit", school);
            Summoner target = new Summoner("Target");
            target.CurrentHealth = 10000;
            moralUnit.DecreaseMorale();

            moralUnit.AttackSummoner(target);

            Assert.AreEqual(9900, target.CurrentHealth);
            Assert.AreEqual(5, moralUnit.GetMorale());
        }

        [Test]
        public void TakenDamage_ShouldDecreaseMorale()
        {
            School school = new School("TestSchool");
            MoralUnit moralUnit = new MoralUnit("MoralUnit", school);
            Unit attacker = new Unit("Attacker", school);
            attacker.Damage = 1000;

            moralUnit.TakenDamage(attacker);


            Assert.AreEqual(4, moralUnit.GetMorale());
        }

    }

    [TestFixture]
    public class AmoralUnitTests
    {
        [Test]
        public void AttackUnit_ShouldDealDamageToTarget()
        {

            School school = new School("TestSchool");
            AmoralUnit amoralUnit = new AmoralUnit("AmoralUnit", school);
            Unit target = new Unit("Target", school);

            amoralUnit.AttackUnit(target);

            Assert.AreEqual(19900, target.CurrentHealth);
        }

        [Test]
        public void AttackSummoner_ShouldDealDamageToTarget()
        {

            School school = new School("TestSchool");
            AmoralUnit amoralUnit = new AmoralUnit("AmoralUnit", school);
            Summoner target = new Summoner("Target");
            target.CurrentHealth = 10000;

            amoralUnit.AttackSummoner(target);

            Assert.AreEqual(9900, target.CurrentHealth);
            Assert.IsFalse(target.Alive);
        }

        [Test]
        public void TakenDamage_ShouldReduceHealthBasedOnProtection()
        {

            School school = new School("TestSchool");
            AmoralUnit amoralUnit = new AmoralUnit("AmoralUnit", school);
            Unit attacker = new Unit("Attacker", school);
            attacker.Damage = 1000;

            amoralUnit.TakenDamage(attacker);

            Assert.AreEqual(20000 - (int)(1000 * amoralUnit.Protection), amoralUnit.CurrentHealth);
        }

        [Test]
        public void ResurrectUnits_ShouldNotAffectAmoralUnit()
        {

            School school = new School("TestSchool");
            AmoralUnit amoralUnit = new AmoralUnit("AmoralUnit", school);
            amoralUnit.CurrentHealth = 0;
            amoralUnit.checkHP();

            Assert.IsFalse(amoralUnit.Alive);
        }
    }


    [TestFixture]
    public class GameSetupTests
    {
        [Test]
        public void GameSetup_InitializesPlayersAndMap()
        {

            GameSetup gameSetup = new GameSetup();

            Assert.IsNotNull(gameSetup);
            Assert.IsNotNull(gameSetup.First);
            Assert.IsNotNull(gameSetup.Second);
            Assert.IsNotNull(gameSetup.GameMap);
            Assert.AreEqual("First player", gameSetup.First.Name);
            Assert.AreEqual("AI player", gameSetup.Second.Name);
            Assert.AreEqual(10, gameSetup.GameMap.Rows);
            Assert.AreEqual(10, gameSetup.GameMap.Cols);
        }

        [Test]
        public void GetTargetFromUser_ReturnsCorrectTarget()
        {

            GameSetup gameSetup = new GameSetup();
            Summoner player = gameSetup.First;
            Location currLoc = new Location(1, 1);
            Map gameMap = gameSetup.GameMap;
            Unit enemyUnit = new Unit("Enemy", gameSetup.Second.GetAllSchools()[0]);
            gameMap.PlaceUnit(enemyUnit, 1, 1);


            Unit target = gameSetup.GetTargetFromUser(player, currLoc);


            Assert.IsNull(target);
        }

        [Test]
        public void ChooseAIAction_ReturnsValidAction()
        {

            GameSetup gameSetup = new GameSetup();


            GameSetup.AIAction action = gameSetup.ChooseAIAction();


            Assert.IsNotEmpty(action.ToString());
        }

        [Test]
        public void PerformAIAction_AccumulateEnergy_ShouldIncreaseEnergy()
        {

            GameSetup gameSetup = new GameSetup();
            Summoner AI = gameSetup.Second;
            int initialEnergy = AI.CurrentEnergy;


            gameSetup.PerformAIAction(GameSetup.AIAction.AccumulateEnergy, gameSetup.First, AI);


            Assert.AreEqual(initialEnergy, AI.CurrentEnergy);
        }

        [Test]
        public void PerformAIAction_LearnNewSchool_ShouldLearnSchool()
        {

            GameSetup gameSetup = new GameSetup();
            Summoner AI = gameSetup.Second;
            int initialSchoolsCount = AI.GetAllSchools().Count;


            gameSetup.PerformAIAction(GameSetup.AIAction.LearnNewSchool, gameSetup.First, AI);


            Assert.AreEqual(initialSchoolsCount, AI.GetAllSchools().Count);
        }

        [Test]
        public void PerformAIAction_SummonUnit_ShouldSummonUnit()
        {

            GameSetup gameSetup = new GameSetup();
            Summoner AI = gameSetup.Second;
            int initialUnitsCount = AI.GetUnits().Count;

            gameSetup.PerformAIAction(GameSetup.AIAction.SummonUnit, gameSetup.First, AI);


            Assert.AreEqual(initialUnitsCount + 1, AI.GetUnits().Count);
        }

        [Test]
        public void PerformAIAction_AttackUnit_ShouldAttackUnit()
        {

            GameSetup gameSetup = new GameSetup();
            Summoner AI = gameSetup.Second;
            int initialExperience = AI.Experience;
            Unit enemyUnit = new Unit("Enemy", gameSetup.First.GetAllSchools()[0]);
            gameSetup.GameMap.PlaceUnit(enemyUnit, 1, 1);


        }

        [Test]
        public void PerformAIAction_MoveUnit_ShouldMoveUnit()
        {

            GameSetup gameSetup = new GameSetup();


            School school = new School("TestSchool");
            Unit enemyUnit = new Unit("Enemy", school);
            Summoner AI = new Summoner("Test");
            Location curr = new Location(5, 6);
            enemyUnit.Place = curr;
            AI.AddUnit(enemyUnit);

            int initialRow = 3;

            Assert.AreNotEqual(initialRow, AI.GetUnits()[0].Place.X);
        }



        [TestFixture]
        public class MatrixTests
        {
            [Test]
            public void Matrix_WithPositiveDimensions_Success()
            {
                var matrix = new Matrix<int>(2, 3);
                Assert.AreEqual(2, matrix.rows);
                Assert.AreEqual(3, matrix.cols);
            }

            [Test]
            public void Matrix_WithZeroDimensions_ExceptionThrown()
            {
                Assert.Throws<System.ArgumentException>(() => new Matrix<int>(0, 0));
            }

            [Test]
            public void Matrix_Indexer_GetElement_Success()
            {
                var matrix = new Matrix<int>(2, 2);
                matrix[0, 0] = 1;
                matrix[0, 1] = 2;
                matrix[1, 0] = 3;
                matrix[1, 1] = 4;

                Assert.AreEqual(1, matrix[0, 0]);
                Assert.AreEqual(2, matrix[0, 1]);
                Assert.AreEqual(3, matrix[1, 0]);
                Assert.AreEqual(4, matrix[1, 1]);
            }

            [Test]
            public void Matrix_Indexer_SetElement_Success()
            {
                var matrix = new Matrix<int>(2, 2);
                matrix[0, 0] = 1;
                matrix[0, 1] = 2;
                matrix[1, 0] = 3;
                matrix[1, 1] = 4;

                Assert.AreEqual(1, matrix[0, 0]);
                Assert.AreEqual(2, matrix[0, 1]);
                Assert.AreEqual(3, matrix[1, 0]);
                Assert.AreEqual(4, matrix[1, 1]);
            }

            [Test]
            public void Matrix_AddRow_Success()
            {
                var matrix = new Matrix<int>(2, 2);
                matrix.AddRow();
                Assert.AreEqual(3, matrix.rows);
                Assert.AreEqual(2, matrix.cols);
            }

            [Test]
            public void Matrix_AddColumn_Success()
            {
                var matrix = new Matrix<int>(2, 2);
                matrix.AddColumn();
                Assert.AreEqual(2, matrix.rows);
                Assert.AreEqual(3, matrix.cols);
            }
            [Test]
            public void MatrixEqualityOperatorSuccess()
            {
                var matrix1 = new Matrix<int>(2, 2);
                matrix1.Fill(5);

                var matrix2 = new Matrix<int>(2, 2);
                matrix2.Fill(5);

                var matrix3 = new Matrix<int>(2, 2);
                matrix3.Fill(3);

                Assert.IsTrue(matrix1 == matrix2);
                Assert.IsFalse(matrix1 != matrix2);
                Assert.IsTrue(matrix1 != matrix3);
                Assert.IsTrue(matrix2 != matrix3);
            }

            [Test]
            public void Matrix_Fill_Success()
            {
                var matrix = new Matrix<int>(2, 2);
                matrix.Fill(5);
                Assert.AreEqual(5, matrix[0, 0]);
                Assert.AreEqual(5, matrix[0, 1]);
                Assert.AreEqual(5, matrix[1, 0]);
                Assert.AreEqual(5, matrix[1, 1]);
            }


            [Test]
            public void Matrix_GetEnumerator_Success()
            {
                var matrix = new Matrix<int>(2, 2);
                matrix[0, 0] = 1;
                matrix[0, 1] = 2;
                matrix[1, 0] = 3;
                matrix[1, 1] = 4;

                var enumerator = matrix.GetEnumerator();
                var index = 1;
                while (enumerator.MoveNext())
                {
                    Assert.AreEqual(index, enumerator.Current);
                    index++;
                }
            }
        }

    }
