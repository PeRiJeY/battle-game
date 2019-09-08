using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;

    // public GameObject skinnerParticleSystem;

    AnimationCharacterManager animatorManager;
    CharacterController characterController;

    private AnimationCharacterManager.CharacterStateEnum previousPlayerState;
    private float timeLastChange = 0;

    // Skinner.SkinnerTrail skinnerTrail;
    float relativeSpeed = 0.0f; // From 0 to 1. 1 is 100% of configured speed
    // float skinnerTrailOriginalSpeed = 0.5f;

    void Start()
    {
        animatorManager = GetComponent<AnimationCharacterManager>();
        characterController = gameObject.GetComponent<CharacterController>();
        // skinnerTrail = skinnerParticleSystem.GetComponent<Skinner.SkinnerTrail>();
        // skinnerTrailOriginalSpeed = skinnerTrail.speedToWidth;

        updateHitCollisions(gameObject, false);
    }


    void Update()
    {
        bool inMovement = false;
        bool isCtrlPressed = false;

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            isCtrlPressed = true;
        }

        animatorManager.characterState = AnimationCharacterManager.CharacterStateEnum.Normal;
        if (Input.GetKey(KeyCode.Space))
        {
            animatorManager.characterState = AnimationCharacterManager.CharacterStateEnum.Jumping;
        }
        if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.E))
        {
            animatorManager.characterState = AnimationCharacterManager.CharacterStateEnum.Punching;
        } else if (Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.Q))
        {
            animatorManager.characterState = AnimationCharacterManager.CharacterStateEnum.Kicking;
        }
        updateColliderStatus();

        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        float mouseAxisX = Input.GetAxis("Mouse X");

        if (verticalAxis != 0 || horizontalAxis != 0 || mouseAxisX != 0)
        {
            inMovement = true;
        }

        // Stop movement when is punching
        if (animatorManager.IsPunching())
        {
            // animatorManager.animationSpeed = 0;
            updateRelativeSpeed(0.0f);
        } else
        {
            if (inMovement && isCtrlPressed)
            {
                updateRelativeSpeed(1.0f);

            }
            else if (inMovement && !isCtrlPressed)
            {
                updateRelativeSpeed(0.5f);
            }
            else
            {
                updateRelativeSpeed(0.0f);

            }

            transform.Rotate(Vector3.up, turnSpeed * 0.5f * mouseAxisX * relativeSpeed * Time.deltaTime);
            Vector3 forwardVector = transform.TransformDirection(Vector3.forward);
            Vector3 rightVector = transform.TransformDirection(Vector3.right);
            characterController.SimpleMove(forwardVector * verticalAxis * moveSpeed * relativeSpeed);
            characterController.SimpleMove(rightVector * horizontalAxis * moveSpeed * relativeSpeed);

            // skinnerTrail.speedToWidth = Mathf.Max(0, skinnerTrailOriginalSpeed * relativeSpeed);
            animatorManager.animationSpeed = relativeSpeed;
            animatorManager.horizontalAxis = Mathf.Lerp(horizontalAxis, animatorManager.horizontalAxis, 0.95f);
        }
        
    }

    private void updateRelativeSpeed(float target)
    {
        relativeSpeed = Mathf.Lerp(target, relativeSpeed, 0.95f);
    }

    private void updateColliderStatus()
    {
        float actualTime = Time.realtimeSinceStartup;
        if (previousPlayerState != animatorManager.characterState && (actualTime - timeLastChange) > 0.5f)
        {
            if (animatorManager.characterState == AnimationCharacterManager.CharacterStateEnum.Punching ||
                animatorManager.characterState == AnimationCharacterManager.CharacterStateEnum.Kicking)
            {
                updateHitCollisions(gameObject, true);
            }
            else
            {
                updateHitCollisions(gameObject, false);
            }

            previousPlayerState = animatorManager.characterState;
            timeLastChange = actualTime;
        }

        
    }
    private void updateHitCollisions(GameObject actual, bool enabled)
    {
        HitBox hitBox = actual.GetComponent<HitBox>();
        Collider collider = actual.GetComponent<Collider>();
        if (hitBox != null && collider != null)
        {
            collider.enabled = enabled;
        }

        // Look at childrens
        foreach (Transform child in actual.transform)
        {
            updateHitCollisions(child.gameObject, enabled);
        }
    }

}
