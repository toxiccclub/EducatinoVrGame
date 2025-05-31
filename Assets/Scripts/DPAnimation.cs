using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class DPAnimation : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform dpTableParent;
    public Transform dpTableParentZero;
    public TextMeshProUGUI infoText;
    private int[] denominations = { 1, 5, 10 };
    private int target = 9;
    public Button NextButton;
    public TextMeshProUGUI InstructionText;
    public TextMeshProUGUI targetText;


    private List<TextMeshProUGUI> dpCells = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> dpCellsZero = new List<TextMeshProUGUI>();
    private Color orange = new Color(1f, 0.5f, 0f);     
    private Color purple = new Color(0.5f, 0f, 1f);   
    private List<Color> colors;
    private int[] dp;
    public Color defaultColor = Color.black;    
    public Color highlightColor = Color.green;
    public Color processingColor = Color.yellow;
    private IEnumerator stepCoroutine;

    private Coroutine currentCoroutine;

    public void Interact()
    {
        colors = new List<Color> { orange, Color.blue, purple };
        targetText.text = $"Нужно собрать: {target} Мана: " +
    $"<color=#{ColorUtility.ToHtmlStringRGB(colors[0])}>{1}</color>, " +
    $"<color=#{ColorUtility.ToHtmlStringRGB(colors[1])}>{5}</color>, " +
    $"<color=#{ColorUtility.ToHtmlStringRGB(colors[2])}>{10}</color>";
        InstructionText.text = "";
        NextButton.gameObject.SetActive(true);
        CreateTable();
        stepCoroutine = FillDPTable();
        NextButton.onClick.RemoveAllListeners(); // чтобы не дублировались
        NextButton.onClick.AddListener(NextStep);
    }

    void NextStep()
    {
        if (stepCoroutine != null)
        {
            bool hasNext = stepCoroutine.MoveNext();
            if (!hasNext)
            {
                stepCoroutine = null;
                Debug.Log("Пояснение завершено.");
            }
        }
    }

    private void CreateTable()
    {
        foreach (Transform child in dpTableParent)
        {
            Destroy(child.gameObject);
        }
        dpCells.Clear();

        foreach (Transform child in dpTableParentZero)
        {
            Destroy(child.gameObject);
        }
        dpCellsZero.Clear();


        for (int i = 0; i <= target; i++)
        {
            GameObject cellZero = Instantiate(cellPrefab, dpTableParentZero);
            GameObject cell = Instantiate(cellPrefab, dpTableParent);
            cell.GetComponent<CellIndex>().Index = i;
            TextMeshProUGUI cellText = cell.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cellTextZero = cellZero.GetComponent<TextMeshProUGUI>();
            cellTextZero.text = i.ToString();
            cellText.text = "-";
            dpCells.Add(cellText);
            dpCellsZero.Add(cellTextZero);

        }
    }

    private IEnumerator FillDPTable()
    {
        List<(int value, int coin)> variants = new List<(int, int)>();
        dp = new int[target + 1];
        for (int i = 1; i <= target; i++)
        {
            dp[i] = int.MaxValue;
        }
        dp[0] = 0;

        infoText.text = "Инициализация: dp[0] = 0";
        dpCells[0].text = "0";
        yield return null;

        for (int j = 1; j <= target; j++)
        {
            infoText.text = $"Обрабатываем cумму: {j}";
            yield return null;

            foreach (int coin in denominations)
            {
                if (j >= coin && dp[j - coin] != int.MaxValue)
                {
                    int newValue = dp[j - coin] + 1;
                    variants.Add((newValue, coin));

                    dpCells[j].color = processingColor;
                    yield return null;
                }
            }

            if (variants.Count > 0) {
                string explanation = $"dp[{j}] = min(";
                List<string> options = new List<string>();
                foreach (var (val,coin) in variants)
                {
                    int prev = j - coin;
                    if (prev >= 0 && dp[prev] != int.MaxValue)
                    {
                        string colorHex;
                        //int val = dp[prev] + 1;
                        switch (coin)
                        {
                            case 1:
                                colorHex = ColorUtility.ToHtmlStringRGB(colors[0]);
                                options.Add($"<color=#{colorHex}>dp[{prev}] + 1 = {dp[prev]} + 1 = {val}</color>");
                                break;
                            case 5:
                                colorHex = ColorUtility.ToHtmlStringRGB(colors[1]);
                                options.Add($"<color=#{colorHex}>dp[{prev}] + 1 = {dp[prev]} + 1 = {val}</color>");
                                break;
                            case 10:
                                colorHex = ColorUtility.ToHtmlStringRGB(colors[2]);
                                options.Add($"<color=#{colorHex}>dp[{prev}] + 1 = {dp[prev]} + 1 = {val}</color>");
                                break;
                        }
                    }
                }

                explanation += string.Join(", ", options) + ")";
                int minValue = variants.Min(c => c.value);
                if (minValue < dp[j])
                {
                    dp[j] = minValue;
                    dpCells[j].text = dp[j].ToString();
                    dpCells[j].color = highlightColor;
                    explanation += $" = {dp[j]}";
                    infoText.text = explanation;
                    yield return null;
                }
                else
                {
                    dpCells[j].color = defaultColor;
                    infoText.text = explanation + $" ? остаётся {dp[j]}";
                    yield return null;
                }

                variants.Clear();
            }
        }

        infoText.text = "Алгоритм завершён!";
        yield return null;
    }

    public void DInteract()
    {
        NextButton.gameObject.SetActive(false);
        targetText.text = "";
        foreach (Transform child in dpTableParent)
        {
            Destroy(child.gameObject);
        }
        dpCells.Clear();

        dp = new int[target + 1];

        infoText.text = "";

        if (stepCoroutine != null)
        {
            stepCoroutine = null;
        }

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }
    }

}
