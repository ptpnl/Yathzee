using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.util
{
	public class uSettings
	{
		int diceCount, diceRolls, minFaceValue, maxFaceValue;
		private int leftBonus, fullHouse;
		private int smallStraight, largeStraight;
		private int yathzee, yathzeeBonus;

		private string lastSaveFileAction;

		public int DiceCount
		{
			get { return diceCount; }
			set { diceCount = value; }
		}
		public int DiceRolls
		{
			get { return diceRolls; }
			set { diceRolls = value; }
		}
		public int MinFaceValue
		{
			get { return minFaceValue; }
			set { minFaceValue = value; }
		}
		public int MaxFaceValue
		{
			get { return maxFaceValue; }
			set { maxFaceValue = value; }
		}
		public int LeftBonus
		{
			get { return leftBonus; }
			set { leftBonus = value; }
		}
		public int FullHouse
		{
			get { return fullHouse; }
			set { fullHouse = value; }
		}
		public int SmallStraight
		{
			get { return smallStraight; }
			set { smallStraight = value; }
		}
		public int LargeStraight
		{
			get { return largeStraight; }
			set { largeStraight = value; }
		}
		public int Yahtzee
		{
			get { return yathzee; }
			set { yathzee = value; }
		}
		public int YahtzeeBonus
		{
			get { return yathzeeBonus; }
			set { yathzeeBonus = value; }
		}

		public string LastSaveFileAction
		{
			get { return lastSaveFileAction; }
			set { lastSaveFileAction = value; }
		}

		public uSettings()
		{
			// set default values
			diceCount = 5;
			diceRolls = 3;
			minFaceValue = 1;
			maxFaceValue = 6;
			leftBonus = 35;
			fullHouse = 25;
			smallStraight = 30;
			largeStraight = 40;
			yathzee = 50;
			yathzeeBonus = 100;

			lastSaveFileAction = null;
		}
	}
}
