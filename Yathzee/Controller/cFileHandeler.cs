using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Yahtzee.Model;
using Yahtzee.util;

namespace Yahtzee.Controller
{
	public class cFileHandeler
	{
		public cFileHandeler()
		{

		}

		#region Settings
		public void CreateSettings(string _path)
		{
			cXmlParser xml = new cXmlParser();
			xml.SetInputFile(_path);
			xml.CreateXmlDeclaration();
			xml.CreateParrentNode("Settings");
				xml.CreateParrentNode("Dice");
					xml.CreateElement("DiceCount", "value", "5", null);
					xml.CreateElement("DiceRolls", "value", "3", null);
				xml.CloseParrentNode();
				xml.CreateParrentNode("Score");
					xml.CreateElement("LeftBonus", "value", "35", null);
					xml.CreateElement("FullHouse", "value", "25", null);
					xml.CreateElement("SmallStraight", "value", "30", null);
					xml.CreateElement("LargeStraight", "value", "40", null);
					xml.CreateElement("Yahtzee", "value", "50", null);
					xml.CreateElement("YahtzeeBonus", "value", "100", null);
				xml.CloseParrentNode();
				xml.CreateElement("LastSaveFileAction", "value", "null", null);
			xml.CloseParrentNode();

			xml.SaveFile();
		}

		public void LoadSettings(string _path, ref uSettings _settings)
		{
			cXmlParser xml = new cXmlParser();
			xml.SetInputFile(_path);
			string root = "Settings//Dice//";
			// dice settings
			_settings.DiceCount = int.Parse(xml.GetXmlNodeAttributeValue(root+"DiceCount", "value"));
			_settings.DiceRolls = int.Parse(xml.GetXmlNodeAttributeValue(root+"DiceRolls", "value"));

			root = "Settings//Score//";
			// score settings
			_settings.LeftBonus = int.Parse(xml.GetXmlNodeAttributeValue(root + "LeftBonus", "value"));
			_settings.FullHouse = int.Parse(xml.GetXmlNodeAttributeValue(root + "FullHouse", "value"));
			_settings.SmallStraight = int.Parse(xml.GetXmlNodeAttributeValue(root + "SmallStraight", "value"));
			_settings.LargeStraight = int.Parse(xml.GetXmlNodeAttributeValue(root + "LargeStraight", "value"));
			_settings.Yahtzee = int.Parse(xml.GetXmlNodeAttributeValue(root + "Yahtzee", "value"));
			_settings.YahtzeeBonus = int.Parse(xml.GetXmlNodeAttributeValue(root + "YahtzeeBonus", "value"));

			_settings.LastSaveFileAction = xml.GetXmlNodeAttributeValue("Settings//LastSaveFileAction", "value");

			if (_settings.LastSaveFileAction == "null")
				_settings.LastSaveFileAction = null;
		}

		public void SaveSettings(ref uSettings _settings, string _path)
		{
			// xml.SelectSingleNode("//reminder/Title").InnerText = "NewValue";
			cXmlParser xml = new cXmlParser();
			xml.SetInputFile(_path);
			string root = "Settings//Dice//";

			// Dice
			xml.GetXmlNodeAttribute(root + "DiceCount", "value").InnerText = _settings.DiceCount.ToString();
			xml.GetXmlNodeAttribute(root + "DiceRolls", "value").InnerText = _settings.DiceRolls.ToString();

			// score settings
			root = "Settings//Score//";
			xml.GetXmlNodeAttribute(root + "LeftBonus", "value").InnerText = _settings.LeftBonus.ToString();
			xml.GetXmlNodeAttribute(root + "FullHouse", "value").InnerText = _settings.FullHouse.ToString();
			xml.GetXmlNodeAttribute(root + "SmallStraight", "value").InnerText = _settings.SmallStraight.ToString();
			xml.GetXmlNodeAttribute(root + "LargeStraight", "value").InnerText = _settings.LargeStraight.ToString();
			xml.GetXmlNodeAttribute(root + "Yahtzee", "value").InnerText = _settings.Yahtzee.ToString();
			xml.GetXmlNodeAttribute(root + "YahtzeeBonus", "value").InnerText = _settings.YahtzeeBonus.ToString();

			xml.GetXmlNodeAttribute("Settings//LastSaveFileActoin", "value").InnerText = _settings.LastSaveFileAction;

			xml.SaveFile();

		}
		#endregion

