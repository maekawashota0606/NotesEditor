using System.Collections.Generic;

/// <summary>
/// ���ʃf�[�^�N���X
/// </summary>
[System.Serializable]
public class NotesData
{
    /// <summary>
    /// �y�ȃ^�C�g��
    /// </summary>
    public string title = "NoTitle";
    /// <summary>
    /// �T�u�^�C�g��
    /// </summary>
    public string subTitle = string.Empty;
    /// <summary>
    /// �Ǘ��p��ID
    /// </summary>
    public int musicID = 0;
    /// <summary>
    /// ��Փx���[�h
    /// </summary>
    public int course = 0;
    /// <summary>
    /// ��Փx���x��
    /// </summary>
    public float level = 0;
    /// <summary>
    /// �I�ȉ�ʂȂǂŎg�p(�����ł͎g���܂���)
    /// </summary>
    public float basicBPM = 0;
    /// <summary>
    /// �����ƕR�Â�
    /// </summary>
    public string musicPath = string.Empty;
    /// <summary>
    /// �I�ȉ�ʂȂǂŎg�p(sec)
    /// </summary>
    public float demoStart = 0;
    /// <summary>
    /// Json�������m�[�c�f�[�^���i�[
    /// </summary>
    public List<Note> noteList = new List<Note>();

    /// <summary>
    /// 1�m�[�c�Ɋ܂܂����
    /// </summary>
    [System.Serializable]
    public struct Note
    {
        /// <summary>
        /// �m�[�c�̎�ނ����ʂ���ԍ�(���[�U�[��`)
        /// </summary>
        public int notesType;
        /// <summary>
        /// �ǂ̃��[���ɑ����邩
        /// </summary>
        public int lane;
        /// <summary>
        /// �J�n���牽�t���[���ڂɂ���̂�(60fps?)
        /// </summary>
        public int frame;
        /// <summary>
        /// �J�n���牽�b�ŗ���̂�
        /// </summary>
        public float time;
        /// <summary>
        /// ���̒���(sec)
        /// </summary>
        public float length;


        // �����\��
        //public float highSpeed;
        //public float BPM;
        //public float offset;
        //public List<Note> notes;
        // ����H
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
