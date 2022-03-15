using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionInventoryController : MonoBehaviour
{
    private bool _isShowingPotion;

    private PlayerController _playerController;
    private SpriteRenderer _spriteRenderer;
    public Sprite sanityPotionSprite;
    public Sprite insanityPotionSprite;
    private Sprite _noPotionSprite;


    // Start is called before the first frame update
    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _noPotionSprite = _spriteRenderer.sprite;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_playerController.isHoldingPotion)
        {
            if (_isShowingPotion)
            {
                return;
            }

            _isShowingPotion = true;
            _spriteRenderer.sprite =
                _playerController.heldPotionSanityToAdd >= 0 ? sanityPotionSprite : insanityPotionSprite;
        }

        else if (_isShowingPotion)
        {
            _spriteRenderer.sprite = _noPotionSprite;
            _isShowingPotion = false;
        }
    }
}