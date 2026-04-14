using System.Collections;
using UnityEngine;

public class UITextPopup : MonoBehaviour
{
    private Animator anim;
    private AnimatorStateInfo currentState;

    [SerializeField] private float aliveTime = 3.5f;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        
    }

    private void OnEnable()
    {
        AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
        Destroy(gameObject, currentState.length);
    }

    private void Start()
    {
        
    }

   
}
