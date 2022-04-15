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

        //foreach (Bar bar in DataManager.Instance.barList)
        //{
        //    for (int i = 0; i < DataManager.MAX_LANE; i++)
        //        for (int j = 0; j < DataManager.MAX_LPB; j++)
        //        {
        //            if (0 < bar.notesArray[i, j].notesType)
        //                DrawNote(bar.notesArray[i, j].pos, 1, 0.2f);
        //        }
        //}
    }

    public void DrawNote(Vector3 pos, float height, float width)
    {
        // ¶ã
        Vector3 vertex0 = new Vector3(pos.x - width / 2, pos.y + height / 2, pos.z);
        // ‰Eã
        Vector3 vertex1 = new Vector3(pos.x + width / 2, pos.y + height / 2, pos.z);
        // ‰E‰º
        Vector3 vertex2 = new Vector3(pos.x + width / 2, pos.y - height / 2, pos.z);
        // ¶‰º
        Vector3 vertex3 = new Vector3(pos.x - width / 2, pos.y - height / 2, pos.z);
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