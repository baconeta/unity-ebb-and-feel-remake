using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] GameObject platformForBounding;
    private float _leftBounds;
    private float _rightBounds;

    // Start is called before the first frame update
    void Start()
    {
        if (platformForBounding != null)
        {
            // The enemy should gets its movement bounds from its set platform
            _leftBounds = platformForBounding.transform.position.x;
            _rightBounds = _leftBounds + platformForBounding.GetComponent<BoxCollider2D>().size.x;
        }
        else
        {
            Debug.Log("Enemy: " + name + ", has no platform bounding set");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_leftBounds);
        Debug.Log(_rightBounds);
    }
}