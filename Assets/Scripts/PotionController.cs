using UnityEngine;

public class PotionController : MonoBehaviour
{
    public bool isSanityBoost = true;
    [Range(0, 100)] public int sanityEffectValue = 50;
    private AudioSource _bottlePickupSound;

    private void Start()
    {
        _bottlePickupSound = GetComponent<AudioSource>();
    }

    public void Pickup()
    {
        _bottlePickupSound.PlayOneShot(_bottlePickupSound.clip);
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        Invoke(nameof(DestroySelf), 2);
    }

    private void DestroySelf()
    {
        Destroy(this);
    }
}