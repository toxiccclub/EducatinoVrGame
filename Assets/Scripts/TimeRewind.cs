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
            if (positions.Count > 0) // ���� ���� ���������� �������
            {
                transform.position = positions[positions.Count - 1]; // ����� ��������� �������
                positions.RemoveAt(positions.Count - 1); // ������� � �� ������
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
                Debug.Log("�������� �������: " + transform.position);// ���� �� ������������ � ���������� �������
            }
        }
    }

    public void StartRewind()
    {
        isRewinding = true;
        rb.isKinematic = true; // ��������� ������ (����� �� ����� ��� ���������)
    }

    public void StopRewind()
    {
        isRewinding = false;
        //rb.isKinematic = false;
        key.SetActive(true); // �������� ������ �������
    }


}
