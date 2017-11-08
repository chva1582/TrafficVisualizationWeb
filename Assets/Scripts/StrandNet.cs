using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrandNet : MonoBehaviour
{
    List<Strand> strands = new List<Strand>();

    List<Vector3[]> strandPoints = new List<Vector3[]>
    {
        new Vector3[3]{new Vector3(0,0,0), new Vector3(3,5,4), new Vector3(10,10,7)},
        new Vector3[2]{new Vector3(3,0,5), new Vector3(10,10,7)},
        new Vector3[2]{new Vector3(3,5,5), new Vector3(10,7,10)},
        new Vector3[3]{new Vector3(0,0,0), new Vector3(5,3,7), new Vector3(10,6,7)},
        new Vector3[2]{new Vector3(3,0,5), new Vector3(3,10,5)},
        new Vector3[2]{new Vector3(4,0,5), new Vector3(4,10,5)},
        new Vector3[3]{new Vector3(1,0,1), new Vector3(7,5,8), new Vector3(0,10,3)},
        new Vector3[2]{new Vector3(2,0,6), new Vector3(9,10,8)},
        new Vector3[2]{new Vector3(0,4,6), new Vector3(10,6,10)},
        new Vector3[2]{new Vector3(0,6,8), new Vector3(10,7,8)}
    };

    List<Transport> strandTransports = new List<Transport>
    {
        Transport.Walking,
        Transport.Backpack,
        Transport.Bicycle,
        Transport.Walking,
        Transport.Backpack,
        Transport.Backpack,
        Transport.Walking,
        Transport.Backpack,
        Transport.Bicycle,
        Transport.Car
    };

    public GameObject strandPrefab;

    public List<int> GetTransportIndexes(Transport transport)
    {
        List<int> transports = new List<int>();
        for (int i = 0; i < strandTransports.Count; i++)
        {
            if (strandTransports[i] == transport)
                transports.Add(i);
        }
        return transports;
    }

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < strandPoints.Count; i++)
        {
            Strand strandInstance = Instantiate(strandPrefab, transform).GetComponent<Strand>();
            strandInstance.linePoints = strandPoints[i];
            strandInstance.transport = strandTransports[i];
            strandInstance.ID = i;
        }
	}
}
