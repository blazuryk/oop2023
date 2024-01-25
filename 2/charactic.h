#ifndef CHARACTICS
#define CHARACTICS
#include<iostream>
#include<string>
#include<cmath>
#include <random>
#include <sstream>
namespace Prog2 {
	/*!
	*	@class A simple class of characteristics
	*	@brief Contains the name and level of the characteristic
	*	@param name - name of characteristic
	*	@param value - level of characteristic
	*/
	class charactic {
	private:

		std::string name;

		int value;

	public:
		/*!
		* @brief Default constructor with a name(value) assignment
		* @param name - name characteristic 
		* @param value- level of characteristic
		*/
		explicit charactic(const std::string& name, int value = 1);
		/*!
		* @brief  Default constructor with default values
		*/
		explicit charactic() : name("Strenght"), value(1) {};
		/*!
		* @brief Function to get level of characteristic
		* @returns vlaue- level of characteristic 
		*/
		int getV() const { return value; }
		/*!
		* @brief Function to get name of characteristic
		* @returns vlaue- level of characteristic
		*/
		std::string getName() const { return name; }
		/*!
			* @brief Function to set level of characteristic
			* @returns v- level of characteristic
		*/
		charactic& setV(const int v);
		/*!
			* @brief Function to set name of characteristic
			* @param v- new level of characteristic
		*/

		charactic& setN(const std::string& n);
		
		/*!
			* @brief Function to up level of characteristic
			* @param c- the value by which the characteristic will be increased
		*/

		charactic& valueUP(int c);

		/*!
			* @brief Function for routine verification 
			* @param number- the number that adds up to the level
			* * @param test- for test seed
			* @returns True or False depends on the success of the check 
		*/

	
		bool checkD(const int number, int test = 0);
	
		/*!
			* @brief Function for checking with advantage
			* @param number- the number that adds up to the level
			* @param test- for test seed
			* @returns True or False depends on the success of the check
		*/

		bool checkGD(const int number, int test = 0);
		/*!
			* @brief Function for checking with interference
			* @param number- the number that adds up to the level
			* * @param test- for test seed
			* @returns True or False depends on the success of the check
		*/
		bool checkIGD(const int number, int test=0);
		/*!
			* @brief Operator to up level of characteristic
			* @param increment- the value by which the characteristic will be increased
		*/

		charactic& operator+=(int increment) {
			if ((value + increment < 1) || (value + increment > 20)) {
				throw std::invalid_argument("Invalid increment");
			}
				value += increment;
				return *this;

		}
		/*!
			* @brief Operator to output characteristic
			* @param os - output stream
			* @returns os -output stream
		*/
		friend std::ostream& operator<<(std::ostream& os, charactic& obj) {
			
			os << "Name: " << obj.name << ", Value: " << obj.value;
			return os;

		}
		/*!
			* @brief Operator to input characteristic
			* @param is - input stream
			* @returns is - input stream
		*/

		friend std::istream& operator>>(std::istream& is, charactic& obj) {
			

	//		std::cout << "Value: ";
			int value;
			is >> value;
			if ((value < 1) || (value > 20)) {
				is.setstate(std::ios::failbit);
				throw std::invalid_argument("Invalid value");
			}
	//		std::cout << "Name: ";
			is >> obj.name;
			obj.value=value;
			if (!is.good()) {
				throw std::invalid_argument("Invalid input");
			}
			return is;

		}
		/*!
			* @brief Operator to input string
			* @param ss - string stream
			* @returns ss - string stream
		*/
		std::stringstream& operator>>(std::stringstream& ss) {
			
			ss >> this->value;
			ss.ignore();
			std::getline(ss, this->name);
			return ss;

		}

	};
	/*!		
		*   @class A complex class of characteristics
		*	@brief Contains the array of simple class
		*	@param characters  - array of simple class
		*	@param size - current size
		*	@param capacity - maxsize
	*/
	class CharacterArray {

	private:

		charactic* characters = nullptr;
		
		int size;
		
		int capacity;

	public:
		
		/*!
			* @brief  Default constructor with default values
		*/
		explicit CharacterArray();
		/*!
			* @brief Default constructor with a name(value) assignment
			* @param name - name of characteristic
			* @param value- level of characteristic
		*/
		
