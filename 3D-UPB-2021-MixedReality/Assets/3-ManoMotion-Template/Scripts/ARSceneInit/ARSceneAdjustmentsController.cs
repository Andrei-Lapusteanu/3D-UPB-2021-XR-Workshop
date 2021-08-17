using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class ARSceneAdjustmentsController : MonoBehaviour
{
    public ARSessionOrigin m_sessionOrigin;
    public Slider sliderRotation;
    public Slider sliderScale;
    public TextMeshProUGUI tmpRotation;
    public TextMeshProUGUI tmpScale;
    
    private Vector3 currentScale = Vector3.one;
    private Quaternion currentRot = Quaternion.identity;

    private void Start()
    {
        tmpRotation.text = sliderRotation.value.ToString("0");
        tmpScale.text = sliderScale.value.ToString("0.00");
    }

    public void SliderRotationChange()
    {
        // Get slider value, set text
        float sliderValue = sliderRotation.value;
        tmpRotation.text = sliderValue.ToString("0");

        // Set rotation around Y axis
        currentRot = Quaternion.Euler(0, sliderValue, 0);
        GameObject.Find("ScenePrefab(Clone)").transform.rotation = currentRot;

        // MakeContentAppearAt() solver AR rotation & scalign issues
        m_sessionOrigin.MakeContentAppearAt(
            GameObject.Find("ScenePrefab(Clone)").transform,
            GameObject.Find("ScenePrefab(Clone)").transform.position,
            GameObject.Find("ScenePrefab(Clone)").transform.rotation);
    }

    public void SliderScaleChange()
    {
        // Get slider value, set text
        float sliderValue = sliderScale.value;
        tmpScale.text = sliderValue.ToString("0.00");

        // Set scale
        currentScale = Vector3.one * sliderValue;
        GameObject.Find("ScenePrefab(Clone)").transform.localScale = currentScale;

        // MakeContentAppearAt() solver AR rotation & scalign issues
        m_sessionOrigin.MakeContentAppearAt(
            GameObject.Find("ScenePrefab(Clone)").transform,
            GameObject.Find("ScenePrefab(Clone)").transform.position,
            GameObject.Find("ScenePrefab(Clone)").transform.rotation);
    }
}