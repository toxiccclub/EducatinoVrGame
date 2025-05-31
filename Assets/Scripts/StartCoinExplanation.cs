using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartCoinExplanation : MonoBehaviour
{
    public TMP_Text explanationText;
    public TMP_Text targteText;
    public DPTableSelf dptable;  // Это ссылка на твой объект с таблицей
    public Button NextButton;

    private List<TextMeshProUGUI> resultCells = new List<TextMeshProUGUI>();
    private int currentAmount;
    private int[] correctDP;
    private int[] denominations = { 1, 3, 4 };
    private bool isExplaining = false;
    private IEnumerator stepCoroutine;

    public void ActiveExplanation()
    {
        NextButton.gameObject.SetActive(true);
        dptable.resultTableParent.gameObject.SetActive(false);
        NextButton.onClick.AddListener(NextStep);
        stepCoroutine = ExplainStepByStep();
        currentAmount = dptable.targetAmount;
        resultCells = dptable.resultCells;
        targteText.text = "Тебе нужно собрать : " + currentAmount;

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

    IEnumerator ExplainStepByStep()
    {
        string trace = "Нвбор маны: ";
        explanationText.text = ""; // Сбросим текст перед началом

        dptable.resultTableParent.gameObject.SetActive(true);
        yield return null;

        while (currentAmount > 0)
        {
            int currentDP;
            int.TryParse(resultCells[currentAmount].text, out currentDP);
            resultCells[currentAmount].color = Color.green;
            yield return null;
            bool found = false;

            foreach (int coin in denominations)
            {
                int prev = currentAmount - coin;
                int CP;
                int.TryParse(resultCells[prev].text, out CP);
                if (prev >= 0 && CP == currentDP - 1)
                {
                    resultCells[currentAmount].color = Color.yellow;
                    resultCells[prev].color = Color.green;
                    yield return null;

                    explanationText.text =
                        $"Для {currentAmount} монет: dp[{currentAmount}] = {currentDP}\n" +
                        $"Проверяем: dp[{prev}] + 1 = {CP} + 1 = {CP + 1}\n" +
                        $"Значит, используем монету номиналом {coin}";
                    trace += coin + " ";
                    yield return null;

                    currentAmount = prev;
                    found = true;
                    yield return null; // задержка для игрока
                    break;
                }
            }

            if (!found)
            {
                explanationText.text = "Невозможно восстановить путь!";
                yield break;
            }
        }

        explanationText.text = "Готово! Путь полностью восстановлен.";
        yield return null;
        explanationText.text = trace;
        isExplaining = false;
    }
}
