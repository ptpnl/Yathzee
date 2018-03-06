using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

using Yahtzee.Model;
using Yahtzee.util;
using Yahtzee.View;
using Yahtzee.ViewModel.Commands;
using Yahtzee.Controller;

namespace Yahtzee.ViewModel
{
	public class vmYahtzee : INotifyPropertyChanged
	{
		private vYathzee viewYathzee;

		private uSettings settings;
		private cScore scoreController;

		private ObservableCollection<mPlayer> players;
		private mPlayer currentPlayer, selectedPlayer;

		private cDice dices;

		Brush lblDefaultBackground;

		private List<Label> scoreBoxes;
		private List<CheckBox> checkBoxes;

		private bool cheats, debug;
		private int diceRolls;

		public cDice DiceController
		{
			get { return dices; }
			private set { dices = value; }
		}

		public bool Cheats
		{
			get { return cheats; }
			private set { cheats = value; }
		}
		public bool Debug
		{
			get { return debug; }
			private set { debug = value; }
		}

		public List<Label> ScoreBoxes
		{
			get { return scoreBoxes; }
			set { scoreBoxes = value; }
		}
		public List<CheckBox> CheckBoxes
		{
			get { return checkBoxes; }
			set { checkBoxes = value; }
		}

		public ObservableCollection<mPlayer> Players
		{
			get { return players; }
			set { players = value; }
		}

		List<Rectangle> diceRectangle;
		List<Ellipse> diceFaceValue;

		private bool canRollDice, canAddScore, canNextTurn;

		public mPlayer CurrentPlayer
		{
			get { return currentPlayer; }
			private set
			{
				currentPlayer = value;
				OnPropertyChanged();
			}
		}
		public mPlayer SelectedPlayer
		{
			get { return selectedPlayer; }
			private set 
			{
				selectedPlayer = value;
				OnPropertyChanged();
			}
		}

		#region Commands
		#region Menu Commands
		// Menu File
		private RelayCommand newGamePlayersCommand;
		public RelayCommand NewGamePlayersCommand
		{
			get { return newGamePlayersCommand; }
		}

		private RelayCommand newGamePlayerVsAI;
		public RelayCommand NewGamePlayerVsAICommand
		{
			get { return newGamePlayerVsAI; }
		}

		private RelayCommand continueGameCommand;
		public RelayCommand ContinueGameCommand
		{
			get { return continueGameCommand; }
		}

		private RelayCommand saveGameCommand;
		public RelayCommand SaveGameCommand
		{
			get { return saveGameCommand; }
		}

		private RelayCommand loadGameCommand;
		public RelayCommand LoadGameCommand
		{
			get { return loadGameCommand; }
		}

		// Menu Help
		private RelayCommand toggleCheatsCommand;
		public RelayCommand ToggleCheatsCommand
		{
			get { return new RelayCommand(ToggleCheats); }
		}

		private RelayCommand toggleDebugCommand;
		public RelayCommand ToggleDebugCommand
		{
			get { return new RelayCommand(ToggleCheats); }
		}
		#endregion

		#region Game Commands
		private RelayCommand rollDiceCommand;
		public RelayCommand RollDiceCommand
		{
			get { return rollDiceCommand; }
		}

		private RelayCommand nextTurnCommand;
		public RelayCommand NextTurnCommand
		{
			get { return nextTurnCommand; }
		}

		private RelayCommand addScoreCommand;
		public RelayCommand AddScoreCommand
		{
			get { return addScoreCommand; }
		}

		private RelayCommand addCheatScoreCommand;
		public RelayCommand AddCheatScoreCommand
		{
			get { return addCheatScoreCommand; }
		}

		// Listbox Commands
		private RelayCommand selectedItemChangedCommand;
		public RelayCommand SelectedItemChangedCommand
		{
			get { return selectedItemChangedCommand; }
		}
		#endregion
		#endregion

		public vYathzee ViewYathzee
		{
		  get { return viewYathzee; }
		  set { viewYathzee = value; }
		}

