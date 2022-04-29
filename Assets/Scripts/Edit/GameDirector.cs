using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    public GameState state = GameState.Edit;
    private float _musicCue = -1;
    private float _duration = 0;
    private float _SECue = -1;

    public enum GameState
    {
        Edit,
        Playing
        //Option
    }


    public void Play()
    {
        // 再生中なら
        if(state == GameState.Playing)
        {
            AudioManager.Instance.StopMusic();
            state = GameState.Edit;
            return;
        }


        // 途中から再生
        if (0 <= _musicCue)
            DataManager.Instance.time = _musicCue;

        // 再生
        if (AudioManager.Instance.PlayMusic(DataManager.Instance.time))
        {
            state = GameState.Playing;

            // タイミングの一覧をリスト化
            NotesManager.Instance.AlignNoteTimes();
            // 最初のSEを鳴らすべきタイミングをセット
            _SECue = NotesManager.Instance.NextCue(DataManager.Instance.time);

            //
            StartCoroutine(OnPlaying());
        }
    }


    private IEnumerator OnPlaying()
    {
        while(state == GameState.Playing)
        {
            // 再生時間が終了したなら
            if (_duration < DataManager.Instance.time)
                state = GameState.Edit;

            // 経過時間カウント
            DataManager.Instance.time += Time.deltaTime;
            EditUIManager.Instance.SetPlaySlider(DataManager.Instance.time, _duration);


            // Cueが存在しない(マイナス)ならスルー
            if (0 <= _SECue)
            {
                // SEが鳴るタイミングが来たら
                if (_SECue < DataManager.Instance.time)
                {
                    AudioManager.Instance.PlaySE();
                    // 次のSEが鳴るタイミングをセット
                    _SECue = NotesManager.Instance.NextCue(DataManager.Instance.time);
                }
            }

            yield return null;
        }
    }


    public void Skip(float ratio)
    {
        if (state == GameState.Edit)
        {
            _musicCue = _duration * ratio;
            DataManager.Instance.time = _musicCue;
        }
    }


    /// <summary>
    /// 曲の長さを検知
    /// </summary>
    /// <param name="d"></param>
    public void SetDuration(float d)
    {
        _duration = d;
    }
}
