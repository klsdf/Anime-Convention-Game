using Febucci.UI.Effects;
using UnityEditor;
using UnityEngine;

namespace Febucci.UI.Core
{
    [CustomPropertyDrawer(typeof(ColorCurveProperty))]
    public class ColorCurvePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty enabled = property.FindPropertyRelative(nameof(ColorCurve.enabled));
            Rect lineByLine = position;
            lineByLine.height = EditorGUIUtility.singleLineHeight;
            
            Rect enabledRect = new Rect(lineByLine.xMax-EditorGUIUtility.singleLineHeight, lineByLine.y, EditorGUIUtility.singleLineHeight, lineByLine.height);
            Rect expandedRect = new Rect(lineByLine.x, lineByLine.y, position.width-EditorGUIUtility.singleLineHeight, lineByLine.height);
            enabled.boolValue = GUI.Toggle(enabledRect, enabled.boolValue, GUIContent.none);
            GUI.color = enabled.boolValue ? Color.white : Color.gray;
            
            property.isExpanded = EditorGUI.Foldout(expandedRect, property.isExpanded, label, true);
            GUI.color = Color.white;
            if(!property.isExpanded) return;
            GUI.enabled = enabled.boolValue;

            // -- inner area smaller ---
            position.y = lineByLine.yMax;
            position.height -= lineByLine.height;
            position.x += 15;
            position.width-=15;

            lineByLine.x = position.x;
            lineByLine.width = position.width;
            lineByLine.y += lineByLine.height;
            
            EditorGUI.PropertyField(lineByLine, property.FindPropertyRelative(nameof(ColorCurve.colorOverTime)));
            lineByLine.y += lineByLine.height;
            EditorGUI.PropertyField(lineByLine, property.FindPropertyRelative(nameof(ColorCurve.duration)));
            lineByLine.y += lineByLine.height;
            EditorGUI.PropertyField(lineByLine, property.FindPropertyRelative(nameof(ColorCurve.waveSize)));
            GUI.enabled = true;
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {           
            return (property.isExpanded)
                    ? EditorGUIUtility.singleLineHeight * 4
                    : EditorGUIUtility.singleLineHeight;
        }
    }
}