/*#define CATCH_CONFIG_MAIN
#include"ch.hpp"
#include"charactic.h"
// Тестирование класса Prog2::charactic
TEST_CASE("CharacticConstructor") {
    SECTION("ValidConstructor") {
        REQUIRE_NOTHROW(Prog2::charactic("Strength", 5));
        REQUIRE_NOTHROW(Prog2::charactic("Intelligence", 10));
    }

    SECTION("InvalidConstructor") {
        REQUIRE_THROWS_AS(Prog2::charactic("", 5), std::invalid_argument);
        REQUIRE_THROWS_AS(Prog2::charactic("Agility", 0), std::invalid_argument);
        REQUIRE_THROWS_AS(Prog2::charactic("Charisma", -2), std::invalid_argument);
    }
}

TEST_CASE("CharacticMethods") {
    Prog2::charactic c("Strength", 5);

    SECTION("setV") {
        REQUIRE_NOTHROW(c.setV(10));
        REQUIRE(c.getV() == 10);
        REQUIRE_THROWS_AS(c.setV(0), std::invalid_argument);
    }

    SECTION("setN") {
        REQUIRE_NOTHROW(c.setN("Agility"));
        REQUIRE(c.getName() == "Agility");
        REQUIRE_THROWS_AS(c.setN(""), std::invalid_argument);
    }

    SECTION("valueUP") {
        REQUIRE_NOTHROW(c.valueUP(3));
        REQUIRE(c.getV() == 8);
    }

    SECTION("checkD") {
        REQUIRE(c.checkD(10, 1) == true); 
    }

    SECTION("checkGD") {
        REQUIRE(c.checkGD(10,1) == true); 
    }

    SECTION("checkIGD") {
        REQUIRE(c.checkIGD(10,1) == true); 
    }
}

// Тестирование класса Prog2::CharacterArray
TEST_CASE("CharacterArrayConstructor") {
    SECTION("DefaultConstructor") {
        Prog2::CharacterArray charArray;
        REQUIRE(charArray.getCapacity() == 10);
        REQUIRE(charArray.getSize() == 0);
        REQUIRE(charArray.getStatus() != nullptr);
    }

    SECTION("ParametrizedConstructor") {
        const char* charNames[] = { "Strength", "Intelligence", "Agility" };
        Prog2::CharacterArray charArray(charNames, 3);
        REQUIRE(charArray.getCapacity() == 3);
        REQUIRE(charArray.getSize() == 3);
        REQUIRE(charArray.getStatus() != nullptr);
    }

    SECTION("InvalidConstructor") {
        const char* emptyName[] = { "" };
        REQUIRE_THROWS_AS(Prog2::CharacterArray(emptyName, 1), std::invalid_argument);
        REQUIRE_THROWS_AS(Prog2::CharacterArray(nullptr, 0), std::invalid_argument);
    }
}

TEST_CASE("CharacterArrayMethods") {
    const char* charNames[] = { "Strength", "Intelligence", "Agility" };
    Prog2::CharacterArray charArray(charNames, 3);

    SECTION("getValue") {
        int value = 0;
        REQUIRE(charArray.getValue("Strength", value) == true);
        REQUIRE(value == 1); // По умолчанию значение равно 1
        REQUIRE(charArray.getValue("Charisma", value) == false);
    }

    SECTION("operator[]") {
        REQUIRE_NOTHROW(charArray["Strength"]);
        REQUIRE_THROWS_AS(charArray["Charisma"], std::invalid_argument);
    }

    SECTION("operator()") {
        Prog2::charactic c("Strength", 5);
        REQUIRE(charArray(10, 1, c, 1) == true);
        REQUIRE_THROWS_AS(charArray(10, 5, c), std::invalid_argument);
    }

    SECTION("sortName") {
        charArray.sortName();
        REQUIRE(charArray.getValFrInd(0).getName() == "Agility");
        REQUIRE(charArray.getValFrInd(1).getName() == "Intelligence");
        REQUIRE(charArray.getValFrInd(2).getName() == "Strength");
    }

    SECTION("maxObjValue") {
        const char* charNames2[] = { "Strength", "Intelligence", "Agility", "Charisma" };
        Prog2::CharacterArray charArray2(charNames2, 4);
        Prog2::charactic maxObj = charArray2.maxObjValue(charNames, 3);
        REQUIRE(maxObj.getName() == "Strength");
    }
}

TEST_CASE("InputOutputOperators") {
    SECTION("CharacticInputOutput") {
        Prog2::charactic inputObj;
        std::stringstream input("19\nSara\n");
        input >> inputObj;
        std::stringstream out;
        REQUIRE(inputObj.getName() == "Sara");
        REQUIRE(inputObj.getV() == 19);
        out << inputObj;
        std::string expected_output = "Name: Sara, Value: 19";
        REQUIRE(out.str() == expected_output);
    }

    SECTION("CharacterArrayInputOutput") {
        Prog2::CharacterArray charArray;
        const char* charNames[] = { "Strength", "Intelligence", "Agility" };
        std::stringstream input("3\nStrength 5\nIntelligence 10\nAgility 8");
        input >> charArray;
        int value;
        REQUIRE(charArray.getSize() == 3);
        REQUIRE(charArray.getValue("Strength", value) == true);
        REQUIRE(value == 5);

        std::stringstream output;
        output << charArray;
        std::string expected_output = "Name: Strength, Value: 5\nName: Intelligence, Value: 10\nName: Agility, Value: 8\n";
        REQUIRE(output.str() == expected_output);
    }
}*/