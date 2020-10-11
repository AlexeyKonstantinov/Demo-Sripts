using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

namespace Autoclicking { 
    public class Dart : MonoBehaviour, IAutoclickerAnimation
    {
        [SerializeField] private Transform effectSpawnPosition = default;

        [Space(20)]
        [SerializeField] private GameObject dartPrefab = default;
        [SerializeField] private float speed = default;

        private Autoclicker autoclicker;

        private Action<double, Vector2> callback;

        private Queue<GameObject> dartPool = new Queue<GameObject>();
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
            autoclicker = AutoclickersController.instance.autoclickers.Find(x => x.autoclickerType == AutoclickerType.dart);
        }

        [ContextMenu("PlayAnimation")]
        public void PlayAnimation(Action<double, Vector2> callback)
        {
            if (dartPool.Count <= 0)
                return;
            this.callback = callback;
            GameObject dart = dartPool.Dequeue(); 
            dart.GetComponent<DartPhysics>().Initialize(_transform.position, speed, OnCompleteAction);
        }

        void OnCompleteAction(GameObject dart, Vector2 position, bool damage)
        {
            dart.SetActive(false);
            dartPool.Enqueue(dart);

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
                GameObject dart = Instantiate(dartPrefab, _transform);
                dart.SetActive(false);
                dartPool.Enqueue(dart);
            }
        }
    }
}
