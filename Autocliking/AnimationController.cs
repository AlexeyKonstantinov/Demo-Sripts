using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;

namespace Autoclicking
{
    public class AnimationController : MonoBehaviour
    {

        public static AnimationController instance;        

        public AutoclickAnimators pinAnimators;

        public AutoclickAnimators forkAnimators;

        public AutoclickAnimators knifeAnimators;

        public Dart dartAnimators;

        public Shuriken shurikenAnimators; 

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        public void ActivateAnimators(AutoclickerType autoclicker, int amount)
        {
            switch (autoclicker)
            {
                case AutoclickerType.pin:
                    pinAnimators.Activate(amount);
                    break;
                case AutoclickerType.fork:
                    forkAnimators.Activate(amount);
                    break;
                case AutoclickerType.knife:
                    knifeAnimators.Activate(amount);
                    break;
                case AutoclickerType.dart:
                    dartAnimators.Activate(amount);
                    break;
                case AutoclickerType.shuriken:
                    shurikenAnimators.Activate(amount);
                    break;
            }
        }

        public void PlayAnimation(AutoclickerType autoclicker, Action<double, Vector2> callback)
        {
            switch (autoclicker)
            {
                case AutoclickerType.pin:
                    pinAnimators.PlayAnimation(callback);
                    break;
                case AutoclickerType.fork:
                    forkAnimators.PlayAnimation(callback);
                    break;
                case AutoclickerType.knife:
                    knifeAnimators.PlayAnimation(callback);
                    break;
                case AutoclickerType.dart:
                    dartAnimators.PlayAnimation(callback);
                    break;
                case AutoclickerType.shuriken:
                    shurikenAnimators.PlayAnimation(callback);
                    break;
            }

        }

    }
}