		#region Save
		public void CreateSave(string _path, mPlayer[] _player, uSettings _settings)
		{
			cXmlParser xml = new cXmlParser();
			xml.SetInputFile(_path);
			xml.CreateXmlDeclaration();

			xml.CreateParrentNode("Yahtzee");

			// save game settings
			xml.CreateParrentNode("Settings");
				xml.CreateParrentNode("Dice");
					xml.CreateElement("DiceCount", "value", _settings.DiceCount.ToString(), null);
					xml.CreateElement("DiceRolls", "value", _settings.DiceRolls.ToString(), null);
					xml.CloseParrentNode();
				xml.CreateParrentNode("Score");
					xml.CreateElement("LeftBonus", "value", _settings.LeftBonus.ToString(), null);
					xml.CreateElement("FullHouse", "value", _settings.FullHouse.ToString(), null);
					xml.CreateElement("SmallStraight", "value", _settings.SmallStraight.ToString(), null);
					xml.CreateElement("LargeStraight", "value", _settings.LargeStraight.ToString(), null);
					xml.CreateElement("Yahtzee", "value", _settings.Yahtzee.ToString(), null);
					xml.CreateElement("YahtzeeBonus", "value", _settings.YahtzeeBonus.ToString(), null);
				xml.CloseParrentNode();
				xml.CreateElement("LastSaveFileAction", "value", _settings.LastSaveFileAction, null);
			xml.CloseParrentNode();
			xml.CreateParrentNode("Players");
			for (int i = 0; i < _player.Length; i++)
			{
				xml.CreateParrentNode("Player");
					xml.CreateElement("PlayerId", "value", _player[i].PlayerId.ToString(), null);
					xml.CreateElement("PlayerName", "value", _player[i].PlayerName.ToString(), null);
					xml.CreateElement("PlayerTurns", "value", _player[i].PlayerTurns.ToString(), null);
					xml.CreateElement("IsAI", "value", _player[i].IsAI.ToString(), null);

					xml.CreateParrentNode("Score");
						// left side scoreboxes
						xml.CreateElement("Ones", "value", _player[i].PlayerScore.Ones, null);
						xml.CreateElement("Twos", "value", _player[i].PlayerScore.Twos, null);
						xml.CreateElement("Threes", "value", _player[i].PlayerScore.Threes, null);
						xml.CreateElement("Fours", "value", _player[i].PlayerScore.Fours, null);
						xml.CreateElement("Fives", "value", _player[i].PlayerScore.Fives, null);
						xml.CreateElement("Sixes", "value", _player[i].PlayerScore.Sixes, null);

						// Right side scoreboxes
						xml.CreateElement("ThreeKind", "value", _player[i].PlayerScore.ThreeKind, null);
						xml.CreateElement("FourKind", "value", _player[i].PlayerScore.FourKind, null);
						xml.CreateElement("FullHouse", "value", _player[i].PlayerScore.FullHouse, null);
						xml.CreateElement("SmallStraight", "value", _player[i].PlayerScore.SmallStraight, null);
						xml.CreateElement("LargeStraight", "value", _player[i].PlayerScore.LargeStraight, null);
						xml.CreateElement("Yahtzee", "value", _player[i].PlayerScore.Yahtzee, null);
						xml.CreateElement("YahtzeeBonusCount", "value", _player[i].PlayerScore.YahtzeeBonusCount.ToString(), null);
						xml.CreateElement("Chance", "value", _player[i].PlayerScore.Chance, null);
					xml.CloseParrentNode();
				xml.CloseParrentNode();
			}
			// close <players
			xml.CloseParrentNode();
			// close root element
			xml.CloseParrentNode();

			xml.SaveFile();
		}

