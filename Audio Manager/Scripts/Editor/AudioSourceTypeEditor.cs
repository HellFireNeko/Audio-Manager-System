using UnityEditor;

namespace HellFireNeko.AudioManagerSystem
{
    [CustomEditor(typeof(AudioSourceType))]
    public class AudioSourceTypeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var item = target as AudioSourceType;
            
            if (item.audioTypeCollection != null)
            {
                if (item.audioTypeCollection.Count > 0)
                {
                    int indx = EditorGUILayout.Popup(item.sourceType, item.audioTypeCollection.types.ToArray());
                    if (indx != item.sourceType)
                    {
                        Undo.RecordObject(item, "Type changed");
                        item.sourceType = indx;
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("No audio types defined!", MessageType.Error);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No collection found!", MessageType.Error);
            }
        }
    }
}