		explicit CharacterArray(const std::string& name, int value = 1);
		/*!
			* @brief Constructor with a array of names assignment
			* @param strings - array names of characteristic
			* @param sz- size of array names
		*/
		
		explicit CharacterArray(const char** strings, int sz);
		/*!
			* @brief Function to return a value by reference
			* @param name - name of characteristic
			* @param v- variable for returning the level value
			* @returns true if the characteristic is found and false in the opposite case
		*/
		bool getValue(const std::string& name, int& v);
		/*!
			* @brief Getting a characteristic by its name
			* @param name - name of characteristic
			* @returns characteristic
		*/
		charactic& operator[](const std::string& name);
		/*!
			* @brief Passing the test according to a given characteristic with a given complexity in one of the modes
			* @param complexity - the complexity of the check
			* @param mode - to change of the check
			* @param obj - the characteristic supplied for input
			* @returns true if the characteristic is found and check is down good, and false in the opposite case
		*/
		bool operator()(int complexity, int mode, charactic obj, int test=0) const;
		/*!
			* @brief Adding a new feature
			* @param obj - the characteristic supplied for input
		*/
		CharacterArray& operator+=(const charactic& obj);
		/*!
			* @brief Sorting from the name
		*/
		CharacterArray& sortName();
		/*!
			* @brief Search for a characteristic with a higher value
			* @param strings - array names of characteristic
			* @param sz- size of array names
		*/
		
		charactic maxObjValue(const char** strings, int sz);
		/*!
			* @brief Constructor which copy the information from another array
			* @param other - another array
		*/
		
		explicit CharacterArray(CharacterArray& other) noexcept;
		/*!
				* @brief Constructor which move information from another array
				* @param other - another array
		*/
		
		explicit CharacterArray(CharacterArray&& other) noexcept;
		/*!
				* @brief Operator which move information from another array
				* @param other - another array
		*/
		
		CharacterArray& operator=(CharacterArray&& other) noexcept;
		/*!
				* @brief Getting size from characters array
				* @returns size of characters array
		*/
		
		int getSize() const{ return size; };
		/*!
			* @brief Getting maxsize from characters array
			* @returns maxsize of characters array
		*/	
		
		int getCapacity() const{ return capacity; };
		/*!
			* @brief Setting size from characters array
			* @param size of characters array
		*/
		CharacterArray& setSize(const int num);
		/*!
			* @brief Setting maxsize from characters array
			* @param maxsize of characters array
		*/
		
		CharacterArray& setCapacity(const int num);
		/*!
			* @brief Setting status from characters array(nullptr)
		*/
		
		CharacterArray& setStatus() {
			characters = nullptr;
		};
		/*!
			* @brief Getting status from characters array(nullptr)
			* return status characters
		*/
		
		charactic* getStatus() const{ return characters; }
		/*!
			* @brief Getting value from index of characters array
			* @pram num - index 
			* returns value from index
		*/
		
		charactic getValFrInd(int num) const{

			return characters[num];

		}
		/*!
			* @brief Output operator
			* @pram os - output stream
			* @pram arr - array of characteristics
			* returns output stream
		*/
		friend std::ostream& operator<<(std::ostream& os, const CharacterArray& arr) {
		
			for (int i = 0; i < arr.getSize(); i++) {
				os << "Name: " << arr.getValFrInd(i).getName() << ", Value: " << arr.getValFrInd(i).getV() << std::endl;
			}
			return os;
		
		}
		/*!
			* @brief Input operator
			* @pram os - input stream
			* @pram arr - array of characteristics
			* returns input stream
		*/

		
		friend std::istream& operator>>(std::istream& is, CharacterArray& arr) {
		
			int numElements;
	//		std::cout << "Enter the number of elements: ";	
			is >> numElements;
			for (int i = 0; i < numElements; i++) {
				std::string name;
				int value;
	//			std::cout << "Enter Name for Character " << i + 1 << ": ";
				is >> name;
	//			std::cout << "Enter Value for Character " << i + 1 << ": ";
				is >> value;
				charactic charac(name, value);
				arr += charac; 
			}
			return is;
		
		}
	/*!
		* @brief Destructor
	*/

		~CharacterArray() {
		
			delete[] characters;
		
		}
	};
};
#endif
