using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputReceptor : MonoBehaviour
{
    private void Update()
    {
        // 必要なタイミングで
        NotesManager.Instance.CalNotes();

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Vector3 mousePos = Input.mousePosition;
            // zを正しく指定し直す(なぜか反転)
            mousePos.z = EditCameraController.Instance.gameObject.transform.position.z * -1;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            // 
            int lastBarNum = DataManager.Instance.GetChoosingBarNum();
            DataManager.Instance.SetChoosingBarNum(GridManager.Instance.CheckHitBar(mousePos));

            // 非選択なら処理しない
            if (DataManager.Instance.GetChoosingBarNum() < 0)
                return;
            // すでに選択中の小節なら
            else if (DataManager.Instance.GetChoosingBarNum() == lastBarNum)
            {
                int lane, cell;
                GridManager.Instance.CheckHitCell(mousePos, DataManager.Instance.GetChoosingBarNum(), out lane, out cell);

                // 該当しなければ処理しない
                if (lane < 0 || cell < 0)
                    return;

                // クリックした場所にノーツを追加
                BarManager.Instance.AddNotes(lane, cell, DataManager.Instance.GetEditMode());
            }
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
