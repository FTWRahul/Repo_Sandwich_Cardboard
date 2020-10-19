using UnityEngine;

namespace General
{
    public class DestroyAfterDelay : MonoBehaviour
    {
        [SerializeField] private float delay;

        private void Awake()
        {
            Destroy(gameObject, delay);
        }
    }
}
