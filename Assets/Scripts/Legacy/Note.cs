/// <summary>
/// ノーツ1つに含まれる情報
/// </summary>
public class Note
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


    // TODO:実装予定
    //public float highSpeed;
    //public float BPM;
    //public float offset;
    //public List<Note> notes;
}
