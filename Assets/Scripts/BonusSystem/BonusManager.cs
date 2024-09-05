using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [Serializable]
    private struct BonusProbability
    {
        public Bonus bonusPrefab;
        public float probabilityWeight;
    }

    [SerializeField]
    private List<BonusProbability> bonusesPrefabs;
    [HideInInspector, SerializeField]
    private float _weightSum;

    private Main _main;

    public void Init(Main main)
    {
        _main = main;
    }

    private void OnValidate()
    {
        _weightSum = 0f;
        for(int i = 0; i < bonusesPrefabs.Count; i++)
        {
            _weightSum += bonusesPrefabs[i].probabilityWeight;
        }
    }

    public void SpawnRandomBonus(Vector3 position)
    {
        float rundomNum = UnityEngine.Random.Range(0f, _weightSum);
        float currentWeight = 0f;
        for(int i = 0; i < bonusesPrefabs.Count; i++)
        {
            currentWeight += bonusesPrefabs[i].probabilityWeight;
            if(rundomNum <= currentWeight)
            {
                SpawnBonus(bonusesPrefabs[i].bonusPrefab, position);
                break;
            }
        }
    }

    private void SpawnBonus(Bonus prefab, Vector3 position)
    {
        var bonus = Instantiate(prefab, position, Quaternion.identity);
        bonus.Init(_main);
    }
}
