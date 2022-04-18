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

        foreach (BarData bar in DataManager.Instance.barList)
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
        // Šg‘å—¦‚È‚Ç‚ð‰Á–¡
        pos *= DataManager.Instance.stretchRatio;
        pos += GridManager.Instance.gridOffset;
        height *= DataManager.Instance.stretchRatio.y;
        width *= DataManager.Instance.stretchRatio.x / 50;


        // ¶ã
        Vector3 vertex0 = new Vector3(pos.x - width / 2, pos.y, pos.z);
        // ‰Eã
        Vector3 vertex1 = new Vector3(pos.x + width / 2, pos.y , pos.z);
        // ‰E‰º
        Vector3 vertex2 = new Vector3(pos.x + width / 2, pos.y - height, pos.z);
        // ¶‰º
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