using System.Collections.Generic;
using System.Linq;

namespace MyWpfSudoku.Model.Analyzer
{
    /// <summary>
    /// 横（X軸）、縦（Y軸）、ボックス（3x3）で決まっている数字を候補の数字から消していく.
    /// </summary>
    public class Scrub : IAnalyzer
    {
        /// <summary>
        /// 解析処理.
        /// </summary>
        /// <param name="sudokuGrid"></param>
        /// <returns></returns>
        public SudokuGrid Analyze(SudokuGrid sudokuGrid)
        {
            for (int y = 0; y < sudokuGrid.GridSizeY; y++)
            {
                for (int x = 0; x < sudokuGrid.GridSizeX; x++)
                {
                    Cell cell = sudokuGrid.GetCell(x, y);
                    // すでに数字が決定している場合は次のセルへ進める.
                    if (cell.IsDecided)
                    {
                        continue;
                    }

                    // 横（X軸）・縦（Y軸）・ボックス（3x3）で決定している数字を取得する.
                    List<int> decideDigitColumn = GetDecidedDigits(sudokuGrid.GetColumn(y));
                    List<int> decideDigitRow = GetDecidedDigits(sudokuGrid.GetRow(x));
                    List<int> decideDigitBox = GetDecidedDigits(sudokuGrid.GetBox(x, y));

                    // 候補の数字の差分を取得する.
                    List<int> diffCandidates = cell.GetCandidates
                        .Except(decideDigitColumn)
                        .Except(decideDigitRow)
                        .Except(decideDigitBox).ToList();

                    cell.SetCandidates(diffCandidates);
                }
            }

            return sudokuGrid;
        }

        /// <summary>
        /// 決まっている数字を取得する処理.
        /// </summary>
        /// <param name="cells"></param>
        /// <returns></returns>
        private List<int> GetDecidedDigits(List<Cell> cells)
        {
            List<int> decidedDigits = new List<int>();
            foreach(Cell cell in cells)
            {
                if (!cell.IsDecided)
                {
                    continue;
                }

                decidedDigits.Add(cell.Digit);
            }

            return decidedDigits;
        }
    }
}
