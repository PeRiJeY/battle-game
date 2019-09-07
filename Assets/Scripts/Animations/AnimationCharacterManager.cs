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
        Punching = 2,
        Kicking = 3
    };

    enum PunchTypes
    {
        LEFT = 1,
        RIGHT = 2,
        MUTANT = 3,
        ZOMBIE = 4,
        MMA_KICK01 = 5,
        MMA_KICK02 = 6,
        NUMBER_OF_TYPES = 7
    };

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

        PunchTypes nextState;
        if (characterState == AnimationCharacterManager.CharacterStateEnum.Kicking)
        {
            nextState = (PunchTypes)Random.Range((int)PunchTypes.MMA_KICK01, (int)PunchTypes.NUMBER_OF_TYPES);
            animator.SetInteger("PunchType", (int)nextState);
        }
        else if (characterState == AnimationCharacterManager.CharacterStateEnum.Punching)
        {
            nextState = (PunchTypes)Random.Range(1, (int)PunchTypes.MMA_KICK01);
            animator.SetInteger("PunchType", (int)nextState);
        }

        animator.SetInteger("State", (int)characterState);
        animator.SetFloat("Speed", animationSpeed);

    }
}
