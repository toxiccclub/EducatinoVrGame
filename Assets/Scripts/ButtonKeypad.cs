using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonKeypad : MonoBehaviour
{
    public List<Button> Keypad = new List<Button>();
    public DPTableSelf tabelSelf;

    private void Start()
    {
       for(int i = 0; i < Keypad.Count; i++)
        {
            int ButtonDef = i + 1;
            Keypad[i].onClick.AddListener(() => FillSelectedInput(ButtonDef));
        }
    }

    public void FillSelectedInput(int amount)
    {
        if (tabelSelf.SelectedInputField == null) return;
        tabelSelf.SelectedInputField.text = amount.ToString();
    }
}
