using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RequireAttributes.Editor
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
#endif
    public class RequireInterfaceAttributeDrawer
#if UNITY_EDITOR
    : PropertyDrawer
#endif
    {

#if UNITY_EDITOR
        private const string REQUIRE_INTERFACE_ATTRIBUTE_DEFINE = "REQUIRE_INTERFACE_ATTRIBUTE";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                const string ERROR_MESSAGE = nameof(RequireInterfaceAttributeDrawer) + " can only be used on object references";
                Debug.LogError(ERROR_MESSAGE);
                return;
            }

            RequireInterfaceAttribute requireInterfaceAttribute = attribute as RequireInterfaceAttribute;

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();

            Object obj = EditorGUI.ObjectField(position, label, property.objectReferenceValue, requireInterfaceAttribute.SearchType, !EditorUtility.IsPersistent(property.serializedObject.targetObject));
            if (!EditorGUI.EndChangeCheck()) return;

            if (obj is GameObject gameObject
                && gameObject.TryGetComponent(requireInterfaceAttribute.RequiredType, out var component))
            {
                property.objectReferenceValue = component;
                EditorGUI.EndProperty();
                return;
            }

            property.objectReferenceValue = requireInterfaceAttribute.RequiredType.IsAssignableFrom(obj.GetType())
                                            ? obj
                                            : null;
            EditorGUI.EndProperty();
        }

        [InitializeOnLoadMethod]
        private static void InitializeOnLoadMethod()
        {
            PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, REQUIRE_INTERFACE_ATTRIBUTE_DEFINE);
        }
#endif

    }
}