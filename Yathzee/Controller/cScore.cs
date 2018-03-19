using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.Model;
using Yahtzee.util;

namespace Yahtzee.Controller
{
	public class cScore
	{
		private int[] scoreValues;
		private uSettings settings;

		public cScore(uSettings _settings)
		{
			scoreValues = new int[] { 0, 0, 0, 0, 0, 0 };
			settings = _settings;
		}

		public void ResetScoreValues()
		{
			for (int i = 0; i < scoreValues.Length; i++)
				scoreValues[i] = 0;
		}

		public void AddDiceFaceValuesToScore(List<mDice> _dices)
		{
			ResetScoreValues();
			foreach (var item in _dices)
			{
				if (item.FaceValue > 0 && item.FaceValue < 7)
					scoreValues[item.FaceValue - 1] += 1;
				else
					scoreValues[0] += 1;
			}
		}

		#region Score checks
		public int CalculateScore(string _boxName, bool _cheats = false)
		{
			int score = 0;
			switch (_boxName)
			{
				case "txtOnes":
					score = CheckSingleScoreBoxes(1, _cheats);
					break;
				case "txtTwos":
					score = CheckSingleScoreBoxes(2, _cheats);
					break;
				case "txtThrees":
					score = CheckSingleScoreBoxes(3, _cheats);
					break;
				case "txtFours":
					score = CheckSingleScoreBoxes(4, _cheats);
					break;
				case "txtFives":
					score = CheckSingleScoreBoxes(5, _cheats);
					break;
				case "txtSixes":
					score = CheckSingleScoreBoxes(6, _cheats);
					break;
				case "txt3AKind":
					score = CheckOfAKind(3, _cheats);
					break;
				case "txt4AKind":
					score = CheckOfAKind(4, _cheats);
					break;
				case "txtFullHouse":
					score = CheckFullHouse(_cheats);
					break;
				case "txtSmStraight":
					score = CheckStrait(false, _cheats);
					break;
				case "txtLgStraight":
					score = CheckStrait(true, _cheats);
					break;
				case "txtChance":
					score = CalculateChance(_cheats);
					break;
				case "txtYahtzee":
					score = CheckYahtzee(_cheats);
					break;
				default:
					score = 0;
					break;
			}

			return score;
		}

		private int CheckSingleScoreBoxes(int _faceValue, bool _cheats)
		{
			if (_cheats)
				return _faceValue * settings.DiceCount;

			return scoreValues[_faceValue - 1] * _faceValue;
		}

		private int CheckOfAKind(int _value, bool _cheats)
		{
			if (_cheats)
				return _value * settings.MaxFaceValue;

			int i = Array.FindLastIndex(scoreValues, item => item == _value);

			if(i > -1)
				return scoreValues[i] * (i+1);

			return 0;
		}

		private int CheckFullHouse(bool _cheats)
		{
			if ((scoreValues.Contains(3) && scoreValues.Contains(2))|| _cheats)
				return settings.FullHouse;

			return 0;
		}

		private int CheckStrait(bool _isLgStraight, bool _cheats)
		{
			int straightLength, countMod, score;

			// return score if cheats are on
			if (_cheats)
			{
				if (_isLgStraight)
					return settings.LargeStraight;
				else
					return settings.SmallStraight;
			}

			// set values for small or large straight
			if (_isLgStraight)
			{
				straightLength = 5;
				countMod = 1;
				score = settings.LargeStraight;
			}
			else
			{
				straightLength = 4;
				countMod = 2;
				score = settings.SmallStraight;
			}

			if (CountValueInArray(scoreValues, 0) < scoreValues.Length - (straightLength - 1))
			{
				int countLow = 0, countHigh = 0;

				for (int i = 0, j = scoreValues.Length - 1; i < scoreValues.Length - countMod; i++, j--)
				{
					// check for smallStraight
					if (scoreValues[i] > 0)
						++countLow;
					if (scoreValues[j] > 0)
						++countHigh;
					if (countLow == straightLength || countHigh == straightLength)
						return score;
				}
			}

			return 0;
		}

		private int CalculateChance(bool _cheats)
		{
			if (_cheats)
				return settings.DiceCount * settings.MaxFaceValue;
			
			int score = 0;
			for (int i = 0; i < scoreValues.Length; i++)
				score += (scoreValues[i] * (i + 1));

			return score;
		}

		private int CheckYahtzee(bool _cheats)
		{
			if (_cheats)
				return settings.Yahtzee;

			if (scoreValues.Contains(5))
				return settings.Yahtzee;

			return 0;
		}

		private int CountValueInArray(int[] array, int _value)
		{
			int count = 0;
			foreach (int item in array)
			{
				if (item == _value)
					++count;
			}
			return count;
		}
		#endregion

		internal int GetFaceValueScore(int _value)
		{
			return scoreValues[_value - 1] * _value;
		}
	}
}
