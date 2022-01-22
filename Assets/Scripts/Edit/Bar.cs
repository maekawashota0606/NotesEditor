using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar
{
    public int barNum = 0;
    public Measure measure = new Measure();
    public int LPB = 0;
    //
    public NotesData.Note[,] notesArray = new NotesData.Note[DataManager.MAX_LANE, DataManager.MAX_LPB];
    public Vector3 origin  = Vector3.zero;

    public float barDuration = 0;
    public List<float> verticalLinePosXList = new List<float>(DataManager.MAX_LPB);
    public List<float> notesDuration = new List<float>(DataManager.MAX_LPB);

    public struct Measure
    {
        public int denominator;
        public int numerator;
        public Measure(int denom, int num)
        {
            denominator = denom;
            numerator = num;
        }
    }

    public void Init(int num, Measure m, int lpb)
    {
        barNum = num;
        measure = m;
        LPB = lpb;
        origin = Vector3.zero;
        notesArray = new NotesData.Note[DataManager.MAX_LANE, DataManager.MAX_LPB];
        CalBar();
    }

    private void CalBar()
    {
        barDuration = 60 / DataManager.Instance.BPM * 4 * measure.numerator / measure.denominator;
        //
        verticalLinePosXList.Clear();
        notesDuration.Clear();
        for(int i = 0; i < LPB; i++)
        {
            // –{—ˆ‚Í1”’PˆÊ‚ÅŒvŽZ
            float x = barDuration / LPB * i;
            verticalLinePosXList.Add(origin.x + x);
        }

        // LPB‚Ì•‚ðŒvŽZ
        float lastduration = 0;
        for (int i = 0; i < LPB; i++)
        {
            int backIdx = LPB - i - 1;
            float duration = barDuration - verticalLinePosXList[backIdx];
            notesDuration.Add(duration - lastduration);
            lastduration = duration;
        }
    }

    public void SetNotePos(int lane, int cell)
    {
        Vector3 pos = origin + new Vector3(DataManager.Instance.barList[DataManager.Instance.choosingBarNum].verticalLinePosXList[cell], -lane - 1);
        pos *= DataManager.Instance.stretchRatio;
        DataManager.Instance.barList[DataManager.Instance.choosingBarNum].notesArray[lane, cell].pos = pos;
    }
}