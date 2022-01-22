//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//// ‹ŒŒ^Ž®
//public class Line : MonoBehaviour
//{
//    static Material lineMaterial;
//    static void CreateLineMaterial()
//    {
//        if (!lineMaterial)
//        {
//            Shader shader = Shader.Find("Hidden/Internal-Colored");
//            lineMaterial = new Material(shader);
//        }
//    }

//    public void OnRenderObject()
//    {
//        DrawLine(7, 16);
//    }

//    private void DrawLine(int block, int Split)
//    {
//        CreateLineMaterial();
//        lineMaterial.SetPass(0);

//        GL.PushMatrix();
//        GL.MultMatrix(transform.localToWorldMatrix);

//        GL.Begin(GL.LINES);
//        for (int i = 0; i < block; ++i)
//        {
//            GL.Vertex3(0, 0, 0);
//            GL.Vertex3(5, 0, 0);
//        }

//        for (int i = 0; i < block; ++i)
//        {
//            GL.Vertex3(0, 0, 0);
//            GL.Vertex3(5, 0, 0);
//        }
//        GL.End();
//        GL.PopMatrix();
//    }
//}