using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputReceptor : MonoBehaviour
{
    private void Update()
    {
        // �K�v�ȃ^�C�~���O��
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


        // �J��������
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            move.z = Input.GetAxis("Mouse ScrollWheel");
        else if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            move.y = Input.GetAxis("Mouse ScrollWheel");
        else
            move.x = Input.GetAxis("Mouse ScrollWheel");

        EditCameraController.Instance.Move(move);
    }
}
