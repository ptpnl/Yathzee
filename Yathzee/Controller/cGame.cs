using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yahtzee.Controller
{
	public class cGame
	{
		bool cheats;

		public bool Cheats
		{
			get { return cheats; }
			private set { cheats = value; }
		}

		public cGame()
		{
			Cheats = false;
		}

		public void ToggleCheats()
		{
			Cheats = !cheats;
		}
	}
}
