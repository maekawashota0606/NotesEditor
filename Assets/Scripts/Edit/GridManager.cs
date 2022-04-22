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
    /// �n�_�ƏI�_��2�_�Ԃ�����1�{�̐�������
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="end"></param>
    private void DrawLine(Vector3 origin, Vector3 end)
    {
        // �I�t�Z�b�g�A�g�嗦���l��
        origin *= DataManager.Instance.stretchRatio;
        origin += gridOffset;
        end *= DataManager.Instance.stretchRatio;
        end += gridOffset;

        // �`��
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

        // ���؂�钷����K���Ɏw��
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
            // 1���Â��ׂ�
            for(int i = 0; i < bar.LPB; i++)
            {
                // ���ߐ��������\��
                if (i == 0)
                    _lineMaterialRed.SetPass(0);
                else
                    _lineMaterial.SetPass(0);

                // Vector3�Ɋi�[���Ċ֐��ɓn��
                // �n�_
                _lineOrigin = Vector3.zero;
                // 0�ԃ��[���̂ݒ��ׂ�(���[���ɂ���Ĕ��̒������ς�邱�Ƃ͂Ȃ�����)
                _lineOrigin.x = bar.notesArray[0, i].time;
                // �I�_
                _lineEnd = _lineOrigin;
                // ���[���̐��������ɐL�΂�
                _lineEnd.y += y;
                DrawLine(_lineOrigin, _lineEnd);
                //Debug.Log(bar.notesArray[0, i].time);
            }
        }

        // �Ō��Bar��`��
        //DrawLine(_lineOrigin + _gridOffset, _lineEnd + _gridOffset);
    }


    private void DrawHorizontalLine()
    {
        if (DataManager.Instance.barList.Count < 1)
            return;

        int lastBarIdx = DataManager.Instance.barList.Count - 1;
        int lastNoteIdx = DataManager.Instance.barList[lastBarIdx].LPB - 1;
        // �Ō�̔������鎞�Ԃ��擾
        float x = DataManager.Instance.barList[lastBarIdx].notesArray[0, lastNoteIdx].time;

        _lineMaterial.SetPass(0);
        // 1���[���܂Ƃ߂ĕ`��(���߂��Ƃɕ����Ȃ�)
        for (int i = 0; i < DataManager.Instance.lane + 1; i++)
        {
            // Vector3�Ɋi�[���Ċ֐��ɓn��
            // �n�_
            _lineOrigin.x = 0;
            _lineOrigin.y = -i;
            // �I�_
            _lineEnd = _lineOrigin;
            _lineEnd.x = x;
            DrawLine(_lineOrigin, _lineEnd);
        }
    }


    /// <summary>
    /// �I�𒆂̏��߂������\��
    /// </summary>
    private void BarHighLight(int idx)
    {
        if (idx < 0)
            return;

        float left = DataManager.Instance.barList[idx].notesArray[0, 0].time;
        float right = DataManager.Instance.barList[idx].notesArray[0, DataManager.Instance.barList[idx].LPB - 1].time + DataManager.Instance.barList[idx].notesArray[0, DataManager.Instance.barList[idx].LPB - 1].length;
        float top = 0;
        float bottom = -DataManager.Instance.lane;

        //��`��4�ӂ�`��
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
    /// �N���b�N�������W���牽���ߖڂ�I�����������v�Z
    /// </summary>
    /// <param name="clickPos"></param>
    /// <returns>�I�𒆂̏��ߔԍ�(��I���Ȃ�-1)</returns>
    public int CheckHitBar(Vector3 clickPos)
    {
        // ��ӂ�y���W(�Œ�)
        float top = 0;
        // ��ӂ�y���W(�Œ�)
        float bottom = -DataManager.Instance.lane;
        // 1�O�̏��߂̍��v�̒���
        float lastTotalLength = 0;


        // 1���߂Â`�F�b�N
        foreach (BarData bar in DataManager.Instance.barList)
        {
            // ���̏��߂��܂߂�����܂ł̏��߂̍��v�̒���
            float totalLength = 0;
            // ���̏��߂̒���
            float length = 0;
            for (int i = 0; i < bar.LPB; i++)
                length += bar.notesArray[0, i].length;
            
            totalLength = lastTotalLength + length;

            //��`��2�_�̍��W�����߂�
            Vector3 leftTop = new Vector3(lastTotalLength + DataManager.Instance.offset, top);
            Vector3 rightBottom = new Vector3(totalLength + DataManager.Instance.offset, bottom);


            // �q�b�g�����Ȃ�
            if (IsHitSquareAndDot(clickPos, leftTop, rightBottom))
                return bar.barNum;

            // �q�b�g���Ȃ���Ύ��̏��߂𒲂ׂ�
            lastTotalLength = totalLength;
        }

        // �Y���Ȃ�
        return -1;
    }


    /// <summary>
    /// �w�肵�����߂̂ǂ̃}�X�Ƀq�b�g����������
    /// </summary>
    /// <param name="clickPos"></param>
    /// <param name="idx"></param>
    /// <param name="lane"></param>
    /// <param name="cell"></param>
    public void CheckHitCell(Vector3 clickPos, int idx, out int lane, out int cell)
    {
        // 1���[�����Ƃɒ��ׂ�
        for (int i = 0; i < DataManager.Instance.lane; i++)
        {
            // ��ӂ�y���W
            float top = i * -1;
            // ��ӂ�y���W
            float bottom = (i + 1) * -1;


            // 1���Â���
            for (int j = 0; j < DataManager.Instance.barList[idx].LPB; j++)
            {
                // ���ӂ�x���W(���̔������鎞��)
                float left = DataManager.Instance.barList[idx].notesArray[i, j].time;
                // �E�ӂ�x���W(���̔������鎞�� + ���̔��̒��� = ���̔��̏I���)
                float right = left + DataManager.Instance.barList[idx].notesArray[i, j].length;

                // 2�_�̍��W���Z�b�g
                Vector3 leftTop = new Vector3(left, top);
                Vector3 rightBottom = new Vector3(right, bottom);


                // �ڐG���Ă���Ȃ烌�[���ƍ��W��Ԃ�
                if (IsHitSquareAndDot(clickPos, leftTop, rightBottom))
                {
                    lane = i;
                    cell = j;
                    //Debug.Log(lane + "," + cell);

                    return;
                }
            }
        }

        // ����ɂ��q�b�g���Ȃ������ꍇ
        lane = -1;
        cell = -1;
    }


    /// <summary>
    /// �_�Ƌ�`�̓����蔻����Ƃ�
    /// </summary>
    /// <param name="clickPos"></param>
    /// <param name="leftTop"></param>
    /// <param name="rightBottom"></param>
    /// <returns></returns>
    private bool IsHitSquareAndDot(Vector3 clickPos, Vector3 leftTop, Vector3 rightBottom)
    {
        // �I�t�Z�b�g�A�g�嗦���l��
        leftTop *= DataManager.Instance.stretchRatio;
        leftTop += gridOffset;
        rightBottom *= DataManager.Instance.stretchRatio;
        rightBottom += gridOffset;

        // x���͈͓��łȂ����
        if ((clickPos.x < leftTop.x) || (rightBottom.x < clickPos.x))
            return false;
        // y���͈͓��łȂ����
        if ((leftTop.y < clickPos.y) || (clickPos.y < rightBottom.y))
            return false;

        return true;
    }
}
