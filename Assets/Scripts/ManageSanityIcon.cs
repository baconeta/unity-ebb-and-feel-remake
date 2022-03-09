using UnityEngine;

public class ManageSanityIcon : MonoBehaviour
{
    public SanityManager sanityManager;
    public Sprite highSanity;
    public Sprite mediumSanity;
    public Sprite lowSanity;

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
        switch (currentSanity)
        {
            case SanityManager.SanityLevel.High:
                _iconSpriteRenderer.sprite = highSanity;
                break;
            case SanityManager.SanityLevel.Low:
                _iconSpriteRenderer.sprite = lowSanity;
                break;
            case SanityManager.SanityLevel.Medium:
                _iconSpriteRenderer.sprite = mediumSanity;
                break;
        }
    }
}