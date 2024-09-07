using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Vector3 GetRandomInsideUnitCircle()
    {
        Vector3 randomCorclePoint = Random.insideUnitCircle;
        return new Vector3(randomCorclePoint.x, 0f, randomCorclePoint.y);
    }

    private static AudioSource _audioSource2D = null;
    public static void PlayClip2D(AudioClip clip)
    {
        if(_audioSource2D == null)
        {
            var audioListener = GameObject.FindObjectOfType<AudioListener>(includeInactive: true);
            _audioSource2D = audioListener.gameObject.AddComponent<AudioSource>();
        }

        _audioSource2D.PlayOneShot(clip);
    }

}
