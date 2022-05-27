using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditUIManager : SingletonMonoBehaviour<EditUIManager>
{
    [SerializeField]
    private InputField _pathField = null;
    [SerializeField]
    private InputField _BPMField = null;
    [SerializeField]
    private InputField _offsetField = null;
    [SerializeField]
    private InputField _denominatorField = null;
    [SerializeField]
    private InputField _numeratorField = null;
    [SerializeField]
    private InputField _LPBField = null;
    [SerializeField]
    private InputField _laneField = null;
    [SerializeField]
    private Scrollbar _horizontalBar = null;
    [SerializeField]
    private Scrollbar _verticalBar = null;
    [SerializeField]
    private Slider _playSlider = null;
    [SerializeField]
    private Text _titleText = null;
    [SerializeField]
    private Button _playButton = null;
    [SerializeField]
    private Sprite _play = null;
    [SerializeField]
    private Sprite _pause = null;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SetPathField(DataManager.MUSIC_PATH_HEAD);
        SetBPMField(DataManager.Instance.GetBPM());
        SetOffsetField(DataManager.Instance.GetOffset());
        SetDenominatorFiled(DataManager.Instance.GetMeasure().denominator);
        SetNumeratorFiled(DataManager.Instance.GetMeasure().numerator);
        SetLPBField(DataManager.Instance.GetLPB());
        SetLaneField(DataManager.Instance.GetLane());
    }

    /// <summary>
    /// UIからパスを変更した場合
    /// </summary>
    public void OnChangedPathField()
    {
        string path = _pathField.text;

#if UNITY_EDITOR
        path = "sample.wav";
#endif

        DataManager.Instance.SetTime(0);
        SetPlaySlider(0f, 1f);
        if (path != string.Empty)
            DataManager.Instance.SetMusicPath(path);
        else
            SetPathField(DataManager.MUSIC_PATH_HEAD);
    }

    /// <summary>
    /// プログラム側がパスを変更した場合
    /// </summary>
    public void SetPathField(string path)
    {
        Debug.Log("パスを変更");
        _pathField.text = path;
    }

    /// <summary>
    /// UIからBPMを変更した場合
    /// </summary>
    public void OnChangedBPMField()
    {
        if (_BPMField.text != string.Empty)
            DataManager.Instance.SetBPM(int.Parse(_BPMField.text));
        else
            SetBPMField(DataManager.Instance.GetBPM());
    }

    /// <summary>
    /// プログラム側がBPMを変更した場合
    /// </summary>
    public void SetBPMField(float bpm)
    {
        Debug.Log("BPMを変更");
        _BPMField.text = bpm.ToString();
    }

    /// <summary>
    /// UIからオフセットを変更した場合
    /// </summary>
    public void OnChangedOffsetField()
    {
        if (_offsetField.text != string.Empty)
            DataManager.Instance.SetOffset(float.Parse(_offsetField.text));
        else
            SetOffsetField(DataManager.Instance.GetOffset());
    }

    /// <summary>
    /// プログラム側がオフセットを変更した場合
    /// </summary>
    public void SetOffsetField(float offset)
    {
        Debug.Log("オフセットを変更");
        _offsetField.text = offset.ToString();
    }

    /// <summary>
    /// UIから拍子の分母を変更した場合
    /// </summary>
    public void OnChangedDenominatorField()
    {
        if (_denominatorField.text != string.Empty)
            DataManager.Instance.SetMeasure(int.Parse(_numeratorField.text), -1);
        else
            SetDenominatorFiled(DataManager.Instance.GetMeasure().denominator);
    }

    /// <summary>
    /// プログラム側が拍子の分母を変更した場合
    /// </summary>
    public void SetDenominatorFiled(int d)
    {
        Debug.Log("拍子の分母を変更");
        _denominatorField.text = d.ToString();
    }

    /// <summary>
    /// UIから拍子の分子を変更した場合
    /// </summary>
    public void OnChangedNumeratorField()
    {
        if (_numeratorField.text != string.Empty)
            DataManager.Instance.SetMeasure(-1, int.Parse(_numeratorField.text));
        else
            SetNumeratorFiled(DataManager.Instance.GetMeasure().numerator);
    }

    /// <summary>
    /// プログラム側が拍子の分子を変更した場合
    /// </summary>
    public void SetNumeratorFiled(int n)
    {
        Debug.Log("拍子の分子を変更");
        _numeratorField.text = n.ToString();
    }

    /// <summary>
    /// UIからLPBを変更した場合
    /// </summary>
    public void OnChangedLPBField()
    {
        if (_LPBField.text != string.Empty)
            DataManager.Instance.SetLPB(int.Parse(_LPBField.text));
        else
            SetLPBField(DataManager.Instance.GetLPB());
    }

    /// <summary>
    /// プログラム側がLPBを変更した場合
    /// </summary>
    /// <param name="lpb"></param>
    public void SetLPBField(int lpb)
    {
        Debug.Log("LPBを変更");
        _LPBField.text = lpb.ToString();
    }

    /// <summary>
    /// UIからレーンを変更した場合
    /// </summary>
    public void OnChangedLaneField()
    {
        if (_laneField.text != string.Empty)
            DataManager.Instance.SetLane(int.Parse(_laneField.text));
        else
            SetLaneField(DataManager.Instance.GetLane());
    }

    /// <summary>
    /// プログラム側がレーンを変更した場合
    /// </summary>
    public void SetLaneField(int lane)
    {
        Debug.Log("レーンを変更");
        _laneField.text = lane.ToString();
    }

    /// <summary>
    /// UIから小節を作成した場合
    /// </summary>
    public void OnPushNewBarButton()
    {
        BarManager.Instance.NewBar();
    }

    /// <summary>
    /// UIから再生をした場合
    /// </summary>
    public void OnPushPlayButton()
    {
        GameDirector.Instance.Play();
    }

    public void OnChengedPlayState(bool isPlaying)
    {
        if(isPlaying)
            _playButton.image.sprite = _pause;
        else
            _playButton.image.sprite = _play;
    }

    /// <summary>
    /// UIからデータ生成をした場合
    /// </summary>
    public void OnPushGenerateButton()
    {
        DataManager.Instance.SetSheetData();
    }

    /// <summary>
    /// UIからスライダーを操作した場合
    /// </summary>
    public void OnChangedPlaySlider()
    {
        // スライダーの進捗率が変更された際に反映
        GameDirector.Instance.Skip(_playSlider.value);
    }

    /// <summary>
    /// プログラムから再生時間を操作した場合
    /// </summary>
    /// <param name="time"></param>
    /// <param name="duration"></param>
    public void SetPlaySlider(float time, float duration)
    {
        _playSlider.value = time / duration;
    }

    /// <summary>
    /// 曲ファイルを選択した際に曲名を表示する
    /// </summary>
    /// <param name="title"></param>
    public void SetMusicTitle(string title)
    {
        Debug.Log("タイトルを変更");
        _titleText.text = title;
    }

    /// <summary>
    /// 終了ボタンを押した場合
    /// </summary>
    public void OnPushExitButton()
    {
        Debug.Log("終了します");
        Application.Quit();
    }
}
