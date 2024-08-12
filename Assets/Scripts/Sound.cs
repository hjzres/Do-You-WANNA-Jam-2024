using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [CreateAssetMenu(fileName = "New Sound", menuName = "Scriptable Objects/Sound")]
public class Sound : ScriptableObject
{
    public string Name;

    [Range(0.0f, 1.0f)] public float Volume = 1;
    [Range(0.1f, 3.0f)] public float Pitch = 1;
    public bool Loop;

    [HideInInspector] public AudioSource Source;
}
