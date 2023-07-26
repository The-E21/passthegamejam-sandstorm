using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    public Image fillImage;

    public float value = 0;
    public float maxValue = 1;

    private void OnValidate()
    {
        UpdateDisplay();
    }

    public void SetValue(float value)
    {
        this.value = value;
        UpdateDisplay();
    }

    public void SetValue(float value, float maxValue)
    {
        this.value = value;
        this.maxValue = maxValue;

        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        if (!fillImage) return;

        float fill = GetFill();
        fillImage.fillAmount = fill;
    }

    public float GetFill() => Mathf.Clamp01(value / maxValue);
}
