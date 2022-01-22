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
            // zを正しく指定し直す(なぜか反転)
            mousePos.z = EditCameraController.Instance.gameObject.transform.position.z * -1;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            // 
            int lastBarNum = DataManager.Instance.choosingBarNum;
            DataManager.Instance.choosingBarNum = GridManager.Instance.CheckHitBar(mousePos);
            // 非選択なら前回の番号へ
            if (-1 < DataManager.Instance.choosingBarNum)
            {
                // すでに選択中の小節なら
                if (DataManager.Instance.choosingBarNum == lastBarNum)
                {
                    int lane = -1;
                    int cell = -1;
                    GridManager.Instance.CheckHitCell(mousePos, DataManager.Instance.choosingBarNum, out lane, out cell);

                    // メッシュを追加
                    DataManager.Instance.AddNotes(lane, cell);
                }
                else
                {
                    // 選択している小節のデータをUIに表示
                    DataManager.Instance.OnChangedChoosingBar();
                }
            }
            else
                DataManager.Instance.choosingBarNum = lastBarNum;
        }


        // カメラ操作
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
