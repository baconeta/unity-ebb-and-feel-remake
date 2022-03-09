using UnityEngine;

public class BushController : MonoBehaviour
{
    public Sprite bushSprite;
    public Sprite spikeSprite;

    private SanityManager _gameSanityManager;
    private SpriteRenderer _spriteRenderer;

    private bool _isSpiky;

    // Start is called before the first frame update
    private void Start()
    {
        _isSpiky = false;
        _gameSanityManager = FindObjectOfType<SanityManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        SanityManager.SanityLevel sanityLevel = _gameSanityManager.GetSanityLevel();

        if (sanityLevel != SanityManager.SanityLevel.High)
        {
            MakeSpiky();
        }
        else if (_isSpiky)
        {
            MakeSafe();
        }
    }

    private void MakeSpiky()
    {
        _isSpiky = true;
        _spriteRenderer.sprite = spikeSprite;
        tag = "Enemy";
    }

    private void MakeSafe()
    {
        _isSpiky = false;
        _spriteRenderer.sprite = bushSprite;
        tag = "Untagged";
    }
}