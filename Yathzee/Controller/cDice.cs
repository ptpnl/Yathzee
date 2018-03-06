using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

using Yahtzee.Model;
using Yahtzee.util;

namespace Yahtzee.Controller
{
	public class cDice
	{
		private List<mDice> dices;

		public List<mDice> Dices
		{
			get { return dices; }
			set { dices = value; }
		}

		public cDice(int _diceCount)
		{
			dices = new List<mDice>();
			for (int i = 0; i < _diceCount; i++)
				dices.Add(new mDice(i));
			RollDice();
		}

		public void RollDice()
		{
			var rand = new Random();
			foreach (var item in dices)
			{
				if (item.DiceState == eDiceState.Unlocked)
					item.FaceValue = rand.Next(1, 7);
			}
		}

		public void ChangeDiceState(int _index)
		{
			dices.ElementAt(_index).ToggleDiceState();
		}

		public void UnlockDiceState()
		{
			foreach (var item in dices)
				item.DiceState = eDiceState.Unlocked;
		}
	}
}
