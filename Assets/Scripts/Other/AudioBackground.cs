using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Other
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioBackground : MonoBehaviour
    {
        [SerializeField] private List<AudioClip> _backgroundClips;

        private AudioSource _audioSource;
        private int _currentClip;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(SwitchAudio());
        }

        private IEnumerator SwitchAudio()
        {
            while (true)
            {
                if (_currentClip >= _backgroundClips.Count)
                {
                    _currentClip = 0;
                }
                var clip = _backgroundClips[_currentClip];
                _audioSource.clip = clip;
                _audioSource.Play();
                yield return new WaitForSeconds(clip.length);
                _currentClip += 1;
            }
        }
    }
}