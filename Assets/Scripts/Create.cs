using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Create : MonoBehaviour
{
    public TMP_Dropdown droplist;                                   
    public Transform pos;

    private GameObject currenttargets = null;


    public void CreateObj(GameObject prefab)
    {
        if (currenttargets != null && droplist == null)
            Destroy(currenttargets);

        GameObject currentBall = Instantiate(prefab, pos.position, pos.rotation);
        currenttargets = currentBall;

        if (droplist == null)
            return;

        switch (droplist.value)
        {
            case 0:
                {
                    currentBall.GetComponent<Renderer>().material.color = Color.white;
                }
                break;
            case 1:
                {
                    currentBall.GetComponent<Renderer>().material.color = Color.blue;
                }
                break;
            case 2:
                {
                    currentBall.GetComponent<Renderer>().material.color = Color.green;
                }
                break; 

        }

    }
}
