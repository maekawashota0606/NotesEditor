using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField]
    private AudioSource _music = null;
    [SerializeField]
    private AudioSource _tapSE = null;

    // 
    private bool _isLoading = false;

    /// <summary>
    /// ���[�J���t�@�C������p�X���w�肵�Ȃ��擾
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public IEnumerator SetAudioFile(string fileName)
    {
        // �Ȃ̍Đ����͏������Ȃ�
        if (_music.isPlaying)
            yield break;

        // 
        _isLoading = true;
        string path = "file://" + Application.dataPath + "/StreamingAssets/";

        // ���[�J���t�@�C��������(�Ƃ肠����wav�̂�)
        using (UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip(path + fileName, AudioType.WAV))
        {
            // ���[�h�҂�
            yield return req.SendWebRequest();
            _isLoading = false;

            // �������擾
            // TODO:�����̗�O���������܂������Ȃ�
            AudioClip audioClip = ((DownloadHandlerAudioClip)req.downloadHandler).audioClip;

            if (audioClip.loadState != AudioDataLoadState.Loaded)
            {
                // ���[�h���s����
                Debug.LogError("���[�h���s");
                yield break;
            }

            _music.clip = audioClip;
            GameDirector.Instance.SetDuration(_music.clip.length);
        }
    }


    public bool PlayMusic(float time = 0)
    {
        // �ȃt�@�C�����Ȃ��A���[�h���̏ꍇ�͏������Ȃ�
        if (_isLoading || _music.clip == null)
            return false;

        // �w��ʒu����Đ�
        _music.time = time;
        _music.Play();
        return true;
    }


    public float StopMusic()
    {
        _music.Stop();
        return _music.time;
    }
}
