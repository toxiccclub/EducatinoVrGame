using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Instruction : MonoBehaviour
{
    public TextMeshProUGUI[] targetText;
    public TextMeshProUGUI InstrText; 
    private int currentIndex = 0;
    public GameObject InstrCanvas;
    public GameObject gridInstr;
    public GameObject gridInstrZero;
    public GameObject startButton;
    public GameObject video;
    public GameObject playButton;
    public GameObject pauseButton;

    public void Interact()
    {
        if (currentIndex >= 0 && currentIndex < 9)
        {
            currentIndex = currentIndex + 1;
            Debug.Log(currentIndex);

            DPAnimation dptable = InstrCanvas.GetComponent<DPAnimation>();
            dptable.DInteract();
            //InstrCanvas.SetActive(true);
            gridInstr.SetActive(false);
            gridInstrZero.SetActive(false);
            startButton.SetActive(false);
            playButton.SetActive(false);
            pauseButton.SetActive(false);
            video.SetActive(false);
            if (currentIndex == 6)
            {
                gridInstr.SetActive(true);
                gridInstrZero.SetActive(true);
                InstrText.text = targetText[currentIndex].text;
                dptable.Interact();
                //currentIndex = currentIndex + 1;
                return;
            }
            if(currentIndex == 4)
            {
                playButton.SetActive(true);
                pauseButton.SetActive(true);
                video.SetActive(true);
                InstrText.text = targetText[currentIndex].text;
                return;
            }
            if (currentIndex == 9)
            {
                startButton.SetActive(true);
                InstrText.text = "";
                //currentIndex = currentIndex+1;
                return;
            }
            InstrText.text = targetText[currentIndex].text;
            //currentIndex=currentIndex+1;
        }
    }

    public void DInteract()
    {
        if (currentIndex > 0 && currentIndex <= 9)
        {
            currentIndex = currentIndex - 1;
            Debug.Log(currentIndex);

            DPAnimation dptable = InstrCanvas.GetComponent<DPAnimation>();
            dptable.DInteract();
            //InstrCanvas.SetActive(true);
            gridInstr.SetActive(false);
            gridInstrZero.SetActive(false);
            startButton.SetActive(false);
            playButton.SetActive(false);
            pauseButton.SetActive(false);
            video.SetActive(false);
            if (currentIndex == 6)
            {
                gridInstr.SetActive(true);
                gridInstrZero.SetActive(true);
                InstrText.text = targetText[currentIndex].text;
                dptable.Interact();
                //currentIndex = currentIndex - 1;
                return;
            }
            if (currentIndex == 4) {
                playButton.SetActive(true);
                pauseButton.SetActive(true);
                video.SetActive(true);
                InstrText.text = targetText[currentIndex].text;
                return;
            }
            if (currentIndex == 9)
            {
                startButton.SetActive(true);
                InstrText.text = "";
                //currentIndex = currentIndex - 1;
                return;

            }
            InstrText.text = targetText[currentIndex].text;
            //currentIndex = currentIndex - 1;
        }
    }

    public void Instr()
    {
        InstrCanvas.SetActive(true);
        InstrText.text = targetText[0].text;

    }

    public void DInstr()
    {
        InstrCanvas.SetActive(false);
        currentIndex = 0;
        startButton.SetActive(false);
    }

}
