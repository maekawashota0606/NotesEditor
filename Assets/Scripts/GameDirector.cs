using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : SingletonMonoBehaviour<GameDirector>
{
    private bool _isGenerated = false;
    // Debug
    bool isFirstPush = true;
    float timeCount = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            NotesReader.Instance.TextTojson();

        if (Input.GetKeyDown(KeyCode.Space) && !_isGenerated)
        {
            NotesGenerator.Instance.GenerateNotes("Assets/Resources/NotesAtJson/0_0.json");
            _isGenerated = true;
        }
            
        // à⁄ìÆèàóù
        if(_isGenerated)
        {
            float speedPerFrame = Player.Instance.highSpeed * Time.deltaTime * -1;
            foreach(GameObject note in NotesManager.Instance.notes)
                note.transform.Translate(new Vector3(0, 0, speedPerFrame));
        }
    }

    private void GetTapTime()
    {
        float times;
        timeCount += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isFirstPush)
            {
                timeCount = 0;
                times = timeCount;
                isFirstPush = false;
            }
            else
            {
                times = timeCount;
                timeCount = 0;
            }
            Debug.Log(times);
        }
    }
}
