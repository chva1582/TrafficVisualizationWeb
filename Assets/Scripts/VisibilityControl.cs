using System.Collections.Generic;
using UnityEngine;

public class VisibilityControl : MonoBehaviour
{
    public StrandNet net;
    public int test;

	public void BlackVisibility(bool show)
    {
        List<int> strandIndexList = net.GetTransportIndexes(Transport.Car);
        foreach (int strandIndex in strandIndexList)
            net.transform.GetChild(strandIndex).gameObject.SetActive(show);
    }

    public void GreenVisibility(bool show)
    {
        List<int> strandIndexList = net.GetTransportIndexes(Transport.Bicycle);
        foreach (int strandIndex in strandIndexList)
            net.transform.GetChild(strandIndex).gameObject.SetActive(show);
    }

    public void BlueVisibility(bool show)
    {
        List<int> strandIndexList = net.GetTransportIndexes(Transport.Backpack);
        foreach (int strandIndex in strandIndexList)
            net.transform.GetChild(strandIndex).gameObject.SetActive(show);
    }

    public void RedVisibility(bool show)
    {
        List<int> strandIndexList = net.GetTransportIndexes(Transport.Walking);
        foreach (int strandIndex in strandIndexList)
            net.transform.GetChild(strandIndex).gameObject.SetActive(show);
    }
}
