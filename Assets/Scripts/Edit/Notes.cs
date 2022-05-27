namespace Notes
{
    /// <summary>
    /// ���߃f�[�^�N���X
    /// </summary>
    [System.Serializable]
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
        public Note[,] notesArray = new Note[DataManager.MAX_LANE, DataManager.MAX_LPB];


        public void Init(int num)
        {
            barNum = num;
            measure = DataManager.Instance.GetMeasure();
            LPB = DataManager.Instance.GetLPB();
            notesArray = new Note[DataManager.MAX_LANE, DataManager.MAX_LPB];

            // �m�[�c�̃^�C�~���O���Čv�Z
        }

        public void SetLPB(int lpb)
        {
            int LPB = this.LPB;
            // ���݂�LPB�ƕύX���LPB�̍ő���񐔂����߂� EX: 24:16 = 8
            int gcd = MyMath.Gcd(LPB, lpb);
            // ���݂�LPB���ő���񐔂Ŋ��� EX: 24 / 8 = 3
            int multiple = LPB / gcd;
            // �ύX���LPB���ő���񐔂Ŋ��� EX: 16 / 8 = 2
            int changedMultiple = lpb / gcd;


            // �ύX��̂̊e���́A���݂̊e�� * [�ύX���LPB���ő���񐔂Ŋ������{��] / [���݂�LPB���ő���񐔂Ŋ������{��]�ɓ�����
            // EX: (LPB:24)3n = 2n (LPB:16)    EX2: (LPB:48)3n = n (LPB:16)


            Bar tempBar = new Bar();
            tempBar.notesArray = this.notesArray;

            Init(barNum);
            // �������ǂ��̂ŗ񂲂ƂɌ���
            UnityEngine.Debug.Log($"{LPB}��{lpb}�ɕύX");
            for (int i = 0; i < LPB; i++)
            {
                // ���݂̊e�� = ���݂�LPB���ő���񐔂Ŋ������{���̂Ƃ�
                if (i % multiple == 0)
                {
                    UnityEngine.Debug.Log(i * changedMultiple / multiple + "�ɂ�" + i);
                    // �ύX���LPB���ő���񐔂Ŋ���������
                    for (int j = 0; j < DataManager.Instance.GetLane(); j++)
                        notesArray[j, i * changedMultiple / multiple] = tempBar.notesArray[j, i];
                }
            }
        }
    }

    /// <summary>
    /// �m�[�c1�Ɋ܂܂����
    /// </summary>
    public struct Note
    {
        /// <summary>
        /// �m�[�c�̎�ނ����ʂ���ԍ�(���[�U�[��`)
        /// </summary>
        public int notesType;
        /// <summary>
        /// �ǂ̃��[���ɑ����邩
        /// </summary>
        public int lane;
        /// <summary>
        /// �J�n���牽�t���[���ڂɂ���̂�(60fps?)
        /// </summary>
        public int frame;
        /// <summary>
        /// �J�n���牽�b�ŗ���̂�
        /// </summary>
        public float time;
        /// <summary>
        /// ���̒���(sec)
        /// </summary>
        public float length;


        // TODO:�����\��
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
