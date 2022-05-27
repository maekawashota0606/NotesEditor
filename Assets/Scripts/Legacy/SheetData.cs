using System.Collections.Generic;

/// <summary>
/// ���ʃf�[�^�N���X
/// </summary>
[System.Serializable]
public class SheetData
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
    /// �����̃p�X
    /// </summary>
    public string musicPath = string.Empty;
    /// <summary>
    /// �I�ȉ�ʂȂǂŎg�p(sec)
    /// </summary>
    public float demoStart = 0;
    /// <summary>
    /// ���߂��Ƃ̃m�[�c�f�[�^���i�[
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
