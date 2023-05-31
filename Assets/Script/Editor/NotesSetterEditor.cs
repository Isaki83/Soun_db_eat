using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(NotesSetter))]
public class NotesSetterEditor : Editor
{
    NotesSetter _NotesSetter;

    // --- ノーマルノーツ ---
    bool show_NormalNotes = false;
    SerializedProperty _NormalNotesTime;
    bool show_NormalNotesTime = false;
    SerializedProperty _NormalNotesList;
    bool show_NormalNotesList = false;
    SerializedProperty N_RemoveListNumber;
    // --- ロングノーツ ---
    bool show_LongNotes = false;
    SerializedProperty _LongNotesTime;
    bool show_LongNotesTime = false;
    SerializedProperty _LongNotesList;
    bool show_LongNotesList = false;
    SerializedProperty L_RemoveListNumber;

    private void OnEnable()
    {
        _NotesSetter = (NotesSetter)target;
        // --- ノーマルノーツ ---
        _NormalNotesTime = serializedObject.FindProperty("_NormalNotesTime");
        _NormalNotesList = serializedObject.FindProperty("_NormalNotesList");
        N_RemoveListNumber = serializedObject.FindProperty("N_RemoveListNumber");
        // --- ロングノーツ ---
        _LongNotesTime = serializedObject.FindProperty("_LongNotesTime");
        _LongNotesList = serializedObject.FindProperty("_LongNotesList");
        L_RemoveListNumber = serializedObject.FindProperty("L_RemoveListNumber");
    }

    public override void OnInspectorGUI()
    {
        NotesSetter _NotesSetter = (NotesSetter)target;
        if (_NotesSetter == null) { return; }

        base.OnInspectorGUI();

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // ＜ボタン＞ノーマルノーツ設置
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        if (GUILayout.Button("ノーマルノーツ設置")) { _NotesSetter.SetNotes(NotesSetter.NotesType.Normal); }
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // ＜ボタン＞ロングノーツ設置
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        if (GUILayout.Button("ロングノーツ設置")) { _NotesSetter.SetNotes(NotesSetter.NotesType.Long); }

        // --- 空白を追加 ---
        EditorGUILayout.Space(10);

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // ＜ボタン＞オーディオ再生
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        if (GUILayout.Button("オーディオ再生")) { _NotesSetter.AudioPlay(); }
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // ＜ボタン＞オーディオ停止
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        if (GUILayout.Button("オーディオ停止")) { _NotesSetter.AudioStop(); }

        // --- 空白を追加 ---
        EditorGUILayout.Space(10);

        #region ノーマルノーツ
        show_NormalNotes = EditorGUILayout.Foldout(show_NormalNotes, "・ノーマルノーツ");
        if (show_NormalNotes)
        {
            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            // ＜List型＞Time
            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            show_NormalNotesTime = EditorGUILayout.Foldout(show_NormalNotesTime, "時間");
            if (show_NormalNotesTime)
            {
                // Listの要素数を表示
                EditorGUILayout.PropertyField(_NormalNotesTime.FindPropertyRelative("Array.size"));

                // Listの各要素を表示
                for (int i = 0; i < _NormalNotesTime.arraySize; i++)
                {
                    SerializedProperty element = _NormalNotesTime.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(element);
                }
            }

            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            // ＜List型＞List
            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            show_NormalNotesList = EditorGUILayout.Foldout(show_NormalNotesList, "オブジェクト");
            if (show_NormalNotesList)
            {
                // Listの要素数を表示
                EditorGUILayout.PropertyField(_NormalNotesList.FindPropertyRelative("Array.size"));

                // Listの各要素を表示
                for (int i = 0; i < _NormalNotesList.arraySize; i++)
                {
                    SerializedProperty element = _NormalNotesList.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(element);
                }
            }

            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            // ＜Int型＞要素番号
            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            EditorGUILayout.PropertyField(N_RemoveListNumber);

            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            // ＜ボタン＞削除
            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            if (GUILayout.Button("削除")) { _NotesSetter.DeleteObject(NotesSetter.NotesType.Normal); }
        }
        #endregion
        #region ロングノーツ
        show_LongNotes = EditorGUILayout.Foldout(show_LongNotes, "・ロングノーツ");
        if (show_LongNotes)
        {
            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            // ＜List型＞Time
            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            show_LongNotesTime = EditorGUILayout.Foldout(show_LongNotesTime, "時間");
            if (show_LongNotesTime)
            {
                // Listの要素数を表示
                EditorGUILayout.PropertyField(_LongNotesTime.FindPropertyRelative("Array.size"));

                // Listの各要素を表示
                for (int i = 0; i < _LongNotesTime.arraySize; i++)
                {
                    SerializedProperty element = _LongNotesTime.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(element);
                }
            }

            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            // ＜List型＞List
            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            show_LongNotesList = EditorGUILayout.Foldout(show_LongNotesList, "オブジェクト");
            if (show_LongNotesList)
            {
                // Listの要素数を表示
                EditorGUILayout.PropertyField(_LongNotesList.FindPropertyRelative("Array.size"));

                // Listの各要素を表示
                for (int i = 0; i < _LongNotesList.arraySize; i++)
                {
                    SerializedProperty element = _LongNotesList.GetArrayElementAtIndex(i);
                    EditorGUILayout.PropertyField(element);
                }
            }

            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            // ＜Int型＞要素番号
            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            EditorGUILayout.PropertyField(N_RemoveListNumber);

            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            // ＜ボタン＞削除
            //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
            if (GUILayout.Button("削除")) { _NotesSetter.DeleteObject(NotesSetter.NotesType.Long); }
        }
        #endregion

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // ＜ボタン＞配列クリア
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        if (GUILayout.Button("配列クリア")) { _NotesSetter.ListClear(); }

        // --- 空白を追加 ---
        EditorGUILayout.Space(10);

        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        // ＜ボタン＞全オブジェクト生成
        //＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝＝
        if (GUILayout.Button("全オブジェクト生成")) { _NotesSetter.setObject(); }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif
