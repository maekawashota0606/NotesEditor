using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//  ���^��
public class JsonGenerator : MonoBehaviour
{
    [SerializeField, Header("���t�@�C���̃p�X")]
    public string musicSheetPath = "NotesAtText/example";
    private string _savePath = "Assets/Resources/NotesAtJson/";
    private List<string> _musicSheetDatas = new List<string>();
    //  �f�[�^�i�[��
    private NotesData _notesData = new NotesData();
    //
    private float bpm = 0;
    private byte measureNumerator = 4;
    private byte measureDenominator = 4;
    private float scrollSpeed = 1.0f;
    private double pastTime = 0;
    //
    private const char _HEADER_SYMBOL = ':';
    private const string _TITLE_HEADER = "TITLE";
    private const string _ID_HEADER = "ID";
    private const string _COURSE_HEADER = "COURSE";
    private const string _LEVEL_HEADER = "LEVEL";
    private const string _BPM_HEADER = "BPM";
    private const string _OFFSET_HEADER = "OFFSET";
    private const string _MUSIC_PATH_HEADER = "PATH";
    private const string _DEMO_START_HEADER = "DEMOSTART";
    private const string _BLOCK_HEADER = "BLOCK";
    private const char _COMMAND_SYMBOL = '#';
    private const string _CHANGE_BPM_COMMAND = "BPM";
    private const string _CHANGE_SCROLL_COMMAND = "SCROLL";
    private const string _CHANGE_MEASURE_COMMAND = "MEASURE";
    private const string _DELAY_COMMAND = "DELAY";
    private const string _START_COMMAND = "START";
    private const string _END_COMMAND = "END";
    private const char _END_ROW_SYMBOL = ',';

    private void Start()
    {
        // �t�@�C���̒��g�����o��
        TextAsset textAsset = new TextAsset();
        textAsset = Resources.Load(musicSheetPath) as TextAsset;
        StringReader reader = new StringReader(textAsset.text);
        while (reader.Peek() > -1)
            _musicSheetDatas.Add(reader.ReadLine());

        //
        bool isStarted = false;
        bool isEnded = false;
        string notes = string.Empty;
        foreach (string row in _musicSheetDatas)
        {
            if (row == string.Empty)
                continue;

            // �{���ɓ����Ă���ꍇ
            if (isStarted)
            {
                foreach (char c in row)
                {
                    if(c >= '0' && c <= '9')
                        notes += c;
                    else if(c == _END_ROW_SYMBOL)
                    {
                        // �F�X�v�Z
                        CalNotesPos(notes, 0);
                        notes = string.Empty;
                        // �o�ߎ��Ԃ��L�^����
                        //pastTime +=
                    }
                }
            }
            // �w�b�_�[�̏ꍇ
            else if (row.IndexOf(_HEADER_SYMBOL) != -1)
            {
                string[] rows = row.Split(_HEADER_SYMBOL);
                //switch (rows[0].Trim())
                //{
                //    case _TITLE_HEADER:
                //        _notesData.title = rows[1];
                //        break;
                //    case _ID_HEADER:
                //        _notesData.musicID = byte.Parse(rows[1].Trim());
                //        break;
                //    case _COURSE_HEADER:
                //        int course = int.Parse(rows[1].Trim());
                //        _notesData.course = (NotesData.Course)course;
                //        break;
                //    case _LEVEL_HEADER:
                //        _notesData.level = float.Parse(rows[1].Trim());
                //        break;
                //    case _BPM_HEADER:
                //        // ��{BPM(�I�ȉ�ʂƂ��Ŏg������?)
                //        _notesData.bpm = float.Parse(rows[1].Trim());
                //        // �����p
                //        bpm = _notesData.bpm;
                //        break;
                //    case _OFFSET_HEADER:
                //        _notesData.offset = float.Parse(rows[1].Trim());
                //        break;
                //    case _MUSIC_PATH_HEADER:
                //        _notesData.musicPath = rows[1].Trim();
                //        break;
                //    case _DEMO_START_HEADER:
                //        _notesData.demoStart = float.Parse(rows[1].Trim());
                //        break;
                //    case _BLOCK_HEADER:
                //        //block = byte.Parse(rows[1].Trim());
                //        break;
                //    default:
                //        Debug.LogError("���������͂���Ă��Ȃ��w�b�_�[������܂�");
                //        break;
                //}
            }

            if (isEnded)
            {
                Debug.Log("����ɏI�����܂���");
                break;
            }
        }

        // json�t�@�C�������A�o�̓p�X�w��
        string saveName = $"{_notesData.musicID}_{(int)_notesData.course}.json";
        _savePath += saveName;

        // �o��
        StreamWriter writer = new StreamWriter(_savePath, false);
        string data = JsonUtility.ToJson(_notesData);
        writer.WriteLine(data);
        writer.Flush();
        writer.Close();
    }

    private void CalNotesPos(string notes, byte lane)
    {
        int secondPerMinute = 60;
        // ���̏��ߓ��̌o�ߎ���
        double advancedTime = 0;
        // 1��������̎���
        double timePerBeat = secondPerMinute / bpm;
        // 1���߂�����̎��� + offset
        double timePerBar = 4 * timePerBeat * measureNumerator / measureDenominator;
        // 1���߂��ŏ��܂ŕ�����������
        double timePerBarMin = timePerBar / notes.Length;

        // �x���݂̂̏ꍇ�͗�O�Ƃ��ď���
        if (CheckPause(notes, '0'))
            return;

        for (int i = 0; i < notes.Length; i++)
        {
            // �^�ϊ�
            //string s = notes[i].ToString();
            //byte b = byte.Parse(s);
            //NotesData.NotesType type = (NotesData.NotesType)b;
            //// �m�[�c�̃^�C�~���O(����)
            //int frame = (int)(pastTime + advancedTime + timePerBarMin * scrollSpeed);
            //// �m�[�c�f�[�^�i�[�@
            //if (type != NotesData.NotesType.None)
            //    _notesData.noteList.Add(new NotesData.Note { notesType = type, lane = lane, frame = frame });

            advancedTime += timePerBarMin;
        }
    }

    private bool CheckPause(string notes , char target)
    {
        foreach(char s in notes)
            if (s == target)
                return false;
        return true;
    }
}



//// ���炩�̃R�}���h�̏ꍇ
//if (row.IndexOf(_COMMAND_SYMBOL) == 0)
//{
//    switch (row.Trim(_COMMAND_SYMBOL))
//    {
//        case _CHANGE_BPM_COMMAND:
//            break;
//        case _CHANGE_SCROLL_COMMAND:
//            break;
//        case _CHANGE_MEASURE_COMMAND:
//            break;
//        case _DELAY_COMMAND:
//            break;
//        case _START_COMMAND:
//            isStarted = true;
//            break;
//        case _END_COMMAND:
//            isEnded = true;
//            break;
//        default:
//            break;
//}