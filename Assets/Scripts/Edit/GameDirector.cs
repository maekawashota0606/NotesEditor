using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    public GameState state = GameState.Edit;
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
            DataManager.Instance.time = _cue;

        // Ä¶
        if (AudioManager.Instance.PlayMusic(DataManager.Instance.time))
        {
            state = GameState.Playing;
            StartCoroutine(OnPlaying());
        }
    }


    private IEnumerator OnPlaying()
    {
        while(state == GameState.Playing)
        {
            if (_duration < DataManager.Instance.time)
                state = GameState.Edit;

            DataManager.Instance.time += Time.deltaTime;
            EditUIManager.Instance.SetPlaySlider(DataManager.Instance.time, _duration);

            yield return null;
        }
    }


    public void Skip(float ratio)
    {
        if (state == GameState.Edit)
        {
            _cue = _duration * ratio;
            DataManager.Instance.time = _cue;
        }
    }


    /// <summary>
    /// ‹È‚Ì’·‚³‚ğŒŸ’m
    /// </summary>
    /// <param name="d"></param>
    public void SetDuration(float d)
    {
        _duration = d;
    }
}
