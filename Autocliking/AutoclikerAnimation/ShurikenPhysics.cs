using System;
using System.Collections;
using UnityEngine;

namespace Autoclicking
{
    public class ShurikenPhysics : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody = default;

        private Action<GameObject, Vector2, bool> onComplete;

        
        public void Initialize(Vector2 position, float speed, Action<GameObject, Vector2, bool> onComplete)
        {
            transform.position = position;
            transform.rotation = GetComponentInParent<Transform>().rotation;
            gameObject.SetActive(true);
            _rigidbody.velocity = transform.right * speed;
            _rigidbody.angularVelocity = -750f;

            this.onComplete = onComplete;

            StartCoroutine("DestroySelfCoroutine");
        }

        IEnumerator DestroySelfCoroutine()
        {
            float t = 3f;
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
