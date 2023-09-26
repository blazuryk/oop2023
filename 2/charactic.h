#ifndef CHARACTICS
#define CHARACTICS
#include<iostream>
#include<string>
#include<cmath>
#include <random>
#include <sstream>
namespace Prog2 {
	class charactic {
	private:
		std::string name;
		int value;
	public:
		explicit charactic(const std::string& name, int value=1);
		explicit charactic() : name("Strenght"), value(1) {};
		int getV() const {return value;}
		std::string getName() const {return name;}
		charactic& setV(const int v);
		charactic& setN(const std::string& n);
		charactic& valueUP(int c);
		charactic input();
		bool checkD(int number);
		bool checkGD(int number);
		bool checkIGD(int number);
//		bool output(charactic instance);
/*
		перегрузка оператора value изменение += имеется ввиду увелечение уровня характеристики от дургой характеристики или значения
проверки возврат значения а не вывод в поток
ВСЕ ТЕСТЫ НА ПРОСТОЙ КЛАСС ИНАЧЕ МЕЙН*/
		charactic& operator+=(int increment) {
			value += increment;
			return *this;
		}
		std::ostream& operator<<(std::ostream& os) {
			os << "Name: " << this->name << ", Value: " << this->value;
			return os;
		}

		std::istream& operator>>(std::istream& is) {
			std::cout << "Enter Name: ";
			is >> this->name;
			std::cout << "Enter Value: ";
			is >> this->value;
			return is;
		}
		std::stringstream& operator>>(std::stringstream& ss) {
			ss >> this->value;
			ss.ignore();
			std::getline(ss, this->name);
			return ss;
		}
	};
}
#endif
