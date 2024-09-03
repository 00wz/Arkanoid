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
        SpawnAnimal();
    }

    private void SpawnAnimal()
    {
        var animal = Instantiate<Animal>(animalPrefab, GetRandomFieldPoint(), Quaternion.identity);
        _animals.Add(animal);
        animal.GoTo(GetRandomFieldPoint(), null);
    }

    private Vector3 GetRandomFieldPoint()
    {
        Vector3 randomCorclePoint = Random.insideUnitCircle;
        randomCorclePoint = new Vector3(randomCorclePoint.x, 0f, randomCorclePoint.y);
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
