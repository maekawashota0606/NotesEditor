using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    public GameState state = GameState.Edit;
    // Ä¶ŠÔ‹L‰¯
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
        // Ä¶’†‚È‚ç
        if(state == GameState.Playing)
        {
            AudioManager.Instance.StopMusic();
            state = GameState.Edit;
            return;
        }


        // “r’†‚©‚çÄ¶
        if (0 <= _cue)
            _passedTime = _cue;

        // Ä¶
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
