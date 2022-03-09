using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageSanityIcon : MonoBehaviour
{
    public SanityManager sanityManager;
    public Sprite highSanity;
    public Sprite mediumSanity;
    public Sprite lowSanity;

    public float highSanityMinimum = 75;
    public float lowSanityMaximum = 25;

    private SpriteRenderer _iconSpriteRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        if (sanityManager == null)
        {
            Debug.Log("Set a Sanity Manager on the ManageSanityScript under the Icon class.");
        }

        _iconSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        SanityManager.SanityLevel currentSanity = sanityManager.GetSanityLevel();
        if (currentSanity == SanityManager.SanityLevel.High)
        {
            _iconSpriteRenderer.sprite = highSanity;
        }

        else if (currentSanity == SanityManager.SanityLevel.Low)
        {
            _iconSpriteRenderer.sprite = lowSanity;
        }
        else if (currentSanity == SanityManager.SanityLevel.Medium)
        {
            _iconSpriteRenderer.sprite = mediumSanity;
        }
    }
}