#include "charactic.h"
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
