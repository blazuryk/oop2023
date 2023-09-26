#include <iostream>
#include <vector>
#include <limits>
#include <functional>
#include <string>
#include"func.h"
static const char* msgs[] = { "0.Quit", "1.Add", "2.Indtask", "3.Print" };
static const int Nmsgs = sizeof(msgs) / sizeof(msgs[0]);
int addElement(int row, int col, int value, Matrix* m) {
    if (row < 0 || row >= m->nrow || col < 0 || col >= m->ncol) {
        std::cerr << "Invalid indexes" << std::endl;
        return 1;
    }
    if (value <= 0) {
        std::cerr << "Invalid value" << std::endl;
        return 1;
    }

    for (int i = 0; i < m->data.size(); ++i) {
        if (m->data[i].row == row && m->data[i].col == col) {
            std::cerr << "The element already exists" << std::endl;
            return 1;
        }
    }

    m->data.push_back({ row, col, value });
    return 0;

}

int getElement(int row, int col, Matrix m) {
    if (row < 0 || row >= m.nrow || col < 0 || col >= m.ncol) {
        std::cerr << "Invalid indexes" << std::endl;
        return 0;
    }

    for (int i = 0; i < m.data.size(); ++i) {
        if (m.data[i].row == row && m.data[i].col == col) {
            return m.data[i].value;
        }
    }

    return 0;
}
int insert(Matrix* m) {
    int value;
    int row;
    int col;
    int number;
    while (true) {
        std::cout << "Enter the line number: ";
        if (!(std::cin >> number)) {
            if (std::cin.eof()) {
                return 0;
                break;
            }
            std::cerr << "Error: Incorrect input." << std::endl;
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        }
        else {
            break;
        }
    }
    row = number;
    while (true) {
        std::cout << "Enter the column number: ";
        if (!(std::cin >> number)) {
            if (std::cin.eof()) {
                return 0;
                break;
            }
            std::cerr << "Error: Incorrect input." << std::endl;
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        }
        else {
            break;
        }
    }
    col = number;
    while (true) {
        std::cout << "Enter a value: ";
        if (!(std::cin >> number)) {
            if (std::cin.eof()) {
                return 0;
                break;
            }
            std::cerr << "Invalid line number" << std::endl;
            std::cin.clear();
            std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
        }
        else {
            break;
        }
    }
    value = number;
    addElement(row, col, value, m);
    return 1;
}
int printMatrix(Matrix* matrix) {
    for (int row = 0; row < matrix->nrow; ++row) {
        for (int col = 0; col < matrix->ncol; ++col) {
            int element = getElement(row, col, *matrix);
            std::cout << element << " ";
        }
        std::cout << std::endl;

    }
    return 1;
}
int ch(int el) {
    if (el % 2 == 0) {
        return el;
    }
    else {
        return 0;
    }
}
int nc(int el) {
    if (el % 2 != 0) {
        return el;
    }
    else {
        return 0;
    }
}
int getRowSum(Matrix matrix, int row, Iback func) {
    if (row < 0 || row >= matrix.nrow) {
        std::cerr << "Invalid line number" << std::endl;
        return 0;
    }

    int rowSum = 0;
    for (int col = 0; col < matrix.ncol; ++col) {
        int el = getElement(row, col, matrix);
        rowSum += func(el);
    }

    return rowSum;
}
int id(Matrix* m) {
    std::vector<int> b(m->nrow);

    for (int i = 0; i < m->nrow; i++) {
        b[i] = getRowSum(*m, i, ch) - getRowSum(*m, i, nc);
        std::cout << b[i] << " ";
    }
    std::cout << std::endl;
    return 1;
}
int getInt(int* rc) {
    int number;
    if (!(std::cin >> number)) {
        if (std::cin.eof()) {
            return -1;
        }
        std::cerr << "Error: Incorrect input." << std::endl;
        std::cin.clear();
        std::cin.ignore(std::numeric_limits<std::streamsize>::max(), '\n');
    }
    else {
        *rc = number;
        return 1;
    }
    return 1;
}
int dialog(const char* msgs[], int N) {
    std::string errmsg = "";
    int rc;
    int i, n;
    do {
        std::cout << errmsg << std::endl;
        errmsg = "You are wrong.Repeat please!";
        for (i = 0; i < N; i++) {
            puts(msgs[i]);
        }
        puts("Make your choise: -->");
        n = getInt(&rc);
        if (n == -1) {
            rc = 0;
        }
        if (n == 0) {
            rc = -1;
        }
    } while (rc < 0 || rc >= N);
    return rc;
}
//static int (*fptr[])(Matrix*) = { NULL, insert, id, printMatrix };
//std::function<int(Matrix*)>* fptr = { NULL, insert, id, printMatrix };