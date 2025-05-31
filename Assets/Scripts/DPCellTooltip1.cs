using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class DPCellTooltip1 : MonoBehaviour
{
    public GameObject canv;
    public int cellIndex;
    private XRGrabInteractable interactable;
    public DPanimation3 tableScript;

    void OnEnable()
    {
        Debug.Log("DPCellTooltip1 активен на: " + gameObject.name);
    }

    private void Start()
    {
        //tableScript = canv.GetComponent<DPanimation3>();
        cellIndex = GetComponent<CellIndex>().Index;
        tableScript = canv.GetComponent<DPanimation3>();
        //Debug.Log(cellIndex);
    }

    public void OnHoverEnter()
    {
        tableScript.ShowTooltip(cellIndex);
    }

    public void OnHoverExit()
    {
        tableScript.HideTooltip();
    }
}
