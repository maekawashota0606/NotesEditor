using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonGenerator : SingletonMonoBehaviour<JsonGenerator>
{
    public void Generate(SheetData data)
    {
        // 出力パス
        string filePath = "file://" + Application.dataPath + "/StreamingAssets/";

        // jsonファイル名
        string fileName = $"{data.musicID}_{data.course}.json";

        // 出力
        StreamWriter writer = new StreamWriter(filePath + fileName, true);
        string jsonData = JsonUtility.ToJson(data);
        writer.WriteLine(jsonData);
        writer.Flush();
        writer.Close();
    }
}