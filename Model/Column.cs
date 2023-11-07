using System.Collections.Generic;

namespace MyWpfSudoku.Model
{
    public class Column
    {
        public string Cell0Digit { get; private set; }
        public string Cell1Digit { get; private set; }
        public string Cell2Digit { get; private set; }
        public string Cell3Digit { get; private set; }
        public string Cell4Digit { get; private set; }
        public string Cell5Digit { get; private set; }
        public string Cell6Digit { get; private set; }
        public string Cell7Digit { get; private set; }
        public string Cell8Digit { get; private set; }

        /// <summary>
        /// コンストラクタ.
        /// </summary>
        /// <param name="cells"></param>
        public Column(List<Cell> cells)
        {
            if (cells.Count != 9)
            {
                return;
            }

            Cell0Digit = GetDigitString(cells[0]);
            Cell1Digit = GetDigitString(cells[1]);
            Cell2Digit = GetDigitString(cells[2]);
            Cell3Digit = GetDigitString(cells[3]);
            Cell4Digit = GetDigitString(cells[4]);
            Cell5Digit = GetDigitString(cells[5]);
            Cell6Digit = GetDigitString(cells[6]);
            Cell7Digit = GetDigitString(cells[7]);
            Cell8Digit = GetDigitString(cells[8]);
        }

        /// <summary>
        /// セルの数字文字列取得処理.
        /// </summary>
        /// <param name="cell">数字文字列を取得するセル</param>
        /// <returns>数字が0の場合：空文字, それ以外の場合：数字文字列</returns>
        private static string GetDigitString(Cell cell)
        {
            if (cell.Digit == 0)
            {
                return string.Empty;
            }
            return cell.Digit.ToString();
        }
    }
}
