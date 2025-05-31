using UnityEngine;

public class TimeButton : MonoBehaviour
{
    public TimeRewind[] objectsToRewind;

    public void OnButtonPress()
    {
        foreach (var obj in objectsToRewind)
        {
            obj.StartRewind();           
        }
    }
}