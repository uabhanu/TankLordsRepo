using System.Collections.Generic;
using UnityEngine;
using Utils;

public class ObjectPool : MonoBehaviour
{
    #region Variables
    
    private Transform _spawnedObjectsParent;
    protected Queue<GameObject> _objectPool; 
        
    [SerializeField] private bool alwaysDestroy;
    [SerializeField] protected GameObject objectToPool;
    [SerializeField] protected int poolSize = 10;

    #endregion
    
    #region Functions

    private void Awake()
    {
        _objectPool = new Queue<GameObject>();
    }

    private void OnDestroy()
    {
        foreach(var item in _objectPool)
        {
            if(item == null)
            {
                continue;
            }
            
            else if(!item.activeSelf || alwaysDestroy)
            {
                Destroy(item);
            }

            else
            {
                item.GetComponent<DestroyIfDisabledUtil>().SelfDestructionEnabled = true;
            }
        }
    }

    private void CreateObjectParentIfNeeded()
    {
        if(_spawnedObjectsParent == null)
        {
            string name = "ObjectPool_" + objectToPool.name;

            var parentObject = GameObject.Find(name);

            if(parentObject != null)
            {
                _spawnedObjectsParent = parentObject.transform;
            }
            else
            {
                _spawnedObjectsParent = new GameObject(name).transform;
            }
        }
    }

    public GameObject CreateObject()
    {
        CreateObjectParentIfNeeded();

        GameObject spawnedObject = null;

        if(_objectPool.Count < poolSize)
        {
            spawnedObject = Instantiate(objectToPool , transform.position , Quaternion.identity);
            spawnedObject.name = transform.root.name + "_" + objectToPool.name + "_" + _objectPool.Count;
            spawnedObject.transform.SetParent(_spawnedObjectsParent);
            spawnedObject.AddComponent<DestroyIfDisabledUtil>();
        }
        else
        {
            spawnedObject = _objectPool.Dequeue();
            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.rotation = Quaternion.identity;
            spawnedObject.SetActive(true);
        }
        
        _objectPool.Enqueue(spawnedObject);
        return spawnedObject;
    }

    public void Initialize(GameObject objectToPool , int poolSize = 10)
    {
        this.objectToPool = objectToPool;
        this.poolSize = poolSize;
    }

    #endregion
}
