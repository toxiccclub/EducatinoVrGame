using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystallBomb : MonoBehaviour
{
    public List<Rigidbody> crystals; // Список всех кристаллов
    public float explosionForce = 10f; // Сила разлета
    public float explosionRadius = 5f; // Радиус взрыва
    public Transform explosionPoint; // Точка, откуда исходит взрыв

   /* public GameObject door; // Дверь, которую нужно открыть
    public float doorOpenHeight = 3f; // Насколько поднимется дверь
    public float doorOpenSpeed = 2f;*/

    private bool isActivated = false;

    void Start()
    {
        // Получаем все кристаллы внутри объекта
        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                crystals.Add(rb);
                rb.isKinematic = true; // Отключаем физику, пока не разрушим
            }
        }
    }

    public void DestroyWall()
    {
        if (isActivated) return;
        isActivated = true;

        // Включаем физику для каждого кристалла и взрываем
        foreach (Rigidbody rb in crystals)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(explosionForce, explosionPoint.position, explosionRadius);
        }


    }
}
