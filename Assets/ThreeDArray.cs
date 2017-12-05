using System.Collections;

[System.Serializable]
public class ThreeDArray {
    public TwoDArray[] Array;

    public IEnumerator GetEnumerator() {
        return Array.GetEnumerator();
    }

}
