using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAppear : MonoBehaviour
{
    [SerializeField] private GameObject wizrd1;
    [SerializeField] private GameObject wizrd2;
    [SerializeField] private GameObject wizrd3;
    [SerializeField] private GameObject wizrd4;
    [SerializeField] private AudioSource WizardSound;
    [SerializeField] private AudioClip WizardClip;
    [SerializeField] private ParticleSystem appearEffect; // Эффект появления
    [SerializeField] private float effectDuration = 1f; // Время эффекта

    private void Start()
    {
        appearEffect.Stop();
       /* wizrd1.SetActive(true);
        wizrd2.SetActive(false);*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("player"))
        {
            if (gameObject.name == "TriggerWzrd1")
            {
                StartCoroutine(AppearWithEffect(wizrd1, wizrd2, wizrd3, wizrd4));
            }
            else if (gameObject.name == "TriggerWzrd2")
            {
                StartCoroutine(AppearWithEffect(wizrd2, wizrd1, wizrd3, wizrd4));
            }
            else if(gameObject.name == "TriggerWzrd3")
            {
                StartCoroutine(AppearWithEffect(wizrd3, wizrd2, wizrd1, wizrd4));
            }
            else if (gameObject.name == "TriggerWzrd4")
            {
                StartCoroutine(AppearWithEffect(wizrd4, wizrd3, wizrd2, wizrd1));
            }
        }
    }

    private IEnumerator AppearWithEffect(GameObject toEnable, GameObject toDisable, GameObject toDisable2, GameObject toDisable3)
    {
        if (appearEffect != null)
        {
            appearEffect.Play(); 
        }

        if (WizardClip != null)
        {
            WizardSound.clip = WizardClip;
            WizardSound.Play();
        }

        toDisable.SetActive(false);
        toDisable2.SetActive(false);
        toDisable3.SetActive(false);
        toEnable.SetActive(true);

        yield return new WaitForSeconds(effectDuration); 

        /*toDisable.SetActive(false);
        toEnable.SetActive(true);*/

        if (appearEffect != null)
        {
            appearEffect.Stop(); 
        }
    }
}

