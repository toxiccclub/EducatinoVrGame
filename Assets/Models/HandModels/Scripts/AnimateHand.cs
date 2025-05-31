using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHand : MonoBehaviour
{
    public InputActionProperty pinchAction;
    public InputActionProperty gripAction;

    private Animator anim = null;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float triggerValue = pinchAction.action.ReadValue<float>();
        //Debug.Log(triggerValue);
        anim.SetFloat("Trigger", triggerValue);

        float gripValue = gripAction.action.ReadValue<float>();
        //Debug.Log(gripValue);
        anim.SetFloat("Grip", gripValue);
    }
}
