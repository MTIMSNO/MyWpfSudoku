using MyWpfSudoku.Model;
using MyWpfSudoku.Model.Analyzer;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace MyWpfSudoku.ViewModel
{
    public class MainWindowViewModel
    {
        /// <summary>
        /// 画面に表示する数独データ.
        /// </summary>
        public ObservableCollection<Column> SudokuData { get; set; } = new ObservableCollection<Column>();

        /// <summary>
        /// 数独グリッド.
        /// </summary>
        private SudokuGrid sudokuGrid;

        /// <summary>
        /// 取込ボタン押下時処理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnImportClick(object sender, RoutedEventArgs e)
        {
            try
            {
                sudokuGrid = CsvReader.Read();
                SetSudokuData(sudokuGrid);
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        /// <summary>
        /// 解析ボタン押下時処理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnAnalysisClick(object sender, RoutedEventArgs e)
        {
            // CSV未取込の場合、処理を終了する.
            if (sudokuGrid == null)
            {
                return;
            }

            SudokuAnalyzer analyzer = new SudokuAnalyzer();
            sudokuGrid = analyzer.Analyze(sudokuGrid);

            SetSudokuData(sudokuGrid);
        }

        /// <summary>
        /// SudokuData反映処理.
        /// </summary>
        /// <param name="sudokuGrid"></param>
        private void SetSudokuData(SudokuGrid sudokuGrid)
        {
            SudokuData.Clear();
            for (int y = 0; y < sudokuGrid.GridSizeY; y++)
            {
                SudokuData.Add(new Column(sudokuGrid.GetColumn(y)));
            }
        }
    }
}
