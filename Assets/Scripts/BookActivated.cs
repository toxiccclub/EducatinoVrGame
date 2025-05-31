using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookActivated : MonoBehaviour
{
    public GameObject book;
    public GameObject BookTable;
    public ParticleSystem appearEffect;
    public GameObject CodeInstruction;
    public float effectDuration = 1f;

   void Start()
    {
        appearEffect.Stop();
    }
   public void Activate()
    {
        StartCoroutine(AppearWithEffect());
    }

    private IEnumerator AppearWithEffect()
    {
        book.SetActive(false);
        BookTable.SetActive(false);
        if (appearEffect != null)
        {
            appearEffect.Play(); // Запускаем эффект
        }

        yield return new WaitForSeconds(effectDuration);

        if (appearEffect != null)
        {
            appearEffect.Stop(); // Останавливаем эффект после задержки
        }
        CodeInstruction.SetActive(true);
    }
}
