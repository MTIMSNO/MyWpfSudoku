using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyWpfSudoku.Model
{
    /// <summary>
    /// CSVファイル読み込み.
    /// </summary>
    public class CsvReader
    {
        /// <summary>
        /// CSV読み込み処理.
        /// </summary>
        /// <returns></returns>
        public static SudokuGrid Read()
        {
            string filePath = GetCsvFilePath();

            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("ファイルが選択されませんでした");
            }

            using (StreamReader reader = new(File.OpenRead(filePath)))
            {
                List<List<Cell>> grid = new();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(',');

                    List<Cell> column = new();
                    for (int i = 0; i < values.Length; i++)
                    {
                        column.Add(new Cell(values[i], i, grid.Count));
                    }
                    grid.Add(column);
                }
                return new SudokuGrid(grid);
            }
        }
        
        /// <summary>
        /// CSVファイルパスの取得処理.
        /// </summary>
        /// <returns></returns>
        private static string GetCsvFilePath()
        {
            OpenFileDialog dialog = new();
            dialog.Filter = "CSVファイル|*.csv";

            return dialog.ShowDialog() == true ? dialog.FileName : string.Empty;
        }
    }
}
