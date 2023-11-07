using System.Collections.Generic;
using System.Linq;

namespace MyWpfSudoku.Model.Analyzer
{
    /// <summary>
    /// 横（X軸）、縦（Y軸）、ボックス（3x3）で候補の数字が1つのセルを見つけ、確定する.
    /// </summary>
    public class Alone : IAnalyzer
    {
        /// <summary>
        /// 解析処理.
        /// </summary>
        /// <param name="sudokuGrid"></param>
        /// <returns></returns>
        public SudokuGrid Analyze(SudokuGrid sudokuGrid)
        {
            int x = 0;
            int y = 0;
            while((x < sudokuGrid.GridSizeX) && (y < sudokuGrid.GridSizeY))
            {
                // 横（X軸）の単独セルを取得する.
                Dictionary<int, Cell> aloneCellsForColumn = GetAloneCells(sudokuGrid.GetColumn(y));
                foreach (KeyValuePair<int, Cell> aloneCell in aloneCellsForColumn)
                {
                    aloneCell.Value.SetCandidates(new List<int> { aloneCell.Key });
                }

                // 縦（Y軸）の単独セルを取得する.
                Dictionary<int, Cell> aloneCellsForRow = GetAloneCells(sudokuGrid.GetRow(x));
                foreach (KeyValuePair<int, Cell> aloneCell in aloneCellsForRow)
                {
                    aloneCell.Value.SetCandidates(new List<int> { aloneCell.Key });
                }

                // ボックス（3x3）の単独セルを取得する.
                Dictionary<int, Cell> aloneCellsForBox = GetAloneCells(sudokuGrid.GetBox(x, y));
                foreach (KeyValuePair<int, Cell> aloneCell in aloneCellsForBox)
                {
                    aloneCell.Value.SetCandidates(new List<int> { aloneCell.Key });
                }

                // 下記のようにx,yを進める.
                // ①, □, □, △, △, △, □, □, □
                // □, □, □, ②, △, △, □, □, □
                // □, □, □, △, △, △, ③, □, □
                // △, ④, △, □, □, □, △, △, △
                // △, △, △, □, ⑤, □, △, △, △
                // △, △, △, □, □, □, △, ⑥, △
                // □, □, ⑦, △, △, △, □, □, □
                // □, □, □, △, △, ⑧, □, □, □
                // □, □, □, △, △, △, □, □, ⑨
                int nextX = x + 3;
                x = nextX % 9 + nextX / 9;
                y++;
            }
            return sudokuGrid;
        }

        /// <summary>
        /// 候補の数字に対してセルが1つの単独セル取得処理.
        /// </summary>
        /// <param name="cells"></param>
        private Dictionary<int, Cell> GetAloneCells(List<Cell> cells)
        {
            // 候補の数字が入るセルごとにまとめる.
            // ex) candidateCells[1] = cell1, cell2
            //     candidateCells[2] = cell2
            //     candidateCells[3] = cell1, cell3
            Dictionary<int, List<Cell>> candidateCells = new Dictionary<int, List<Cell>>();
            foreach (Cell cell in cells)
            {
                foreach (int candidateDigit in cell.GetCandidates)
                {
                    if (candidateCells.ContainsKey(candidateDigit))
                    {
                        candidateCells[candidateDigit].Add(cell);
                        continue;
                    }

                    candidateCells.Add(candidateDigit, new List<Cell>() { cell });
                }
            }

            return candidateCells.Where(candidateCell => candidateCell.Value.Count == 1)
                .ToDictionary(candidateCell => candidateCell.Key, cell => cell.Value.First());
        }
    }
}
