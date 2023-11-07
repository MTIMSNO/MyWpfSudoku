using System;
using System.Collections.Generic;

namespace MyWpfSudoku.Model
{
    public class SudokuGrid
    {
        /// <summary>
        /// 数独全体.
        /// </summary>
        private List<List<Cell>> grid;

        /// <summary>
        /// コンストラクタ.
        /// </summary>
        /// <param name="grid">数独全体</param>
        public SudokuGrid(List<List<Cell>> grid)
        {
            this.grid = grid;
        }

        /// <summary>
        /// ディープコピー.
        /// </summary>
        /// <returns></returns>
        public SudokuGrid DeepCopy()
        {
            List<List<Cell>> copyGrid = new List<List<Cell>>();
            foreach(List<Cell> rows in grid)
            {
                List<Cell> copyRows = new List<Cell>();
                foreach(Cell cell in rows)
                {
                    Cell copyCell = new Cell(cell);
                    copyRows.Add(copyCell);
                }
                copyGrid.Add(copyRows);
            }

            return new SudokuGrid(copyGrid);
        }

        /// <summary>
        /// 横（X軸）のサイズ.
        /// </summary>
        /// <returns></returns>
        public int GridSizeX
        {
            get
            {
                if (grid != null)
                {
                    return grid[0].Count;
                }
                return 0;
            }
        }

        /// <summary>
        /// 縦（Y軸）のサイズ.
        /// </summary>
        /// <returns></returns>
        public int GridSizeY
        {
            get
            {
                if (grid != null)
                {
                    return grid.Count;
                }
                return 0;
            }
        }

        /// <summary>
        /// セルの取得処理.
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <returns>セル</returns>
        public Cell GetCell(int x, int y)
        {
            return grid[y][x];
        }

        /// <summary>
        /// 横（X軸）方向のセルリスト取得処理.
        /// </summary>
        /// <param name="y">Y座標</param>
        /// <returns>横（X軸）方向セルリスト</returns>
        public List<Cell> GetColumn(int y)
        {
            return grid[y];
        }

        /// <summary>
        /// 縦（Y軸）方向のセルリスト取得処理.
        /// </summary>
        /// <param name="x">X座標</param>
        /// <returns>縦（Y軸）セルリスト</returns>
        public List<Cell> GetRow(int x)
        {
            List<Cell> cells = new List<Cell>();
            grid.ForEach(row => cells.Add(row[x]));
            return cells;
        }

        /// <summary>
        /// ボックス（3x3）のセルリスト取得処理.
        /// </summary>
        /// <param name="x">X座標</param>
        /// <param name="y">Y座標</param>
        /// <returns>ボックス（3x3）セルリスト</returns>
        public List<Cell> GetBox(int x, int y)
        {
            // ボックスの左上座標を求める.
            int sX = (int)(Math.Floor((double)(x / 3)) * 3);
            int sY = (int)(Math.Floor((double)(y / 3)) * 3);

            List<Cell> box = new List<Cell>();
            for (int cY = sY; cY < sY + 3; cY++)
            {
                for (int cX = sX; cX < sX + 3; cX++)
                {
                    box.Add(grid[cY][cX]);
                }
            }
            return box;
        }

        /// <summary>
        /// 解が有効か(数字が重複していないか).
        /// </summary>
        /// <returns>true : 有効, false : 無効</returns>
        public bool IsValid()
        {
            // 横（X軸）方向を確認する.
            for (int y = 0; y < GridSizeY; y++)
            {
                if (!IsDuplicateDigits(GetColumn(y)))
                {
                    return false;
                }
            }

            // 縦（Y軸）方向を確認する.
            for (int x = 0; x < GridSizeX; x++)
            {
                if (!IsDuplicateDigits(GetRow(x)))
                {
                    return false;
                }
            }

            // ボックス（3x3）を確認する.
            for (int y = 0; y < GridSizeY; y += 3)
            {
                for (int x = 0; x < GridSizeX; x += 3)
                {
                    if (!IsDuplicateDigits(GetBox(x, y)))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 解が求められているか.
        /// </summary>
        /// <returns>true : 解あり, false : 解なし</returns>
        public bool IsAnalyzed()
        {
            for (int y = 0; y < GridSizeY; y++)
            {
                for (int x = 0; x < GridSizeX; x++)
                {
                    if (!grid[y][x].IsDecided)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 数字の重複確認処理.
        /// </summary>
        /// <param name="cells"></param>
        /// <returns>true : 重複なし, false : 重複あり</returns>
        private static bool IsDuplicateDigits(List<Cell> cells)
        {
            List<int> digits = new List<int>();
            foreach (Cell cell in cells)
            {
                if (cell.Digit != 0)
                {
                    if (digits.Contains(cell.Digit))
                    {
                        return false;
                    }
                    digits.Add(cell.Digit);
                }
            }
            return true;
        }
    }
}
