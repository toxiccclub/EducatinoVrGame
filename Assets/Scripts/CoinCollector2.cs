using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class CoinSpawnPoint
{
    public Transform point;
    public int value; // 1, 5, 10
}

public class CoinCollector2 : MonoBehaviour
{
    [SerializeField] public int targetAmount;
    private int currentAmount = 0;
    private int[] denominations = { 1, 3, 4 };

    public GameObject doorUnlcok;
    public GameObject Crystall;
    public bool doorOpen;

    public GameObject CanvasInstruction;
    public GameObject CanvasInstructionSelf;
    public GameObject explosionEffectPrefab1;
    public GameObject explosionEffectPrefab5;
    public GameObject explosionEffectPrefab10;

    public DPanimation3 dpAnimation;
    public DPTableSelf dpAnimationSelf;
    public GameObject resCanvas;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI targetText;
    public TextMeshProUGUI targetText_col;
    public Button restartButton;

    // ?? Монеты и точки спавна
    public GameObject coinPrefab1;
    public GameObject coinPrefab5;
    public GameObject coinPrefab10;
    public List<CoinSpawnPoint> spawnPoints = new List<CoinSpawnPoint>();
    private List<GameObject> spawnedCoins = new List<GameObject>();
    public NPCYnteractable npcdestroy;

    private List<int> collectedCoins = new List<int>();

    private void Start()
    {
        Debug.Log(targetAmount);
        targetText.text = "Собери ману: " + targetAmount;
        restartButton.gameObject.SetActive(false);
        restartButton.onClick.AddListener(RestartGame);
        doorOpen = false;
        doorUnlcok.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
        Crystall.SetActive(true);
        RespawnCoins();
    }

    private void OnTriggerEnter(Collider other)
    {
        CoinValue coin = other.GetComponent<CoinValue>();
        if (coin != null)
        {
            collectedCoins.Add(coin.value);
            currentAmount += coin.value;

            switch (coin.value)
            {
                case 1: Instantiate(explosionEffectPrefab1, coin.transform.position, Quaternion.identity); break;
                case 3: Instantiate(explosionEffectPrefab5, coin.transform.position, Quaternion.identity); break;
                case 4: Instantiate(explosionEffectPrefab10, coin.transform.position, Quaternion.identity); break;
            }

            Destroy(other.gameObject);
            dpAnimation.DInteract2();
            targetText_col.text = "Собрано: " + currentAmount;


            if (CanvasInstruction.activeSelf)
            {
                dpAnimation.UpdateTable(targetAmount - currentAmount);
            }
            if (CanvasInstructionSelf.activeSelf)
            {
                dpAnimationSelf.UpdateTable(targetAmount - currentAmount);
            }
            CheckSolution();
        }
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
            int minCoins = CountCoins();

            if (GetCurrentCoinCount() == minCoins)
            {
                ShowMessage("Отлично! Ты использовал минимальное количество монет!");
                Crystall.GetComponent<CrystallBomb>().DestroyWall();
                doorOpen = true;
                doorUnlcok.GetComponent<Animator>().SetBool("IsOpen", true);
                StartCoroutine(DisableCrystallAfterDelay(5f));

            }
            else
            {
                ShowMessage("Ты собрал сумму, но можно лучше!");
                Crystall.SetActive(true);
                restartButton.gameObject.SetActive(true);
                doorUnlcok.GetComponent<Animator>().SetBool("IsOpen", false);
            }
        }
        else if (currentAmount > targetAmount)
        {
            ShowMessage("Перебор! Попробуй ещё раз.");
            restartButton.gameObject.SetActive(true);
        }
        else
        {
            HideMessage();
            Crystall.SetActive(true);
            doorUnlcok.GetComponent<Animator>().SetBool("IsOpen", false);
        }
    }

    private IEnumerator DisableCrystallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Crystall.SetActive(false);
    }

    private int GetCurrentCoinCount()
    {
        return collectedCoins.Count;
    }

    public int CountCoins()
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
        currentAmount = 0;
        CanvasInstruction.SetActive(false);
        CanvasInstructionSelf.SetActive(true);
        npcdestroy.DInteract();
        collectedCoins.Clear();
        targetText_col.text = "Собрано: 0";
        HideMessage();
        restartButton.gameObject.SetActive(false);


        dpAnimation.DInteract2();
        dpAnimationSelf.UpdateTable(targetAmount);
        //dpAnimation.UpdateTable(targetAmount);

        Crystall.SetActive(true);
        /*doorOpen = false;
        doorUnlcok.GetComponent<Animator>().SetBool("IsOpen", false);*/

        RespawnCoins();
    }

    private void RespawnCoins()
    {
        foreach (var coin in spawnedCoins)
        {
            if (coin != null)
                Destroy(coin);
        }
        spawnedCoins.Clear();

        foreach (var sp in spawnPoints)
        {
            GameObject prefab = null;
            switch (sp.value)
            {
                case 1: prefab = coinPrefab1; break;
                case 3: prefab = coinPrefab5; break;
                case 4: prefab = coinPrefab10; break;
            }

            if (prefab != null && sp.point != null)
            {
                GameObject coin = Instantiate(prefab, sp.point.position, sp.point.rotation);
                coin.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f) * 10f;
                spawnedCoins.Add(coin);
            }
        }
    }

    public void Table()
    {
        dpAnimation.DInteract2();
        dpAnimation.UpdateTable(targetAmount - currentAmount);
    }
}
