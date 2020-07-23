using UnityEngine;
using UnityEditor;

public class EditorTools : Editor {
	[MenuItem("xdegtyarev/BreakPrefabLink")]
	public static void BreakPrefabLinkFunc (MenuCommand command) {
		PrefabUtility.DisconnectPrefabInstance(Selection.activeGameObject);
		Debug.Log(("Breaking prefab link for " + Selection.activeGameObject));
	}

	[MenuItem("xdegtyarev/ClearPlayerPrefs")]
	public static void ClearPlayerPrefs (MenuCommand command) {
		PlayerPrefs.DeleteAll();
		Debug.Log(("Cleared playerprefs"));
	}
}
