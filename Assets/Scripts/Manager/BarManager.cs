using System.Collections.Generic;

public class BarManager : SingletonMonoBehaviour<BarManager>
{
    public List<Notes.Bar> barList = new List<Notes.Bar>(DataManager.MAX_BAR);

    public void NewBar()
    {
        if (DataManager.MAX_BAR < barList.Count)
            return;

        Notes.Bar bar = new Notes.Bar();
        bar.Init(barList.Count);
        AddBarList(bar);
    }

    private void AddBarList(Notes.Bar bar)
    {
        barList.Add(bar);

        // �����\�[�g
        barList.Sort((a, b) => a.barNum - b.barNum);
    }


    //public void ChangeBarData(Notes.Bar bar)
    //{
    //    barList[bar.barNum] = bar;
    //}

    //public void RemoveBarList(int idx)
    //{
    //    barList.RemoveAt(idx);

    //    // TODO:barnum�̏C��
    //}


    public void AddNotes(int lane, int cell, int type)
    {
        // 
        if (barList[DataManager.Instance.GetChoosingBarNum()].notesArray[lane, cell].notesType == 0)
            barList[DataManager.Instance.GetChoosingBarNum()].notesArray[lane, cell].notesType = type;
        else
            barList[DataManager.Instance.GetChoosingBarNum()].notesArray[lane, cell].notesType = 0;
    }

    /// <summary>
    /// lane��ύX�����ہA�ő僌�[���𒴂������[���ɑ��݂���m�[�c������
    /// </summary>
    /// <param name="maxLane"></param>
    public void ClearLaneNotes(int maxLane)
    {
        foreach(Notes.Bar bar in barList)
        {
            for(int i = maxLane; i < DataManager.MAX_LANE; i++)
            {
                // UnityEngine.Debug.Log("deleteLane" + i);
                for (int j = 0; j < DataManager.Instance.GetLPB(); j++)
                {
                    bar.notesArray[i, j].notesType = 0;
                    // TODO:�����O�m�[�c������͎q�m�[�c������
                }
            }
        }
    }

    /// <summary>
    /// LPB��ύX�����ہA���ɔz�u���Ă���m�[�c��ύX���LPB�ɉ����čĔz�u����
    /// </summary>
    public void SetNotesIndex(Notes.Bar bar, int changedLPB)
    {
        barList[bar.barNum].Init(bar.barNum);

        // ���݂�LPB�ƕύX���LPB�̍ő���񐔂����߂� EX: 24:16 = 8
        int gcd = MyMath.Gcd(bar.LPB, changedLPB);
        // ���݂�LPB���ő���񐔂Ŋ��� EX: 24 / 8 = 3
        int multiple = bar.LPB / gcd;
        // �ύX���LPB���ő���񐔂Ŋ��� EX: 16 / 8 = 2
        int changedMultiple = changedLPB / gcd;


        // �ύX��̂̊e���́A���݂̊e�� * [�ύX���LPB���ő���񐔂Ŋ������{��] / [���݂�LPB���ő���񐔂Ŋ������{��]�ɓ�����
        // EX: (LPB:24)3n = 2n (LPB:16)    EX2: (LPB:48)3n = n (LPB:16)

        // �������ǂ��̂ŗ񂲂ƂɌ���
        for (int i = 0; i < bar.LPB; i++)
        {
            // ���݂̊e�� = ���݂�LPB���ő���񐔂Ŋ������{���̂Ƃ�
            if (i % multiple == 0)
            {
                // �ύX���LPB���ő���񐔂Ŋ���������
                for (int j = 0; j < DataManager.Instance.GetLane(); j++)
                    barList[bar.barNum].notesArray[j, i * changedMultiple / multiple] = bar.notesArray[j, i];
            }
        }
    }
}
