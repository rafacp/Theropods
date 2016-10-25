/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2014
 *	
 *	"Conversation.cs"
 * 
 *	This script is handles character conversations.
 *	It generates instances of DialogOption for each line
 *	that the player can choose to say.
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AC;

public class Conversation : MonoBehaviour
{
	
	public bool isTimed = false;
	public float timer = 5f;
	public int defaultOption = 0;
	
	private float startTime;
	private bool isRunning;
	public List<ButtonDialog> options = new List<ButtonDialog>();
	
	private PlayerInput playerInput;
	
	
	void Awake ()
	{
		if (GameObject.FindWithTag (Tags.gameEngine) && GameObject.FindWithTag (Tags.gameEngine).GetComponent <PlayerInput>())
		{
			playerInput = GameObject.FindWithTag (Tags.gameEngine).GetComponent <PlayerInput>();
		}
	}
	
	
	public void Interact ()
	{
		// End the conversation if no options are present
		
		bool onePresent = false;
		foreach (ButtonDialog _option in options)
		{
			if (_option.isOn)
			{
				onePresent = true;
			}
		}

		if (playerInput)
		{
			if (onePresent)
			{
				playerInput.activeConversation = this;
				GameObject.FindWithTag (Tags.persistentEngine).GetComponent <StateHandler>().gameState = GameState.DialogOptions;
			}
			else
			{
				playerInput.activeConversation = null;
			}
		}
		
		if (isTimed)
		{
			startTime = Time.time;
			StartCoroutine (RunDefault ());
		}
	}

	
	public void TurnOn ()
	{
		Interact ();
	}

	
	public void TurnOff ()
	{
		if (playerInput)
		{
			playerInput.activeConversation = null;
		}
	}
	
	
	private IEnumerator RunDefault ()
	{
		yield return new WaitForSeconds (timer);
		
		if (playerInput && playerInput.activeConversation != null && options.Count > defaultOption && defaultOption > -1)
		{
			playerInput.activeConversation = null;

			ButtonDialog option = options[defaultOption];
			
			if (option.returnToConversation)
			{
				option.dialogueOption.conversation = this;
			}
			else
			{
				option.dialogueOption.conversation = null;
			}
			
			option.dialogueOption.Interact ();
		}
	}
	
	
	private IEnumerator RunOptionCo (int i)
	{
		yield return new WaitForSeconds (0.3f);

		if (options[i].dialogueOption)
		{
			if (options[i].returnToConversation)
			{
				options[i].dialogueOption.conversation = this;
			}
			else
			{
				options[i].dialogueOption.conversation = null;
			}

			options[i].dialogueOption.Interact ();
		}
	
		else
		{
			Debug.Log ("No Interaction object found!");
			StateHandler stateHandler = GameObject.FindWithTag (Tags.persistentEngine).GetComponent <StateHandler>();
			stateHandler.gameState = GameState.Normal;
		}
	}
	
	
	public void RunOption (int slot)
	{
		int i = ConvertSlotToOption (slot);
		
		if (playerInput)
		{
			playerInput.activeConversation = null;
		}
		
		StartCoroutine (RunOptionCo (i));
	}
	
	
	public float GetTimeRemaining ()
	{
		return ((startTime + timer - Time.time) / timer);
	}
	
	
	private int ConvertSlotToOption (int slot)
	{
		int foundSlots = 0;
		
		for (int j=0; j < options.Count; j++)
		{
			if (options[j].isOn)
			{
				foundSlots ++;
				if (foundSlots == (slot+1))
				{
					return j;
				}
			}
		}
		
		return 0;
	}

	
	public string GetOptionName (int slot)
	{
		int i = ConvertSlotToOption (slot);

		if (Options.GetLanguage () > 0)
		{
			return (SpeechManager.GetTranslation (options[i].lineID, Options.GetLanguage ()));
		}

		return options[i].label;
	}


	public Texture2D GetOptionIcon (int slot)
	{
		int i = ConvertSlotToOption (slot);
		
		return options[i].icon;
	}
	
	
	public void SetOption (int i, bool flag, bool isLocked)
	{
		if (!options[i].isLocked)
		{
			options[i].isLocked = isLocked;
			options[i].isOn = flag;
		}
	}
	
	
	public int GetCount ()
	{
		int numberOn = 0;
		foreach (ButtonDialog _option in options)
		{
			if (_option.isOn)
			{
				numberOn ++;
			}
		}
		return numberOn;
	}
	
	
	public List<bool> GetOptionStates ()
	{
		List<bool> states = new List<bool>();
		foreach (ButtonDialog _option in options)
		{
			states.Add (_option.isOn);
		}
	
		return states;
	}
	
	
	public List<bool> GetOptionLocks ()
	{
		List<bool> locks = new List<bool>();
		foreach (ButtonDialog _option in options)
		{
			locks.Add (_option.isLocked);
		}
	
		return locks;
	}
	
	
	public void SetOptionStates (List<bool> states)
	{
		int i=0;
		foreach (ButtonDialog _option in options)
		{
			_option.isOn = states[i];
			i++;
		}
	}
	
	
	public void SetOptionLocks (List<bool> locks)
	{
		int i=0;
		foreach (ButtonDialog _option in options)
		{
			_option.isLocked = locks[i];
			i++;
		}
	}
	
	
	private void OnDestroy ()
	{
		playerInput = null;
	}

}
