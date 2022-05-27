using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    private GameState _state = GameState.Edit;
    private float _musicCue = -1;
    private float _duration = 0;
    private float _SECue = -1;

    public enum GameState
    {
        Edit,
        Playing
        //Option
    }

    public GameState GetState()
    {
        return _state;
    }

    public void SetState(GameState state)
    {
        EditUIManager.Instance.OnChengedPlayState(state == GameState.Playing);
        _state = state;
    }

    public void Play()
    {
        // �Đ����Ȃ�
        if(GetState() == GameState.Playing)
        {
            AudioManager.Instance.StopMusic();
            SetState(GameState.Edit);
            return;
        }


        // �r������Đ�
        if (0 <= _musicCue)
            DataManager.Instance.SetTime(_musicCue);

        // �Đ�
        if (AudioManager.Instance.PlayMusic(DataManager.Instance.GetTime()))
        {
            SetState(GameState.Playing);

            // �^�C�~���O�̈ꗗ�����X�g��
            NotesManager.Instance.AlignNoteTimes();
            // �ŏ���SE��炷�ׂ��^�C�~���O���Z�b�g
            _SECue = NotesManager.Instance.NextCue(DataManager.Instance.GetTime());

            //
            StartCoroutine(OnPlaying());
        }
    }


    private IEnumerator OnPlaying()
    {
        while(GetState() == GameState.Playing)
        {
            // �Đ����Ԃ��I�������Ȃ�
            if (_duration < DataManager.Instance.GetTime())
                SetState(GameState.Edit);

            // �o�ߎ��ԃJ�E���g
            DataManager.Instance.SetTime(DataManager.Instance.GetTime() + Time.deltaTime);
            EditUIManager.Instance.SetPlaySlider(DataManager.Instance.GetTime(), _duration);


            // Cue�����݂��Ȃ�(�}�C�i�X)�Ȃ�X���[
            if (0 <= _SECue)
            {
                // SE����^�C�~���O��������
                if (_SECue < DataManager.Instance.GetTime())
                {
                    AudioManager.Instance.PlayTapSE();
                    // ����SE����^�C�~���O���Z�b�g
                    _SECue = NotesManager.Instance.NextCue(DataManager.Instance.GetTime());
                }
            }

            yield return null;
        }
    }


    public void Skip(float ratio)
    {
        if (GetState() == GameState.Edit)
        {
            _musicCue = _duration * ratio;
            DataManager.Instance.SetTime(_musicCue);
        }
    }


    /// <summary>
    /// �Ȃ̒��������m
    /// </summary>
    /// <param name="d"></param>
    public void SetDuration(float d)
    {
        _duration = d;
    }
}
