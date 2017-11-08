using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Strand : MonoBehaviour
{
    public int ID;
    public Transport transport;
    public Vector3[] linePoints;

    LineRenderer line;

    public Material LineMaterial
    {
        get
        {
            if (transport == Transport.Walking)
                return Resources.Load("StrandMatRed") as Material;
            else if (transport == Transport.Backpack)
                return Resources.Load("StrandMatBlue") as Material;
            else if (transport == Transport.Bicycle)
                return Resources.Load("StrandMatGreen") as Material;
            else
                return Resources.Load("StrandMatBlack") as Material;
        }
    }

    public float Width
    {
        get
        {
            if (transport == Transport.Walking)
                return .1f;
            else if (transport == Transport.Backpack)
                return .1f;
            else if (transport == Transport.Bicycle)
                return .15f;
            else
                return .3f;
        }
    }

    // Use this for initialization
    void Start ()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = linePoints.Length;
        line.SetPositions(linePoints);
        line.material = LineMaterial;
        line.widthCurve = AnimationCurve.Linear(0, Width, 10, Width);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}