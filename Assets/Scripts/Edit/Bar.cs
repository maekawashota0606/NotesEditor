/// <summary>
/// 小節データクラス
/// </summary>
public class BarData
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
    public NotesData.Note[,] notesArray = new NotesData.Note[DataManager.MAX_LANE, DataManager.MAX_LPB];

    // いる？
    //public Vector3 origin  = Vector3.zero;
    //public float barDuration = 0;
    //public List<float> verticalLinePosXList = new List<float>(DataManager.MAX_LPB);
    //public List<float> notesDuration = new List<float>(DataManager.MAX_LPB);

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

    public void Init(int num, Measure m, int lpb)
    {
        barNum = num;
        measure = m;
        LPB = lpb;
        //origin = Vector3.zero;
        notesArray = new NotesData.Note[DataManager.MAX_LANE, DataManager.MAX_LPB];

        // ノーツのタイミングを再計算
    }
}