using System;
using System.Collections;
using UnityEngine;

namespace Basic.Attacks
{
    public class LaserAttack : Attack
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _dulley;
        [SerializeField] private float _waitTime;
        [SerializeField] private AudioClip _fireAudioClip;
        [SerializeField] private float _scaleFireAudioClip;

        private GameObject _bulletCurrent;
        private bool _isReload;

        public override void Fire(AttackInfo info)
        {
            _bulletCurrent = Instantiate(_bullet, _dulley);
            var attack = _bulletCurrent.GetComponent<TriggerAttack>();
            attack._info = info;
            MainCamera.Instance.Audio().PlayOneShot(_fireAudioClip, _scaleFireAudioClip);
        }

        public override bool IsReload()
        {
            return _isReload;
        }

        public override void Reload()
        {
            StartCoroutine(Wait());
        }
        
        private IEnumerator Wait()
        {
            _isReload = true;
            yield return new WaitForSeconds(_waitTime);
            _isReload = false;
            Destroy(_bulletCurrent);
        }
    }
}