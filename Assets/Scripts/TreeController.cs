using UnityEngine;

public class TreeController : MonoBehaviour
{
    public Sprite liveTreeSprite;
    public Sprite deadTreeSprite;

    private SanityManager _gameSanityManager;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;

    private bool _canPass;

    // Start is called before the first frame update
    private void Start()
    {
        _canPass = false;
        _gameSanityManager = FindObjectOfType<SanityManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    private void Update()
    {
        SanityManager.SanityLevel sanityLevel = _gameSanityManager.GetSanityLevel();

        if (sanityLevel == SanityManager.SanityLevel.Low && !_canPass)
        {
            MakePassable();
        }
        else if (_canPass)
        {
            MakeSolid();
        }
    }

    private void MakePassable()
    {
        _canPass = true;
        _spriteRenderer.sprite = deadTreeSprite;
        _boxCollider2D.enabled = false;
    }

    private void MakeSolid()
    {
        _canPass = false;
        _spriteRenderer.sprite = liveTreeSprite;
        _boxCollider2D.enabled = true;
    }
}