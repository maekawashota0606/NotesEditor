/// <summary>
/// ���߃f�[�^�N���X
/// </summary>
public class Bar
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

    public void Init(int num)
    {
        barNum = num;
        measure = DataManager.Instance.GetMeasure();
        LPB = DataManager.Instance.GetLPB();
        notesArray = new NotesData.Note[DataManager.MAX_LANE, DataManager.MAX_LPB];

        // �m�[�c�̃^�C�~���O���Čv�Z
    }
}