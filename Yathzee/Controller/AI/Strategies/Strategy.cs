using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Yahtzee.util;

namespace Yahtzee.Controller.AI.Strategies
{
	public class Strategy
	{
		protected cDice dices;
		protected cScore scoreController, simulationScore;
		protected ScoreBox scoreBox;
		protected double currentScorePercentage, minScorePercentage, sucsesChance;

		private bool isSimulatedMode;

		public ScoreBox ScoreBox
		{
			get { return scoreBox; }
			private set { scoreBox = value; }
		}

		protected bool IsSimulatedMode
		{
			get { return isSimulatedMode; }
			set { isSimulatedMode = value; }
		}

		public Strategy(cScore _scoreController, ScoreBox _scoreBox, uSettings _settings)
		{
			dices = new cDice(_settings.DiceCount);
			scoreController = simulationScore = _scoreController;

			ScoreBox = _scoreBox;
			isSimulatedMode = true;

			currentScorePercentage = sucsesChance = 0.0;
			minScorePercentage = 0.5;
		}

		protected int GetFaceValueScore(int _value)
		{
			return scoreController.GetFaceValueScore(_value);
		}

		public double CalculateScorePercentage()
		{
			currentScorePercentage = ScoreBox.CalculateScorePercentage(scoreController.CalculateScore(ScoreBox.Label.Name, false));
			return currentScorePercentage;
		}


	}
}
