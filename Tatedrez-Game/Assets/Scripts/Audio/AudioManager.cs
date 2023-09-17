using Tatedrez.Libraries;
using Tatedrez.Utils;
using UnityEngine;

namespace Tatedrez.Managers
{
    public static class AudioManager
    {
        public enum Sound
        {
            Select,
            Place,
            InvalidPlacement,
            RestartButton
        }

        public static void PlaySound(Sound sound)
        {
            if (AudioPool.Instance)
            {
                var audioObj = AudioPool.Instance.GetSoundObject();
                if (audioObj != null)
                {
                    audioObj.gameObject.SetActive(true);
                    audioObj.PlayOneShot(GetAudioClip(sound));
                }
            }
        }

        private static AudioClip GetAudioClip(Sound sound)
        {
            foreach (AudioClip soundAudioClip in LibrariesHandler.GetAudioLibrary().AudioClips)
            {
                if (soundAudioClip.name.Equals(sound.ToString()))
                {
                    return soundAudioClip;
                }
            }

            return null;
        }
    }
}