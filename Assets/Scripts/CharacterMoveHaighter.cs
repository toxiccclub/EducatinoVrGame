using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;

public class CharacterMoveHaighter : MonoBehaviour
{

    private CharacterController characterController;
    private CharacterControllerDriver controllerDriver;
    private XROrigin xROrigin;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        controllerDriver = GetComponent<CharacterControllerDriver>();
        xROrigin = GetComponent<XROrigin>();
    }

    void Update()
    {
        UpdateCharacterController();
    }

    protected virtual void UpdateCharacterController()
    {
        if (xROrigin == null || characterController == null)
            return;

        var height = Mathf.Clamp(xROrigin.CameraInOriginSpaceHeight, controllerDriver.minHeight, controllerDriver.maxHeight);

        Vector3 center = xROrigin.CameraInOriginSpacePos;
        center.y = height / 2f + characterController.skinWidth;

        characterController.height = height;
        characterController.center = center;
    }
}
