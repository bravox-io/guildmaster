using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")]
public class DialogueSets: ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;


    [Header("Dialogue")]
    public Sprite speakerSpriter;
    public string sentence;


    public List<Sentences> dialogue = new List<Sentences>();
}

[System.Serializable]
public class Sentences
{
    public string actorName;
    public Sprite profile;
    public Languages sentence;
}

[System.Serializable]
public class Languages
{
    public string Portugues;
    public string English;
    public string Spanish;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSets))]
public class BuilderEditor: Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueSets ds = (DialogueSets)target;


        Languages l = new Languages();
        l.Portugues = ds.sentence;

        Sentences s = new Sentences();
        s.profile = ds.speakerSpriter;
        s.sentence = l;

        if (GUILayout.Button("Create Dialogue"))
        {
            if(ds.sentence != "")
            {
                ds.dialogue.Add(s);

                ds.speakerSpriter = null;
                ds.sentence = "";
            }

        }
    }
}

#endif