public class NotesManager : SingletonMonoBehaviour<NotesManager>
{
    /// <summary>
    /// �m�[�c�̃^�C�~���O�Ȃǂ��v�Z
    /// </summary>
    public void CalNotes()
    {
        // test
        //DataManager.Instance.BPM = 240;
        //DataManager.Instance.AddBarList();
        //DataManager.Instance.AddBarList();
        //DataManager.Instance.barList[0].LPB = 16;

        // ���[�����ƂɌv�Z
        for(int i = 0; i < DataManager.Instance.lane; i++)
        {
            CalLaneNotes(i);
        }

        //
        //verticalLinePosXList.Clear();
        //notesDuration.Clear();
        //for (int i = 0; i < LPB; i++)
        //{
        //    // �{����1���P�ʂŌv�Z
        //    float x = barDuration / LPB * i;
        //    verticalLinePosXList.Add(origin.x + x);
        //}

        //// LPB�̕����v�Z
        //float lastduration = 0;
        //for (int i = 0; i < LPB; i++)
        //{
        //    int backIdx = LPB - i - 1;
        //    float duration = barDuration - verticalLinePosXList[backIdx];
        //    notesDuration.Add(duration - lastduration);
        //    lastduration = duration;
        //}
    }

    private void CalLaneNotes(int lane)
    {
        float totalTime = 0;
        foreach (BarData barData in DataManager.Instance.barList)
        {
            for (int i = 0; i < barData.LPB; i++)
            {
                // 1���̒������v�Z
                // TODO:�I�t�Z�b�g�Ȃǂ��l��
                float length = 60 / DataManager.Instance.BPM * 4 * barData.measure.numerator / barData.measure.denominator / barData.LPB;
                // �v�Z��̃f�[�^����
                NotesData.Note note = DataManager.Instance.barList[barData.barNum].notesArray[lane, i];
                note.length = length;
                note.time = totalTime;
                //
                // TODO:�t���[�����v�Z
                //
                DataManager.Instance.barList[barData.barNum].notesArray[lane, i] = note;
                //
                totalTime += length;
                //Debug.Log($"barnum{barData.barNum}, lane{lane}, time{DataManager.Instance.barList[barData.barNum].notesArray[lane, i].time}");
            }
        }
    }

    //public void SetNotePos(int lane, int cell)
    //{
    //    Vector3 pos = origin + new Vector3(DataManager.Instance.barList[DataManager.Instance.choosingBarNum].verticalLinePosXList[cell], -lane - 1);
    //    pos *= DataManager.Instance.stretchRatio;
    //    //DataManager.Instance.barList[DataManager.Instance.choosingBarNum].notesArray[lane, cell].pos = pos;
    //}
}
