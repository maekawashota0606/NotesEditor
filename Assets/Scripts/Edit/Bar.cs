/// <summary>
/// ���߃f�[�^�N���X
/// </summary>
public class BarData
{
    /// <summary>
    /// ���ߔԍ�
    /// </summary>
    public int barNum = 0;
    /// <summary>
    /// ���q
    /// </summary>
    public Measure measure = new Measure();
    /// <summary>
    /// 1���߂̕�����
    /// </summary>
    public int LPB = 0;
    /// <summary>
    /// �ҏW���̃m�[�c�f�[�^(����܂�)
    /// </summary>
    public NotesData.Note[,] notesArray = new NotesData.Note[DataManager.MAX_LANE, DataManager.MAX_LPB];

    // ����H
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

        // �m�[�c�̃^�C�~���O���Čv�Z
    }
}