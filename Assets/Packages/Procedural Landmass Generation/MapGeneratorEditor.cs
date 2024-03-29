﻿using UnityEngine;
using System.Collections;
using UnityEditor;

#if DEBUG
[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorEditor : Editor {

	public override void OnInspectorGUI() {
		MapGenerator mapGen = (MapGenerator)target;

		if (DrawDefaultInspector ()) {
			if (mapGen.autoUpdate) {
				mapGen.GenerateMapOld();
			}
		}

		if (GUILayout.Button ("Generate")) {
			mapGen.GenerateMapOld ();
		}
	}
}
#endif