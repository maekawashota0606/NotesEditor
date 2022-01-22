using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : SingletonMonoBehaviour<GridManager>
{
    [SerializeField]
    private Material _lineMaterial = null;
    [SerializeField]
    private Material _lineMaterialRed = null;
    [SerializeField]
    private Material _highlightMat = null;
    private Vector3 _gridOffset = new Vector3(0, -0.5f);
    //
    private float _lineEndX = 0;
    private Vector3 _lineOrigin = Vector3.zero;
    private Vector3 _lineEnd = Vector3.zero;

    private void OnRenderObject()
    {
        // TODO:必要なタイミングのみ実行
        CalBarOrigin();
        //
        DrawHorizontalLine();
        DrawVerticalLine();
        BarHighLight(DataManager.Instance.choosingBarNum);
    }

    private void DrawLine(Vector3 origin, Vector3 end)
    {
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);
        GL.Begin(GL.LINES);
        GL.Vertex3(origin.x, origin.y, origin.z);
        GL.Vertex3(end.x, end.y, end.z);
        GL.End();
        GL.PopMatrix();
    }

    public void DrawVerticalLine()
    {
        float verticalY = -DataManager.Instance.lane * DataManager.Instance.stretchRatio.y;
        foreach (Bar bar in DataManager.Instance.barList)
        {
            bool isStartLine = true;
            foreach(float x in bar.verticalLinePosXList)
            {
                if(isStartLine)
                {
                    isStartLine = false;
                    _lineMaterialRed.SetPass(0);
                }
                else
                    _lineMaterial.SetPass(0);

                _lineOrigin = Vector3.zero;
                _lineOrigin.x = (bar.origin.x + x) * DataManager.Instance.stretchRatio.x;
                _lineEnd = _lineOrigin;
                _lineEnd.y += verticalY;
                DrawLine(_lineOrigin + _gridOffset, _lineEnd + _gridOffset);
            }
        }

        // 最後のBarを描画
        _lineOrigin = Vector3.zero;
        _lineOrigin.x = _lineEndX;
        _lineEnd = _lineOrigin;
        _lineEnd.y += verticalY;
        DrawLine(_lineOrigin + _gridOffset, _lineEnd + _gridOffset);
    }

    public void DrawHorizontalLine()
    {
        float horizontalX = 0;
        foreach (Bar bar in DataManager.Instance.barList)
            horizontalX += bar.barDuration;
        horizontalX *= DataManager.Instance.stretchRatio.x;
        // 縦線描画に使用
        _lineEndX = horizontalX;

        _lineMaterial.SetPass(0);
        // 1レーンまとめて描画(小節ごとに分けない)
        for (int i = 0; i < DataManager.Instance.lane + 1; i++)
        {
            float horizontalY = -i * DataManager.Instance.stretchRatio.y;
            _lineOrigin.x = 0;
            _lineOrigin.y = horizontalY;
            _lineEnd.x = horizontalX;
            _lineEnd.y = horizontalY;
            DrawLine(_lineOrigin + _gridOffset, _lineEnd + _gridOffset);
        }
    }

    private void BarHighLight(int idx)
    {
        if (idx < 0)
            return;

        _highlightMat.SetPass(0);
        Vector3 leftTop = Vector3.Scale(DataManager.Instance.barList[idx].origin, DataManager.Instance.stretchRatio);
        //
        Vector3 rightBottom = leftTop;
        rightBottom.x += DataManager.Instance.barList[idx].barDuration * DataManager.Instance.stretchRatio.x;
        rightBottom.y = -DataManager.Instance.lane * DataManager.Instance.stretchRatio.y;

        DrawLine(leftTop + _gridOffset, new Vector3(leftTop.x, rightBottom.y, 0) + _gridOffset);
        DrawLine(leftTop + _gridOffset, new Vector3(rightBottom.x, 0, 0) + _gridOffset);
        DrawLine(new Vector3(rightBottom.x, leftTop.y) + _gridOffset, rightBottom + _gridOffset);
        DrawLine(new Vector3(leftTop.x, rightBottom.y, 0) + _gridOffset, rightBottom + _gridOffset);
    }

    /// <summary>
    /// クリックした座標から何小節目を選択したかを計算
    /// </summary>
    /// <param name="clickPos"></param>
    /// <returns>選択中の小節番号(非選択なら-1)</returns>
    public int CheckHitBar(Vector3 clickPos)
    {
        float bottom = -(DataManager.Instance.lane) * DataManager.Instance.stretchRatio.y;

        foreach (Bar bar in DataManager.Instance.barList)
        {
            Vector3 leftTop = Vector3.Scale(bar.origin, DataManager.Instance.stretchRatio);
            Vector3 rightBottom = leftTop;
            // 半マスずらす
            leftTop.x -= bar.notesDuration[0] / 2 * DataManager.Instance.stretchRatio.x;
            float right = (bar.barDuration - bar.notesDuration[bar.LPB - 1] / 2) * DataManager.Instance.stretchRatio.x ;

            rightBottom.x += right;
            rightBottom.y += bottom;
            if(IsHitSquareAndDot(clickPos, leftTop + _gridOffset, rightBottom + _gridOffset))
                return bar.barNum;
        }

        return -1;
    }

    public void CheckHitCell(Vector3 clickPos, int idx, out int lane, out int cell)
    {
        lane = -1;
        cell = -1;
        for(int i = 0; i < DataManager.Instance.lane; i++)
        {
            Vector3 leftTop = DataManager.Instance.barList[idx].origin;
            leftTop.x *= DataManager.Instance.stretchRatio.x;
            leftTop.y *= -DataManager.Instance.stretchRatio.y;
            Vector3 rightBottom = leftTop;
            rightBottom.y += -DataManager.Instance.stretchRatio.y * (i + 1);

            for (int j = 0; j < DataManager.Instance.barList[idx].LPB; j++)
            {
                int backIdx = DataManager.Instance.barList[idx].LPB - j - 1;
                // トータルの長さ - 後ろから数えてj + 1番目のライン座標x
                Vector3 rightBottoms = rightBottom;
                rightBottoms.x += (DataManager.Instance.barList[idx].barDuration - DataManager.Instance.barList[idx].verticalLinePosXList[backIdx]) * DataManager.Instance.stretchRatio.x;

                if (IsHitSquareAndDot(clickPos, leftTop + _gridOffset, rightBottoms + _gridOffset))
                {
                    lane = i;
                    cell = j;
                    Debug.Log(lane + "," + cell);
                    return;
                }
            }
        }
    }

    private bool IsHitSquareAndDot(Vector3 clickPos, Vector3 leftTop, Vector3 rightBottom)
    {
        // xが範囲内でなければ
        if ((clickPos.x < leftTop.x) || (rightBottom.x < clickPos.x))
            return false;
        // yが範囲内でなければ
        if ((leftTop.y < clickPos.y) || (clickPos.y < rightBottom.y))
            return false;

        return true;
    }

    private void CalBarOrigin()
    {
        float totalX = 0;
        for(int i = 0; i < DataManager.Instance.barList.Count; i++)
        {
            Bar bar = DataManager.Instance.barList[i];
            float x = bar.barDuration;
            //
            bar.origin = new Vector3(totalX, 0, 0);
            DataManager.Instance.ChangeBarData(bar);
            //
            totalX += x;
        }
    }
}
