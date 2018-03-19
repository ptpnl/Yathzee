using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Yahtzee.ViewModel;

namespace Yahtzee.View
{
	/// <summary>
	/// Interaction logic for vYathzee.xaml
	/// </summary>
	public partial class vYathzee : Window
	{
		private vmYahtzee yahtzee;

		public vYathzee()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			yahtzee = (vmYahtzee)this.TryFindResource("vmYahtzee");
			yahtzee.ViewYathzee = this;
			yahtzee.GetScoreBoxes();
			yahtzee.DrawDice();
		}

		public void EnumVisual(Visual parent, ref List<Label> _lblScoreBox, ref List<CheckBox> _checkBox)
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
			{
				// Get the child visual at specified index value.
				Visual childVisual = (Visual)VisualTreeHelper.GetChild(parent, i);

				if (childVisual.GetType() == typeof(Label))
				{
					Label temp = (Label)childVisual;
					if (temp.Name.Contains("txt") && !temp.Name.Contains("Left") && !temp.Name.Contains("Right"))
						_lblScoreBox.Add(temp);	
				}
				else if(childVisual.GetType() == typeof(CheckBox))
					_checkBox.Add((CheckBox)childVisual);
				
				// Recursively enumerate children of the child visual object.
				EnumVisual(childVisual, ref _lblScoreBox, ref _checkBox);
			}
		}

		private void mniShowRules_Click(object sender, RoutedEventArgs e)
		{
			new vYatzeeRules().Show();
		}

		private void mniHowToPlay_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("How to play!!");
		}
	}
}
