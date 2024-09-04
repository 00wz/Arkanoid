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

    private List<Animal> _animals = new();

    private void Start()
    {
        for(int i = 0; i < 10; i++)
        SpawnAnimal();
    }

    private void SpawnAnimal()
    {
        var animal = Instantiate<Animal>(animalPrefab, GetRandomFieldPoint(), Quaternion.identity);
        _animals.Add(animal);
        ManageAnimal(animal);
    }

    private void ManageAnimal(Animal animal)
    {
        animal.GoTo(GetRandomFieldPoint(), ManageAnimal);
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
