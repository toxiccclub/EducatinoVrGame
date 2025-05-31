using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DPanimation3 : MonoBehaviour
{
    public GameObject cellPrefab;
    public Transform dpTableParent;
    public Transform dpTableParentZero;
    public TextMeshProUGUI infoText;

    private int[] denominations = { 1, 3, 4 };
    private int targetAmount = 13;
    private int currentAmount = 0;

    private List<TextMeshProUGUI> dpCells = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> dpCellsZero = new List<TextMeshProUGUI>();
    private int[] firstChoice;

    public GameObject tooltipPanel;
    public TextMeshProUGUI tooltipText;
    private int[] dp;

    private Color blue = new Color(0f, 1f, 1f);    // RGB для оранжевого
    private Color purple = new Color(0.5f, 0f, 1f);     // RGB для фиолетового
    private List<Color> colors;
    public Color defaultColor = Color.black;
    public Color highlightColor = Color.green;
    public Color processingColor = Color.yellow;

    public bool showSteps = false;
    public bool showResultPlan = false;

    public void UpdateTable(int newAmount)
    {
        colors = new List<Color> { blue, Color.red, purple };
        currentAmount = newAmount;
        targetAmount = currentAmount;
        StopAllCoroutines();
        CreateTable();
        StartCoroutine(FillDPTable());
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

        for (int i = 0; i <= targetAmount; i++)
        {
            GameObject cellZero = Instantiate(cellPrefab, dpTableParentZero);
            GameObject cell = Instantiate(cellPrefab, dpTableParent);
            cell.GetComponent<CellIndex>().Index = i;
            cell.GetComponent<DPCellTooltip1>().canv = gameObject;
            TextMeshProUGUI cellText = cell.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cellTextZero = cellZero.GetComponent<TextMeshProUGUI>();
            cellText.text = "-";
            cellTextZero.text = i.ToString();
            dpCells.Add(cellText);
            dpCellsZero.Add(cellTextZero);
        }
    }

    private IEnumerator FillDPTable()
    {
        List<(int value, int coin)> variants = new List<(int, int)>();
        dp = new int[targetAmount + 1];
        firstChoice = new int[targetAmount + 1];

        for (int i = 1; i <= targetAmount; i++)
        {
            dp[i] = int.MaxValue;
            firstChoice[i] = -1;
        }
        dp[0] = 0;

        if (showSteps) infoText.text = "Инициализация: dp[0] = 0";
        dpCells[0].text = "0";
        yield return new WaitForSeconds(1.5f);

        for (int j = 1; j <= targetAmount; j++)
        {
            if (showSteps) infoText.text = $"Обрабатываем сумму: {j}";
            yield return new WaitForSeconds(1f);

            foreach (int coin in denominations)
            {
                if (j >= coin && dp[j - coin] != int.MaxValue)
                {
                    int newValue = dp[j - coin] + 1;
                    variants.Add((newValue, coin));

                    dpCells[j].color = processingColor;
                    yield return new WaitForSeconds(0.3f);
                }
            }
            if(variants.Count > 0)
            {
                int minValue = variants.Min(c => c.value); 

                if (minValue < dp[j])
                {
                    dp[j] = minValue;
                    var res = variants.Find(v => v.value == minValue);
                    firstChoice[j] = res.coin;

                    dpCells[j].text = dp[j].ToString();
                    dpCells[j].color = highlightColor;
                }

                if (showSteps) infoText.text = $"Сумма {j}: минимум {dp[j]} сфер";
                yield return new WaitForSeconds(0.5f);
                variants.Clear();
            }
        }

        if (showSteps) infoText.text = "Алгоритм завершён!";
        yield return new WaitForSeconds(2f);
        //Debug.Log(showResultPlan);

        if (showResultPlan)
        {
            GetFirstCoin();
        }
    }

    private string GetCoinPlan()
    {
        int amount = targetAmount;
        Dictionary<int, int> coinCounts = new Dictionary<int, int>();

        while (amount > 0 && firstChoice[amount] != -1)
        {
            int coin = firstChoice[amount];
            if (!coinCounts.ContainsKey(coin)) coinCounts[coin] = 0;
            coinCounts[coin]++;
            amount -= coin;
        }

        if (amount != 0) return "Нет решения";

        string result = "";
        foreach (var pair in coinCounts)
        {
            result += $"{pair.Key}";
            break;
        }
        return result;
    }

    public void DInteract2()
    {
        infoText.text = "";
        foreach (var cell in dpCells)
        {
            cell.text = "-";
            cell.color = defaultColor;
        }
    }

    public void GetFirstCoin()
    {
        string plan = GetCoinPlan();
        infoText.text = $"Положите 1: {plan}";
    }

    public void ShowTooltip(int index)
    {
        if (index < 1 || index > targetAmount || dp[index] == int.MaxValue)
        {
            tooltipPanel.SetActive(false);
            return;
        }

        string explanation = $"dp[{index}] = min(";
        List<string> options = new List<string>();

        foreach (int coin in denominations)
        {
            int prev = index - coin;
            if (prev >= 0 && dp[prev] != int.MaxValue)
            {
                string colorHex;
                int val = dp[prev] + 1;
                switch (coin)
                {
                    case 1:
                        colorHex = ColorUtility.ToHtmlStringRGB(colors[0]);
                        options.Add($"<color=#{colorHex}>dp[{prev}] + 1 = {dp[prev]} + 1 = {val}</color>");
                        break;
                    case 3:
                        colorHex = ColorUtility.ToHtmlStringRGB(colors[1]);
                        options.Add($"<color=#{colorHex}>dp[{prev}] + 1 = {dp[prev]} + 1 = {val}</color>");
                        break;
                    case 4:
                        colorHex = ColorUtility.ToHtmlStringRGB(colors[2]);
                        options.Add($"<color=#{colorHex}>dp[{prev}] + 1 = {dp[prev]} + 1 = {val}</color>");
                        break;
                }
            }
        }

        explanation += string.Join(", ", options) + $") = {dp[index]}";
        tooltipText.text = explanation;
        tooltipPanel.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltipPanel.SetActive(false);
    }
}