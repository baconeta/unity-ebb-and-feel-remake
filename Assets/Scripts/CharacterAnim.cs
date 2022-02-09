using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    private Animator _anim;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            _anim.SetBool(IsRunning, true);
        }
        else
        {
            _anim.SetBool(IsRunning, false);
        }
    }
}