using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCYnteractable : MonoBehaviour
{
    public AudioSource WizardSound;
    public AudioClip[] WizardClip;
    public TextMeshProUGUI[] targetText;
    public GameObject Dialog1;
    public GameObject InstructionCanvas;
    public GameObject Tables;
    public TextMeshProUGUI WizPhrase;
    public TextMeshProUGUI Tip1;
    public GameObject canvas1_0;
    //public TextMeshProUGUI Tip3;
    //private string[] messageText = { "Привет, дорогой маг!", "Тебе предстоит нелегкая задача!", "Ты должен зажечь магический кристалл!", "Вот подробная инструкция!" };
    private int currentIndex = 0;
    //private int minCoins;

    public GameObject dropZone;

    public void Interact()
    {
        Dialog1.SetActive(true);
        Debug.Log(currentIndex);

        if (InstructionCanvas.TryGetComponent(out Instruction iNstruction))
        {
            if (currentIndex == 3)
            {
                WizardSound.Stop();
                WizardSound.clip = WizardClip[currentIndex];
                WizardSound.Play();
                WizPhrase.text = targetText[currentIndex].text;
                iNstruction.Instr();
                return;
            }
        }

        if (InstructionCanvas.TryGetComponent(out DPTableSelf dpSelf)){
            if(currentIndex == 1)
            {
                InstructionCanvas.SetActive(true);
                return;
            }
        }

        if(InstructionCanvas.TryGetComponent(out DPanimation3 dpaNimation))
        {
           /* //Debug.Log("Succesfull");
            if (currentIndex == 1) 
            {
                InstructionCanvas.SetActive(true);
                WizPhrase.text = targetText[currentIndex].text;
                CoinCollector2 minCoins = dropZone.GetComponent<CoinCollector2>();
                *//*Debug.Log(minCoins.CountCoins());
                Debug.Log("Минимальное количество: " + minCoins.CountCoins());*//*
                Tip1.text = "Минимальное количество: " + minCoins.CountCoins();
                currentIndex += 1;
                return;
            }*/
            //InstructionCanvas.SetActive(true);
            if ((currentIndex%2) == 0)//need to be 3
            {
                WizPhrase.text = targetText[currentIndex%2].text;
                WizardSound.Stop();
                WizardSound.clip = WizardClip[currentIndex%2];
                WizardSound.Play();
                canvas1_0.SetActive(false);
                InstructionCanvas.SetActive(true);
                Tables.SetActive(true);
                Tip1.text = "";
                dpaNimation.showSteps = true;
                //WizPhrase.text = targetText[currentIndex].text;
                CoinCollector2 tableActive = dropZone.GetComponent<CoinCollector2>();
                tableActive.Table();
                currentIndex += 1;
                return;
            }
            if((currentIndex % 2) != 0)
            {
                canvas1_0.SetActive(true);
                InstructionCanvas.SetActive(false);
                currentIndex += 1;
                return;
            }

           /* if (currentIndex == 5)
            {
                WizPhrase.text = targetText[currentIndex].text;
                dpaNimation.showResultPlan = true;
                dpaNimation.GetFirstCoin();
                currentIndex += 1;
                return;
            }*/
        }
        WizardSound.Stop();
        WizardSound.clip = WizardClip[currentIndex];
        WizardSound.Play();
        WizPhrase.text = targetText[currentIndex].text;
        currentIndex += 1;
    }

   public void DInteract()
    {
        Dialog1.SetActive(false);
        currentIndex = 0;
    }

}
