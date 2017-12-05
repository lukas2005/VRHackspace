using System.Collections;
using UnityEngine;

[System.Serializable]
public class TwoDArray
{
    public Object[] Array;

    public IEnumerator GetEnumerator()
    {
        return Array.GetEnumerator();
    }

}