#include"charactic.h"
#include <string>
#include <format>
#include <algorithm>

namespace Prog2 {
	charactic::charactic(const std::string& n, int v) {
		if (n.empty()) {
			throw std::invalid_argument("Invalid name");
		}
		if (v < 1 || v > 20) {
			throw std::invalid_argument("Invalid value");
		}
		name = n;
		value = v;
	}
	charactic& charactic::setV(const int v) {
		if (v < 1 || v > 20) {
			throw std::invalid_argument("invalid value");
		}
		value = v;
		return *this;
	}
	charactic& charactic::setN(const std::string& n) {
		if (n.empty()) {
			throw std::invalid_argument("invalid name");
		}
		name = n;
		return *this;
	}
	charactic& charactic::valueUP(int c) {
		if (value + c < 1 || value + c > 20) {
			throw std::invalid_argument("invalid value");
		}
		value += c;
		return *this;
	}

	bool charactic::checkD(const int number, int test) {
		std::mt19937 gen;
		if (test != 1) {
			std::random_device rd;
			gen = std::mt19937(rd());
		}
		else {
			unsigned int seed = 42;
			gen = std::mt19937(seed);
		}
		std::uniform_int_distribution<int> distribution(1, 20);
		int rn = distribution(gen);
		if (rn == 1) {
			return false;
		}
		if (rn == 20) {
			return true;
		}
		int h = getV() + rn;
		if (h >= number) {
			return true;
		}
		return false;

	}
	bool charactic::checkGD(const int number, int test) {
		std::mt19937 gen;
		if (test != 1) {
			std::random_device rd;
			gen = std::mt19937(rd());
		}
		else {
			unsigned int seed = 42;
			gen = std::mt19937(seed);
		}
		std::uniform_int_distribution<int> distribution(1, 20);
		int first = distribution(gen);
		int second = distribution(gen);
		int rn = std::max(first, second);
		if (rn == 1) {
			return false;
		}
		if (rn == 20) {
			return true;
		}
		int h = getV() + rn;
		if (h >= number) {
			return true;
		}
		return false;

	}

	bool charactic::checkIGD(const int number, int test) {
		std::mt19937 gen;
		if (test != 1) {
			std::random_device rd;
			gen = std::mt19937(rd());
		}
		else {
			unsigned int seed = 42;
			gen = std::mt19937(seed);
		}
		std::uniform_int_distribution<int> distribution(1, 20);
		int first = distribution(gen);
		int second = distribution(gen);
		int rn = std::min(first, second);
		if (rn == 1) {
			return false;
		}
		if (rn == 20) {
			return true;
		}
		int h = getV() + rn;
		if (h >= number) {
			return true;
		}
		return false;


	}

	CharacterArray::CharacterArray() {
		characters = new charactic[10];
		size = 0;
		capacity = 10;
	}

	CharacterArray::CharacterArray(const std::string& name, int value) {
		charactic obj(name, value);
		characters = new charactic[10];
		characters[0] = obj;
		size = 1;
		capacity = 10;

	}

	CharacterArray::CharacterArray(const char** strings, int sz)
	{
		if (strings == nullptr) {
			throw std::invalid_argument("invalid strings array");
		}
		if (sz < 1) {
			throw std::invalid_argument("Invalid size");
		}
		characters = new charactic[sz];
		charactic obj;
		for (int i = 0; i < sz; i++) {
			obj = charactic(strings[i]);
			characters[i] = obj;

		}
		size = sz;
		capacity = sz;
	}

	bool CharacterArray::getValue(const std::string& name, int& v) {
		if (characters == nullptr) {
			throw std::invalid_argument("nullptr");
		}
		for (int i = 0; i < capacity; i++) {
			if (characters[i].getName() == name) {
				v = characters[i].getV();
				return true;

			}
		}
		return false;
	}

	charactic& CharacterArray::operator[](const std::string& name)
	{
		for (int i = 0; i < capacity; i++) {
			if (characters[i].getName() == name) {
				return characters[i];

			}
		}
		throw std::invalid_argument("Not found charac");
	}

	bool CharacterArray::operator()(int num, int mode, charactic obj, int test) const
	{
		if ((mode < 1) || (mode > 3)) {
			std::cout << "Pizdec" << std::endl;
			throw std::invalid_argument("Invalid version");
		}
		
			for (int i = 0; i < capacity; i++) {
				if (characters[i].getName() == obj.getName()) {
					if (test != 1) {
						if (mode == 1) { return obj.checkD(num); };
						if (mode == 2) { return obj.checkGD(num); };
						if (mode == 3) { return obj.checkIGD(num); };
					}
					else {
						if (mode == 1) { return obj.checkD(num,1); };
						if (mode == 2) { return obj.checkGD(num,1); };
						if (mode == 3) { return obj.checkIGD(num,1); };
					}

				}
			
			return false;
		}

	}

	CharacterArray& CharacterArray::operator+=(const charactic& obj)
	{
		int oldsize = capacity;
		if (size >= capacity) {
			capacity *= 2;
		}
		else if (capacity == 0) {
			capacity++;
		}
		charactic* na = new charactic[capacity];
		std::copy(characters, characters + capacity, na);
		delete[] characters;
		characters = na;
		characters[size] = obj;
		size++;
		return *this;
	}
	bool compareByName(const charactic& obj1, const charactic& obj2) {
		return obj1.getName() < obj2.getName();
	}
	CharacterArray& CharacterArray::sortName()
	{
		if (characters == nullptr) {
			throw std::invalid_argument("nullptr");
		}
		std::sort(characters, characters + size, compareByName);
	}

	charactic CharacterArray::maxObjValue(const char** strings, int sz)
	{
		if (characters == nullptr) {
			throw std::invalid_argument("nullptr");
		}
		if (strings == nullptr) {
			throw std::invalid_argument("Bad strings");
		}
		int maxvalue = characters[0].getV();
		charactic maxobj = characters[0];
		for (int i = 0; i < size; i++) {
			for (int j = 0; j < sz; j++) {
				if ((characters[i].getName() == strings[j]) && (maxvalue < characters[i].getV())) {
					maxvalue = characters[i].getV();
					maxobj = characters[i];
				}

			}
		}
		return maxobj;
	}
	CharacterArray::CharacterArray(CharacterArray& other) noexcept
	{
		size = other.getSize();
		capacity = other.getCapacity();
		characters = new charactic[capacity];
		charactic* a = other.getStatus();
		for (int i = 0; i < size; i++) {
			characters[i] = other.characters[i];
		}
	}

	CharacterArray::CharacterArray(CharacterArray&& other) noexcept
	{
		size = other.getSize();
		capacity = other.getCapacity();
		characters = other.getStatus();
		other.size = 0;
		other.capacity = 0;
		other.characters = nullptr;
	}

	CharacterArray& CharacterArray::operator=(CharacterArray&& other) noexcept
	{
		if (this != &other) {
			if (characters != nullptr) {
				delete[] characters;
			}
			size = other.getSize();
			capacity = other.getCapacity();
			characters = other.getStatus();
			other.size = 0;
			other.capacity = 0;
			other.characters = nullptr;
		}
		else {
			throw std::invalid_argument("Self opreperation");
		}
	}

	CharacterArray& CharacterArray::setSize(const int num)
	{
		if (num < 1) {
			throw std::invalid_argument("Bad index");
		}
		size = num;
	}

	CharacterArray& CharacterArray::setCapacity(const int num)
	{
		if (num < 1) {
			throw std::invalid_argument("Bad index");
		}
		capacity = num;
	}

}
	







