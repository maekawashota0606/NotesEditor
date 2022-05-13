using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshDrawer : SingletonMonoBehaviour<MeshDrawer>
{
    [SerializeField]
    private Material _meshMaterial;

    private void OnRenderObject()
    {
        _meshMaterial.SetPass(0);

        foreach (Bar bar in BarManager.Instance.barList)
        {
            for (int i = 0; i < DataManager.MAX_LANE; i++)
            {
                for (int j = 0; j < bar.LPB; j++)
                {
                    if (0 < bar.notesArray[i, j].notesType)
                    {
                        DrawNote(new Vector3(bar.notesArray[i, j].time, -i), 1, 1);
                    }
                }
            }
        }
    }


    public void DrawNote(Vector3 pos, float height, float width)
    {
        // 拡大率などを加味
        pos *= DataManager.Instance.stretchRatio;
        pos += GridManager.Instance.gridOffset;
        height *= DataManager.Instance.stretchRatio.y;
        width *= DataManager.Instance.stretchRatio.x / 50;


        // 左上
        Vector3 vertex0 = new Vector3(pos.x - width / 2, pos.y, pos.z);
        // 右上
        Vector3 vertex1 = new Vector3(pos.x + width / 2, pos.y , pos.z);
        // 右下
        Vector3 vertex2 = new Vector3(pos.x + width / 2, pos.y - height, pos.z);
        // 左下
        Vector3 vertex3 = new Vector3(pos.x - width / 2, pos.y - height, pos.z);
        GL.PushMatrix();

        GL.MultMatrix(transform.localToWorldMatrix);

        GL.Begin(GL.TRIANGLES);

        GL.Vertex(vertex0);
        GL.Vertex(vertex1);
        GL.Vertex(vertex2);

        GL.Vertex(vertex0);
        GL.Vertex(vertex2);
        GL.Vertex(vertex3);

        GL.End();

        GL.PopMatrix();
    }
}