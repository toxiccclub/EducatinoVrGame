using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateCoin : MonoBehaviour
{
    public Transform pos;
    public TMP_Dropdown droplist;
    //public DynamicNumberOnManna number;
    public Material manna1;
    public Material manna5;
    public Material manna10;

    public void CreateObj(GameObject prefab)
    {
        GameObject currentCoin = Instantiate(prefab, pos.position, pos.rotation);
        currentCoin.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f) * 10f;

        Renderer coinRenderer = currentCoin.GetComponent<Renderer>();
        //Material coinMaterial = new Material(coinRenderer.material); // Создаём копию материала
        //coinRenderer.material = coinMaterial;

        switch (droplist.value)
        {
            case 0:
                {
                    currentCoin.GetComponent<CoinValue>().value = 1;
                    //coinMaterial.color = Color.black;
                    coinRenderer.material = manna1; 
                }
                break;

            case 1:
                {
                    currentCoin.GetComponent<CoinValue>().value = 5;
                    coinRenderer.material = manna5; 
                }
                break;
            case 2:
                {
                    currentCoin.GetComponent<CoinValue>().value = 10;
                    coinRenderer.material = manna10; 
                }
                break;
        }
               
    }

}
