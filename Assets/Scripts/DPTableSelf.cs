using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DPTableSelf : MonoBehaviour
{
    [Header("UI References")]
    public GameObject inputFieldPrefab;
    public Transform tableParent;
    public Transform tableZeroParent;
   /* public Transform tableZeroParentPhase2;
    public Transform tableZeroSelfLast;
    public Transform tableParentLast;
    public Button checkButton;*/
    //public Button hintButton;
   /* public TMP_Text feedbackText;
    public TMP_Text targetText;
    public GameObject door;*/
    //public bool doorOpen;
    public TMP_InputField SelectedInputField;
   /* public GameObject ButtonPanel;
    public GameObject phase1Panel;
    public GameObject Pase2Panel;
    public GameObject Phase2PanelSelf;*/
    public TMP_Text target2;
    public bool AllCorrect = false;

    [Header("Settings")]
    public int targetAmount;
    public int[] denominations = { 1, 3, 4 };
    public int[] denominations2 = { 1, 3, 7 };

    private List<TMP_InputField> inputFields = new List<TMP_InputField>();
    private List<TextMeshProUGUI> zeroRow = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> zeroRowPhase2 = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> resultCells = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> phase2Cells = new List<TextMeshProUGUI>();
    public List<TMP_InputField> inptdenom = new List<TMP_InputField>();
    private int[] correctDP;

    private int[] playerValue = new int[3];
    [Header("Restore UI")]
   public Button coin1Button;
    public Button coin5Button;
    public Button coin10Button;
    public TMP_Text restoreStatusText;
    public TMP_Text reconstructionInfo;
    public GameObject coinButtonPanel;
    public Transform resultTableParent;
    public GameObject resultCellPrefab;
    public TMP_Text coinCollectedText;
    public Transform phase2TableParent;
    public GameObject inptPanel;
    /* public TMP_InputField inptdenom1;
     public TMP_InputField inptdenom2;
     public TMP_InputField inptdenom3;*/
    public Button phase2Button;

    private int[] firstChoice;
    private int currentRestoreAmount;
    private bool isRestoring = false;
    private int currentAmount;
    //private int hintIndex = 1;
    private bool reconstructionStarted = false;

    void Start()
    {
        //targetAmount = 10;
        GenerateDPTable();
        tableParent.gameObject.SetActive(true);
        tableZeroParent.gameObject.SetActive(true);
        //checkButton.onClick.AddListener(CheckAnswers);
        //targetText.text = "Тебе надо собрать: " + targetAmount + " Мана={1, 3, 4}" ;
        /*coin1Button.onClick.AddListener(() => OnCoinButton(1));
        coin5Button.onClick.AddListener(() => OnCoinButton(3));
        coin10Button.onClick.AddListener(() => OnCoinButton(4));*/
        //phase2Button.onClick.AddListener(CheckAnswers2);
        //hintButton.onClick.AddListener(ShowHint);
        //ButtonPanel.SetActive(false);
    }

    public void UpdateTable(int newAmount)
    {
        //colors = new List<Color> { blue, Color.red, purple };newAmount;
        targetAmount = newAmount;
        StopAllCoroutines();
        GenerateDPTable();
        //StartCoroutine(FillDPTable());
    }

    void GenerateDPTable()
    {
        // Шаг 1: Сохраняем текущие значения
        List<string> savedValues = new List<string>();
        foreach (var inputField in inputFields)
        {
            savedValues.Add(inputField.text);
        }

        // Шаг 2: Очищаем старую таблицу
        foreach (Transform child in tableZeroParent)
        {
            Destroy(child.gameObject);
        }
        zeroRow.Clear();
        foreach (Transform child in tableParent)
        {
            Destroy(child.gameObject);
        }
        inputFields.Clear();

        // Шаг 3: Создаём новую таблицу
        for (int i = 0; i <= targetAmount; i++)
        {
            GameObject cellZero = Instantiate(resultCellPrefab, tableZeroParent);
            GameObject cell = Instantiate(inputFieldPrefab, tableParent);
            var inputZero = cellZero.GetComponent<TextMeshProUGUI>();
            TMP_InputField input = cell.GetComponent<TMP_InputField>();

            inputZero.text = i.ToString();

            if (i == 0)
            {
                input.text = "0";
                input.interactable = false;
            }
            else
            {
                input.interactable = true;

                // Восстанавливаем сохранённое значение, если оно есть
                if (i < savedValues.Count)
                {
                    input.text = savedValues[i];
                }

                input.onSelect.AddListener((_) =>
                {
                    SelectedInputField = input;
                });
            }

            zeroRow.Add(inputZero);
            inputFields.Add(input);
        }

        correctDP = CalculateCorrectDP();
    }


    int[] CalculateCorrectDP()
    {
        int[] dp = new int[targetAmount + 1];

        for (int i = 1; i <= targetAmount; i++) dp[i] = int.MaxValue;
        dp[0] = 0;

        foreach (int coin in denominations)
        {
            for (int j = coin; j <= targetAmount; j++)
            {
                if (dp[j - coin] != int.MaxValue && dp[j - coin] + 1 < dp[j])
                {
                    dp[j] = dp[j - coin] + 1;
                }
            }
        }

        return dp;
    }


    /*void CheckAnswers()
    {
        bool allCorrect = true;

        for (int i = 1; i <= targetAmount; i++)
        {
            int playerValue;
            bool parsed = int.TryParse(inputFields[i].text, out playerValue);

            Image bg = inputFields[i].GetComponent<Image>();

            if (!parsed || playerValue != correctDP[i])
            {
                bg.color = Color.red;
                allCorrect = false;
            }
            else
            {
                bg.color = Color.green;
            }
        }

        feedbackText.text = allCorrect ? "Отлично! Всё правильно!" : "Есть ошибки. Попробуй ещё!";
        if (allCorrect)
        {
            doorOpen = true;
            door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
        }
    }*/

    void Update()
    {
        //bool allCorrect = true;


        for (int i = 1; i <= targetAmount; i++)
        {

            string inputText = inputFields[i].text;
            Image bg = inputFields[i].GetComponent<Image>();

            // Если поле пустое — пропускаем проверку и не меняем цвет
            if (string.IsNullOrWhiteSpace(inputText))
            {
                continue;
            }

            int playerValue;
            bool parsed = int.TryParse(inputFields[i].text, out playerValue);

            if (!parsed || playerValue != correctDP[i])
            {
                bg.color = Color.red;
                //allCorrect = false;
            }
            else
            {
                bg.color = Color.green;
            }
        }

        //feedbackText.text = allCorrect ? "Отлично! Всё правильно!" : "Есть ошибки. Попробуй ещё!";

    }

   /* public void StartReconstructionPhase()
    {
        // Очистка UI
        foreach (var input in inputFields)
        {
            Destroy(input.gameObject);
        }
        Pase2Panel.SetActive(true);
        ButtonPanel.SetActive(false);
        inputFields.Clear();
        tableParent.gameObject.SetActive(false);
        checkButton.gameObject.SetActive(false);
        feedbackText.text = "";
        //reconstructionInfo.text = "Восстанови решение, выбирая монеты!";
        //coinButtonPanel.SetActive(true);
        resultTableParent.gameObject.SetActive(true);
        tableZeroParentPhase2.gameObject.SetActive(true);
        //door.SetActive(false);

        currentAmount = targetAmount;
        reconstructionStarted = true;

        // Построить отображение результата
        for (int i = 0; i <= targetAmount; i++)
        {
            GameObject cellZero = Instantiate(resultCellPrefab, tableZeroParentPhase2);
            GameObject cell = Instantiate(resultCellPrefab, resultTableParent);
            var input = cellZero.GetComponent<TextMeshProUGUI>();
            var text = cell.GetComponent<TextMeshProUGUI>();
            input.text = i.ToString();
            text.text = correctDP[i] == int.MaxValue ? "?" : correctDP[i].ToString();
            resultCells.Add(text);
            zeroRowPhase2.Add(input);
        }
    }

    public void OnCoinButton(int coin)
    {
        if (!reconstructionStarted || currentAmount <= 0) return;

        if (coinCollectedText.text == "")
        {
            coinCollectedText.text = "Ты собрал: ";
        }

        if (firstChoice[currentAmount] == coin)
        {
            coinCollectedText.text += " + " + coin;
            currentAmount -= coin;
            reconstructionInfo.text = $"Верно! Осталось собрать: {currentAmount}";

            if (currentAmount == 0)
            {
                reconstructionInfo.text = "Ты правильно восстановил решение!";
                doorOpen = true;
                door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
            }
        }
        else
        {
            reconstructionInfo.text = $"Неверно! Для {currentAmount} нужно начать с {firstChoice[currentAmount]}";
        }
    }

    public void Phase2()
    {
        Phase2PanelSelf.SetActive(true);
        coinButtonPanel.SetActive(true);
        resultTableParent.gameObject.SetActive(false);
        feedbackText.text = "";
        reconstructionInfo.text = "Восстанови решение, выбирая монеты!";
        targetText.text = "";
        phase2TableParent.gameObject.SetActive(true);
        coinButtonPanel.SetActive(true);
        resultTableParent.gameObject.SetActive(true);

        int targetAmount2 = Random.Range(5, 10);

        currentAmount = targetAmount2;
        target2.text = "Целевая сумма: " + targetAmount2;
       
        int[] dp2 = new int[targetAmount2 + 1];
        firstChoice = new int[targetAmount2 + 1];
        for (int i = 1; i <= targetAmount2; i++)
        {
            dp2[i] = int.MaxValue;
        }
        dp2[0] = 0;

        foreach (int coin in denominations)
        {
            for (int j = coin; j <= targetAmount2; j++)
            {
                if (dp2[j - coin] != int.MaxValue && dp2[j - coin] + 1 < dp2[j])
                {
                    dp2[j] = dp2[j - coin] + 1;
                    firstChoice[j] = coin;
                }
            }
        }

        for (int i = 0; i <= targetAmount2; i++)
        {
            GameObject cellZero = Instantiate(resultCellPrefab, tableZeroSelfLast);
            GameObject cell = Instantiate(resultCellPrefab, tableParentLast);
            var input = cellZero.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI cellText = cell.GetComponent<TextMeshProUGUI>();
            cellText.text = dp2[i] == int.MaxValue ? "-" : dp2[i].ToString();
            input.text = i.ToString();
            phase2Cells.Add(cellText);
            zeroRowPhase2.Add(input);
        }
        *//*for (int i = 0; i <= targetAmount2; i++)
        {
            Debug.Log("dp table ="+dp2[i]);
            phase2Cells[i].text = dp2[i] == int.MaxValue ? "?" : dp2[i].ToString();
        }*//*

    }*/

    /*int[] GenerateUniqueCoins(int count, int min, int max)
    {
        List<int> numbers = new List<int>();
        for (int i = min; i <= max; i++)
            numbers.Add(i);

        for (int i = 0; i < numbers.Count; i++)
        {
            int rndIndex = Random.Range(i, numbers.Count);
            int temp = numbers[i];
            numbers[i] = numbers[rndIndex];
            numbers[rndIndex] = temp;
        }

        int[] result = new int[count];
        for (int i = 0; i < count; i++)
            result[i] = numbers[i];

        System.Array.Sort(result);

        return result;
    }

    void CheckAnswers2()
    {
        bool allCorrect = true;
        for (int i = 0; i < 3; i++)
        {
            bool parsed = int.TryParse(inptdenom[i].text, out playerValue[i]);
            Image bg = inptdenom[i].GetComponent<Image>();

            if (!parsed || playerValue[i] != denominations2[i])
            {
                bg.color = Color.red;
                allCorrect = false;
            }
            else
            {
                bg.color = Color.green;
            }
        }

        feedbackText.text = allCorrect ? "Отлично! Всё правильно!" : "Есть ошибки. Попробуй ещё!";

        if (allCorrect)
        {
            doorOpen = true;
            door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
        }
    }*/
}


    /*void ShowHint()
    {
        if (hintIndex > targetAmount)
        {
            feedbackText.text = "Все подсказки уже показаны!";
            return;
        }

        string hint = $"Подсказка для dp[{hintIndex}]:\n";

        List<string> options = new List<string>();
        foreach (int coin in denominations)
        {
            int prev = hintIndex - coin;
            if (prev >= 0 && correctDP[prev] != int.MaxValue)
            {
                options.Add($"dp[{prev}] + 1 = {correctDP[prev] + 1}");
            }
        }

        if (options.Count > 0)
        {
            hint += string.Join("\n", options);
            hint += $"\n? dp[{hintIndex}] = {correctDP[hintIndex]}";
        }
        else
        {
            hint += "Нет возможных комбинаций.";
        }

        feedbackText.text = hint;
        hintIndex++;
    }*/
