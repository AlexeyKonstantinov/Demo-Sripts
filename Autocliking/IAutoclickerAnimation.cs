using System;
using UnityEngine;

public interface IAutoclickerAnimation
{
    void PlayAnimation(Action<double, Vector2> callback);
    void Activate(int amount);
    void Deactivate();

}
