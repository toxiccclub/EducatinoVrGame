using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeRewind : MonoBehaviour
{
    private bool isRewinding = false;
    private List<Vector3> positions = new List<Vector3>();
    private Rigidbody rb;
    public GameObject key;
    private int maxPositions = 15;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        key.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRewinding)
        {
            if (positions.Count > 0) // Если есть записанные позиции
            {
                transform.position = positions[positions.Count - 1]; // Берем последнюю позицию
                positions.RemoveAt(positions.Count - 1); // Удаляем её из списка
            }
            else
            {
                StopRewind();
            }
        }
        else
        {
            if(positions.Count < maxPositions)
            {
                positions.Add(transform.position);
                Debug.Log("Записана позиция: " + transform.position);// Если НЕ перематываем — записываем позицию
            }
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true; // Отключаем физику (чтобы не падал при перемотке)
    }

    public void StopRewind()
    {
        isRewinding = false;
        //rb.isKinematic = false;
        key.SetActive(true); // Включаем физику обратно
    }


}
