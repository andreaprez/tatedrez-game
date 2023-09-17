using System.Collections.Generic;
using UnityEngine;

namespace Tatedrez.Utils
{
    public class AudioPool : MonoBehaviour
    {
        public static AudioPool Instance;

        [SerializeField] private GameObject container;
        [SerializeField] private AudioSource prefab;
        [SerializeField] private int amount;

        private List<AudioSource> soundObjects = new List<AudioSource>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            FillPool();
        }

        private void FillPool()
        {
            for (int i = 0; i < amount; i++)
            {
                var tmpInstance = Instantiate(prefab, container.transform);
                tmpInstance.gameObject.SetActive(false);
                soundObjects.Add(tmpInstance);
            }
        }

        public AudioSource GetSoundObject()
        {
            if (soundObjects != null && soundObjects.Count > 0)
            {
                AudioSource selected = null;
                for (int i = 0; i < soundObjects.Count; i++)
                {
                    if (!soundObjects[i].gameObject.activeInHierarchy)
                    {
                        selected = soundObjects[i];
                    }
                }

                if (selected != null)
                {
                    soundObjects.Remove(selected);
                    soundObjects.Insert(0, selected);
                    return selected;
                }

                // If every object in the pool is active, it will return the one that has been active for longer.
                return soundObjects[soundObjects.Count - 1]; 
            }

            return null;
        }
    }
}