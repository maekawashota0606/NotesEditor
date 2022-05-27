using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputReceptor : MonoBehaviour
{
    private void Update()
    {
        // TODO:�K�v�ȃ^�C�~���O�ł̂ݎ��s
        NotesManager.Instance.CalNotes();

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Vector3 mousePos = Input.mousePosition;
            // z�𐳂����w�肵����(�Ȃ������])
            mousePos.z = EditCameraController.Instance.gameObject.transform.position.z * -1;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            // 
            int lastBarNum = DataManager.Instance.GetChoosingBarNum();
            DataManager.Instance.SetChoosingBarNum(GridManager.Instance.CheckHitBar(mousePos));

            // ��I���Ȃ珈�����Ȃ�
            if (DataManager.Instance.GetChoosingBarNum() < 0)
                return;
            // ���łɑI�𒆂̏��߂Ȃ�
            else if (DataManager.Instance.GetChoosingBarNum() == lastBarNum)
            {
                int lane, cell;
                GridManager.Instance.CheckHitCell(mousePos, DataManager.Instance.GetChoosingBarNum(), out lane, out cell);

                // �Y�����Ȃ���Ώ������Ȃ�
                if (lane < 0 || cell < 0)
                    return;

                // �N���b�N�����ꏊ�Ƀm�[�c��ǉ�
                BarManager.Instance.AddNotes(lane, cell, DataManager.Instance.GetEditMode());
            }
        }
    }
}
