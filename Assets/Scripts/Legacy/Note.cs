/// <summary>
/// �m�[�c1�Ɋ܂܂����
/// </summary>
public class Note
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


    // TODO:�����\��
    //public float highSpeed;
    //public float BPM;
    //public float offset;
    //public List<Note> notes;
}
