//Main to complex class
#include "charactic.h"
#include <iostream>
#include <stdexcept>
#include <limits> 
#include"func.h"
using namespace Prog2;
int main() {
    try {
        CharacterArray characterArray;

        int choice;
        do {
            std::cout << "\nCharacterArray Menu:" << std::endl;
            std::cout << "1. Add a Characteristic" << std::endl;
            std::cout << "2. Get Characteristic by Name" << std::endl;
            std::cout << "3. Display All Characteristics" << std::endl;
            std::cout << "4. Sort by Name" << std::endl;
            std::cout << "5. Check <1,2,3>" << std::endl;
            std::cout << "6. Quit" << std::endl;
            choice = InputInt("Enter your choice: ");

            switch (choice) {
            case 1: {
                std::string name = InputString("Enter Name: ");
                int value = InputInt("Enter level: ");
                charactic charac(name, value);
                characterArray += charac;
                break;
            }
            case 2: {
                std::string name = InputString("Enter Name for find: ");
                int v;
                if (characterArray.getValue(name, v)) {
                    std::cout << "Value of Character " << name << " is " << v << std::endl;
                }
                else {
                    std::cout << "Character " << name << " not found." << std::endl;
                }
                break;
            }
            case 3:
                std::cout << "All Characteristics:" << std::endl;
                std::cout << characterArray;
                break;
            case 4:
                characterArray.sortName();
                std::cout << "Characteristics sorted by Name." << std::endl;
                break;
            case 5: {
                int value = InputInt("Enter difficultly: ");
                if (value < 0) {
                    throw std::invalid_argument("Invalid value");
                    break;
                }
                int index = InputInt("Enter index of ellement: (start from zero)");
                if ((index < 0) || (index>= characterArray.getSize())){
                    throw std::invalid_argument("Invalid index");
                    break;

                }
                int mode = InputInt("Enter mode: ");
                if (characterArray.getSize()!=0) {
                    charactic c =characterArray.getValFrInd(0);
                    if (characterArray(value, mode, c, 1)) {
                        std::cout << "true " << std::endl;
                    }
                    else {
                        std::cout << "false" << std::endl;
                    }
                }
                break;
            }
            case 6:
                std::cout << "Exiting the program." << std::endl;
                break;
            default:
                std::cout << "Invalid choice. Please try again." << std::endl;
            }
        } while (choice != 6);
    }
    catch (const std::exception& e) {
        std::cerr << "An error occurred: " << e.what() << std::endl;
        return 1;
    }

    return 0;
}
/*
// Main for s1mple class
/*#include "charactic.h"
#include <iostream>
#include <stdexcept>

int main() {
    try {
        Prog2::charactic obj;
        int choice;
        do {
            std::cout << "Choose:" << std::endl;
            std::cout << "1. Create object charactic with param" << std::endl;
            std::cout << "2. Input value (setV)" << std::endl;
            std::cout << "3. Input name (setN)" << std::endl;
            std::cout << "4. valueUP" << std::endl;
            std::cout << "5. Check D" << std::endl;
            std::cout << "6. Check GD" << std::endl;
            std::cout << "7. Check IGD" << std::endl;
            std::cout << "8. Output info" << std::endl;
            std::cout << "9. Exit" << std::endl;

            std::cin >> choice;
            int value;
            int number;
            std::string name;
            try {
                switch (choice) {
                case 1:
                    std::cout << "Input name: ";
                    std::cin >> name;
                    std::cout << "Input value: ";
                    std::cin >> value;
                        obj = Prog2::charactic(name, value);
                        break;
                case 2:
                    std::cout << "Input new value: ";
                    std::cin >> value;
                    obj.setV(value);
                    break;
                case 3:
                    std::cout << "Input new name: ";
                    std::cin >> name;
                    obj.setN(name);
                    break;
                case 4:
                    std::cout << "Input value: ";
                    std::cin >> value;
                    obj += value;
                    break;
                case 5: 
                    std::cout << "Input number to check D: ";
                    std::cin >> number;
                    if (obj.checkD(number)) {
                        std::cout << "true" << std::endl;
                    }
                    else {
                        std::cout << "false" << std::endl;
                    }
                    break;
                case 6:
                    int number;
                    std::cout << "Input number to check GD: ";
                    std::cin >> number;
                    if (obj.checkGD(number)) {
                        std::cout << "true" << std::endl;
                    }
                    else {
                        std::cout << "false" << std::endl;
                    }
                    break;
                case 7:
                    std::cout << "Input number to check IGD: ";
                    std::cin >> number;
                    if (obj.checkIGD(number)) {
                        std::cout << "true" << std::endl;
                    }
                    else {
                        std::cout << "false" << std::endl;
                    }
                    break;
                case 8:
                    obj.operator<<(std::cout);
                    std::cout << "" << std::endl;
                    break;
                case 9:
                    std::cout << "Good, bye." << std::endl;
                    break;
                default:
                    std::cerr << "Error choose" << std::endl;
                }
            }
            catch (const std::invalid_argument& ex) {
                std::cerr << "Error: " << ex.what() << std::endl;
            }
        } while ((choice != 9) && (!std::cin.eof()));
        if (std::cin.eof()) {
            std::cout << "EOF" << std::endl;
            return 1;
        }
    }
    catch (const std::exception& ex) {
        std::cerr << "Error: " << ex.what() << std::endl;
    }

    return 0;
}
*/