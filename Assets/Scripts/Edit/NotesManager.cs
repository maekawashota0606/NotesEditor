public class NotesManager : SingletonMonoBehaviour<NotesManager>
{
    /// <summary>
    /// ノーツのタイミングなどを計算
    /// </summary>
    public void CalNotes()
    {
        // レーンごとに計算
        for(int i = 0; i < DataManager.Instance.lane; i++)
        {
            CalLaneNotes(i);
        }
    }


    private void CalLaneNotes(int lane)
    {
        float totalTime = 0;
        foreach (BarData barData in DataManager.Instance.barList)
        {
            for (int i = 0; i < barData.LPB; i++)
            {
                // 1拍の長さを計算
                // TODO:オフセットなどを考慮
                float length = 60 / DataManager.Instance.BPM * 4 * barData.measure.numerator / barData.measure.denominator / barData.LPB;
                // 計算後のデータを代入
                NotesData.Note note = DataManager.Instance.barList[barData.barNum].notesArray[lane, i];
                note.length = length;
                //
                note.time = DataManager.Instance.offset + totalTime;
                //
                // TODO:フレームを計算
                //
                DataManager.Instance.barList[barData.barNum].notesArray[lane, i] = note;
                //
                totalTime += length;
                //Debug.Log($"barnum{barData.barNum}, lane{lane}, time{DataManager.Instance.barList[barData.barNum].notesArray[lane, i].time}");
            }
        }
    }
}
