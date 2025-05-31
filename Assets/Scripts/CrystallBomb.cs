using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystallBomb : MonoBehaviour
{
    public List<Rigidbody> crystals; // ������ ���� ����������
    public float explosionForce = 10f; // ���� �������
    public float explosionRadius = 5f; // ������ ������
    public Transform explosionPoint; // �����, ������ ������� �����

   /* public GameObject door; // �����, ������� ����� �������
    public float doorOpenHeight = 3f; // ��������� ���������� �����
    public float doorOpenSpeed = 2f;*/

    private bool isActivated = false;

    void Start()
    {
        // �������� ��� ��������� ������ �������
        foreach (Transform child in transform)
        {
            Rigidbody rb = child.GetComponent<Rigidbody>();
            if (rb != null)
            {
                crystals.Add(rb);
                rb.isKinematic = true; // ��������� ������, ���� �� ��������
            }
        }
    }

    public void DestroyWall()
    {
        if (isActivated) return;
        isActivated = true;

        // �������� ������ ��� ������� ��������� � ��������
        foreach (Rigidbody rb in crystals)
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(explosionForce, explosionPoint.position, explosionRadius);
        }


    }
}
