using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonGenerator : SingletonMonoBehaviour<JsonGenerator>
{
    public void Generate(SheetData data, string savePath)
    {
        // jsonファイル命名、出力パス指定
        //string saveName = $"{_notesData.musicID}_{(int)_notesData.course}.json";
        //_savePath += saveName;

        //// 出力
        //StreamWriter writer = new StreamWriter(_savePath, false);
        //string data = JsonUtility.ToJson(_notesData);
        //writer.WriteLine(data);
        //writer.Flush();
        //writer.Close();
    }
}