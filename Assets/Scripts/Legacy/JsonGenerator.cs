using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonGenerator : SingletonMonoBehaviour<JsonGenerator>
{
    public void Generate(SheetData data, string savePath)
    {
        // json�t�@�C�������A�o�̓p�X�w��
        //string saveName = $"{_notesData.musicID}_{(int)_notesData.course}.json";
        //_savePath += saveName;

        //// �o��
        //StreamWriter writer = new StreamWriter(_savePath, false);
        //string data = JsonUtility.ToJson(_notesData);
        //writer.WriteLine(data);
        //writer.Flush();
        //writer.Close();
    }
}