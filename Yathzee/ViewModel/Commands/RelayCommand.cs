using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Yahtzee.ViewModel.Commands
{
	public class RelayCommand : ICommand
	{
		private Action<object> executeCommand;
		private Predicate<object> canExecuteCommand;

		public Predicate<object> CanExecuteCommand
		{
			get { return canExecuteCommand; }
			set { canExecuteCommand = value; }
		}

		private event EventHandler CanExecuteChangedInternal;

		public RelayCommand(Action<object> _execute)
			: this(_execute, DefaultCanExecute)
		{ }

		public RelayCommand(Action<object> _execute, Predicate<object> _canExecute)
		{
			executeCommand = _execute;
			canExecuteCommand = _canExecute;
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
				this.CanExecuteChangedInternal += value;
				OnCanExecuteChanged();
			}

			remove
			{
				CommandManager.RequerySuggested -= value;
				this.CanExecuteChangedInternal -= value;
				OnCanExecuteChanged();
			}
		}

		public bool CanExecute(object _parameter)
		{
			return (canExecuteCommand != null && canExecuteCommand(canExecuteCommand));
		}

		public void Execute(object _parameter)
		{
			executeCommand(_parameter);
		}

		public void OnCanExecuteChanged()
		{
			EventHandler handler = CanExecuteChangedInternal;
			if (handler != null)
				handler.Invoke(this, EventArgs.Empty);
		}

		public void Destroy()
		{
			canExecuteCommand = _ => false;
			executeCommand = _ => { return; };
		}

		private static bool DefaultCanExecute(object _parameter)
		{
			return true;
		}
	}
}
