using System.Collections.Generic;
using UnityEngine;

namespace Tatedrez.Libraries
{
    [CreateAssetMenu(menuName = "Tatedrez/Audio Library", fileName = "AudioLibrary")]
    public class AudioLibrary : ScriptableObject
    {
        [Header("Audio Clips")]
        [SerializeField] private List<AudioClip> audioClips;

        public List<AudioClip> AudioClips => audioClips;
    }
}