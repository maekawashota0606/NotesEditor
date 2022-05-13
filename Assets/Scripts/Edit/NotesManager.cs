using System.Collections.Generic;

public class NotesManager : SingletonMonoBehaviour<NotesManager>
{
    private List<float> _noteTimes = new List<float>(19200);


    /// <summary>
    /// ノーツのタイミングなどを計算
    /// </summary>
    public void CalNotes()
    {
        // レーンごとに計算
        for(int i = 0; i < DataManager.Instance.GetLane(); i++)
        {
            CalLaneNotes(i);
        }
    }


    private void CalLaneNotes(int lane)
    {
        float totalTime = 0;
        foreach (Bar barData in BarManager.Instance.barList)
        {
            for (int i = 0; i < barData.LPB; i++)
            {
                // 1拍の長さを計算
                // TODO:オフセットなどを考慮
                float length = 60 / DataManager.Instance.GetBPM() * 4 * barData.measure.numerator / barData.measure.denominator / barData.LPB;
                // 計算後のデータを代入
                NotesData.Note note = BarManager.Instance.barList[barData.barNum].notesArray[lane, i];
                note.length = length;
                //
                note.time = DataManager.Instance.GetOffset() + totalTime;
                //
                // TODO:フレームを計算
                //
                BarManager.Instance.barList[barData.barNum].notesArray[lane, i] = note;
                //
                totalTime += length;
                //Debug.Log($"barnum{barData.barNum}, lane{lane}, time{DataManager.Instance.barList[barData.barNum].notesArray[lane, i].time}");
            }
        }
    }


    /// <summary>
    /// 有効なノーツを検索、流れる順番通りに整列させリストに格納
    /// </summary>
    public void AlignNoteTimes()
    {
        _noteTimes.Clear();
        // ノーツデータを一通りチェック
        foreach (Bar data in BarManager.Instance.barList)
        {
            // 1列づつチェック
            for (int i = 0; i < data.LPB; i++)
            {
                for (int j = 0; j < DataManager.Instance.GetLane(); j++)
                {
                    // ノーツとして登録されているなら
                    if (data.notesArray[j, i].notesType != 0)
                    {
                        // 追加
                        _noteTimes.Add(data.notesArray[j, i].time);
                        UnityEngine.Debug.Log(data.notesArray[j, i].time);
                        // 同タイミングはスルーし、次のLPBを調べる
                        break;
                    }
                }
            }
        }

        // 昇順にソート
        _noteTimes.Sort();
    }

    public float NextCue(float elapseTime)
    {
        // 経過時間から次に来るタイミングを判断
        foreach (float time in _noteTimes)
        {
            if (elapseTime <= time)
                return time;
        }
        
        //値が存在しない場合
        return -1;
    }
}
