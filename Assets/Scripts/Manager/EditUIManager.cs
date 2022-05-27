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
    /// UI����p�X��ύX�����ꍇ
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
    /// �v���O���������p�X��ύX�����ꍇ
    /// </summary>
    public void SetPathField(string path)
    {
        Debug.Log("�p�X��ύX");
        _pathField.text = path;
    }

    /// <summary>
    /// UI����BPM��ύX�����ꍇ
    /// </summary>
    public void OnChangedBPMField()
    {
        if (_BPMField.text != string.Empty)
            DataManager.Instance.SetBPM(int.Parse(_BPMField.text));
        else
            SetBPMField(DataManager.Instance.GetBPM());
    }

    /// <summary>
    /// �v���O��������BPM��ύX�����ꍇ
    /// </summary>
    public void SetBPMField(float bpm)
    {
        Debug.Log("BPM��ύX");
        _BPMField.text = bpm.ToString();
    }

    /// <summary>
    /// UI����I�t�Z�b�g��ύX�����ꍇ
    /// </summary>
    public void OnChangedOffsetField()
    {
        if (_offsetField.text != string.Empty)
            DataManager.Instance.SetOffset(float.Parse(_offsetField.text));
        else
            SetOffsetField(DataManager.Instance.GetOffset());
    }

    /// <summary>
    /// �v���O���������I�t�Z�b�g��ύX�����ꍇ
    /// </summary>
    public void SetOffsetField(float offset)
    {
        Debug.Log("�I�t�Z�b�g��ύX");
        _offsetField.text = offset.ToString();
    }

    /// <summary>
    /// UI���甏�q�̕����ύX�����ꍇ
    /// </summary>
    public void OnChangedDenominatorField()
    {
        if (_denominatorField.text != string.Empty)
            DataManager.Instance.SetMeasure(int.Parse(_numeratorField.text), -1);
        else
            SetDenominatorFiled(DataManager.Instance.GetMeasure().denominator);
    }

    /// <summary>
    /// �v���O�����������q�̕����ύX�����ꍇ
    /// </summary>
    public void SetDenominatorFiled(int d)
    {
        Debug.Log("���q�̕����ύX");
        _denominatorField.text = d.ToString();
    }

    /// <summary>
    /// UI���甏�q�̕��q��ύX�����ꍇ
    /// </summary>
    public void OnChangedNumeratorField()
    {
        if (_numeratorField.text != string.Empty)
            DataManager.Instance.SetMeasure(-1, int.Parse(_numeratorField.text));
        else
            SetNumeratorFiled(DataManager.Instance.GetMeasure().numerator);
    }

    /// <summary>
    /// �v���O�����������q�̕��q��ύX�����ꍇ
    /// </summary>
    public void SetNumeratorFiled(int n)
    {
        Debug.Log("���q�̕��q��ύX");
        _numeratorField.text = n.ToString();
    }

    /// <summary>
    /// UI����LPB��ύX�����ꍇ
    /// </summary>
    public void OnChangedLPBField()
    {
        if (_LPBField.text != string.Empty)
            DataManager.Instance.SetLPB(int.Parse(_LPBField.text));
        else
            SetLPBField(DataManager.Instance.GetLPB());
    }

    /// <summary>
    /// �v���O��������LPB��ύX�����ꍇ
    /// </summary>
    /// <param name="lpb"></param>
    public void SetLPBField(int lpb)
    {
        Debug.Log("LPB��ύX");
        _LPBField.text = lpb.ToString();
    }

    /// <summary>
    /// UI���烌�[����ύX�����ꍇ
    /// </summary>
    public void OnChangedLaneField()
    {
        if (_laneField.text != string.Empty)
            DataManager.Instance.SetLane(int.Parse(_laneField.text));
        else
            SetLaneField(DataManager.Instance.GetLane());
    }

    /// <summary>
    /// �v���O�����������[����ύX�����ꍇ
    /// </summary>
    public void SetLaneField(int lane)
    {
        Debug.Log("���[����ύX");
        _laneField.text = lane.ToString();
    }

    /// <summary>
    /// UI���珬�߂��쐬�����ꍇ
    /// </summary>
    public void OnPushNewBarButton()
    {
        BarManager.Instance.NewBar();
    }

    /// <summary>
    /// UI����Đ��������ꍇ
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
    /// UI����f�[�^�����������ꍇ
    /// </summary>
    public void OnPushGenerateButton()
    {
        DataManager.Instance.SetSheetData();
    }

    /// <summary>
    /// UI����X���C�_�[�𑀍삵���ꍇ
    /// </summary>
    public void OnChangedPlaySlider()
    {
        // �X���C�_�[�̐i�������ύX���ꂽ�ۂɔ��f
        GameDirector.Instance.Skip(_playSlider.value);
    }

    /// <summary>
    /// �v���O��������Đ����Ԃ𑀍삵���ꍇ
    /// </summary>
    /// <param name="time"></param>
    /// <param name="duration"></param>
    public void SetPlaySlider(float time, float duration)
    {
        _playSlider.value = time / duration;
    }

    /// <summary>
    /// �ȃt�@�C����I�������ۂɋȖ���\������
    /// </summary>
    /// <param name="title"></param>
    public void SetMusicTitle(string title)
    {
        Debug.Log("�^�C�g����ύX");
        _titleText.text = title;
    }

    /// <summary>
    /// �I���{�^�����������ꍇ
    /// </summary>
    public void OnPushExitButton()
    {
        Debug.Log("�I�����܂�");
        Application.Quit();
    }
}
