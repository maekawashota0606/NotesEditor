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

        // 昇順ソート
        barList.Sort((a, b) => a.barNum - b.barNum);
    }


    public void ChangeBarData(Bar bar)
    {
        barList[bar.barNum] = bar;
    }

    public void RemoveBarList(int idx)
    {
        barList.RemoveAt(idx);

        // TODO:barnumの修正
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
    /// LPBを変更した際に既に配置してあるノーツを変更後のLPBに沿って再配置する
    /// </summary>
    public void SetNotesIndex(Bar bar, int changedLPB)
    {
        barList[bar.barNum].Init(bar.barNum);

        // 現在のLPBと変更後のLPBの最大公約数を求める EX: 24:16 = 8
        int gcd = MyMath.Gcd(bar.LPB, changedLPB);
        // 現在のLPBを最大公約数で割る EX: 24 / 8 = 3
        int multiple = bar.LPB / gcd;
        // 変更後のLPBを最大公約数で割る EX: 16 / 8 = 2
        int changedMultiple = changedLPB / gcd;


        // 変更後のの各拍は、現在の各拍 * [変更後のLPBを最大公約数で割った倍数] / [現在のLPBを最大公約数で割った倍数]に等しい
        // EX: (LPB:24)3n = 2n (LPB:16)    EX2: (LPB:48)3n = n (LPB:16)

        // 効率が良いので列ごとに検索
        for (int i = 0; i < bar.LPB; i++)
        {
            // 現在の各拍 = 現在のLPBを最大公約数で割った倍数のとき
            if (i % multiple == 0)
            {
                // 変更後のLPBを最大公約数で割った数の
                for (int j = 0; j < DataManager.Instance.GetLane(); j++)
                    barList[bar.barNum].notesArray[j, i * changedMultiple / multiple] = bar.notesArray[j, i];
            }
        }
    }
}
