using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationOnEnable : MonoBehaviour
{
    Animator anim;
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnEnable()
    {
        anim.Play("Spawn");
    }
}
