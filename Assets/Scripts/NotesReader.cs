using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NotesReader : SingletonMonoBehaviour<NotesReader>
{
    [SerializeField, Header("読み込むファイル")]
    private string _fileName = string.Empty;
    private const string _MUSIC_SHEET_Path = "Assets/Resources/NotesAtText/";
    private const string _SAVE_PATH = "Assets/Resources/NotesAtJson/";
    //  データ格納先
    private NotesData _notesData = new NotesData();
    // 楽曲パラメーター
    private float _bpm = 240;
    private byte _block = 4;
    private byte _measureNumerator = 4;
    private byte _measureDenominator = 4;
    private float _scrollSpeed = 1.0f;
    private float _delay = 0;
    private const char _END_ROW_SYMBOL = ',';
    // レーンごとのノーツリスト
    private List<List<string>> _notes = new List<List<string>>();

    public void TextTojson()
    {
        // ファイルの中身を取り出す
        string datas = File.ReadAllText($"{ _MUSIC_SHEET_Path}{_fileName}.txt");
        // 1小節のノーツ
        string notesPerBar = string.Empty;

        // TODO:ヘッダ、命令文の読み取り

        // 譜面のノーツのみを抽出
        byte lane = 0;
        foreach (char c in datas)
        {
            if (c >= '0' && c <= '9')
                notesPerBar += c;
            else if (c == _END_ROW_SYMBOL)
            {
                // レーンごとに1行ずつ格納
                if (lane >= _block)
                    lane = 0;
                else
                    _notes.Add(new List<string>());

                _notes[lane].Add(notesPerBar);
                notesPerBar = string.Empty;
                lane++;
            }
        }

        // レーンごとに計算
        for (byte i = 0; i < _block; i++)
        {
            CalNotesTiming(_notes[i], i);
        }

        // jsonファイル命名、出力パス指定
        string saveName = $"{_notesData.musicID}_{(int)_notesData.course}.json";
        // 出力
        StreamWriter writer = new StreamWriter($"{_SAVE_PATH}{saveName}", false);
        string data = JsonUtility.ToJson(_notesData);
        writer.WriteLine(data);
        writer.Flush();
        writer.Close();

        Debug.Log("Write Ended");
    }

    /// <summary>
    /// 1レーン分のノーツのタイミングを計算
    /// </summary>
    /// <param name="laneNotes"></param>
    /// <param name="lane"></param>
    private void CalNotesTiming(List<string> laneNotes, byte lane)
    {
        // 1分
        int secondPerMinute = 60;
        // 1拍あたりの時間
        double timePerBeat = secondPerMinute / _bpm;
        // 1小節あたりの時間
        double timePerBar = 4 * timePerBeat * _measureNumerator / _measureDenominator;
        // トータルの経過時間
        //double pastTime = 0;

        foreach (string NotesPerBar in laneNotes)
        {
            // 1小節を最小まで分割した時間
            double advancedTime = timePerBar / NotesPerBar.Length;
            for (int i = 0; i < NotesPerBar.Length; i++)
            {
                //// ノーツタイプ判定
                //string s = NotesPerBar[i].ToString();
                //byte b = byte.Parse(s);
                //NotesData.NotesType type = (NotesData.NotesType)b;
                
                //// ノーツが来る時間
                //double timing = (_delay + pastTime) * _scrollSpeed;
                //// ノーツが来るフレーム
                //int frame = (int)System.Math.Round((timing * DataManager.Instance.targetFrameRate), System.MidpointRounding.AwayFromZero);
                //Debug.Log(frame);
                //// この小説内で経過した時間
                //pastTime += advancedTime;
                //// ノーツデータ格納　
                //if (type != NotesData.NotesType.None)
                //    _notesData.noteList.Add(new NotesData.Note { notesType = type, lane = lane, frame = frame});
            }
        }
    }
}
