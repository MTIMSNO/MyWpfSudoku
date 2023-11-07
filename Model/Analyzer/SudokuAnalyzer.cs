using System.Collections.Generic;

namespace MyWpfSudoku.Model.Analyzer
{
    public class SudokuAnalyzer
    {
        /// <summary>
        /// 解析処理リスト.
        /// </summary>
        private List<IAnalyzer> analyzers = new List<IAnalyzer>();

        /// <summary>
        /// コンストラクタ.
        /// </summary>
        public SudokuAnalyzer()
        {
            // 解析処理を増やす場合、下記に追加する.
            analyzers.Add(new Scrub());
            analyzers.Add(new Alone());
        }

        /// <summary>
        /// 解析処理.
        /// </summary>
        /// <param name="sudokuGrid"></param>
        /// <returns></returns>
        public SudokuGrid Analyze(SudokuGrid sudokuGrid)
        {
            Cell temporaryCell = GetTemporaryCell(sudokuGrid);

            if (temporaryCell == null)
            {
                return sudokuGrid;
            }

            // 候補の数字を仮置きする.
            foreach (int digit in temporaryCell.GetCandidates)
            {
                SudokuGrid copySudokuGrid = sudokuGrid.DeepCopy();

                copySudokuGrid.GetCell(temporaryCell.X, temporaryCell.Y).SetCandidates(new List<int> { digit });

                foreach (IAnalyzer analyzer in analyzers)
                {
                    copySudokuGrid = analyzer.Analyze(copySudokuGrid);
                }

                // 解が無効の場合、次の候補の数字を仮置きする.
                if (!copySudokuGrid.IsValid())
                {
                    continue;
                }

                // 再起的に解析を行い、解が求められたGridを返却する.
                copySudokuGrid = Analyze(copySudokuGrid);
                if (copySudokuGrid.IsAnalyzed())
                {
                    return copySudokuGrid;
                }
            }
            // 解が無効であった場合、元のGridを返却する.
            return sudokuGrid;
        }

        /// <summary>
        /// 仮置きを行うセルを取得する.
        /// </summary>
        /// <param name="sudokuGrid"></param>
        /// <returns></returns>
        private static Cell GetTemporaryCell(SudokuGrid sudokuGrid)
        {
            for (int y = 0; y < sudokuGrid.GridSizeY; y++)
            {
                for (int x = 0; x < sudokuGrid.GridSizeX; x++)
                {
                    Cell cell = sudokuGrid.GetCell(x, y);
                    if (cell.IsDecided)
                    {
                        continue;
                    }

                    return cell;
                }
            }
            return null;
        }
    }
}
