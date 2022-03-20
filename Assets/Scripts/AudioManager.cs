using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sprite unmutedImage;
    public Sprite mutedImage;
    private Toggle _isMuted;

    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _isMuted = GetComponent<Toggle>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _isMuted.isOn = false;
    }

    public void MuteButton()
    {
        if (_isMuted.isOn)
        {
            _spriteRenderer.sprite = mutedImage;
            AudioListener.volume = 0;
        }
        else
        {
            _spriteRenderer.sprite = unmutedImage;
            AudioListener.volume = 1;
        }
    }
}