using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yahtzee.util;

namespace Yahtzee.Controller.AI.Strategies.LeftSide
{
	public class EqualFaceValue : Strategy
	{
		private int faceValue;
		public EqualFaceValue(cScore _scoreController, ScoreBox _scoreBox, uSettings _settings) :
			base(_scoreController, _scoreBox, _settings)
		{
			faceValue = _scoreBox.MinScore;
		}


	}
}
