using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Newtonsoft.Json;

namespace HellFireNeko.AudioManagerSystem
{
    [DisallowMultipleComponent, AddComponentMenu("Hell Fire Neko/Audio Manager System/Audio Config Manager")]
    public class AudioConfigManager : MonoBehaviour
    {
        /// <summary>
        /// A permanent reference to a loaded instance of this behavior.
        /// </summary>
        public static AudioConfigManager Instance { get; private set; }

        /// <summary>
        /// A <see cref="UnityEvent"/> that is called whenever config is, set, loaded or saved.
        /// </summary>
        public UnityEvent onConfigUpdate;

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            Instance = this;
            DontDestroyOnLoad(this);
            if (File.Exists($"{Application.persistentDataPath}/audio.json"))
            {
                audioSettings = JsonConvert.DeserializeObject<AudioSettings>(File.ReadAllText($"{Application.persistentDataPath}/audio.json"));
            }
            else
            {
                audioSettings = new AudioSettings();
                foreach (var type in audioTypes.types)
                {
                    audioSettings.TypeVolumes.Add(new AudioConfigTypeSetting() { name = type, volume = 1f });
                }
                Save();
            }
            ValidateSettings();
        }

        /// <summary>
        /// The type collection, used by <see cref="AudioSourceType"/> to validate their references.
        /// </summary>
        public AudioTypeCollection audioTypes;

        /// <summary>
        /// The settings of the system, do not modify it directly
        /// </summary>
        public AudioSettings audioSettings;

        private void ValidateSettings()
        {
            bool validateFail = false;
            foreach (var type in audioTypes.types)
            {
                if (!audioSettings.TypeVolumes.Exists(x => x.name == type))
                {
                    audioSettings.TypeVolumes.Add(new AudioConfigTypeSetting() { name = type, volume = 1f });
                    validateFail = true;
                }
            }
            Queue<AudioConfigTypeSetting> Failed = new();
            foreach (var item in audioSettings.TypeVolumes)
            {
                if (!audioTypes.types.Contains(item.name))
                {
                    Failed.Enqueue(item);
                    validateFail = true;
                }
            }
            if (Failed.Count > 0)
            {
                while (Failed.Count > 0)
                {
                    audioSettings.TypeVolumes.Remove(Failed.Dequeue());
                }
            }
            if (validateFail) Save();
        }

        /// <summary>
        /// Used to set the <see cref="AudioSettings.MasterVolume"/>.
        /// </summary>
        /// <param name="value">The value to set it to, should be between 0 and 1.</param>
        public void SetMaster(float value)
        {
            audioSettings.MasterVolume.volume = value;
            onConfigUpdate.Invoke();
        }

        /// <summary>
        /// Used to set one of the items in <see cref="AudioSettings.TypeVolumes"/>.
        /// </summary>
        /// <param name="type">The audio type to set.</param>
        /// <param name="value">The value to set it to, should be between 0 and 1.</param>
        public void SetVolume(string type, float value)
        {
            audioSettings.SetVolume(type, value);
            onConfigUpdate.Invoke();
        }

        /// <summary>
        /// Gets the volume of one of the items in <see cref="AudioSettings.TypeVolumes"/>.
        /// </summary>
        /// <param name="type">The audio type to get.</param>
        /// <returns>The volume, configured to reflect master volume aswell.</returns>
        public float GetVolume(string type)
        {
            return audioSettings.GetVolume(type);
        }

        /// <summary>
        /// Loads the config from the persistant data path into settings.
        /// </summary>
        public void Load()
        {
            audioSettings = JsonConvert.DeserializeObject<AudioSettings>(File.ReadAllText($"{Application.persistentDataPath}/audio.json"));
            onConfigUpdate.Invoke();
        }

        /// <summary>
        /// Saves the config to the persistant data path.
        /// </summary>
        public void Save()
        {
            File.WriteAllText($"{Application.persistentDataPath}/audio.json", JsonConvert.SerializeObject(audioSettings, Formatting.Indented));
            onConfigUpdate.Invoke();
        }
    }

    public class AudioSettings
    {
        public AudioConfigTypeSetting MasterVolume = new() { name = "Master", volume = 1f };
        public List<AudioConfigTypeSetting> TypeVolumes = new();
        public float GetVolume(string type)
        {
            var tvol = TypeVolumes.Find(x => x.name == type).volume;
            return MasterVolume.volume * tvol;
        }
        public void SetVolume(string type, float volume)
        {
            var i = TypeVolumes.FindIndex(x => x.name == type);
            TypeVolumes[i].volume = volume;
        }
    }

    public class AudioConfigTypeSetting
    {
        public string name;
        public float volume;
    }
}
