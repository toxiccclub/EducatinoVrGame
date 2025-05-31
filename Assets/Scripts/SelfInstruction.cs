using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelfInstruction : MonoBehaviour
{
   /* public TextMeshProUGUI[] targetText;
    public TextMeshProUGUI InstrText;
    private int currentIndex = 0;
    public GameObject InstrCanvas;
    public DPTableSelf dptable;
    public StartCoinExplanation coinExpl;

    public void Interact()
    {
        if (currentIndex >= 0 && currentIndex < 7)
        {
            currentIndex = currentIndex + 1;
            //Debug.Log(currentIndex);

            //InstrCanvas.GetComponent<DPAnimation>().DInteract();
            InstrCanvas.SetActive(true);
            //gridInstr.SetActive(false);
            //startButton.SetActive(false);
            //playButton.SetActive(false);
            //video.SetActive(false);
            dptable.phase1Panel.SetActive(false);
            if(currentIndex == 1) 
            {
                InstrText.text = "";
                dptable.phase1Panel.SetActive(true);
                dptable.ButtonPanel.SetActive(true);
                return;
            }

            if(currentIndex == 5)
            {
                InstrText.text = "";
                coinExpl.ActiveExplanation();
                //dptable.StartReconstructionPhase();
                return;
            }
            if(currentIndex == 6)
            {
                dptable.Pase2Panel.SetActive(false);
            }
            if(currentIndex == 7)
            {
                InstrText.text = "";
                //dptable.Phase2();
                return;
            }
            *//*if(currentIndex == 6)
            {
                dptable.StartReconstructionPhase();
            }*//*

            InstrText.text = targetText[currentIndex].text;
            //currentIndex=currentIndex+1;
        }
    }

    *//*public void DInteract()
    {
        if (currentIndex > 0 && currentIndex <= 7)
        {
            currentIndex = currentIndex - 1;
            Debug.Log(currentIndex);

            InstrCanvas.GetComponent<DPAnimation>().DInteract();
            //InstrCanvas.SetActive(true);
            startButton.SetActive(false);
            gridInstr.SetActive(false);
            playButton.SetActive(false);
            video.SetActive(false);
            if (currentIndex == 6)
            {
                gridInstr.SetActive(true);
                InstrText.text = targetText[currentIndex].text;
                InstrCanvas.GetComponent<DPAnimation>().Interact();
                //currentIndex = currentIndex - 1;
                return;
            }
            if (currentIndex == 5)
            {
                playButton.SetActive(true);
                video.SetActive(true);
                InstrText.text = targetText[currentIndex].text;
                return;
            }
            if (currentIndex == 7)
            {
                startButton.SetActive(true);
                InstrText.text = "";
                //currentIndex = currentIndex - 1;
                return;

            }
            InstrText.text = targetText[currentIndex].text;
            //currentIndex = currentIndex - 1;
        }
    }*//*

    public void Instr()
    {
        InstrCanvas.SetActive(true);
        InstrText.text = targetText[0].text;

    }

    *//*public void DInstr()
    {
        InstrCanvas.SetActive(false);
        currentIndex = 0;
        startButton.SetActive(false);
    }*/

}