		public vmYahtzee()
		{
			diceRectangle = new List<Rectangle>();
			diceFaceValue = new List<Ellipse>();

			settings = new uSettings();
			scoreController = new cScore(settings);

			players = new ObservableCollection<mPlayer>();
			currentPlayer = new mPlayer();

			dices = new cDice(settings.DiceCount);
			dices.RollDice();
			scoreController.AddDiceFaceValuesToScore(dices.Dices);

			var converter = new BrushConverter();
			lblDefaultBackground = (Brush)converter.ConvertFromString("#FFF0F0F0");

			cheats = debug = false;
			diceRolls = 0;

			#region Commands
			// Menu File Actions
			newGamePlayersCommand= new RelayCommand(NewGameWithPlayerCommand);
			newGamePlayerVsAI = new RelayCommand(NewGameWithAICommand);
			continueGameCommand = new RelayCommand(ContinueGame);
			saveGameCommand = new RelayCommand(SaveGame, param => false);
			loadGameCommand = new RelayCommand(LoadGame);

			// Menu Help Actions
			toggleCheatsCommand = new RelayCommand(ToggleCheats);
			toggleDebugCommand = new RelayCommand(ToggleDebug);

			// game Actions
			rollDiceCommand = new RelayCommand(RollDices, param => canRollDice);
			addScoreCommand = new RelayCommand(AddScore, param => canAddScore);
			addCheatScoreCommand = new RelayCommand(AddCheatScore, param => Cheats);
			nextTurnCommand = new RelayCommand(NextTurn, param => canNextTurn);

			// Listbox Commands
			selectedItemChangedCommand = new RelayCommand(SelectedIndexChanged);

			// command canExecute flags
			canRollDice = canAddScore = canNextTurn = false;
			#endregion
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName]string _propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null && _propertyName != null)
				handler.Invoke(this, new PropertyChangedEventArgs(_propertyName));
		}

		public void GetScoreBoxes()
		{
			scoreBoxes = new List<Label>();
			checkBoxes = new List<CheckBox>();
			viewYathzee.EnumVisual(viewYathzee.grdScoreBoxes, ref scoreBoxes, ref checkBoxes);
		}

		#region Menu Commands
		#region File Menu
		private void NewGameWithPlayerCommand(object obj)
		{
			int playerCount = Convert.ToInt32(obj);
			StartNewGame(playerCount);
			canRollDice = true;
		}

		private void NewGameWithAICommand(object obj)
		{
			MessageBox.Show("Not yet implemented");
		}

		private void ContinueGame(object obj)
		{
			ContinueGame();
		}

		private void SaveGame(object obj)
		{
			SaveGame();
		}

		private void LoadGame(object obj)
		{
			LoadGame(null);
		}
		#endregion

		#region Help Menu
		private void ToggleCheats(object obj)
		{
			MenuItem temp = obj as MenuItem;
			Cheats = !Cheats;
			if (cheats)
				temp.Header = "Disable _Cheats";
			else
				temp.Header = "Enable _Cheats";
		}

		private void ToggleDebug(object obj)
		{
			MenuItem temp = obj as MenuItem;
			Debug = !Debug;
			if (cheats)
				temp.Header = "Disable _Debug";
			else
				temp.Header = "Enable _Debug";
		}
		#endregion
		#endregion

		#region Menu Actions
		#region File Actions
		private void ContinueGame()
		{

		}

		private void SaveGame()
		{

		}

		private void LoadGame()
		{

		}
		#endregion
		#endregion

		#region Game Actions
		private void RollDices(object obj)
		{
			canRollDice = RollDices();
			canAddScore = true;
			AddScoreCommand.CanExecute(new Object());
		}

		private void NextTurn(object obj)
		{
			canRollDice = NextTurn();
			canAddScore = false;
			AddScoreCommand.CanExecute(new Object());
			ResetDiceRectangle();
			canNextTurn = false;
			NextTurnCommand.CanExecute(new Object());
		}

		private void AddScore(object obj)
		{
			AddScore(obj as Label);
			canAddScore = false;
			AddScoreCommand.CanExecute(new Object());
			canNextTurn = true;
			NextTurnCommand.CanExecute(new Object());
		}

		private void AddCheatScore(object obj)
		{
			AddCheatScore(obj as Label);
		}

		private void ToggleDiceState(Rectangle _rect)
		{
			int i = 0;
			foreach (var item in diceRectangle)
			{
				if (item == _rect)
				{
					DiceController.ChangeDiceState(i);
					break;
				}
				++i;
			}

			if (DiceController.Dices.ElementAt(i).DiceState == eDiceState.Unlocked)
				_rect.Stroke = Brushes.Black;
			else
				_rect.Stroke = Brushes.Red;
		}
		#endregion

		private void StartNewGame(int _players, bool _ai = false)
		{
			players.Clear();
			CreatePlayer(_players);

			if (_ai)
				// Inplement AI logic, Yard

			canRollDice = true;
			RollDiceCommand.CanExecute(new Object());

			CurrentPlayer = SelectedPlayer = players.First();
			viewYathzee.lbPlayer.SelectedIndex = currentPlayer.PlayerId - 1;

			RollDices();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		private bool RollDices()
		{
			if (diceRolls < settings.DiceRolls)
			{
				if (!cheats)
					++diceRolls;

				dices.RollDice();
				RedrawFaceValues();
				AddDiceFaceValuesToScore();
				return true;
			}
			return false;
		}

		private bool NextTurn()
		{
			diceRolls = 0;
			scoreController.ResetScoreValues();
			dices.UnlockDiceState();

			foreach (var item in ScoreBoxes)
				item.Background = lblDefaultBackground;

			if (currentPlayer.PlayerId < players.Count)
				CurrentPlayer = SelectedPlayer = players.ElementAt(currentPlayer.PlayerId);
			else
				CurrentPlayer = SelectedPlayer = players.First();

			ViewYathzee.lbPlayer.SelectedIndex = CurrentPlayer.PlayerId - 1;

			return true;
		}

		private void AddDiceFaceValuesToScore()
		{
			scoreController.AddDiceFaceValuesToScore(DiceController.Dices);
		}

		private void AddScore(Label _lbl)
		{
			if (currentPlayer.PlayerId == selectedPlayer.PlayerId && String.IsNullOrEmpty(_lbl.Content.ToString()))
			{
				_lbl.Content = scoreController.CalculateScore(_lbl.Name);
				canAddScore = false;
				AddScoreCommand.CanExecute(new Object());
			}
		}
		
		private void AddCheatScore(Label _lbl)
		{
			if (currentPlayer.PlayerId == selectedPlayer.PlayerId)
				_lbl.Content = scoreController.CalculateScore(_lbl.Name, true);

			canAddScore = false;
			AddScoreCommand.CanExecute(new Object());
		}

		#region Draw dice and faceValues
		public void DrawDice()
		{
			int size = 100;
			int left, top, right, bottom;
			left = right = 8;
			top = bottom = 10;

			for (int i = 0; i < DiceController.Dices.Count; i++)
			{
				CreateRect(i, size, left * (i + 1) + size * i, top, right, bottom);
				ViewYathzee.cvsDiceRoll.Children.Add(diceRectangle.ElementAt(i));
			}

			for (int i = 0; i < DiceController.Dices.Count; i++)
			{
				DrawFaceValue(diceRectangle.ElementAt(i), DiceController.Dices.ElementAt(i).FaceValue);
			}
		}

		/// <summary>
		/// Sets the Rectangle border(.Stroke) to Brushes.Black
		/// </summary>
		private void ResetDiceRectangle()
		{
			foreach (var item in diceRectangle)
				item.Stroke = Brushes.Black;
		}

		/// <summary>
		/// Creates an rectangle to be used for dice
		/// and adds an delegate function to fire an ClickEvent
		/// </summary>
		/// <param name="_i"></param> index of the rectangle
		/// <param name="_size"></param> size of rectagnle
		/// <param name="_left"></param> margen left
		/// <param name="_top"></param> margin top
		/// <param name="_right"></param> margin right
		/// <param name="_bottom"></param> margin bottom
		/// <returns></returns>
		private Rectangle CreateRect(int _i, int _size, int _left, int _top, int _right, int _bottom)
		{
			Rectangle rect = new Rectangle
			{
				Name = "dDice" + _i,
				Width = _size,
				Height = _size,
				Stroke = Brushes.Black,
				StrokeThickness = 1,
				Fill = Brushes.Silver,
				RadiusX = 17.5,
				RadiusY = 17.5,
				HorizontalAlignment = HorizontalAlignment.Left,
				VerticalAlignment = VerticalAlignment.Top,
				Margin = new Thickness(_left, _top, 10, 10),
			};
			rect.MouseLeftButtonDown += delegate
			{
				ToggleDiceState(rect);
			};
			diceRectangle.Add(rect);
			return rect;
		}

		/// <summary>
		/// Creates an ellipse to be used for dice facevalues
		/// and adds an delegate function to fire an ClickEvent
		/// </summary>
		/// <param name="_name"></param> string, derived form the rectangle name
		/// <param name="_size"></param> size of the ellipse
		/// <param name="_left"></param> margin lest
		/// <param name="_top"></param> margin top
		/// <param name="_right"></param> margin right
		/// <param name="_bottom"></param> margin bottom
		/// <returns></returns>
		private Ellipse CreateEllipse(string _name, int _size, double _left, double _top, double _right, double _bottom)
		{
			Ellipse elps = new Ellipse
			{
				Name = _name,
				Width = _size,
				Height = _size,
				Stroke = Brushes.Gray,
				StrokeThickness = 1,
				Fill = Brushes.Gray,
				HorizontalAlignment = HorizontalAlignment.Left,
				VerticalAlignment = VerticalAlignment.Top,
				Margin = new Thickness(_left, _top, _right, _bottom)
			};
			elps.MouseLeftButtonDown += delegate
			{
				int i = Convert.ToInt32(elps.Name.Substring(elps.Name.Length - 1, 1));
				ToggleDiceState(diceRectangle.ElementAt(i));
			};
			diceFaceValue.Add(elps);
			return elps;
		}

		/// <summary>
		/// Draws the dice face value on the ractangle
		/// </summary>
		/// <param name="_rect"></param> ractangle for the ellipses relative pos
		/// <param name="_value"></param> dice facevalue
		private void DrawFaceValue(Rectangle _rect, int _value)
		{
			switch (_value)
			{
				case 1:
					DrawOne(_rect);
					break;
				case 2:
					DrawTwo(_rect);
					break;
				case 3:
					DrawThree(_rect);
					break;
				case 4:
					DrawFour(_rect);
					break;
				case 5:
					DrawFive(_rect);
					break;
				case 6:
					DrawSix(_rect);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// Redraws the ellipses on the screen
		/// </summary>
		public void RedrawFaceValues()
		{
			ClearFaceValue();
			for (int i = 0; i < DiceController.Dices.Count; i++)
				DrawFaceValue(diceRectangle.ElementAt(i), DiceController.Dices.ElementAt(i).FaceValue);
		}

		/// <summary>
		/// Clears the Ellipse on the screen
		/// </summary>
		private void ClearFaceValue()
		{
			foreach (var item in diceFaceValue)
				ViewYathzee.cvsDiceRoll.Children.Remove(item as UIElement);

			diceFaceValue.Clear();
		}

		/// <summary>
		/// Draws the number 1 on a rectangle
		/// </summary>
		/// <param name="_rect"></param> the rectangle for ellipses reletive pos
		/// <param name="_size"></param> int, size of ellipse
		private void DrawOne(Rectangle _rect, int _size = 20)
		{
			string name = "elpsMiddle_";
			Thickness margin = _rect.Margin;
			double left = margin.Left + (_rect.Width / 2) - margin.Right;
			double top = _rect.Height / 2;
			ViewYathzee.cvsDiceRoll.Children.Add(CreateEllipse(name + _rect.Name, _size, left, top, left, left));
		}

		/// <summary>
		/// Draws the number 2 on a rectangle
		/// </summary>
		/// <param name="_rect"></param> the rectangle for ellipses reletive pos
		/// <param name="invert"></param> bool, if ellipse loc needs to be inverted
		/// <param name="_size"></param> int, size of ellipse
		private void DrawTwo(Rectangle _rect, bool invert = false, int _size = 20)
		{
			string name;
			Thickness margin;
			double left, top;
			if (!invert)
			{
				name = "elpsTopLeft_";
				margin = _rect.Margin;
				left = margin.Left + (_rect.Width / 4) - margin.Right;
				top = _rect.Height / 4;
				ViewYathzee.cvsDiceRoll.Children.Add(CreateEllipse(name + _rect.Name, _size, left, top, left, left));

				name = "elpsBottomRight_";
				left = left = margin.Left + (_rect.Width / 4 * 3) - margin.Right;
				top = _rect.Height / 4 * 3;
				ViewYathzee.cvsDiceRoll.Children.Add(CreateEllipse(name + _rect.Name, _size, left, top, left, left));
			}
			else
			{
				name = "elpsTopRight_";
				margin = _rect.Margin;
				left = margin.Left + (_rect.Width / 4 * 3) - margin.Right;
				top = _rect.Height / 4;
				ViewYathzee.cvsDiceRoll.Children.Add(CreateEllipse(name + _rect.Name, _size, left, top, left, left));

				name = "elpsBottomLeft_";
				left = left = margin.Left + (_rect.Width / 4) - margin.Right;
				top = _rect.Height / 4 * 3;
				ViewYathzee.cvsDiceRoll.Children.Add(CreateEllipse(name + _rect.Name, _size, left, top, left, left));
			}

		}

		/// <summary>
		/// Draws the number 3 on a rectangle
		/// </summary>
		/// <param name="_rect"></param> the rectangle for ellipses reletive pos
		private void DrawThree(Rectangle _rect)
		{
			DrawOne(_rect);
			DrawTwo(_rect, true);
		}

		/// <summary>
		/// Draws the number 4 on a rectangle
		/// </summary>
		/// <param name="_rect"></param> the rectangle for ellipses reletive pos
		private void DrawFour(Rectangle _rect)
		{
			DrawTwo(_rect);
			DrawTwo(_rect, true);
		}

		/// <summary>
		/// Draws the number 5 on a rectangle
		/// </summary>
		/// <param name="_rect"></param> the rectangle for ellipses reletive pos
		private void DrawFive(Rectangle _rect)
		{
			DrawOne(_rect);
			DrawFour(_rect);
		}

		/// <summary>
		/// Draws the number 3 on a rectangle
		/// </summary>
		/// <param name="_rect"></param> the rectangle for ellipses reletive pos
		private void DrawSix(Rectangle _rect)
		{
			DrawFour(_rect);

			int size = 20;
			string name = "elpsLeftMiddle_";
			Thickness margin = _rect.Margin;

			double left = margin.Left + (_rect.Width / 4) - margin.Right;
			double top = _rect.Height / 2;
			ViewYathzee.cvsDiceRoll.Children.Add(CreateEllipse(name + _rect.Name, size, left, top, left, left));

			name = "elpsRightMiddle_";
			left = left = margin.Left + (_rect.Width / 4 * 3) - margin.Right;
			top = _rect.Height / 2;
			ViewYathzee.cvsDiceRoll.Children.Add(CreateEllipse(name + _rect.Name, size, left, top, left, left));
		}
		#endregion

		private void NewPlayers(object obj)
		{
			CreatePlayer(1);
		}

		public void CreatePlayer(int _count, string _name = "Player")
		{
			for (int i = 0; i < _count; i++)
				Players.Add(new mPlayer(Players.Count + 1, _name + " " + (Players.Count + 1)));
		}

		public void SelectedIndexChanged(object obj)
		{
			SelectedPlayer = obj as mPlayer;
		}
	}
}
