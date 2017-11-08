using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeConstructor : MonoBehaviour
{
    public GameObject pipePrefab;
	// Use this for initialization
	void Start ()
    {
        List<PipeData> data = GetPositionsFromFile();
        for (int i = 0; i < data.Count; i++)
        {
            Pipe pipe = Instantiate(pipePrefab).GetComponent<Pipe>();
            pipe.transform.parent = transform;
            pipe.Trans = data[i].trans;
            ConstructPipeFromList(data[i].points, pipe);
        }
	}

    List<PipeData> GetPositionsFromFile()
    {
        List<PipeData> o = new List<PipeData>();
        string[] dataStrings = (Resources.Load("Traffic Data") as TextAsset).text.Split('\n');
        for (int i = 2; i < dataStrings.Length; i++)
        {
            PipeData pipeData = new PipeData(Transport.Walking);
            string[] firstSplit = dataStrings[i].Split(':');

            if (firstSplit[0] == "B")
                pipeData.trans = Transport.Backpack;

            else if (firstSplit[0] == "W")
                pipeData.trans = Transport.Walking;

            else if (firstSplit[0] == "K")
                pipeData.trans = Transport.Bicycle;

            else if (firstSplit[0] == "S")
                pipeData.trans = Transport.Skateboard;

            else
                pipeData.trans = Transport.Car;

            string[] positionStrings = firstSplit[1].Split(';');
            for (int j = 0; j < positionStrings.Length; j++)
            {
                string[] dimStrings = positionStrings[j].Split(new char[2] { ' ', ',' });
                pipeData.points.Add(new Vector3(float.Parse(dimStrings[1]), float.Parse(dimStrings[0]), float.Parse(dimStrings[2])));
            }
            o.Add(pipeData);
        }
        return o;
    }

    void ConstructPipeFromList(List<Vector3> positions, Pipe pipe)
    {
        for (int i = 2; i < positions.Count; i++)
        {
            pipe.AddCurve();
        }
        for (int i = 0; i < positions.Count; i++)
        {
            pipe.SetControlPoint(3 * i, positions[i]);
            if (i != 0)
                pipe.SetControlPoint(3 * i - 1, positions[i] - new Vector3(0, 10, 0));
            if(i != positions.Count - 1)
                pipe.SetControlPoint(3 * i + 1, positions[i] + new Vector3(0, 10, 0));
        }
    }
}

public struct PipeData
{
    public List<Vector3> points;
    public Transport trans;

    //Going to be used as empty either way just giving it parameter so it stops bitching
    public PipeData(Transport inTrans)
    {
        points = new List<Vector3>();
        trans = Transport.Walking;
    }
}
