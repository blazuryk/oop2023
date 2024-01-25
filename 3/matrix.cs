using System;
using System.Collections;
using System.Collections.Generic;

namespace Prog3
{
#pragma warning disable CS0661 // Тип определяет оператор == или оператор !=, но не переопределяет Object.GetHashCode()
#pragma warning disable CS0660 // Тип определяет оператор == или оператор !=, но не переопределяет Object.Equals(object o)
    /// <summary>
    /// template class of matrix
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Matrix<T> : IEnumerable<T>
#pragma warning restore CS0660 // Тип определяет оператор == или оператор !=, но не переопределяет Object.Equals(object o)
#pragma warning restore CS0661 // Тип определяет оператор == или оператор !=, но не переопределяет Object.GetHashCode()
    {
        private T[,] matrix;
        /// <summary>
        /// Getter and Setter of rows
        /// </summary>
        public int rows { get; private set; }
        /// <summary>
        /// Getter and Setter of cols
        /// </summary>
        public int cols { get; private set; }
        /// <summary>
        /// Default constructor for Matrix
        /// </summary>
        public Matrix()
        {
            rows = 0;
            cols = 0;
            matrix = new T[rows, cols];
        }
        /// <summary>
        /// Constructor with rows and cols
        /// </summary>
        /// <param name="rows"> Matrix rows</param>
        /// <param name="cols">Matrix cols</param>
        /// <exception cref="ArgumentException"> Throws exception if rows or cols is lass as zero or zero </exception>
        public Matrix(int rows, int cols)
        {
            if (rows <= 0 || cols <= 0)
            {
                throw new ArgumentException("Matrix dimensions must be positive.");
            }
            this.rows = rows;
            this.cols = cols;
            matrix = new T[rows, cols];
        }
        /// <summary>
        /// Getter and Setter of elements matrix
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">If row or col out of matrix</exception>
        public T this[int row, int col]
        {
            get
            {
                if (row < 0 || row >= rows || col < 0 || col >= cols)
                {
                    throw new IndexOutOfRangeException("Index out of range.");
                }
                return matrix[row, col];
            }
            set
            {
                if (row < 0 || row >= rows || col < 0 || col >= cols)
                {
                    throw new IndexOutOfRangeException("Index out of range.");
                }
                matrix[row, col] = value;
            }
        }

        /// <summary>
        /// Adding element in matrix
        /// </summary>
        /// <param name="value"></param>
        public void Fill(T value)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = value;
                }
            }
        }
        /// <summary>
        /// Adding row in matrix;
        /// </summary>
        public void AddRow()
        {
            if (rows == 0)
            {
                matrix = new T[1, cols];
                rows = 1;
            }
            else
            {
                T[,] newMatrix = new T[rows + 1, cols];
                Array.Copy(matrix, 0, newMatrix, 0, matrix.Length);
                matrix = newMatrix;
                rows++;
            }
        }
        /// <summary>
        /// Adding col in matrix
        /// </summary>
        public void AddColumn()
        {
            if (cols == 0)
            {
                matrix = new T[rows, 1];
                cols = 1;
            }
            else
            {
                T[,] newMatrix = new T[rows, cols + 1];
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        newMatrix[i, j] = matrix[i, j];
                    }
                }
                matrix = newMatrix;
                cols++;
            }
        }

        /// <summary>
        /// Operator Comparisons(==) a and b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(Matrix<T> a, Matrix<T> b)
        {
            if ((a.rows == b.rows) && (a.cols == b.cols))
            {
                for (int i = 0; i < a.rows; i++)
                {
                    for (int j = 0; j < b.rows; j++)
                    {
                        if (!(a[i, j].Equals(b[i, j]))) return false;
                    }
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// Operator Comparisons(!=) a and b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Matrix<T> a, Matrix<T> b) { return !(a == b); }
        /// <summary>
        /// Printing the matrix
        /// </summary>
        public void Print()
        {
            if (rows == 0 || cols == 0)
            {
                //              throw new ArgumentException("Matrix dimensions must be positive.");
                //              Console.WriteLine("Matrix is empty.");
                //              return;
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Iterator for matrix elements
        /// </summary>
        /// <returns>elements of matrix</returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    yield return matrix[i, j];
                }
            }
        }
        /// <summary>
        /// Auxiliary function of the iterator
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
