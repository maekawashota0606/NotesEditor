public class NotesManager : SingletonMonoBehaviour<NotesManager>
{
    /// <summary>
    /// �m�[�c�̃^�C�~���O�Ȃǂ��v�Z
    /// </summary>
    public void CalNotes()
    {
        // ���[�����ƂɌv�Z
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
                // 1���̒������v�Z
                // TODO:�I�t�Z�b�g�Ȃǂ��l��
                float length = 60 / DataManager.Instance.BPM * 4 * barData.measure.numerator / barData.measure.denominator / barData.LPB;
                // �v�Z��̃f�[�^����
                NotesData.Note note = DataManager.Instance.barList[barData.barNum].notesArray[lane, i];
                note.length = length;
                //
                note.time = DataManager.Instance.offset + totalTime;
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
}
