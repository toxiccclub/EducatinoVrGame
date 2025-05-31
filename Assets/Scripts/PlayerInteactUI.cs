using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteactUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private PlayerInteractNPC playerInteractNPC;
    [SerializeField] private InputActionProperty interactButton;
    [SerializeField] private GameObject npc;
    public bool isNear;


    private void Update()
    {
        if (playerInteractNPC.GetInteractableObject() != null)
        {
            isNear = true;
            Show();
            if (interactButton.action.WasPressedThisFrame())
            {
                playerInteractNPC.GetInteractableObject().Interact();
            }

        }
        else
        {
            isNear=false;
            Hide();
        }
    }

    private void Show()
    {
        containerGameObject.SetActive(true);
        npc.GetComponent<Animator>().SetBool("IsNear", isNear);

    }

    private void Hide()
    {
        containerGameObject.SetActive(false);
        npc.GetComponent<Animator>().SetBool("IsNear", isNear);
    }
}
