using System;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Yahtzee.Model
{
	public class mPlayerScore : INotifyPropertyChanged
	{
		private string ones, twos, threes, fours, fives, sixes, leftSubScore, leftTotScore;
		private string threeKind, fourKind, fullHouse, smallStraight, largeStraight, yahtzee, chance, rightToTScore;
		private string leftBonus, yahtzeeBonus;
		private int yahtzeeBonusCount;

		#region Left score sextion
		public string Ones
		{
			get { return ones; }
			set 
			{
				ones = value;
				OnPropertyChanged();
			}
		}
		public string Twos
		{
			get { return twos; }
			set
			{
				twos = value;
				OnPropertyChanged();
			}
		}

		public string Threes
		{
			get { return threes; }
			set
			{
				threes = value;
				OnPropertyChanged();
			}
		}

		public string Fours
		{
			get { return fours; }
			set
			{
				fours = value;
				OnPropertyChanged();
			}
		}

		public string Fives
		{
			get { return fives; }
			set
			{
				fives = value;
				OnPropertyChanged();
			}
		}

		public string Sixes
		{
			get { return sixes; }
			set
			{
				sixes = value;
				OnPropertyChanged();
			}
		}

		public string LeftSubScore
		{
			get { return leftSubScore; }
			set
			{
				leftSubScore = value;
				OnPropertyChanged();
			}
		}

		public string LeftTotScore
		{
			get { return leftTotScore; }
			set
			{
				leftTotScore = value;
				OnPropertyChanged();
			}
		}
		#endregion

		#region Right score secion
		public string ThreeKind
		{
			get { return threeKind; }
			set
			{
				threeKind = value;
				OnPropertyChanged();
			}
		}

		public string FourKind
		{
			get { return fourKind; }
			set
			{
				fourKind = value;
				OnPropertyChanged();
			}
		}

		public string FullHouse
		{
			get { return fullHouse; }
			set
			{
				fullHouse = value;
				OnPropertyChanged();
			}
		}

		public string SmallStraight
		{
			get { return smallStraight; }
			set { 
				smallStraight = value;
				OnPropertyChanged();
			}
		}

		public string LargeStraight
		{
			get { return largeStraight; }
			set { 
				largeStraight = value;
				OnPropertyChanged();
			}
		}

		public string Yahtzee
		{
			get { return yahtzee; }
			set
			{
				yahtzee = value;
				OnPropertyChanged();
			}
		}

		public string Chance
		{
			get { return chance; }
			set
			{
				chance = value;
				OnPropertyChanged();
			}
		}

		public string RightToTScore
		{
			get { return rightToTScore; }
			set 
			{
				rightToTScore = value;
				OnPropertyChanged();
			}
		}
		#endregion

		#region Bonmus section
		public string LeftBonus
		{
			get { return leftBonus; }
			set
			{
				leftBonus = value;
				OnPropertyChanged();
			}
		}

		public string YahtzeeBonus
		{
			get { return yahtzeeBonus; }
			set
			{
				yahtzeeBonus = value;
				OnPropertyChanged();
			}
		}

		public int YahtzeeBonusCount
		{
			get { return yahtzeeBonusCount; }
			set
			{
				yahtzeeBonusCount = value;
				OnPropertyChanged();
			}
		}
		#endregion

		public mPlayerScore()
		{
			YahtzeeBonusCount = 0;
			Ones = Twos = Threes = Fours = Fives = Sixes = LeftSubScore = LeftBonus = LeftTotScore = String.Empty;
			ThreeKind = FourKind = FullHouse = SmallStraight = LargeStraight = Chance = Yahtzee = YahtzeeBonus = String.Empty;
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string _propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null && _propertyName != null)
				handler.Invoke(this, new PropertyChangedEventArgs(_propertyName));
		}
	}
}
