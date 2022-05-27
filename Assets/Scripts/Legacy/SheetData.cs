using System.Collections.Generic;

/// <summary>
/// 譜面データクラス
/// </summary>
[System.Serializable]
public class SheetData
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
    /// 音源のパス
    /// </summary>
    public string musicPath = string.Empty;
    /// <summary>
    /// 選曲画面などで使用(sec)
    /// </summary>
    public float demoStart = 0;
    /// <summary>
    /// 小節ごとのノーツデータを格納
    /// </summary>
    public List<Bar> barList = new List<Bar>();


    public SheetData()
    {
        //
    }

    public SheetData(string title, string subTitle, int musicID, int course, float level, float basicBpm, string musicPath, float demoStart, List<Bar> barList)
    {
        this.title = title;
        this.subTitle = subTitle;
        this.musicID = musicID;
        this.course = course;
        this.level = level;
        this.basicBPM = basicBpm;
        this.musicPath = musicPath;
        this.demoStart = demoStart;
        this.barList = barList;
    }
}
