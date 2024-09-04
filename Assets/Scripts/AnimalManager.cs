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
    private Animal animalPrefab;

    public Action OnDeathAllAnimals;

    private List<Animal> _animals = new();

    public void SpawnAnimal()
    {
        var animal = Instantiate<Animal>(animalPrefab, GetRandomFieldPoint(), Quaternion.identity);
        _animals.Add(animal);
        animal.OnDie += OnDieAnimal;
        MoveAnimal(animal);
    }

    private void MoveAnimal(Animal animal)
    {
        animal.GoTo(GetRandomFieldPoint(), MoveAnimal);
    }

    private void OnDieAnimal(Animal animal)
    {
        _animals.Remove(animal);
        if(_animals.Count == 0)
        {
            OnDeathAllAnimals?.Invoke();
        }
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