		public void SaveGame(string _path, ref mPlayer _player, uSettings _settings)
		{
			cXmlParser xml = new cXmlParser();
			xml.SetInputFile(_path);
			xml.CreateXmlDeclaration();
			string root = "Yahtzee//Settings//Dice//";

			// Dice
			xml.GetXmlNodeAttribute(root + "DiceCount", "value").InnerText = _settings.DiceCount.ToString();
			xml.GetXmlNodeAttribute(root + "DiceRolls", "value").InnerText = _settings.DiceRolls.ToString();

			// score settings
			root = "Yahtzee//Settings//Score//";
			xml.GetXmlNodeAttribute(root + "LeftBonus", "value").InnerText = _settings.LeftBonus.ToString();
			xml.GetXmlNodeAttribute(root + "FullHouse", "value").InnerText = _settings.FullHouse.ToString();
			xml.GetXmlNodeAttribute(root + "SmallStraight", "value").InnerText = _settings.SmallStraight.ToString();
			xml.GetXmlNodeAttribute(root + "LargeStraight", "value").InnerText = _settings.LargeStraight.ToString();
			xml.GetXmlNodeAttribute(root + "Yahtzee", "value").InnerText = _settings.Yahtzee.ToString();
			xml.GetXmlNodeAttribute(root + "YahtzeeBonus", "value").InnerText = _settings.YahtzeeBonus.ToString();

			xml.GetXmlNodeAttribute("Settings//LastSaveFileActoin", "value").InnerText = _settings.LastSaveFileAction;

			root = "Yahtzee//Players//";
			XmlNode players = xml.GetSingleXamlNode(root);
			root = "Yahtzee//Players//player";
			foreach (var item in players.ChildNodes)
			{
				xml.GetXmlNodeAttribute(root + "PlayerId", "value").InnerText = _player.PlayerId.ToString();
				xml.GetXmlNodeAttribute(root + "PlayerName", "value").InnerText = _player.PlayerName;
				xml.GetXmlNodeAttribute(root + "PlayerTurns", "value").InnerText = _player.PlayerTurns.ToString();
				xml.GetXmlNodeAttribute(root + "IsAI", "value").InnerText = _player.IsAI.ToString();

				root = "Yahtzee//Player//Score//";
				// left side scoreboxes
				xml.GetXmlNodeAttribute(root + "Ones", "value").InnerText = _player.PlayerScore.Ones;
				xml.GetXmlNodeAttribute(root + "Twos", "value").InnerText = _player.PlayerScore.Twos;
				xml.GetXmlNodeAttribute(root + "Threes", "value").InnerText = _player.PlayerScore.Threes;
				xml.GetXmlNodeAttribute(root + "Fours", "value").InnerText = _player.PlayerScore.Fours;
				xml.GetXmlNodeAttribute(root + "Fives", "value").InnerText = _player.PlayerScore.Fives;
				xml.GetXmlNodeAttribute(root + "Sixes", "value").InnerText = _player.PlayerScore.Sixes;

				// Right side scoreboxes
				xml.CreateElement("ThreeKind", "value", _player.PlayerScore.ThreeKind, null);
				xml.CreateElement("FourKind", "value", _player.PlayerScore.FourKind, null);
				xml.CreateElement("FullHouse", "value", _player.PlayerScore.FullHouse, null);
				xml.CreateElement("SmallStraight", "value", _player.PlayerScore.SmallStraight, null);
				xml.CreateElement("LargeStraight", "value", _player.PlayerScore.LargeStraight, null);
				xml.CreateElement("Yahtzee", "value", _player.PlayerScore.Yahtzee, null);
				xml.CreateElement("YahtzeeBonusCount", "value", _player.PlayerScore.YahtzeeBonusCount.ToString(), null);
				xml.CreateElement("Chance", "value", _player.PlayerScore.Chance, null);
			}
		}

		#endregion

