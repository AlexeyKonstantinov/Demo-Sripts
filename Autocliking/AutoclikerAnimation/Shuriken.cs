using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace Autoclicking
{
    public class Shuriken : MonoBehaviour, IAutoclickerAnimation
    {
        [SerializeField] private Transform effectSpawnPosition = default;

        [Space(20)]
        [SerializeField] private GameObject shurikenPrefab = null;

        [SerializeField] private float speed = default;       

        private Autoclicker autoclicker;

        private Action<double, Vector2> callback;

        private Queue<GameObject> shurikenPool = new Queue<GameObject>();
        private const int poolSize = 5;

        private Transform _transform;
        private void Awake()
        {
            _transform = transform;
            PreparePool();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.3f);
            autoclicker = AutoclickersController.instance.autoclickers.Find(x => x.autoclickerType == AutoclickerType.shuriken);
        }

        [ContextMenu("PlayAnimation")]
        public void PlayAnimation(Action<double, Vector2> callback)
        {
            if (shurikenPool.Count <= 0)
                return;
            this.callback = callback;
            GameObject dart = shurikenPool.Dequeue();
            dart.GetComponent<ShurikenPhysics>().Initialize(_transform.position, speed, OnCompleteAction);
        }

        void OnCompleteAction(GameObject shuriken, Vector2 position, bool damage)
        {
            shuriken.SetActive(false);
            shurikenPool.Enqueue(shuriken);

            if (damage)
            {
                callback?.Invoke(autoclicker.Damage, effectSpawnPosition.position);
            }
        }

        public void Activate(int amount)
        {

        }

        public void Deactivate()
        {

        }
        void PreparePool()
        {
            for (int i = 0; i < poolSize; i++)
            {
                GameObject shuriken = Instantiate(shurikenPrefab, _transform);
                shuriken.SetActive(false);
                shurikenPool.Enqueue(shuriken);
            }
        }
    }
}
