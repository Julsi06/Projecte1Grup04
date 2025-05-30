using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer mainMixer;

    public SpriteRenderer[] spriteRenderer;

    private void Start()
    {
        spriteRenderer = FindObjectsOfType<SpriteRenderer>();
    }

    public void SetVolume(float volume)
    {
        mainMixer.SetFloat("Volume", volume);
    }

    public void SetFullScreen(bool fullscreen)
    {
        Debug.Log("The game is on fullscreen");
        Screen.fullScreen = fullscreen;
    }

    public void AdjustBrightness(float brightness)
    {
        foreach(SpriteRenderer spriteRenderer in spriteRenderer) 
        {
            spriteRenderer.color = new Color(brightness, brightness, brightness, spriteRenderer.color.a);
        }
    }
}
