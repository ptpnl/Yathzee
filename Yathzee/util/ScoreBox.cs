using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Yahtzee.util
{
	public class ScoreBox
	{
		Label label;
		int minScore, maxScore;
		Action action;

		public Label Label
		{
			get { return label; }
			private set { label = value; }
		}

		public int MinScore
		{
			get { return minScore; }
			private set { minScore = value; }
		}

		public int MaxScore
		{
			get { return maxScore; }
			private set { maxScore = value; }
		}

		public ScoreBox(Label _lbl, int _minScore = 0, int _maxScore = 0)
		{
			label = _lbl;
			minScore = _maxScore;
			if (_maxScore == 0)
				maxScore = minScore;
			else
				maxScore = _maxScore;
		}

		public double CalculateScorePercentage(int _score = 0)
		{
			if (_score == 0)
				return 0;
			else
				return _score / maxScore;
		}
	}
}
