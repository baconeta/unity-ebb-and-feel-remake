using System;
using PlayerNotifications;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    public Sprite liveTreeSprite;
    public Sprite deadTreeSprite;

    private SanityManager _gameSanityManager;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private NotificationController _nc;

    private bool _canPass;

    // Start is called before the first frame update
    private void Start()
    {
        _canPass = false;
        _gameSanityManager = FindObjectOfType<SanityManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _nc = FindObjectOfType<NotificationController>();
    }

    // Update is called once per frame
    private void Update()
    {
        SanityManager.SanityLevel sanityLevel = _gameSanityManager.GetSanityLevel();

        if (!_canPass && sanityLevel == SanityManager.SanityLevel.Low)
        {
            MakePassable();
        }
        else if (_canPass && (sanityLevel == SanityManager.SanityLevel.Medium ||
                              sanityLevel == SanityManager.SanityLevel.High))
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _nc.DisplayNotificationMessage(
                "It seems this tree is really here. Is there a way past it? Around? It seems so.... normal.",
                1, false);
        }
    }
}