using System.Collections.Generic;

/// <summary>
/// 譜面データクラス
/// </summary>
[System.Serializable]
public class NotesData
{
    /// <summary>
    /// 楽曲タイトル
    /// </summary>
    public string title = "NoTitle";
    /// <summary>
    /// サブタイトル
    /// </summary>
    public string subTitle = string.Empty;
    /// <summary>
    /// 管理用曲ID
    /// </summary>
    public int musicID = 0;
    /// <summary>
    /// 難易度モード
    /// </summary>
    public int course = 0;
    /// <summary>
    /// 難易度レベル
    /// </summary>
    public float level = 0;
    /// <summary>
    /// 選曲画面などで使用(内部では使いません)
    /// </summary>
    public float basicBPM = 0;
    /// <summary>
    /// 音源と紐づけ
    /// </summary>
    public string musicPath = string.Empty;
    /// <summary>
    /// 選曲画面などで使用(sec)
    /// </summary>
    public float demoStart = 0;
    /// <summary>
    /// Json化されるノーツデータを格納
    /// </summary>
    public List<Note> noteList = new List<Note>();

    /// <summary>
    /// 1ノーツに含まれる情報
    /// </summary>
    [System.Serializable]
    public struct Note
    {
        /// <summary>
        /// ノーツの種類を識別する番号(ユーザー定義)
        /// </summary>
        public int notesType;
        /// <summary>
        /// どのレーンに属するか
        /// </summary>
        public int lane;
        /// <summary>
        /// 開始から何フレーム目にくるのか(60fps?)
        /// </summary>
        public int frame;
        /// <summary>
        /// 開始から何秒で来るのか
        /// </summary>
        public float time;
        /// <summary>
        /// 拍の長さ(sec)
        /// </summary>
        public float length;


        // 実装予定
        //public float highSpeed;
        //public float BPM;
        //public float offset;
        //public List<Note> notes;
        // いる？
        //public Vector3 pos;

        public Note(int type, int lane, int frame, float time, float length)
        {
            this.notesType = type;
            this.lane = lane;
            this.frame = frame;
            this.time = time;
            this.length= length;
            //this.BPM = bpm;
            //this.highSpeed = speed;
            //this.offset = offset;
            //this.pos = pos;
        }
    }
}
