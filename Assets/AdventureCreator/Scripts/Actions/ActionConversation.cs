/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2014
 *	
 *	"ActionConversation.cs"
 * 
 *	This action turns on a conversation.
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AC;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class ActionConversation : Action
{
	
	public Conversation conversation;
	
	
	public ActionConversation ()
	{
		this.isDisplayed = true;
		title = "Dialogue: Start conversation";
	}
	
	
	override public float Run ()
	{
		if (conversation)
		{
			conversation.Interact ();
		}
		
		return 0f;
	}
	
	
	override public int End (List<AC.Action> actions)
	{
		return -1;
	}
	
	
	#if UNITY_EDITOR

	override public void ShowGUI ()
	{
		conversation = (Conversation) EditorGUILayout.ObjectField ("Conversation:", conversation, typeof (Conversation), true);
	}
	
	override public string SetLabel ()
	{
		string labelAdd = "";
		
		if (conversation)
		{
			labelAdd = " (" + conversation + ")";
		}
		
		return labelAdd;
	}

	#endif
	
}