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
        // �Đ����Ȃ�
        if(state == GameState.Playing)
        {
            AudioManager.Instance.StopMusic();
            state = GameState.Edit;
            return;
        }


        // �r������Đ�
        if (0 <= _musicCue)
            DataManager.Instance.time = _musicCue;

        // �Đ�
        if (AudioManager.Instance.PlayMusic(DataManager.Instance.time))
        {
            state = GameState.Playing;

            // �^�C�~���O�̈ꗗ�����X�g��
            NotesManager.Instance.AlignNoteTimes();
            // �ŏ���SE��炷�ׂ��^�C�~���O���Z�b�g
            _SECue = NotesManager.Instance.NextCue(DataManager.Instance.time);

            //
            StartCoroutine(OnPlaying());
        }
    }


    private IEnumerator OnPlaying()
    {
        while(state == GameState.Playing)
        {
            // �Đ����Ԃ��I�������Ȃ�
            if (_duration < DataManager.Instance.time)
                state = GameState.Edit;

            // �o�ߎ��ԃJ�E���g
            DataManager.Instance.time += Time.deltaTime;
            EditUIManager.Instance.SetPlaySlider(DataManager.Instance.time, _duration);


            // Cue�����݂��Ȃ�(�}�C�i�X)�Ȃ�X���[
            if (0 <= _SECue)
            {
                // SE����^�C�~���O��������
                if (_SECue < DataManager.Instance.time)
                {
                    AudioManager.Instance.PlaySE();
                    // ����SE����^�C�~���O���Z�b�g
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
    /// �Ȃ̒��������m
    /// </summary>
    /// <param name="d"></param>
    public void SetDuration(float d)
    {
        _duration = d;
    }
}
