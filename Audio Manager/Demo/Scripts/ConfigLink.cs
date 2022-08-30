using UnityEngine;
using UnityEngine.UI;

namespace HellFireNeko.AudioManagerSystem.Demo
{
    [RequireComponent(typeof(Slider)), DisallowMultipleComponent, AddComponentMenu("Hell Fire Neko/Audio Manager System/Demo/Config Link")]
    public class ConfigLink : MonoBehaviour
    {
        public string TargetConfig;
        private void Start()
        {
            var slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(OnChange);
            slider.SetValueWithoutNotify(AudioConfigManager.Instance.GetVolume(TargetConfig));
        }

        private void OnChange(float val)
        {
            AudioConfigManager.Instance.SetVolume(TargetConfig, val);
        }
    }
}
