using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace Autoclicking
{
    public class AutoclickAnimators : MonoBehaviour, IAutoclickerAnimation
    {
        public AutoclickerType autoclickerType;

        public List<Animator> animators;

        public List<Transform> effectSpawnPoints;

        private int _amount = 1;

        private int _currentAnimatorIndex = 0;

        private Action<double, UnityEngine.Vector2> callback;

        private Autoclicker autoclicker;

        private void Awake()
        {
            //autoclicker = AutoclickersController.instance.autoclickers.Find(x => x.autoclickerType == autoclickerType);
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.2f);
            autoclicker = AutoclickersController.instance.autoclickers.Find(x => x.autoclickerType == autoclickerType);
        }

        public void PlayAnimation(Action<double, Vector2> callback)
        {
            if (_currentAnimatorIndex < _amount)
            {
                animators[_currentAnimatorIndex].SetTrigger("click");
                _currentAnimatorIndex++;
            }
            if (!(_currentAnimatorIndex < _amount))
                _currentAnimatorIndex = 0;

            this.callback = callback;
            Invoke("OnAnimationComplete", 0.13f);
        }

        public void Activate(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                animators[i].SetBool("isActive", true);
            }

            _amount = amount;
        }

        public void Deactivate()
        {
            for (int i = 0; i < animators.Count; i++)
            {
                animators[i].SetBool("isActive", false);
            }

            _amount = 0;
        }

        private void OnAnimationComplete()
        {
            callback?.Invoke(autoclicker.Damage, effectSpawnPoints[_currentAnimatorIndex].position);
        }
    }
    
}
