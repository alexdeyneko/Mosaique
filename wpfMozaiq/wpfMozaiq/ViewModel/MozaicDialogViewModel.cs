using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using wpfMozaiq.Models;

namespace wpfMozaiq.ViewModel
{
	public class MozaicDialogViewModel : ViewModelBase, INotifyPropertyChanged
	{
		public MozaicDialogViewModel()
		{
			
		}

		private ICommand _okCommand;
		public ICommand OkCommand
		{
			get => _okCommand ?? (_okCommand = new RelayCommand(() =>
			{
				MessengerInstance.Send<NotificationMessage<string>>(new NotificationMessage<string>("DeleteMozaic", "MozaicDialogViewModel"));
				Messenger.Default.Send("CloseMozaicDialogViewModel");
			}));
		}

		private ICommand _cancelCommand;
		public ICommand CancelCommand
		{
			get => _cancelCommand ?? (_cancelCommand = new RelayCommand(() =>
			{
				MessengerInstance.Send<NotificationMessage<string>>(new NotificationMessage<string>("NoDeleteMozaic", "MozaicDialogViewModel"));
				Messenger.Default.Send("CloseMozaicDialogViewModel");
			}));
		}

	}
}
