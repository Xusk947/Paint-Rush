using UnityEngine;
using System;
using UnityEditor;

namespace CustomAttributes
{

    [AttributeUsage(AttributeTargets.Method)]
    public class VoidButtonAttribute : PropertyAttribute
    {
        public readonly string buttonName;

        public VoidButtonAttribute(string buttonName)
        {
            this.buttonName = buttonName;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class VoidButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var mono = target as MonoBehaviour;

            foreach (var method in mono.GetType().GetMethods())
            {
                var attr = Attribute.GetCustomAttribute(method, typeof(VoidButtonAttribute)) as VoidButtonAttribute;

                if (attr != null)
                {
                    if (GUILayout.Button(attr.buttonName))
                    {
                        method.Invoke(mono, null);
                    }
                }
            }
        }
    }
#endif
}
