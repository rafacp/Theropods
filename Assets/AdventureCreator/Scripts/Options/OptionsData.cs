/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2016
 *	
 *	"OptionsData.cs"
 * 
 *	This script contains any variables we want to appear in our Options menu.
 * 
 */

namespace AC
{

	/**
	 * A data container for all variables stored as Options data, and those associated with player profiles and save game filenames.
	 */
	[System.Serializable]
	public class OptionsData
	{

		/** The current language, represented by an index of languages in Speech Manager */
		public int language;

		/** True if subtitles are enabled */
		public bool showSubtitles;

		/** The current SFX volume (ranges from 0 to 1) */
		public float sfxVolume;

		/** The current music volume (ranges from 0 to 1) */
		public float musicVolume;

		/** The current speech volume (ranges from 0 to 1) */
		public float speechVolume;

		/** A condensed string representing the values of all Global Variables that link to Options Data */
		public string linkedVariables = "";

		/** A condensed string representing the labels of all save game files */
		public string saveFileNames = "";

		/** A unique identifier of the last save game to be written */
		public int lastSaveID = -1;

		/** The name of the profile associated with this instance */
		public string label;	

		/** A unique identifier */
		public int ID;
		

		/**
		 * The default Constructor.
		 */
		public OptionsData ()
		{
			language = 0;
			showSubtitles = false;
			
			sfxVolume = 0.9f;
			musicVolume = 0.6f;
			speechVolume = 1f;

			linkedVariables = "";
			saveFileNames = "";
			lastSaveID = -1;

			ID = 0;
			label = "Profile " + (ID + 1).ToString ();
		}


		/**
		 * A Constructor in which the basic options values are explicitly set.
		 */
		public OptionsData (int _language, bool _showSubtitles, float _sfxVolume, float _musicVolume, float _speechVolume, int _ID)
		{
			language = _language;
			showSubtitles = _showSubtitles;

			sfxVolume = _sfxVolume;
			musicVolume = _musicVolume;
			speechVolume = _speechVolume;

			linkedVariables = "";
			saveFileNames = "";
			lastSaveID = -1;

			ID = _ID;
			label = "Profile " + (ID + 1).ToString ();
		}


		/**
		 * A Constructor in which the basic options values are copied from another instance of OptionsData.
		 */
		public OptionsData (OptionsData _optionsData, int _ID)
		{
			language = _optionsData.language;
			showSubtitles = _optionsData.showSubtitles;
			
			sfxVolume = _optionsData.sfxVolume;
			musicVolume = _optionsData.musicVolume;
			speechVolume = _optionsData.speechVolume;
			
			linkedVariables = _optionsData.linkedVariables;
			saveFileNames = _optionsData.saveFileNames;
			lastSaveID = -1;

			ID =_ID;
			label = "Profile " + (ID + 1).ToString ();
		}

	}

}