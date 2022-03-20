using UnityEngine;

public class FlowerController : MonoBehaviour
{
    private SanityManager _gameSanityManager;

    public bool insaneFlowers;
    public float cooldownInSeconds;
    private float _timeSinceLastSanityTick;
    public float sanityEffectPerCooldown;
    private bool _playerTouching;
    private float _sanityDrainPerSecond;

    // Start is called before the first frame update
    private void Start()
    {
        _gameSanityManager = FindObjectOfType<SanityManager>();
        _playerTouching = false;
        _timeSinceLastSanityTick = 0.0f;
        _sanityDrainPerSecond = _gameSanityManager.sanityDrainPerSecond;
    }

    private void Update()
    {
        _timeSinceLastSanityTick += Time.deltaTime;

        if (!_playerTouching) return;
        if (_timeSinceLastSanityTick < cooldownInSeconds) return;

        _timeSinceLastSanityTick = 0.0f;

        if (insaneFlowers)
        {
            _gameSanityManager.AddSanity(-sanityEffectPerCooldown + _sanityDrainPerSecond);
        }
        else
        {
            _gameSanityManager.AddSanity(sanityEffectPerCooldown + _sanityDrainPerSecond);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _playerTouching = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            _playerTouching = false;
        }
    }
}