		#region Load
		public void LoadGame(string _path, ref mPlayer[] _player, uSettings _settings)
		{
			cXmlParser xml = new cXmlParser();
			xml.SetInputFile(_path);
			string root = "Yahtzee//Settings//Dice//";
			// dice settings
			_settings.DiceCount = int.Parse(xml.GetXmlNodeAttributeValue(root + "DiceCount", "value"));
			_settings.DiceRolls = int.Parse(xml.GetXmlNodeAttributeValue(root + "DiceRolls", "value"));

			root = "Yahtzee//Settings//Score//";
			// score settings
			_settings.LeftBonus = int.Parse(xml.GetXmlNodeAttributeValue(root + "LeftBonus", "value"));
			_settings.FullHouse = int.Parse(xml.GetXmlNodeAttributeValue(root + "FullHouse", "value"));
			_settings.SmallStraight = int.Parse(xml.GetXmlNodeAttributeValue(root + "SmallStraight", "value"));
			_settings.LargeStraight = int.Parse(xml.GetXmlNodeAttributeValue(root + "LargeStraight", "value"));
			_settings.Yahtzee = int.Parse(xml.GetXmlNodeAttributeValue(root + "Yahtzee", "value"));
			_settings.YahtzeeBonus = int.Parse(xml.GetXmlNodeAttributeValue(root + "YahtzeeBonus", "value"));

			_settings.LastSaveFileAction = xml.GetXmlNodeAttributeValue("Settings//LastSaveFileAction", "value");

			if (_settings.LastSaveFileAction == "null")
				_settings.LastSaveFileAction = null;

			root = "Yahtzee//Players//Player";
			XmlNode node = xml.GetSingleXamlNode(root);

			for (int i = 0; i < node.ChildNodes.Count; i++)
			{
				root = "Yahtzee//Players//Player";
				_player[i].PlayerId = int.Parse(xml.GetXmlNodeAttributeValue(root + "PlayerId", "value"));
				_player[i].PlayerName = xml.GetXmlNodeAttributeValue(root + "PlayerName", "value");
				_player[i].PlayerTurns = int.Parse(xml.GetXmlNodeAttributeValue(root + "PlayerTurns", "value"));
				if (xml.GetXmlNodeAttributeValue(root + "PlayerTurns", "value").Equals("ture"))
					_player[i].IsAI = true;
				else
					_player[i].IsAI = false;

				root = "Yahtzee//Players//Player//Score//";

				_player[i].PlayerScore.Ones = xml.GetXmlNodeAttributeValue(root + "Ones", "value");
				_player[i].PlayerScore.Twos = xml.GetXmlNodeAttributeValue(root + "Twos", "value");
				_player[i].PlayerScore.Threes = xml.GetXmlNodeAttributeValue(root + "Threes", "value");
				_player[i].PlayerScore.Fours = xml.GetXmlNodeAttributeValue(root + "Fours", "value");
				_player[i].PlayerScore.Fives = xml.GetXmlNodeAttributeValue(root + "Fives", "value");
				_player[i].PlayerScore.Sixes = xml.GetXmlNodeAttributeValue(root + "Sixes", "value");

				_player[i].PlayerScore.ThreeKind = xml.GetXmlNodeAttributeValue(root + "ThreeKind", "value");
				_player[i].PlayerScore.FourKind = xml.GetXmlNodeAttributeValue(root + "FourKind", "value");
				_player[i].PlayerScore.FullHouse = xml.GetXmlNodeAttributeValue(root + "FullHouse", "value");
				_player[i].PlayerScore.SmallStraight = xml.GetXmlNodeAttributeValue(root + "SmallStraight", "value");
				_player[i].PlayerScore.LargeStraight = xml.GetXmlNodeAttributeValue(root + "LargeStraight", "value");
				_player[i].PlayerScore.Yahtzee = xml.GetXmlNodeAttributeValue(root + "Yahtzee", "value");
				_player[i].PlayerScore.YahtzeeBonusCount = int.Parse(xml.GetXmlNodeAttributeValue(root + "YahtzeeBonusCount", "value"));
				_player[i].PlayerScore.Chance = xml.GetXmlNodeAttributeValue(root + "Chance", "value");
			}
			
		}
		#endregion
	}
}
