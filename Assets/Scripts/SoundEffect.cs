using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Effect", menuName = "Scriptable Objects/Sound Effect")]
public class SoundEffect : Sound
{
    public AudioClip[] Clips;
    public int RandomIndex() => Random.Range(0, Clips.Length);
}
