namespace Notes
{
    /// <summary>
    /// 小節データクラス
    /// </summary>
    [System.Serializable]
    public class Bar
    {
        /// <summary>
        /// 小節番号
        /// </summary>
        public int barNum = 0;
        /// <summary>
        /// 拍子
        /// </summary>
        public Measure measure = new Measure();
        /// <summary>
        /// 1小節の分割数
        /// </summary>
        public int LPB = 0;
        /// <summary>
        /// 編集中のノーツデータ(空も含む)
        /// </summary>
        public Note[,] notesArray = new Note[DataManager.MAX_LANE, DataManager.MAX_LPB];


        public void Init(int num)
        {
            barNum = num;
            measure = DataManager.Instance.GetMeasure();
            LPB = DataManager.Instance.GetLPB();
            notesArray = new Note[DataManager.MAX_LANE, DataManager.MAX_LPB];

            // ノーツのタイミングを再計算
        }

        public void SetLPB(int lpb)
        {
            int LPB = this.LPB;
            // 現在のLPBと変更後のLPBの最大公約数を求める EX: 24:16 = 8
            int gcd = MyMath.Gcd(LPB, lpb);
            // 現在のLPBを最大公約数で割る EX: 24 / 8 = 3
            int multiple = LPB / gcd;
            // 変更後のLPBを最大公約数で割る EX: 16 / 8 = 2
            int changedMultiple = lpb / gcd;


            // 変更後のの各拍は、現在の各拍 * [変更後のLPBを最大公約数で割った倍数] / [現在のLPBを最大公約数で割った倍数]に等しい
            // EX: (LPB:24)3n = 2n (LPB:16)    EX2: (LPB:48)3n = n (LPB:16)


            Bar tempBar = new Bar();
            tempBar.notesArray = this.notesArray;

            Init(barNum);
            // 効率が良いので列ごとに検索
            UnityEngine.Debug.Log($"{LPB}を{lpb}に変更");
            for (int i = 0; i < LPB; i++)
            {
                // 現在の各拍 = 現在のLPBを最大公約数で割った倍数のとき
                if (i % multiple == 0)
                {
                    UnityEngine.Debug.Log(i * changedMultiple / multiple + "には" + i);
                    // 変更後のLPBを最大公約数で割った数の
                    for (int j = 0; j < DataManager.Instance.GetLane(); j++)
                        notesArray[j, i * changedMultiple / multiple] = tempBar.notesArray[j, i];
                }
            }
        }
    }

    /// <summary>
    /// ノーツ1つに含まれる情報
    /// </summary>
    public struct Note
    {
        /// <summary>
        /// ノーツの種類を識別する番号(ユーザー定義)
        /// </summary>
        public int notesType;
        /// <summary>
        /// どのレーンに属するか
        /// </summary>
        public int lane;
        /// <summary>
        /// 開始から何フレーム目にくるのか(60fps?)
        /// </summary>
        public int frame;
        /// <summary>
        /// 開始から何秒で来るのか
        /// </summary>
        public float time;
        /// <summary>
        /// 拍の長さ(sec)
        /// </summary>
        public float length;


        // TODO:実装予定
        //public float highSpeed;
        //public float BPM;
        //public float offset;
        //public List<Note> notes;
    }

    public struct Measure
    {
        public int denominator;
        public int numerator;
        public Measure(int denom, int num)
        {
            denominator = denom;
            numerator = num;
        }
    }
}
