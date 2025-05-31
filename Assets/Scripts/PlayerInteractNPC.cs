using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractNPC : MonoBehaviour
{

    public NPCYnteractable GetInteractableObject()
    {
        LayerMask layerMask = LayerMask.GetMask("NPC");
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange, layerMask);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCYnteractable npcYnteractable))
            {
                return npcYnteractable;
            }
        }
        return null;
    }

   /* public NPCYnteractable GetInteractableNPC()
    {
        LayerMask layerMask = LayerMask.GetMask("NPC");
        float interactRange = 10f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange, layerMask);
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCYnteractable npcYnteractable))
            {
                return npcYnteractable;
            }
        }
        return null;
    }*/
}
