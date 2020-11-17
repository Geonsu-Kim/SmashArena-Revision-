using UnityEngine;
using UnityEditor;

public class DupWithoutRename
{
    [MenuItem("GameObject/Duplicate Without Renaming %#d")]
    public static void DuplicateWithoutRenaming()
    {
        foreach (Transform t in Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.ExcludePrefab | SelectionMode.Editable))
        {
            GameObject newObject = null;

            PrefabType pt = PrefabUtility.GetPrefabType(t.gameObject);
            if (pt == PrefabType.PrefabInstance || pt == PrefabType.ModelPrefabInstance)
            {
                // it's an instance of a prefab! Create a new instance of the same prefab!
                Object prefab = PrefabUtility.GetPrefabParent(t.gameObject);
                newObject = (GameObject)PrefabUtility.InstantiatePrefab(prefab);

                // we've got a brand new prefab instance, but it doesn't have the same overrides as our original. Fix that...
                PropertyModification[] overrides = PrefabUtility.GetPropertyModifications(t.gameObject);
                PrefabUtility.SetPropertyModifications(newObject, overrides);
            }
            else
            {
                // not a prefab... so just instantiate it!
                newObject = Object.Instantiate(t.gameObject);
                newObject.name = t.name;
            }

            // okay, prefab is instantiated (or if it's not a prefab, we've just cloned it in the scene)
            // Make sure it's got the same parent and position as the original!
            newObject.transform.parent = t.parent;
            newObject.transform.position = t.position;
            newObject.transform.rotation = t.rotation;

            // tell the Undo system we made this, so you can undo it if you did it by mistake
            Undo.RegisterCreatedObjectUndo(newObject, "Duplicate Without Renaming");
        }
    }


    [MenuItem("GameObject/Duplicate Without Renaming %#d", true)]
    public static bool ValidateDuplicateWithoutRenaming()
    {
        return Selection.GetFiltered(typeof(GameObject), SelectionMode.TopLevel | SelectionMode.ExcludePrefab | SelectionMode.Editable).Length > 0;
    }

}
