using UnityEditor;
using UnityEngine;

namespace CatCode.AlphaModifiers.EditorTools
{
    [CustomEditor(typeof(MonoAlphaModifier))]
    public class MonoAlphaModifierEditor : Editor
    {
        private SerializedProperty _alphaProp;
        private SerializedProperty _strategiesProp;

        private void OnEnable()
        {
            if (target == null)
                return;
            _alphaProp = serializedObject.FindProperty("_alpha");
            _strategiesProp = serializedObject.FindProperty("_alphaStrategies");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            var comp = (MonoAlphaModifier)target;

            EditorGUILayout.LabelField("Dependencies", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            EditorGUI.BeginDisabledGroup(true);

            if (!Application.isPlaying)
            {
                EditorGUILayout.ObjectField("Parent (computed)", comp.EditorParent, typeof(MonoAlphaModifier), true);
                EditorGUILayout.LabelField("Children (computed)");
                EditorGUI.indentLevel++;
                foreach (var c in comp.EditorChildren)
                    EditorGUILayout.ObjectField(c, typeof(MonoAlphaModifier), true);
                EditorGUI.indentLevel--;
            }
            else
            {
                EditorGUILayout.ObjectField("Parent (runtime)", comp.Parent, typeof(MonoAlphaModifier), true);
                EditorGUILayout.LabelField("Children (runtime)");
                EditorGUI.indentLevel++;
                foreach (var c in comp.Children)
                    EditorGUILayout.ObjectField(c, typeof(MonoAlphaModifier), true);
                EditorGUI.indentLevel--;
            }

            EditorGUI.EndDisabledGroup();
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();

            // Настройки — редактируемы только в Edit Mode
            EditorGUILayout.PropertyField(_alphaProp);

            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            EditorGUILayout.PropertyField(_strategiesProp, true);
            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();
        }
    }
}