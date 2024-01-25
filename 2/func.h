#pragma once
#include <iostream>
#include <stdexcept> 
#include <limits>
#include"charactic.h"
int InputInt(const std::string& str) {
    int value;
    while (true) {
        std::cout << str;
        try {
            if (!(std::cin >> value)) {
                throw std::invalid_argument("Invalid value");
            }
            break; 
        }
        catch (const std::invalid_argument& e) {
            std::cerr << e.what() << std::endl;
            std::cin.clear(); 
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n'); 
        }
        catch (const std::ios_base::failure& e) {
            std::cerr << "EOF. Exiting." << std::endl;
            exit(1);
        }
    }
    return value;
}

std::string InputString(const std::string& str) {
    std::string input;
    while (true) {
        std::cout << str;
        try {
            if (!(std::cin >> input)) {
                throw std::invalid_argument("Invalid string");
            }
            break; 
        }
        catch (const std::invalid_argument& e) {
            std::cerr << e.what() << std::endl;
            std::cin.clear(); 
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n'); 
        }
        catch (const std::ios_base::failure& e) {
            std::cerr << " EOF. Exiting." << std::endl;
            exit(1);
        }
    }
    return input;
}