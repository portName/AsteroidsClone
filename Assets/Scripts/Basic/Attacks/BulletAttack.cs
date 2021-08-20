using System.Collections;
using Basic.Attacks.Info;
using UnityEngine;

namespace Basic.Attacks
{
    public class BulletAttack : Attack
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private Transform _dulley;
        [SerializeField] private float _waitTime;
        [SerializeField] private AudioClip _fireAudioClip;
        [SerializeField] private float _scaleFireAudioClip;

        private bool _isReload;
        
        public override void Fire(AttackInfo info)
        {
           var instance = Instantiate(_bullet, _dulley.position   , _dulley.rotation);
           var attack = instance.GetComponent<TriggerAttack>();
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
        }
    }
}