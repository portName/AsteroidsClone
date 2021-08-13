using System;
using System.Collections;
using System.Collections.Generic;
using Basic.Attacks;
using Global;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private  GameObject _prefab;
        [SerializeField] private  float _wait;
        [SerializeField] private  int _maxGameObjectsSpawn;
        [SerializeField] private  float _randomSpawner;
        private List<GameObject> _spawnGameObjects;
        private Coroutine _spawnCoroutine;

        private void Start()
        {
            _spawnGameObjects = new List<GameObject>();
            App.EndGameEvent += DestroyAllListGameObjects;
            _spawnCoroutine = StartCoroutine(Period());
        }

        private void OnDestroy()
        {
            App.EndGameEvent -= DestroyAllListGameObjects;
        }

        private IEnumerator Period()
        {
            while (true)
            {
                yield return new WaitForSeconds(_wait * Random.Range(0.1f, _randomSpawner));
                Create();
            }
        }

        private void Create()
        {
            if (_spawnGameObjects.Count >= _maxGameObjectsSpawn) return;

            var spawnGameObject = Instantiate(_prefab, transform.position, transform.rotation);
            var deleteAble = spawnGameObject.GetComponent<IDeleteAble>();
            if (deleteAble == null)
            {
                Destroy(spawnGameObject);
                throw new Exception("Created not deleted object " + spawnGameObject);
            }

            deleteAble.DeleteGameObjectEvent += DeleteFromListGameObjects;
            _spawnGameObjects.Add(spawnGameObject); 
        }

        private void DeleteFromListGameObjects(GameObject deleteGameObject)
        {
            _spawnGameObjects.Remove(deleteGameObject);
        }
        
        private void DestroyAllListGameObjects()
        {
            StopCoroutine(_spawnCoroutine);
            var _spawnGameObjectsCopy = _spawnGameObjects.ToArray();
            foreach (var deleteGameObject in _spawnGameObjectsCopy)
            {
                deleteGameObject.GetComponent<IDeleteAble>()?.Delete();
            }
            _spawnGameObjects.Clear();
            _spawnCoroutine = StartCoroutine(Period());
        }
    }
}