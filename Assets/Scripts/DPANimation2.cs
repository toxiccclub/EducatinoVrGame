using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DPANimation2 : MonoBehaviour
{
    public GameObject cellPrefab;  // Префаб ячейки таблицы
    public Transform[] dpTablesParentsArray; // Родительские объекты для каждой таблицы
    public TextMeshProUGUI infoText;
    //public TextMeshProUGUI Tip3;
    private Dictionary<int, Transform> dpTablesParents;
    public bool flag = false;
    public bool flag2 = false;

    private int[] denominations = { 1, 5, 10 };
    private int targetAmount = 13;
    private int currentAmount = 0;

    private Dictionary<int, List<TextMeshProUGUI>> dpTables = new Dictionary<int, List<TextMeshProUGUI>>();
    private int[] firstChoice;

    public Color defaultColor = Color.white;
    public Color highlightColor = Color.green;
    public Color processingColor = Color.yellow;

    public void UpdateTable(int newAmount)
    {
        //Tip3.text = "";
        currentAmount = newAmount;
        targetAmount = currentAmount;
        StopAllCoroutines();
        CreateDPTables();
        StartCoroutine(FillDPTable());
    }

    private void CreateDPTables()
    {
        // Связываем номиналы монет с их таблицами
        dpTablesParents = new Dictionary<int, Transform>();
        for (int i = 0; i < denominations.Length; i++)
        {
            dpTablesParents[denominations[i]] = dpTablesParentsArray[i];
        }

        // Удаляем старые таблицы
        foreach (var table in dpTables.Values)
        {
            foreach (var cell in table)
            {
                Destroy(cell.gameObject);
            }
        }
        dpTables.Clear();

        // Создаём таблицу для каждого номинала монеты
        foreach (int coin in denominations)
        {
            dpTables[coin] = new List<TextMeshProUGUI>();

            for (int i = 0; i <= targetAmount; i++)
            {
                GameObject newCell = Instantiate(cellPrefab, dpTablesParents[coin]); // Используем dpTablesParents[coin]
                TextMeshProUGUI cellText = newCell.GetComponent<TextMeshProUGUI>();
                cellText.text = "-";
                dpTables[coin].Add(cellText);
            }
        }
    }

    private IEnumerator FillDPTable()
    {
        Dictionary<int, int[]> dp = new Dictionary<int, int[]>();
        firstChoice = new int[targetAmount + 1];

        for (int i = 0; i <= targetAmount; i++)
        {
            firstChoice[i] = -1; // Изначально не знаем, какую монету брать первой
        }

        foreach (int coin in denominations)
        {
            dp[coin] = new int[targetAmount + 1];
            for (int i = 1; i <= targetAmount; i++)
            {
                dp[coin][i] = int.MaxValue;
            }
            dp[coin][0] = 0;
        }

        if (flag2) infoText.text = "Инициализация: dp[0] = 0, остальные - бесконечность.";
        yield return new WaitForSeconds(2f);

        foreach (int coin in denominations)
        {
            if (flag2) infoText.text = $"Обрабатываем сферу: {coin}";
            yield return new WaitForSeconds(1f);

            for (int j = coin; j <= targetAmount; j++)
            {
                if (dp[coin][j - coin] != int.MaxValue)
                {
                    int new_value = dp[coin][j - coin] + 1;
                    dpTables[coin][j].color = processingColor;
                    yield return new WaitForSeconds(0.3f);

                    if (new_value < dp[coin][j])
                    {
                        dp[coin][j] = new_value;
                        dpTables[coin][j].text = dp[coin][j].ToString();
                        dpTables[coin][j].color = highlightColor;
                        firstChoice[j] = coin;
                    }
                    else
                    {
                        dpTables[coin][j].color = defaultColor;
                    }

                    if (flag2) infoText.text = $"Сумма {j}: минимум {dp[coin][j]}";
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }

        if (flag2) infoText.text = "Алгоритм завершён!";
        yield return new WaitForSeconds(3f);
        infoText.text = "";
        if (flag)
        {
           GetFirstCoin();
        }
    }

    public void DInteract2()
    {
        infoText.text = "";
        foreach (var table in dpTables.Values)
        {
            foreach (var cell in table)
            {
                cell.text = "-";
                cell.color = defaultColor;
            }
        }
    }

    public void GetFirstCoin()
    {

        infoText.text = $"Оптимальный план: положите {firstChoice[targetAmount]}!";
    }

}