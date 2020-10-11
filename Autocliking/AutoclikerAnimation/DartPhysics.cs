using System;
using System.Collections;
using UnityEngine;

namespace Autoclicking
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DartPhysics : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody = default;

        private Action<GameObject, Vector2, bool> onComplete;

        public const float lifeTime = 3f;

        void Update()
        {
            if (_rigidbody.velocity.magnitude > 0.2f)
            {
                float angle = Mathf.Atan2(_rigidbody.velocity.y, _rigidbody.velocity.x) * Mathf.Rad2Deg;
                _rigidbody.rotation = angle;
            }
        }

        public void Initialize(Vector2 position, float speed, Action<GameObject, Vector2, bool> onComplete)
        {
            transform.position = position;
            gameObject.SetActive(true);
            _rigidbody.velocity = Vector2.right * speed;

            this.onComplete = onComplete;

            StartCoroutine("DestroySelfCoroutine");
        }

        IEnumerator DestroySelfCoroutine()
        {
            float t = lifeTime;
            while (t > 0)
            {
                t -= Time.deltaTime;
                yield return null;
            }
            onComplete?.Invoke(this.gameObject, _rigidbody.position, false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Bubble")
            {
                StopCoroutine("DestroySelfCoroutine");
                onComplete?.Invoke(this.gameObject, _rigidbody.position, true);
            }
        }
    }
}
