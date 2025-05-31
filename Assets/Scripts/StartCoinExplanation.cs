using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartCoinExplanation : MonoBehaviour
{
    public TMP_Text explanationText;
    public TMP_Text targteText;
    public DPTableSelf dptable;  // ��� ������ �� ���� ������ � ��������
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
        targteText.text = "���� ����� ������� : " + currentAmount;

    }

    void NextStep()
    {
        if (stepCoroutine != null)
        {
            bool hasNext = stepCoroutine.MoveNext();
            if (!hasNext)
            {
                stepCoroutine = null;
                Debug.Log("��������� ���������.");
            }
        }
    }

    IEnumerator ExplainStepByStep()
    {
        string trace = "����� ����: ";
        explanationText.text = ""; // ������� ����� ����� �������

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
                        $"��� {currentAmount} �����: dp[{currentAmount}] = {currentDP}\n" +
                        $"���������: dp[{prev}] + 1 = {CP} + 1 = {CP + 1}\n" +
                        $"������, ���������� ������ ��������� {coin}";
                    trace += coin + " ";
                    yield return null;

                    currentAmount = prev;
                    found = true;
                    yield return null; // �������� ��� ������
                    break;
                }
            }

            if (!found)
            {
                explanationText.text = "���������� ������������ ����!";
                yield break;
            }
        }

        explanationText.text = "������! ���� ��������� ������������.";
        yield return null;
        explanationText.text = trace;
        isExplaining = false;
    }
}
