using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using Yahtzee.ViewModel;
using Yahtzee.Controller.AI.Strategies;
using Yahtzee.util;

namespace Yahtzee.Controller.AI
{
	public class AiHandeler
	{
		protected Strategy currentStrategy;
		protected List<Strategy> strategies;
		private StrategyFactory factory;

		private Dictionary<string, ScoreBox> scoreBoxes;

		vmYahtzee yahtzee;

		public AiHandeler(vmYahtzee _yahtzeeVM)
		{
			yahtzee = _yahtzeeVM;
			scoreBoxes = yahtzee.ScoreBoxes;
			strategies = new List<Strategy>();
			factory = new StrategyFactory();
		}

		public void AiHandelTurn()
		{
			if (currentStrategy != null)
				AiSimulateTurn();
			
			AiRollDice();


		}

		private void AiWaitAfterAction(int _waitTime = 1200)
		{
			Thread.Sleep(_waitTime);
		}

		private void AiRollDice()
		{
			if (yahtzee.RollDiceCommand.CanExecute(new Object()))
			{
				yahtzee.RollDiceCommand.Execute(new Object());
				AiWaitAfterAction();
			}
		}

		private void AiToggleDice()
		{
			AiWaitAfterAction();
		}

		private void AiAddScore()
		{
			if (yahtzee.AddScoreCommand.CanExecute(new Object()))
			{
				yahtzee.AddScoreCommand.Execute(new Object());
				AiWaitAfterAction();
			}
		}

		private void AiAddCheatScore()
		{
			if (yahtzee.AddCheatScoreCommand.CanExecute(new Object()))
			{
				yahtzee.AddScoreCommand.Execute(new Object());
				AiWaitAfterAction();
			}
		}

		private void AINextTurn()
		{
			if (yahtzee.NextTurnCommand.CanExecute(new Object()))
			{
				yahtzee.NextTurnCommand.Execute(new Object());
				AiWaitAfterAction();
			}
		}

		private void AiSelectScoreBox()
		{
			
		}

		private void AiSimulateTurn()
		{
			strategies.Clear();
			foreach (var item in yahtzee.ScoreableBoxes)
			{
				if (String.IsNullOrEmpty(item.Value.Label.Content.ToString()))
				{
					strategies.Add(factory.CreateNewStrategy(yahtzee.ScoreController, item.Value, yahtzee.Settings));
				}
			}
		}
	}
}
