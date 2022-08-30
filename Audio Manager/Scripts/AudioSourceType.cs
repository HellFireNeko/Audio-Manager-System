using UnityEngine;

namespace HellFireNeko.AudioManagerSystem
{
    [RequireComponent(typeof(AudioSource)), DisallowMultipleComponent, AddComponentMenu("Hell Fire Neko/Audio Manager System/Audio Source Type")]
    public class AudioSourceType : MonoBehaviour
    {
        protected AudioSource source;

        public AudioTypeCollection audioTypeCollection;
        [HideInInspector] public int sourceType;

        private void Start()
        {
            if (AudioConfigManager.Instance == null)
            {
                Debug.LogWarning("No config manager was loaded!", this);
                return;
            }
            if (AudioConfigManager.Instance.audioTypes != audioTypeCollection)
            {
                Debug.LogWarning("Type collection was not equal to this object!", this);
                return;
            }
            source = GetComponent<AudioSource>();
            source.volume = AudioConfigManager.Instance.GetVolume(audioTypeCollection[sourceType]);
            AudioConfigManager.Instance.onConfigUpdate.AddListener(UpdateVolume);
        }

        public void UpdateVolume()
        {
            source.volume = AudioConfigManager.Instance.GetVolume(audioTypeCollection[sourceType]);
        }
    }
}
