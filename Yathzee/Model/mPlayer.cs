using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Yahtzee.Model
{
	public class mPlayer : INotifyPropertyChanged
	{
		private int playerId, playerTurns;
		private string playerName;
		private mPlayerScore playerScore;

		public int PlayerId
		{
			get { return playerId; }
			private set { playerId = value; }
		}
		public string PlayerName
		{
			get { return playerName; }
			set
			{
				playerName = value;
				OnPropertyChanged("PlayerName");
			}
		}
		public int PlayerTurns
		{
			get { return playerTurns; }
			set
			{
				playerTurns = value;
				OnPropertyChanged("PlayerTurns");
			}
		}
		public mPlayerScore PlayerScore
		{
			get { return playerScore; }
			set
			{
				playerScore = value;
				OnPropertyChanged("Score");
			}
		}
		
		public mPlayer() :
			this(-1, "player")
		{
			
		}

		public mPlayer(int _id = -1, string _name = "Player")
		{
			PlayerId = _id;
			PlayerName = _name;
			PlayerTurns = 0;
			PlayerScore = new mPlayerScore();
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
