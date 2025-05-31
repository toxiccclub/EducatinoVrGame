using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    public GameObject door;
    [SerializeField] public GameObject InstrCanvas;
    public bool doorOpen;

    public void OpenTheDoor()
    {
        if (InstrCanvas != null)
        {
            if (InstrCanvas.TryGetComponent(out Instruction iNstruction))
            {
                iNstruction.DInstr();
            }
        }
        if (doorOpen == false)
        {
            doorOpen = !doorOpen;
            door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
        }
    }
    
}
