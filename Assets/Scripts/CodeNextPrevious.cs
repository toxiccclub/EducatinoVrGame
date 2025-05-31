using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CodeNextPrevious : MonoBehaviour
{
    public List<TextMeshProUGUI> Langugage = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> Code = new List<TextMeshProUGUI>();
    public TextMeshProUGUI NameLang;
    public TextMeshProUGUI NameCode;
    private int currentIndex = 1;

    void Start()
    {
        NameLang.text = Langugage[0].text;
        NameCode.text = Code[0].text;
    }
    
    public void Interact()
    {
        if (currentIndex < 2) currentIndex = currentIndex + 1;
        if (currentIndex >= 0 && currentIndex <= 2)
        {
            NameLang.text = Langugage[currentIndex].text;
            NameCode.text = Code[currentIndex].text;
        }
    }

    public void DInteract()
    {
        if(currentIndex > 0) currentIndex = currentIndex - 1;
        if (currentIndex >= 0 && currentIndex <= 2)
        {
            NameLang.text = Langugage[currentIndex].text;
            NameCode.text = Code[currentIndex].text;
        }
    }

}
