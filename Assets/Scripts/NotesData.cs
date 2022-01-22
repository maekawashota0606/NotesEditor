using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NotesData
{
    public string title = "NoTitle";
    public string subTitle = string.Empty;
    public int musicID = 0;
    public int course = 0;
    public float level = 0;
    public float basicBPM = 0;
    public string musicPath = string.Empty;
    public float demoStart = 0;
    public List<Note> noteList = new List<Note>();

    [System.Serializable]
    public struct Note
    {
        public int notesType;
        public int lane;
        public float BPM;
        public int frame;
        public float highSpeed;
        public float offset;
        public Vector3 pos;

        public Note(int type, int lane, float bpm, int frame, float speed, float offset, Vector3 pos)
        {
            this.notesType = type;
            this.lane = lane;
            this.BPM = bpm;
            this.frame = frame;
            this.highSpeed = speed;
            this.offset = offset;
            this.pos = pos;
        }
    }
}
