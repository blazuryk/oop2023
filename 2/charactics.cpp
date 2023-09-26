#include"charactic.h"
#include <string>
#include <format>
#include <algorithm>

namespace Prog2 {
	charactic::charactic(const std::string& n, int v) {
		if (n.empty()) {
			throw std::invalid_argument("Invalid name");
		}
		if (v < 1) {
			throw std::invalid_argument("Invalid value");
		}
		name = n;
		value = v;
		}
	charactic &charactic::setV(const int v) {
		if (v < 1) {
			throw std::invalid_argument("invalid value");
		}
		value = v;
		return *this;
	}
	charactic &charactic::setN(const std::string& n) {
		if (n.empty()) {
			throw std::invalid_argument("invalid name");
		}
		name = n;
		return *this;
	}
	charactic& charactic::valueUP(int c) {
		value += c;
		return *this;
	}

	bool charactic::checkD(int number) {
		std::random_device rd;
		std::mt19937 gen(rd()); 
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
	bool charactic::checkGD(int number) {
		std::random_device rd;
		std::mt19937 gen(rd());
		std::uniform_int_distribution<int> distribution(1, 20);
		int first = distribution(gen);
		int second = distribution(gen);
		int rn=std::max(first, second);
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

	bool charactic::checkIGD(int number) {
			std::random_device rd;
			std::mt19937 gen(rd()); 
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

}