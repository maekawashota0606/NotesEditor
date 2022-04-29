using System.Collections.Generic;

public class NotesManager : SingletonMonoBehaviour<NotesManager>
{
    private List<float> _noteTimes = new List<float>(19200);


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


    /// <summary>
    /// �L���ȃm�[�c�������A����鏇�Ԓʂ�ɐ��񂳂����X�g�Ɋi�[
    /// </summary>
    public void AlignNoteTimes()
    {
        _noteTimes.Clear();
        // �m�[�c�f�[�^����ʂ�`�F�b�N
        foreach (BarData data in DataManager.Instance.barList)
        {
            // 1��Â`�F�b�N
            for (int i = 0; i < data.LPB; i++)
            {
                for (int j = 0; j < DataManager.Instance.lane; j++)
                {
                    // �m�[�c�Ƃ��ēo�^����Ă���Ȃ�
                    if (data.notesArray[j, i].notesType != 0)
                    {
                        // �ǉ�
                        _noteTimes.Add(data.notesArray[j, i].time);
                        UnityEngine.Debug.Log(data.notesArray[j, i].time);
                        // ���^�C�~���O�̓X���[���A����LPB�𒲂ׂ�
                        break;
                    }
                }
            }
        }

        // �����Ƀ\�[�g
        _noteTimes.Sort();
    }

    public float NextCue(float elapseTime)
    {
        // �o�ߎ��Ԃ��玟�ɗ���^�C�~���O�𔻒f
        foreach (float time in _noteTimes)
        {
            if (elapseTime <= time)
                return time;
        }
        
        //�l�����݂��Ȃ��ꍇ
        return -1;
    }
}
