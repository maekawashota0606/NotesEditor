using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : SingletonMonoBehaviour<BarManager>
{
    public List<Bar> barList = new List<Bar>(DataManager.MAX_BAR);

    public void NewBar()
    {
        if (DataManager.MAX_BAR < barList.Count)
            return;

        Bar bar = new Bar();
        bar.Init(barList.Count);
        AddBarList(bar);
    }

    private void AddBarList(Bar bar)
    {
        barList.Add(bar);

        // �����\�[�g
        barList.Sort((a, b) => a.barNum - b.barNum);
    }


    public void ChangeBarData(Bar bar)
    {
        barList[bar.barNum] = bar;
    }

    public void RemoveBarList(int idx)
    {
        barList.RemoveAt(idx);

        // TODO:barnum�̏C��
    }


    public void AddNotes(int lane, int cell, int type)
    {
        // 
        if (barList[DataManager.Instance.GetChoosingBarNum()].notesArray[lane, cell].notesType == 0)
            barList[DataManager.Instance.GetChoosingBarNum()].notesArray[lane, cell].notesType = type;
        else
            barList[DataManager.Instance.GetChoosingBarNum()].notesArray[lane, cell].notesType = 0;
    }


    /// <summary>
    /// LPB��ύX�����ۂɊ��ɔz�u���Ă���m�[�c��ύX���LPB�ɉ����čĔz�u����
    /// </summary>
    public void SetNotesIndex(Bar bar, int changedLPB)
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
