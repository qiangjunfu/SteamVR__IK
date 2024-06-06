using UnityEditor;
using UnityEngine;

public class FindReferences : EditorWindow
{
    GameObject targetObject;

    [MenuItem("Tools/Find References")]
    public static void ShowWindow()
    {
        GetWindow<FindReferences>("Find References");
    }

    void OnGUI()
    {
        GUILayout.Label("Find References to Object", EditorStyles.boldLabel);
        targetObject = EditorGUILayout.ObjectField("Target Object", targetObject, typeof(GameObject), true) as GameObject;

        if (GUILayout.Button("Find References"))
        {
            FindAllReferences();
        }
    }

    void FindAllReferences()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target object is not set.");
            return;
        }

        string targetName = targetObject.name;
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            Component[] components = obj.GetComponents<Component>();
            foreach (Component component in components)
            {
                SerializedObject so = new SerializedObject(component);
                SerializedProperty sp = so.GetIterator();

                while (sp.NextVisible(true))
                {
                    if (sp.propertyType == SerializedPropertyType.ObjectReference)
                    {
                        if (sp.objectReferenceValue == targetObject)
                        {
                            Debug.Log($"Found reference in {obj.name} ({component.GetType()})", obj);
                        }
                    }
                }
            }
        }
    }
}
