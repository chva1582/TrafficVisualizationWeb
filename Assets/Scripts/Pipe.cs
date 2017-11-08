using System.Collections.Generic;
using UnityEngine;

public enum Transport { Backpack, Walking, Bicycle, Skateboard, Car };

public class Pipe : BezierSpline
{
    public float radius;
    public int sections;
    Transport trans;

    List<Vector3> verts = new List<Vector3>();
    List<int> tris = new List<int>();
    List<Vector2> uvs = new List<Vector2>();

    Mesh mesh;

    static Vector3[] circle = new Vector3[8]
    {
        new Vector3(0,0,1),
        new Vector3(0.707f,0,0.707f),
        new Vector3(1,0,0),
        new Vector3(0.707f,0,-0.707f),
        new Vector3(0,0,-1),
        new Vector3(-0.707f,0,-0.707f),
        new Vector3(-1,0,0),
        new Vector3(-0.707f,0,0.707f)
    };

    public Transport Trans
    {
        set
        {
            if (value == Transport.Walking)
                GetComponent<MeshRenderer>().material =  Resources.Load("StrandMatRed") as Material;
            else if (value == Transport.Backpack)
                GetComponent<MeshRenderer>().material = Resources.Load("StrandMatBlue") as Material;
            else if (value == Transport.Bicycle)
                GetComponent<MeshRenderer>().material = Resources.Load("StrandMatGreen") as Material;
            else
                GetComponent<MeshRenderer>().material = Resources.Load("StrandMatBlack") as Material;
            trans = value;
        }
    }

    Vector3[,] pipePoints;

    void Start()
    {
        pipePoints = new Vector3[8, sections + 1];

        AddFirstCircle(GetPoint(0f));
        for (int i = 1; i <= sections; i++)
        {
            AddCircle(i, GetPoint((1f/sections) * i));
        }
        AddCap(GetPoint(1f));
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.SetVertices(verts);
        mesh.SetTriangles(tris, 0);
        mesh.SetUVs(1, uvs);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void AddFirstCircle(Vector3 center)
    {
        for (int i = 0; i < 8; i++)
        {
            Vector3 point = transform.InverseTransformPoint(center + radius * circle[i]);
            pipePoints[i, 0] = point;
            verts.Add(point);
            uvs.Add(new Vector2(i / 7f, 0));
        }
    }

    void AddCircle(int heightIndex, Vector3 center)
    {
        Vector3 point = transform.InverseTransformPoint(center + radius * circle[0]);
        pipePoints[0, heightIndex] = point;
        verts.Add(point);
        uvs.Add(new Vector2(0, heightIndex/sections));
        for (int i = 1; i < 8; i++)
        {
            point = transform.InverseTransformPoint(center + radius * circle[i]);
            pipePoints[i, heightIndex] = point;
            verts.Add(point);
            uvs.Add(new Vector2(i / 7f, heightIndex/sections));

            tris.Add(i + 8 * heightIndex);
            tris.Add((i - 1) + 8 * heightIndex);
            tris.Add((i - 1) + 8 * (heightIndex - 1));

            tris.Add(i + 8 * heightIndex);
            tris.Add((i - 1) + 8 * (heightIndex - 1));
            tris.Add(i + 8 * (heightIndex - 1));
        }
        tris.Add(8 * heightIndex);
        tris.Add(7 + 8 * heightIndex);
        tris.Add(7 + 8 * (heightIndex - 1));

        tris.Add(8 * heightIndex);
        tris.Add(7 + 8 * (heightIndex - 1));
        tris.Add(8 * (heightIndex - 1));
    }
    
    void AddCap(Vector3 center)
    {
        verts.Add(center);
        uvs.Add(new Vector2(0, 1));

        for (int i = 0; i < 8; i++)
        {
            tris.Add(8 * (sections + 1));
            tris.Add(8 * sections + i);
            tris.Add(8 * sections + ((i + 1) == 8 ? 0 : (i + 1)));
        }
    }
}
