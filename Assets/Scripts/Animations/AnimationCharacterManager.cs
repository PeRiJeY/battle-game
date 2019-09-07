using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCharacterManager : MonoBehaviour
{
    Animator animator;
    public enum CharacterStateEnum
    {
        Normal = 0,
        Jumping = 1,
        Punching = 2
    }

    public CharacterStateEnum characterState { get; set; } = CharacterStateEnum.Normal;
    public float animationSpeed { get; set; } = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        animator.SetInteger("State", (int)characterState);
        animator.SetFloat("Speed", animationSpeed);

    }
}
