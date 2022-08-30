using System.Collections.Generic;
using UnityEngine;

namespace HellFireNeko.AudioManagerSystem
{
    [CreateAssetMenu(fileName = "AudioTypes.asset", menuName = "Hell Fire Neko/Audio Manager System/Audio Type Collection")]
    public class AudioTypeCollection : ScriptableObject
    {
        public string this[int index] { get { return types[index]; } }
        public int Count => types.Count;
        public int IndexOf(string item) => types.IndexOf(item);

        public List<string> types;
    }
}
