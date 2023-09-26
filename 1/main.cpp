#include <iostream>
#include <vector>
#include <limits>
#include <functional>
#include <string>
#include"func.h"
static const char* msgs[] = { "0.Quit", "1.Add", "2.Indtask", "3.Print" };
static const int Nmsgs = sizeof(msgs) / sizeof(msgs[0]);
//static int (*fptr[])(Matrix*) = { NULL, insert, id, printMatrix};
std::function<int(Matrix*)>fptr[4] = { NULL, insert, id, printMatrix };
int main()
{
    int number;
    while (true) {
        std::cout << "Enter an integer n: ";
        if (!(std::cin >> number)) {
            if (std::cin.eof()) {
                return 0;
                break;
            }
            std::cerr << "Error: Incorrect input." << std::endl;
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        }
        else if (number <= 0) {
            std::cerr << "Error: Incorrect input." << std::endl;
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        }
        else {
            break;
        }

    }
    int numRows = number;
    while (true) {
        std::cout << "Enter an integer m: ";
        if (!(std::cin >> number)) {
            if (std::cin.eof()) {
                return 0;
                break;
            }
            std::cerr << "Error: Incorrect input." << std::endl;
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        }
        else if(number<=0){
            std::cerr << "Error: Incorrect input." << std::endl;
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        }
        else {
            break;
        }
    }
    int numCols = number;


    Matrix matrix;
    matrix.nrow = numRows;
    matrix.ncol = numCols;
    std::cout << "Input" << std::endl;
    int rc;
    while (rc = dialog(msgs, Nmsgs)) {
        if (!fptr[rc](&matrix)) {
            break;
        }
    }
    printf("Thx, Bye!\n");

}