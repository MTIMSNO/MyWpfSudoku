namespace MyWpfSudoku.Model.Analyzer
{
    /// <summary>
    /// 分析インターフェース.
    /// 分析処理を追加する場合、このインターフェースを使用すること.
    /// </summary>
    public interface IAnalyzer
    {
        /// <summary>
        /// 分析処理.
        /// </summary>
        /// <param name="sudokuGrid"></param>
        /// <returns></returns>
        SudokuGrid Analyze(SudokuGrid sudokuGrid);
    }
}
