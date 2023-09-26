#ifndef LAB1
#define LAB1
#include <iostream>
#include <vector>
#include <limits>
#include <functional>
#include <string>
typedef std::function<int(int)> Iback;
struct Ks {
    int row;
    int col;
    int value;
};
struct Matrix {
    int nrow;
    int ncol;
    std::vector<Ks> data;

};

int addElement(int row, int col, int value, Matrix* m);
int getElement(int row, int col, Matrix m);
int insert(Matrix* m);
int printMatrix(Matrix* matrix);
int ch(int el);
int nc(int el);
int getRowSum(Matrix matrix, int row, Iback func);
int id(Matrix* m);
int getInt(int* rc);
int dialog(const char* msgs[], int N);
//std::function<int(Matrix*)>* fptr = { NULL, insert, id, printMatrix };
#endif
