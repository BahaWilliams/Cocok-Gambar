using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardManager))]
public class CardManagerEditor : Editor
{
    SerializedObject manager;
    SerializedProperty _pairAmount;
    SerializedProperty _width;
    SerializedProperty _height;
    SerializedProperty _spritesList;

    private int spriteAmount;
    private float w, h;

    private void OnEnable()
    {
        manager = new SerializedObject(target);
        _pairAmount = manager.FindProperty("pairAmount");
        _width = manager.FindProperty("width");
        _height = manager.FindProperty("height");
        _spritesList = manager.FindProperty("spritesList");
        spriteAmount = _spritesList.arraySize;
    }

    public override void OnInspectorGUI()
    {
        float temporary = _width.intValue * (float)_height.intValue / 2; 

        manager.Update();
        EditorGUILayout.BeginVertical(GUI.skin.box);
        GUI.enabled = false;
        EditorGUILayout.PropertyField(_pairAmount);
        GUI.enabled = true;
        EditorGUILayout.PropertyField(_width);
        EditorGUILayout.PropertyField(_height);
        _pairAmount.intValue = (int)System.Math.Ceiling(temporary);

        if(_pairAmount.intValue > spriteAmount)
        {
            EditorGUILayout.HelpBox("To much card pairs", MessageType.Error);
        }

        if(_width.intValue  < 0)
        {
            _width.intValue = 0;
        }

        if (_height.intValue < 0)
        {
            _height.intValue = 0;
        }

        EditorGUILayout.EndVertical();
        manager.ApplyModifiedProperties();
        DrawDefaultInspector();
    }
}
