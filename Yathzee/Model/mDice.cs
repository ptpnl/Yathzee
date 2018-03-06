using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Yahtzee.util;

namespace Yahtzee.Model
{
	public class mDice : INotifyPropertyChanged
	{
		private int id, faceValue;
		private eDiceState diceState;

		public int Id
		{
			get { return id; }
			set 
			{
				id = value;
				OnPropertyChanged();
			}
		}
		public int FaceValue
		{
			get { return faceValue; }
			set
			{
				faceValue = value;
				OnPropertyChanged();
			}
		}
		public eDiceState DiceState
		{
			get { return diceState; }
			set
			{
				diceState = value;
				OnPropertyChanged();
			}
		}

		public mDice(int _id)
		{
			Id = _id;
			DiceState = eDiceState.Unlocked;
			FaceValue = 1;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string _propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null && _propertyName != null)
				handler.Invoke(this, new PropertyChangedEventArgs(_propertyName));
		}

		public void ToggleDiceState()
		{
			DiceState = (DiceState == eDiceState.Unlocked) ? eDiceState.Locked : eDiceState.Unlocked;
		}
	}
}
