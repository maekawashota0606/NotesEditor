using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    public GameState state = GameState.Edit;
    // 再生時間記憶
    private float _passedTime = 0;
    private float _cue = -1;
    private float _duration = 0;

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
        if (0 <= _cue)
            _passedTime = _cue;

        // 再生
        if (AudioManager.Instance.PlayMusic(_passedTime))
        {
            state = GameState.Playing;
            StartCoroutine(OnPlaying());
        }
    }


    private IEnumerator OnPlaying()
    {
        while(state == GameState.Playing)
        {
            if (_duration < _passedTime)
                state = GameState.Edit;

            _passedTime += Time.deltaTime;
            EditUIManager.Instance.SetPlaySlider(_passedTime, _duration);

            yield return null;
        }
    }

    public void Skip(float ratio)
    {
        if (state == GameState.Edit)
        {
            _cue = _duration * ratio;
        }
    }


    public void SetDuration(float d)
    {
        _duration = d;
    }
}
