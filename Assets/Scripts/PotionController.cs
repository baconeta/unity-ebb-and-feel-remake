using UnityEngine;

public class PotionController : MonoBehaviour
{
    public bool isSanityBoost = true;
    [Range(0, 100)] public int sanityEffectValue = 50;
}