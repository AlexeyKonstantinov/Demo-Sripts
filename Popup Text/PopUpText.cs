using System;
using TMPro;
using UnityEngine;

public class PopUpText : MonoBehaviour
{
    public TextMeshPro text;

    public Animator animator;

    private Action onCompleteAction;

    public void PlayAnimation(string text, Action onComplete)
    {
        this.text.text = text;
        animator.Play("popup");
        onCompleteAction = onComplete;
    }

    public void EndAnimation()
    {
        gameObject.SetActive(false);
        onCompleteAction?.Invoke();
    }
}
