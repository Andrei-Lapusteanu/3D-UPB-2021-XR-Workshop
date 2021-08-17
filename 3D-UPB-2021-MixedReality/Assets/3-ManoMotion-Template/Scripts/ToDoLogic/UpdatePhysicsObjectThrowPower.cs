using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpdatePhysicsObjectThrowPower : MonoBehaviour
{
    [SerializeField] private float powerBarTimer = 3.0f;    // How much does it take for the power bar to reach min->max (and max->min)
    [SerializeField] private float throwPowerMin = 0.0f;
    [SerializeField] private float throwPowerMax = 10.0f;

    private float currentTime = 0.0f;
    private float currentThrowPower = 0.0f;

    private bool shouldUpdate = false;

    private void Start()
    {
        currentTime = 0.0f;
    }

    public void StartTimer()
    {
        // Enable flag
        shouldUpdate = true;
    }

    public void StopTimer()
    {
        // Reset timer and set flag
        currentTime = 0.0f;
        currentThrowPower = 0.0f;
        shouldUpdate = false;

        // TODO: Update UI
    }

    void Update()
    {
        if(shouldUpdate)
        {
            /* TODO: Update current time
             *       Compute time-depenent throw power, betwewn 'throwPowerMin' and 'throwPowerMax'
             *       Hint: Mathf.PingPong() could be useful
             *       Update UI power bar (slider) and text value
             */
        }
    }
}
