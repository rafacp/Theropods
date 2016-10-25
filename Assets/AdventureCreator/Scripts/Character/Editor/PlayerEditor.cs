﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using AC;

[CustomEditor (typeof (Player))]

public class PlayerEditor : CharEditor
{

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		Player _target = (Player) target;
		
		SettingsManager settingsManager = AdvGame.GetReferences ().settingsManager;
		if (settingsManager && settingsManager.hotspotDetection == HotspotDetection.PlayerVicinity)
		{
			EditorGUILayout.BeginVertical ("Button");
			EditorGUILayout.LabelField ("Player settings", EditorStyles.boldLabel);
			_target.hotspotDetector = (DetectHotspots) EditorGUILayout.ObjectField ("Hotspot detector child:", _target.hotspotDetector, typeof (DetectHotspots), true);
			EditorGUILayout.EndVertical ();
		}
	}

}
