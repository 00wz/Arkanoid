using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    protected Main _main;
    private const int BALL_LAYER_NUMBER = 3;

    public void Init(Main main)
    {
        _main = main;
    }

    public abstract void ActivateBonus();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == BALL_LAYER_NUMBER)
        {
            ActivateBonus();
            Destroy(gameObject);
        }
    }
}
