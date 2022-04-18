using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NotesReader : SingletonMonoBehaviour<NotesReader>
{
    [SerializeField, Header("�ǂݍ��ރt�@�C��")]
    private string _fileName = string.Empty;
    private const string _MUSIC_SHEET_Path = "Assets/Resources/NotesAtText/";
    private const string _SAVE_PATH = "Assets/Resources/NotesAtJson/";
    //  �f�[�^�i�[��
    private NotesData _notesData = new NotesData();
    // �y�ȃp�����[�^�[
    private float _bpm = 240;
    private byte _block = 4;
    private byte _measureNumerator = 4;
    private byte _measureDenominator = 4;
    private float _scrollSpeed = 1.0f;
    private float _delay = 0;
    private const char _END_ROW_SYMBOL = ',';
    // ���[�����Ƃ̃m�[�c���X�g
    private List<List<string>> _notes = new List<List<string>>();

    public void TextTojson()
    {
        // �t�@�C���̒��g�����o��
        string datas = File.ReadAllText($"{ _MUSIC_SHEET_Path}{_fileName}.txt");
        // 1���߂̃m�[�c
        string notesPerBar = string.Empty;

        // TODO:�w�b�_�A���ߕ��̓ǂݎ��

        // ���ʂ̃m�[�c�݂̂𒊏o
        byte lane = 0;
        foreach (char c in datas)
        {
            if (c >= '0' && c <= '9')
                notesPerBar += c;
            else if (c == _END_ROW_SYMBOL)
            {
                // ���[�����Ƃ�1�s���i�[
                if (lane >= _block)
                    lane = 0;
                else
                    _notes.Add(new List<string>());

                _notes[lane].Add(notesPerBar);
                notesPerBar = string.Empty;
                lane++;
            }
        }

        // ���[�����ƂɌv�Z
        for (byte i = 0; i < _block; i++)
        {
            CalNotesTiming(_notes[i], i);
        }

        // json�t�@�C�������A�o�̓p�X�w��
        string saveName = $"{_notesData.musicID}_{(int)_notesData.course}.json";
        // �o��
        StreamWriter writer = new StreamWriter($"{_SAVE_PATH}{saveName}", false);
        string data = JsonUtility.ToJson(_notesData);
        writer.WriteLine(data);
        writer.Flush();
        writer.Close();

        Debug.Log("Write Ended");
    }

    /// <summary>
    /// 1���[�����̃m�[�c�̃^�C�~���O���v�Z
    /// </summary>
    /// <param name="laneNotes"></param>
    /// <param name="lane"></param>
    private void CalNotesTiming(List<string> laneNotes, byte lane)
    {
        // 1��
        int secondPerMinute = 60;
        // 1��������̎���
        double timePerBeat = secondPerMinute / _bpm;
        // 1���߂�����̎���
        double timePerBar = 4 * timePerBeat * _measureNumerator / _measureDenominator;
        // �g�[�^���̌o�ߎ���
        //double pastTime = 0;

        foreach (string NotesPerBar in laneNotes)
        {
            // 1���߂��ŏ��܂ŕ�����������
            double advancedTime = timePerBar / NotesPerBar.Length;
            for (int i = 0; i < NotesPerBar.Length; i++)
            {
                //// �m�[�c�^�C�v����
                //string s = NotesPerBar[i].ToString();
                //byte b = byte.Parse(s);
                //NotesData.NotesType type = (NotesData.NotesType)b;
                
                //// �m�[�c�����鎞��
                //double timing = (_delay + pastTime) * _scrollSpeed;
                //// �m�[�c������t���[��
                //int frame = (int)System.Math.Round((timing * DataManager.Instance.targetFrameRate), System.MidpointRounding.AwayFromZero);
                //Debug.Log(frame);
                //// ���̏������Ōo�߂�������
                //pastTime += advancedTime;
                //// �m�[�c�f�[�^�i�[�@
                //if (type != NotesData.NotesType.None)
                //    _notesData.noteList.Add(new NotesData.Note { notesType = type, lane = lane, frame = frame});
            }
        }
    }
}
