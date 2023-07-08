using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator playerAnimation;

    float currentTime = 0;
    const float targetTime = 3;

    void Start()
    {
        playerAnimation = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerAnimation();
    }

    void PlayerAnimation()
    {
        currentTime += Time.deltaTime;

        if (currentTime > targetTime)
            playerAnimation.SetBool("Blinking", true);

        if (playerAnimation.GetCurrentAnimatorStateInfo(0).IsName("New State") && playerAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            playerAnimation.SetBool("Blinking", false);
            currentTime = 0;
        }
    }
}
