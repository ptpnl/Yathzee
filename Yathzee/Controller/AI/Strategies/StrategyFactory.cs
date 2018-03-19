using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Yahtzee.util;

namespace Yahtzee.Controller.AI.Strategies
{
	public class StrategyFactory
	{
		public StrategyFactory()
		{

		}

		public Strategy CreateNewStrategy(cScore _scoreController, ScoreBox _scoreBox, uSettings _settings)
		{
			switch (_scoreBox.Label.Name)
			{
				case "txtOnes":
				case "txtTwos":
				case "txtThrees":
				case "txtFours":
				case "txtFives":
				case "txtSixes":
					return new Strategies.LeftSide.EqualFaceValue(_scoreController, _scoreBox, _settings);
				
				case "txt3AKind":
				case "txt4AKind":
					// return somekind of stugg
				case "txtFullHouse":
					// return somekind of stugg
				case "txtSmStraight":
				case "txtLgStraight":
					// return somekind of stugg
				case "txtChance":
					// return somekind of stugg
				case "txtYahtzee":
					// return somekind of stugg
				default:
					return null;
				
			}
		}
	}
}
