using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerINstruction : MonoBehaviour
{
    public GameObject[] manageElems;
    private int currentIndex = 0;

    public void Interact()
    {
        if (manageElems == null || manageElems.Length == 0) return;

        if (currentIndex < manageElems.Length - 1)
        {
            manageElems[currentIndex].SetActive(false);
            currentIndex++;
            manageElems[currentIndex].SetActive(true);
        }
    }

    public void InteractBack()
    {
        if (manageElems == null || manageElems.Length == 0) return;

        if (currentIndex > 0)
        {
            manageElems[currentIndex].SetActive(false);
            currentIndex--;
            manageElems[currentIndex].SetActive(true);
        }
    }
}
