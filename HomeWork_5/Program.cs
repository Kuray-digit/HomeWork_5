using System;
using System.Collections.Generic;

namespace Homework
{
    class Program
    {
        public static LinkedList<LinkedList<int>> MatrixMaker(string[] array)
        {
            LinkedList<LinkedList<int>> matrix = new LinkedList<LinkedList<int>>();
            LinkedList<int> currentRow = new LinkedList<int>();

            foreach (string i in array)
            {
                currentRow.AddLast(int.Parse(i));

                if (currentRow.Count == 2)
                {
                    matrix.AddLast(currentRow);
                    currentRow = new LinkedList<int>();
                }
            }

            return matrix;
        }

        public static void Main(string[] args)
        {
            LinkedList<LinkedList<int>> firstMatrix = new LinkedList<LinkedList<int>>();
            LinkedList<LinkedList<int>> secondMatrix = new LinkedList<LinkedList<int>>();

            Console.WriteLine("Введите значения элементов матрицы:");
            string[] firstArray = (Console.ReadLine() ?? "").Split();
            firstMatrix = MatrixMaker(firstArray);

            Console.WriteLine("Введите значения элементов матрицы:");
            string[] secondArray = (Console.ReadLine() ?? "").Split();
            secondMatrix = MatrixMaker(secondArray);


            LinkedList<LinkedList<int>> multiplication = new LinkedList<LinkedList<int>>();

            int[][] firstArr = new int[2][];
            int[][] secondArr = new int[2][];

            int rowIndex = 0;
            foreach (LinkedList<int> row in firstMatrix)
            {
                firstArr[rowIndex] = new int[2];
                int colIndex = 0;
                foreach (int val in row)
                {
                    firstArr[rowIndex][colIndex] = val;
                    colIndex++;
                }
                rowIndex++;
            }

            rowIndex = 0;
            foreach (LinkedList<int> row in secondMatrix)
            {
                secondArr[rowIndex] = new int[2];
                int colIndex = 0;
                foreach (int val in row)
                {
                    secondArr[rowIndex][colIndex] = val;
                    colIndex++;
                }
                rowIndex++;
            }


            for (int i = 0; i < 2; i++)
            {
                LinkedList<int> resultRow = new LinkedList<int>();
                for (int j = 0; j < 2; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < 2; k++)
                    {
                        sum += firstArr[i][k] * secondArr[k][j];
                    }
                    resultRow.AddLast(sum);
                }
                multiplication.AddLast(resultRow);
            }

            foreach (LinkedList<int> row in multiplication)
            {
                foreach (int i in row)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}