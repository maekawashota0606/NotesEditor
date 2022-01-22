using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReceptor : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            // z�𐳂����w�肵����(�Ȃ������])
            mousePos.z = EditCameraController.Instance.gameObject.transform.position.z * -1;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            // 
            int lastBarNum = DataManager.Instance.choosingBarNum;
            DataManager.Instance.choosingBarNum = GridManager.Instance.CheckHitBar(mousePos);
            // ��I���Ȃ�O��̔ԍ���
            if (-1 < DataManager.Instance.choosingBarNum)
            {
                // ���łɑI�𒆂̏��߂Ȃ�
                if (DataManager.Instance.choosingBarNum == lastBarNum)
                {
                    int lane = -1;
                    int cell = -1;
                    GridManager.Instance.CheckHitCell(mousePos, DataManager.Instance.choosingBarNum, out lane, out cell);

                    // ���b�V����ǉ�
                    DataManager.Instance.AddNotes(lane, cell);
                }
                else
                {
                    // �I�����Ă��鏬�߂̃f�[�^��UI�ɕ\��
                    DataManager.Instance.OnChangedChoosingBar();
                }
            }
            else
                DataManager.Instance.choosingBarNum = lastBarNum;
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
