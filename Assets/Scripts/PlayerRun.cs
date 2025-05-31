using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerRun : MonoBehaviour
{
    public XRController controller; 
    public ContinuousMoveProviderBase moveProvider;
    public float runMultiplier = 2.0f;

    private float defaultSpeed;

    void Start()
    {
        if (moveProvider)
            defaultSpeed = moveProvider.moveSpeed;
    }

    void Update()
    {
        if (controller.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool isRunning) && isRunning)
        {
            moveProvider.moveSpeed = defaultSpeed * runMultiplier;
        }
        else
        {
            moveProvider.moveSpeed = defaultSpeed;
        }
    }
}
