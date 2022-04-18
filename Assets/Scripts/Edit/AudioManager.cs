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
    /// ローカルファイルからパスを指定し曲を取得
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public IEnumerator SetAudioFile(string fileName)
    {
        // 曲の再生中は処理しない
        if (_music.isPlaying)
            yield break;

        // 
        _isLoading = true;
        string path = "file://" + Application.dataPath + "/StreamingAssets/";

        // ローカルファイルを検索(とりあえずwavのみ)
        using (UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip(path + fileName, AudioType.WAV))
        {
            // ロード待ち
            yield return req.SendWebRequest();
            _isLoading = false;

            // 音源を取得
            // TODO:ここの例外処理がうまくいかない
            AudioClip audioClip = ((DownloadHandlerAudioClip)req.downloadHandler).audioClip;

            if (audioClip.loadState != AudioDataLoadState.Loaded)
            {
                // ロード失敗処理
                Debug.LogError("ロード失敗");
                yield break;
            }

            _music.clip = audioClip;
            GameDirector.Instance.SetDuration(_music.clip.length);
        }
    }


    public bool PlayMusic(float time = 0)
    {
        // 曲ファイルがない、ロード中の場合は処理しない
        if (_isLoading || _music.clip == null)
            return false;

        // 指定位置から再生
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
