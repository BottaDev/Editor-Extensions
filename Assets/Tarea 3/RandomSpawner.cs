using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RandomSpawner : MonoBehaviour
{
    public List<GameObject> objectsToSpawn;
    public int objectsAmount;

    private List<GameObject> _spawnedObjs;

    public void SpawnObjects()
    {
        for (int i = 0; i < objectsAmount; i++)
        {
            bool canSpawn = false;
            
            while (!canSpawn)
            {
                Vector3 position = new Vector3(Random.insideUnitSphere.x * 40, Random.insideUnitSphere.y * 40, Random.insideUnitSphere.z * 40);

                GameObject tempObj = new GameObject("temporary cube with collider (SpawnLocationValid)");
                tempObj.transform.position = position;
                var bCol = tempObj.AddComponent<SphereCollider>();
                bCol.isTrigger = true;
                bCol.radius = 1.5f;

                Collider[] colls = Physics.OverlapSphere(position, bCol.radius, 10);

                if (colls.Length == 0)
                {
                    DestroyImmediate(tempObj);

                    GameObject spawnedObj = Instantiate(objectsToSpawn[Random.Range(0, objectsToSpawn.Count)], position, new Quaternion(0, Random.Range(0, 361), 0, 0));

                    _spawnedObjs.Add(spawnedObj);
                    
                    canSpawn = true;
                }
                else
                {
                    DestroyImmediate(tempObj);

                    canSpawn = false;
                }
            }
        }
    }

    public void DestroyAllObjects()
    {
        if (_spawnedObjs == null)
            return;
        
        if (Application.isEditor)
        {
            foreach (GameObject obj in _spawnedObjs)
            {
                DestroyImmediate(obj);
            }
        }
    }
}
