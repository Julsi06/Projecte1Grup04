using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    [SerializeField] private Image brightnessPanel;

    private void Awake()
    {
        Color colorPanel = brightnessPanel.color;
        colorPanel.a = SettingsManager.Instance.brightness;
        brightnessPanel.color = colorPanel;
    }
}