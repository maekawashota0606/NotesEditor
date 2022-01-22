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
    private GameObject _addButton = null;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _pathField.text = DataManager.MUSIC_PATH_HEAD;
        _BPMField.text = DataManager.Instance.BPM.ToString();
        _denominatorField.text = DataManager.Instance.measure.denominator.ToString();
        _numeratorField.text = DataManager.Instance.measure.numerator.ToString();
        _LPBField.text = DataManager.Instance.LPB.ToString();
        _laneField.text = DataManager.Instance.lane.ToString();
    }

    public void SetPath()
    {
        if (_pathField.text == string.Empty)
            _pathField.text = DataManager.MUSIC_PATH_HEAD;
        else
            DataManager.Instance.musicPath = _pathField.text;
    }

    public void SetBPM()
    {
        float bpm = DataManager.Instance.BPM;
        if (_BPMField.text != string.Empty)
        {
            bpm = int.Parse(_BPMField.text);
            if (DataManager.MAX_BPM < bpm)
                bpm = DataManager.MAX_BPM;
        }

        _BPMField.text = bpm.ToString();
        DataManager.Instance.BPM = bpm;
    }

    public void SetOffset()
    {
        if (_offsetField.text == string.Empty)
            _offsetField.text = DataManager.Instance.offset.ToString();
        else
            DataManager.Instance.offset = float.Parse(_offsetField.text);
    }

    public void SetDenominator()
    {
        int d = int.Parse(_denominatorField.text);
        if (_denominatorField.text == string.Empty || d < 1)
            SetDenominatorText();
        else
        {
            DataManager.Instance.measure.denominator = d;
            DataManager.Instance.OnChangedBarDataAtUI();
        }
    }

    public void SetDenominatorText()
    {
        _denominatorField.text = DataManager.Instance.measure.denominator.ToString();
    }

    public void SetNumerator()
    {
        int n = int.Parse(_numeratorField.text);
        if (_numeratorField.text == string.Empty || n < 1)
            SetNumeratorText();
        else
        {
            DataManager.Instance.measure.numerator = n;
            DataManager.Instance.OnChangedBarDataAtUI();
        }

    }

    public void SetNumeratorText()
    {
        _numeratorField.text = DataManager.Instance.measure.numerator.ToString();
    }

    public void SetLPB()
    {
        if (int.TryParse(_LPBField.text, out int lpb))
        {
            if (lpb < DataManager.MIN_LPB)
                DataManager.Instance.LPB = DataManager.MIN_LPB;
            else if (DataManager.MAX_LPB < lpb)
                DataManager.Instance.LPB = DataManager.MAX_LPB;
            else
                DataManager.Instance.LPB = lpb;

            DataManager.Instance.OnChangedBarDataAtUI();
        }

        SetLPBText();
    }

    public void SetLPBText()
    {
        _LPBField.text = DataManager.Instance.LPB.ToString();
    }

    public void SetLane()
    {
        int lane = DataManager.Instance.lane;
        if (_laneField.text != string.Empty)
        {
            lane = int.Parse(_laneField.text);
            if (DataManager.MAX_LANE < lane)
                lane = DataManager.MAX_LANE;
        }

        _laneField.text = lane.ToString();
        DataManager.Instance.lane = lane;
    }

    public void AddBar()
    {
        DataManager.Instance.AddBarList();
    }
}
