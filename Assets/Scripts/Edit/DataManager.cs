using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    // 定数
    public const int MAX_BPM = 1000;
    public const int MAX_LPB = 64;
    public const int MIN_LPB = 4;
    public const int MAX_LANE = 16;
    public const int MAX_BAR = 300;
    public const string MUSIC_PATH_HEAD = "Hoge/Foo/Huga/";

    // ユーザー設定変数
    public int targetFrameRate = 60;
    public Vector2 stretchRatio = new Vector2(1, 1);
    public string musicPath = string.Empty;
    public float BPM = 150;
    public float offset = 0;
    public BarData.Measure measure = new BarData.Measure(4, 4);
    public int LPB = 16;
    public int lane = 7;
    public int editMode = 1;

    // 選択中の小節(非選択なら-1)
    public int choosingBarNum = -1;
    public List<BarData> barList = new List<BarData>(MAX_BAR);

    public void AddBarList()
    {
        if (MAX_BAR < barList.Count)
            return;

        BarData bar = new BarData();
        bar.Init(barList.Count, measure, LPB);
        barList.Add(bar);

        // 昇順ソート
        barList.Sort((a, b) => a.barNum - b.barNum);
    }

    public void ChangeBarData(BarData bar)
    {
        barList[bar.barNum] = bar;
    }

    public void RemoveBarList(int idx)
    {
        barList.RemoveAt(idx);
    }

    /// <summary>
    /// 小節を選択し直した際にUIの値を書き換える(小節単位で保持するデータ)
    /// </summary>
    public void OnChangedChoosingBar()
    {
        measure.denominator = barList[choosingBarNum].measure.denominator;
        measure.numerator = barList[choosingBarNum].measure.numerator;
        LPB = barList[choosingBarNum].LPB;
        EditUIManager.Instance.SetDenominatorText();
        EditUIManager.Instance.SetNumeratorText();
        EditUIManager.Instance.SetLPBText();
    }

    /// <summary>
    /// UIの値を書き換えた時、その値を選択中の小節に与える
    /// </summary>
    public void OnChangedBarDataAtUI()
    {
        if (choosingBarNum < 0)
            return;

        BarData bar = barList[choosingBarNum];
        bar.Init(choosingBarNum, measure, LPB);
        ChangeBarData(bar);
    }

    public void AddNotes(int lane, int cell)
    {
        //
        if (barList[choosingBarNum].notesArray[lane, cell].notesType == editMode)
            barList[choosingBarNum].notesArray[lane, cell].notesType = 0;
        else
            barList[choosingBarNum].notesArray[lane, cell].notesType = editMode;
    }
}
