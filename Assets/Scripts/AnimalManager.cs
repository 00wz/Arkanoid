using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    [SerializeField]
    private Vector3 centerOfField;
    [SerializeField]
    private float radiusOfField;
    [SerializeField]
    private bool drowField;
    [SerializeField]
    private float spawnIntervalSeconds = 0.2f;
    [SerializeField]
    private AudioClip spawnAnimalAudioClip;

    public int AnimalsCount => _animals.Count;
    public Action<Animal> OnDeathAnimal;

    private List<Animal> _animals = new();

    public async UniTask SpawnAnimalWave(AnimalWave animalWave)
    {
        for(int i = 0; i < animalWave.AnimalCounts.Count; i++)
        {
            for(int k = 0; k < animalWave.AnimalCounts[i].Count; k++)
            {
                SpawnAnimal(animalWave.AnimalCounts[i].AnimalPrefab);
                await UniTask.Delay(TimeSpan.FromSeconds(spawnIntervalSeconds));
            }
        }
    }

    public void SpawnAnimal(Animal animalPrefab)
    {
        var animal = Instantiate<Animal>(animalPrefab, GetRandomFieldPoint(), Quaternion.identity);
        _animals.Add(animal);
        animal.OnDie += OnDieAnimal;
        MoveAnimal(animal);
        Utils.PlayClip2D(spawnAnimalAudioClip);
    }

    private void MoveAnimal(Animal animal)
    {
        animal.GoTo(GetRandomFieldPoint(), MoveAnimal);
    }

    private void OnDieAnimal(Animal animal)
    {
        _animals.Remove(animal);
        OnDeathAnimal?.Invoke(animal);
    }

    private Vector3 GetRandomFieldPoint()
    {
        var randomCorclePoint = Utils.GetRandomInsideUnitCircle();
        return centerOfField + (randomCorclePoint * radiusOfField);
    }

    private void OnDrawGizmos()
    {
        if(drowField)
        {
            Gizmos.color = UnityEngine.Color.blue;
            Gizmos.DrawWireSphere(centerOfField, radiusOfField);
        }
    }
}
