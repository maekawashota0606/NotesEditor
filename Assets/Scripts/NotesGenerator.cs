using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class NotesGenerator : SingletonMonoBehaviour<NotesGenerator>
{
    [SerializeField]
    private GameObject _notePrefab = null;
    [SerializeField]
    private GameObject _rootObject = null;
    private GameObject[] _setPosition = null;

    private void Start()
    {
        _setPosition = new GameObject[_rootObject.transform.childCount];
        for (int i = 0; i < _rootObject.transform.childCount; i++)
            _setPosition[i] = _rootObject.transform.GetChild(i).gameObject;
    }

    public void GenerateNotes(string path)
    {
        if (File.Exists(path))
        {
            // 読み込み
            StreamReader reader = new StreamReader(path);
            string data = reader.ReadToEnd();
            reader.Close();

            NotesData notesData = JsonUtility.FromJson<NotesData>(data);
            
            foreach (NotesData.Note note in notesData.noteList)
            {
                GameObject noteObj = null;
                // NotesTypeに応じて生成
                switch (note.notesType)
                {
                    //case NotesData.NotesType.Single:
                    //     noteObj = Instantiate(_notePrefab);
                    //    break;
                    default:
                        break;
                }
                noteObj.name = $"note_{note.lane}_{note.frame}";
                noteObj.transform.parent = _setPosition[note.lane].transform;
                // 座標を決定
                float notesPosZ = note.frame * Player.Instance.highSpeed;
                noteObj.transform.position = new Vector3(_setPosition[note.lane].transform.position.x, 0, notesPosZ);
                NotesManager.Instance.notes.Add(noteObj);
            }
        }
        else
            Debug.LogError($"{path}という名前のファイルは存在しません");
    }
}
