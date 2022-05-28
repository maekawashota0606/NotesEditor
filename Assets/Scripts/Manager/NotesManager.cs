using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class NotesManager : SingletonMonoBehaviour<NotesManager>
{
    private List<float> _noteTimes = new List<float>(19200);

    /// <summary>
    /// �m�[�c�̃^�C�~���O�Ȃǂ��v�Z
    /// </summary>
    public void CalNotes()
    {
        // ���[�����ƂɌv�Z
        for(int i = 0; i < DataManager.Instance.GetLane(); i++)
        {
            CalLaneNotes(i);
        }
    }


    private void CalLaneNotes(int lane)
    {
        float totalTime = 0;
        foreach (Notes.Bar barData in BarManager.Instance.barList)
        {
            for (int i = 0; i < barData.LPB; i++)
            {
                // �v�Z��̃f�[�^����
                Notes.Note note = BarManager.Instance.barList[barData.barNum].notesArray[lane, i];
                // 1���̒������v�Z
                // TODO:�m�[�c�ɐݒ肳�ꂽ�I�t�Z�b�g�Ȃǂ��l��
                float length = 60 / DataManager.Instance.GetBPM() * 4 * barData.measure.numerator / barData.measure.denominator / barData.LPB;
                //
                note.length = length;
                // ����܂ł̌o�ߎ���
                note.time = DataManager.Instance.GetOffset() + totalTime;
                // ���t���[���ڂɂ��邩���v�Z
                double frame = System.Math.Round(note.time / (1f / DataManager.Instance.GetTargetFrameRate()), System.MidpointRounding.AwayFromZero);
                note.frame = (int)frame;
                //UnityEngine.Debug.Log(note.time + "_" + frame);
                //
                note.lane = lane;
                // ���f�[�^�ɔ��f
                BarManager.Instance.barList[barData.barNum].notesArray[lane, i] = note;
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
        foreach (Notes.Bar data in BarManager.Instance.barList)
        {
            // 1��Â`�F�b�N
            for (int i = 0; i < data.LPB; i++)
            {
                for (int j = 0; j < DataManager.Instance.GetLane(); j++)
                {
                    // �m�[�c�Ƃ��ēo�^����Ă���Ȃ�
                    if (data.notesArray[j, i].notesType != 0)
                    {
                        // �ǉ�
                        _noteTimes.Add(data.notesArray[j, i].time);
                        //UnityEngine.Debug.Log(data.notesArray[j, i].time);
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
