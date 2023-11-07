using System.Collections.Generic;

namespace MyWpfSudoku.Model
{
    public class Cell
    {
        /// <summary>
        /// セルの数字.
        /// </summary>
        public int Digit { get; private set; }

        /// <summary>
        /// 候補の数字.
        /// </summary>
        private List<int> candidates;

        /// <summary>
        /// X座標.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Y座標.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// コンストラクタ.
        /// </summary>
        /// <param name="digit">数字</param>
        public Cell(string digit, int x, int y)
        {
            X = x;
            Y = y;
            if (string.IsNullOrEmpty(digit))
            {
                Digit = 0;
                candidates = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                return;
            }

            Digit = int.Parse(digit);
            candidates = new List<int>() { Digit };
        }

        /// <summary>
        /// コピーコンストラクタ.
        /// </summary>
        /// <param name="cell"></param>
        public Cell(Cell cell)
        {
            Digit = cell.Digit;
            candidates = new List<int>();
            foreach(int candidate in cell.candidates)
            {
                candidates.Add(candidate);
            }
            X = cell.X;
            Y = cell.Y;
        }


        /// <summary>
        /// 候補の数字取得処理.
        /// </summary>
        /// <returns></returns>
        public List<int> GetCandidates
        {
            get
            {
                return candidates;
            }
        }

        /// <summary>
        /// 候補の数字設定処理.
        /// </summary>
        /// <param name="candidates"></param>
        public void SetCandidates(List<int> candidates)
        {
            this.candidates = candidates;
            // 候補数が1つ場合、セルの値が決まったと判断し数字を設定する.
            if (candidates.Count == 1)
            {
                Digit = candidates[0];
            }
        }

        /// <summary>
        /// セルの数字が決定しているか.
        /// </summary>
        public bool IsDecided
        {
            get
            {
                return candidates.Count == 1;
            }
        }
    }
}
