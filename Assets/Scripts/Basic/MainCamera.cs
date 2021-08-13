using UnityEngine;

namespace Basic
{
    [RequireComponent(typeof(AudioSource))]
    public class MainCamera : MonoBehaviour
    {
        private AudioSource _audioSource;
        public static MainCamera Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

            _audioSource = GetComponent<AudioSource>();
        }

        public AudioSource Audio()
        {
            return _audioSource;
        }
    }
}