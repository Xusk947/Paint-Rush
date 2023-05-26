using UnityEngine;
using System;
using UnityEditor;

namespace CustomAttributes
{
    /// <summary>
    /// Attribute used to mark a method as a void button.
    /// </summary>
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
    /// <summary>
    /// Custom editor for MonoBehaviour to display void buttons.
    /// </summary>
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class VoidButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // Get the target MonoBehaviour
            var mono = target as MonoBehaviour;

            // Iterate through all methods of the MonoBehaviour
            foreach (var method in mono.GetType().GetMethods())
            {
                // Check if the method has the VoidButtonAttribute
                var attr = Attribute.GetCustomAttribute(method, typeof(VoidButtonAttribute)) as VoidButtonAttribute;

                if (attr != null)
                {
                    // Display a button for the method
                    if (GUILayout.Button(attr.buttonName))
                    {
                        // Invoke the method when the button is clicked
                        method.Invoke(mono, null);
                    }
                }
            }
        }
    }
#endif
}
