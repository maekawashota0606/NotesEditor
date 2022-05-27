using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingletonMonoBehaviour<DataManager>
{
    // 定数
    public const int MAX_BPM = 500;
    public const int MAX_LPB = 64;
    public const int MIN_LPB = 4;
    public const int MAX_LANE = 16;
    public const int MAX_BAR = 300;
    public const string MUSIC_PATH_HEAD = "Hoge/Foo/Huga/";

    // ユーザー設定変数
    public Vector2 stretchRatio = new Vector2(1, 1);
    private int _targetFrameRate = 60;
    private string _musicPath = string.Empty;
    private float _BPM = 150;
    private float _offset = 0;
    private Notes.Measure _measure = new Notes.Measure(4, 4);
    private int _LPB = 16;
    private int _lane = 7;
    private int _editMode = 1;
    private float _time = 0;
    /// <summary>
    /// 選択中の小節(非選択なら-1)
    /// </summary>
    private int _choosingBarNum = -1;
    // Json化する前のデータ
    private SheetData _sheetData = new SheetData();


    public void SetMusicPath(string path)
    {
        _musicPath = path;
        EditUIManager.Instance.SetPathField(_musicPath);
        StartCoroutine(AudioManager.Instance.SetAudioFile(GetMusicPath()));
    }

    public string GetMusicPath()
    {
        return _musicPath;
    }

    public void SetBPM(float bpm)
    {
        if (bpm < MAX_BPM)
            _BPM = bpm;
        EditUIManager.Instance.SetBPMField(_BPM);
    }

    public float GetBPM()
    {
        return _BPM;
    }

    public void SetOffset(float offset)
    {
        _offset = offset;
        EditUIManager.Instance.SetOffsetField(_offset);
    }

    public float GetOffset()
    {
        return _offset;
    }

    public void SetMeasure(int d, int n)
    {
        if(0 < d)
            _measure.denominator = d;
        if(0 < n)
            _measure.numerator = n;
        EditUIManager.Instance.SetDenominatorFiled(_measure.denominator);
        EditUIManager.Instance.SetNumeratorFiled(_measure.numerator);

        // 選択中の小節の拍子を変更
        if (-1 < _choosingBarNum)
            BarManager.Instance.barList[_choosingBarNum].measure = _measure;
    }

    public Notes.Measure GetMeasure()
    {
        return _measure;
    }

    public void SetLPB(int lpb)
    {
        if (lpb < MIN_LPB)
            _LPB = MIN_LPB;
        else if (MAX_LPB < lpb)
            _LPB = MAX_LPB;
        else
            _LPB = lpb;

        EditUIManager.Instance.SetLPBField(_LPB);

        // 選択中の小節のLPBを変更
        if(-1 < _choosingBarNum)
        {
            //BarManager.Instance.barList[_choosingBarNum].LPB = _LPB;
            BarManager.Instance.barList[_choosingBarNum].SetLPB(_LPB);
        }
    }

    public int GetLPB()
    {
        return _LPB;
    }

    public void SetLane(int lane)
    {
        if (lane < MAX_LANE)
            _lane = lane;
        else
            _lane = MAX_LANE;

        EditUIManager.Instance.SetLaneField(_lane);
    }

    public int GetLane()
    {
        return _lane;
    }

    public void SetChoosingBarNum(int num)
    {
        if (_choosingBarNum == num)
            return;

        _choosingBarNum = num;

        // 選択している小節が変わった際の処理
        if ( _choosingBarNum < 0)
            return;

        SetLPB(BarManager.Instance.barList[_choosingBarNum].LPB);
        SetMeasure(BarManager.Instance.barList[_choosingBarNum].measure.denominator, BarManager.Instance.barList[_choosingBarNum].measure.numerator);
    }

    public int GetChoosingBarNum()
    {
        return _choosingBarNum;
    }

    public void SetEditMode(int mode)
    {
        _editMode = mode;
    }

    public int GetEditMode()
    {
        return _editMode;
    }

    public void SetTime(float time)
    {
        _time = time;
    }

    public float GetTime()
    {
        return _time;
    }

    public void SetTargetFrameRate(int frameRate)
    {
        _targetFrameRate = frameRate;
    }

    public int GetTargetFrameRate()
    {
        return _targetFrameRate;
    }

    public void SetSheetData()
    {
        SheetData data = new SheetData("dummy", "sub", 0, 0, 0, 0, "path", 0, BarManager.Instance.barList);
        _sheetData = data;
        JsonGenerator.Instance.Generate(_sheetData);
    }

    public SheetData GetSheetData()
    {
        return _sheetData;
    }
}
