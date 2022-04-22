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
    public Vector3 gridOffset = new Vector3(0, -0.5f);
    // 
    private Vector3 _lineOrigin = Vector3.zero;
    private Vector3 _lineEnd = Vector3.zero;


    private void OnRenderObject()
    {
        DrawHorizontalLine();
        DrawVerticalLines();
        BarHighLight(DataManager.Instance.choosingBarNum);
        DrawTimeLine(DataManager.Instance.time);
    }


    /// <summary>
    /// 始点と終点の2点間を結ぶ1本の線を引く
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="end"></param>
    private void DrawLine(Vector3 origin, Vector3 end)
    {
        // オフセット、拡大率を考慮
        origin *= DataManager.Instance.stretchRatio;
        origin += gridOffset;
        end *= DataManager.Instance.stretchRatio;
        end += gridOffset;

        // 描画
        GL.PushMatrix();
        GL.MultMatrix(transform.localToWorldMatrix);
        GL.Begin(GL.LINES);
        GL.Vertex3(origin.x, origin.y, origin.z);
        GL.Vertex3(end.x, end.y, end.z);
        GL.End();
        GL.PopMatrix();
    }


    private void DrawTimeLine(float time)
    {
        _lineOrigin = Vector3.zero;
        _lineOrigin.x = time;
        _lineEnd = _lineOrigin;

        // 見切れる長さを適当に指定
        _lineOrigin.y = 5;
        _lineEnd.y = -30;

        //
        _lineMaterialRed.SetPass(0);
        DrawLine(_lineOrigin, _lineEnd);
    }


    private void DrawVerticalLines()
    {
        float y = -DataManager.Instance.lane;
        foreach (BarData bar in DataManager.Instance.barList)
        {
            // 1拍づつ調べる
            for(int i = 0; i < bar.LPB; i++)
            {
                // 小節線を強調表示
                if (i == 0)
                    _lineMaterialRed.SetPass(0);
                else
                    _lineMaterial.SetPass(0);

                // Vector3に格納して関数に渡す
                // 始点
                _lineOrigin = Vector3.zero;
                // 0番レーンのみ調べる(レーンによって拍の長さが変わることはないため)
                _lineOrigin.x = bar.notesArray[0, i].time;
                // 終点
                _lineEnd = _lineOrigin;
                // レーンの数だけ下に伸ばす
                _lineEnd.y += y;
                DrawLine(_lineOrigin, _lineEnd);
                //Debug.Log(bar.notesArray[0, i].time);
            }
        }

        // 最後のBarを描画
        //DrawLine(_lineOrigin + _gridOffset, _lineEnd + _gridOffset);
    }


    private void DrawHorizontalLine()
    {
        if (DataManager.Instance.barList.Count < 1)
            return;

        int lastBarIdx = DataManager.Instance.barList.Count - 1;
        int lastNoteIdx = DataManager.Instance.barList[lastBarIdx].LPB - 1;
        // 最後の拍が来る時間を取得
        float x = DataManager.Instance.barList[lastBarIdx].notesArray[0, lastNoteIdx].time;

        _lineMaterial.SetPass(0);
        // 1レーンまとめて描画(小節ごとに分けない)
        for (int i = 0; i < DataManager.Instance.lane + 1; i++)
        {
            // Vector3に格納して関数に渡す
            // 始点
            _lineOrigin.x = 0;
            _lineOrigin.y = -i;
            // 終点
            _lineEnd = _lineOrigin;
            _lineEnd.x = x;
            DrawLine(_lineOrigin, _lineEnd);
        }
    }


    /// <summary>
    /// 選択中の小節を強調表示
    /// </summary>
    private void BarHighLight(int idx)
    {
        if (idx < 0)
            return;

        float left = DataManager.Instance.barList[idx].notesArray[0, 0].time;
        float right = DataManager.Instance.barList[idx].notesArray[0, DataManager.Instance.barList[idx].LPB - 1].time + DataManager.Instance.barList[idx].notesArray[0, DataManager.Instance.barList[idx].LPB - 1].length;
        float top = 0;
        float bottom = -DataManager.Instance.lane;

        //矩形の4辺を描画
        _highlightMat.SetPass(0);
        _lineOrigin.x = left;
        _lineOrigin.y = top;
        _lineEnd.x = right;
        _lineEnd.y = top;
        DrawLine(_lineOrigin, _lineEnd);
        _lineOrigin.x = right;
        _lineOrigin.y = bottom;
        DrawLine(_lineOrigin, _lineEnd);
        _lineEnd.x = left;
        _lineEnd.y = bottom;
        DrawLine(_lineOrigin, _lineEnd);
        _lineOrigin.x = left;
        _lineOrigin.y = top;
        DrawLine(_lineOrigin, _lineEnd);
    }


    /// <summary>
    /// クリックした座標から何小節目を選択したかを計算
    /// </summary>
    /// <param name="clickPos"></param>
    /// <returns>選択中の小節番号(非選択なら-1)</returns>
    public int CheckHitBar(Vector3 clickPos)
    {
        // 上辺のy座標(固定)
        float top = 0;
        // 底辺のy座標(固定)
        float bottom = -DataManager.Instance.lane;
        // 1つ前の小節の合計の長さ
        float lastTotalLength = 0;


        // 1小節づつチェック
        foreach (BarData bar in DataManager.Instance.barList)
        {
            // この小節を含めたこれまでの小節の合計の長さ
            float totalLength = 0;
            // この小節の長さ
            float length = 0;
            for (int i = 0; i < bar.LPB; i++)
                length += bar.notesArray[0, i].length;
            
            totalLength = lastTotalLength + length;

            //矩形の2点の座標を求める
            Vector3 leftTop = new Vector3(lastTotalLength + DataManager.Instance.offset, top);
            Vector3 rightBottom = new Vector3(totalLength + DataManager.Instance.offset, bottom);


            // ヒットしたなら
            if (IsHitSquareAndDot(clickPos, leftTop, rightBottom))
                return bar.barNum;

            // ヒットしなければ次の小節を調べる
            lastTotalLength = totalLength;
        }

        // 該当なし
        return -1;
    }


    /// <summary>
    /// 指定した小節のどのマスにヒットしたか判定
    /// </summary>
    /// <param name="clickPos"></param>
    /// <param name="idx"></param>
    /// <param name="lane"></param>
    /// <param name="cell"></param>
    public void CheckHitCell(Vector3 clickPos, int idx, out int lane, out int cell)
    {
        // 1レーンごとに調べる
        for (int i = 0; i < DataManager.Instance.lane; i++)
        {
            // 上辺のy座標
            float top = i * -1;
            // 底辺のy座標
            float bottom = (i + 1) * -1;


            // 1拍づつ判定
            for (int j = 0; j < DataManager.Instance.barList[idx].LPB; j++)
            {
                // 左辺のx座標(この拍が来る時間)
                float left = DataManager.Instance.barList[idx].notesArray[i, j].time;
                // 右辺のx座標(この拍が来る時間 + この拍の長さ = この拍の終わり)
                float right = left + DataManager.Instance.barList[idx].notesArray[i, j].length;

                // 2点の座標をセット
                Vector3 leftTop = new Vector3(left, top);
                Vector3 rightBottom = new Vector3(right, bottom);


                // 接触しているならレーンと座標を返す
                if (IsHitSquareAndDot(clickPos, leftTop, rightBottom))
                {
                    lane = i;
                    cell = j;
                    //Debug.Log(lane + "," + cell);

                    return;
                }
            }
        }

        // 何れにもヒットしなかった場合
        lane = -1;
        cell = -1;
    }


    /// <summary>
    /// 点と矩形の当たり判定をとる
    /// </summary>
    /// <param name="clickPos"></param>
    /// <param name="leftTop"></param>
    /// <param name="rightBottom"></param>
    /// <returns></returns>
    private bool IsHitSquareAndDot(Vector3 clickPos, Vector3 leftTop, Vector3 rightBottom)
    {
        // オフセット、拡大率を考慮
        leftTop *= DataManager.Instance.stretchRatio;
        leftTop += gridOffset;
        rightBottom *= DataManager.Instance.stretchRatio;
        rightBottom += gridOffset;

        // xが範囲内でなければ
        if ((clickPos.x < leftTop.x) || (rightBottom.x < clickPos.x))
            return false;
        // yが範囲内でなければ
        if ((leftTop.y < clickPos.y) || (clickPos.y < rightBottom.y))
            return false;

        return true;
    }
}
