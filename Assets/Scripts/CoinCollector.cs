using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class CoinCollector : MonoBehaviour
{
    public int targetAmount = 18;
    private int currentAmount = 0;
    private int[] denominations = { 1, 5, 10 };
    public GameObject doorUnlcok;
    public GameObject Crystall;
    public bool doorOpen;
    public GameObject CanvasInstruction;

    //public DPANimation2 dpAnimation;
    public DPanimation3 dpAnimation;
    public GameObject resCanvas;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI targetText;
    public TextMeshProUGUI targetText_col;
    public Button restartButton; // Кнопка перезапуска

    private void Start()
    {
        targetAmount = Random.Range(6, 20);
        //Table();
        targetText.text = "Собери сумму: " + targetAmount;
        restartButton.gameObject.SetActive(false); // Скрываем кнопку при старте
        restartButton.onClick.AddListener(RestartGame);
        doorOpen = false;
        doorUnlcok.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
        Crystall.SetActive(true);
    }

    private List<int> collectedCoins = new List<int>();

    private void OnTriggerEnter(Collider other)
    {
        CoinValue coin = other.GetComponent<CoinValue>();
        if (coin != null)
        {
            dpAnimation.DInteract2();
            collectedCoins.Add(coin.value);
            Debug.Log(GetCurrentCoinCount());
            currentAmount += coin.value;
            //Debug.Log(currentAmount.ToString());
            targetText_col.text = "Собрано: " + currentAmount;
            if (CanvasInstruction.activeSelf)
            {
                dpAnimation.UpdateTable(targetAmount - currentAmount);
            }
            //Destroy(other.gameObject);
            CheckSolution();
        }
    }

    private int GetCurrentCoinCount()
    {
        return collectedCoins.Count;
    }

    private void OnTriggerExit(Collider other)
    {
        CoinValue coin = other.GetComponent<CoinValue>();
        if (coin != null)
        {
            dpAnimation.DInteract2();
            collectedCoins.Remove(coin.value);
            currentAmount -= coin.value;
            targetText_col.text = "Собрано: " + currentAmount;
            if (CanvasInstruction.activeSelf)
            {
                dpAnimation.UpdateTable(targetAmount - currentAmount);
            }
            CheckSolution();
        }
    }

    private void CheckSolution()
    {
        if (currentAmount == targetAmount)
        {
            int minCoins = CountCoins(); // Получаем правильное минимальное количество монет
            /*Debug.Log(minCoins);
            Debug.Log(GetCurrentCoinCount());*/
            if (GetCurrentCoinCount() == minCoins)
            {
                ShowMessage("Отлично! Ты использовал минимальное количество монет!");
                CrystallBomb explosion = Crystall.GetComponent<CrystallBomb>();
                explosion.DestroyWall();
                //Crystall.SetActive(false);
                doorOpen = true;
                doorUnlcok.GetComponent<Animator>().SetBool("IsOpen", doorOpen);

            }
            else
            {
                ShowMessage("Ты собрал сумму, но можно лучше!");
                Crystall.SetActive(true);
                doorOpen = false;
                doorUnlcok.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
            }
        }
        else if (currentAmount > targetAmount)
        {
            ShowMessage("Перебор! Попробуй ещё раз.");
            restartButton.gameObject.SetActive(true); // Показываем кнопку
        }
        else if (currentAmount < targetAmount)
        {
            HideMessage();
            Crystall.SetActive(true);
            doorOpen = false;
            doorUnlcok.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
        }
    }

    /* private int MinCoinsToReach(int amount)
     {
         int[] dp = new int[amount + 1];
         for (int i = 1; i <= amount; i++) dp[i] = int.MaxValue;
         dp[0] = 0;

         foreach (int coin in denominations)
         {
             for (int j = coin; j <= amount; j++)
             {
                 if (dp[j - coin] != int.MaxValue)
                 {
                     dp[j] = Mathf.Min(dp[j], dp[j - coin] + 1);
                 }
             }
         }
         return dp[amount];
     }*/

    public int CountCoins()
    {
        {
            int[] dp = new int[targetAmount + 1];
            for (int i = 1; i <= targetAmount; i++) dp[i] = int.MaxValue;
            dp[0] = 0;

            foreach (int coin in denominations)
            {
                for (int j = coin; j <= targetAmount; j++)
                {
                    if (dp[j - coin] != int.MaxValue)
                    {
                        dp[j] = Mathf.Min(dp[j], dp[j - coin] + 1);
                    }
                }
            }
            return dp[targetAmount];
        }
    }

    private void ShowMessage(string message)
    {
        resCanvas.SetActive(true);
        messageText.text = message;
    }

    private void HideMessage()
    {
        resCanvas.SetActive(false);
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Table()
    {
        dpAnimation.DInteract2();
        dpAnimation.UpdateTable(targetAmount-currentAmount);
    }
}
