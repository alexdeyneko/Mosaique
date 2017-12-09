using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using wpfMozaiq.Models;

namespace wpfMozaiq.ViewModel
{
	public class EditMozaicViewModel : ViewModelBase, INotifyPropertyChanged
	{
		public EditMozaicViewModel()
		{
			
		}


		private string _filenameImage;
		public string FilenameImage
		{
			set
			{
				_filenameImage = value;
				RaisePropertyChanged(() => FilenameImage);
			}
			get { return _filenameImage; }
		}

		private string _editNameMozaic;
		public string EditNameMozaic
		{
			set
			{
				_editNameMozaic = value;
				RaisePropertyChanged(() => EditNameMozaic);
			}
			get { return _editNameMozaic; }
		}

		private ICommand _okCommand;
		public ICommand OkCommand
		{
			get => _okCommand ?? (_okCommand = new RelayCommand(() =>
			{
				Messenger.Default.Send("EditMozaicViewModelAddMozaic" + "&" + FilenameImage + "&" + EditNameMozaic);
				Messenger.Default.Send("CloseWindowEditMozaicViewModel");
			}));

		}

		private ICommand _cancelCommand;
		public ICommand CancelCommand
		{
			get => _cancelCommand ?? (_cancelCommand = new RelayCommand(() =>
			{
				Messenger.Default.Send("CloseWindowEditMozaicViewModel");

			}));
		}

		private ICommand _choiseFileImage;
		public ICommand ChoiseFileImage
		{
			get => _choiseFileImage ?? (_choiseFileImage = new RelayCommand(() =>
			{
				Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
				dlg.FileName = "Image"; // Default file name
				dlg.DefaultExt = ".bmp"; // Default file extension
				dlg.Filter = "Файлы рисунков (*.bmp)|*.bmp;"; // Filter files by extension

				// Show open file dialog box
				Nullable<bool> result = dlg.ShowDialog();

				// Process open file dialog box results
				if (result == true)
				{
					// Open document
					FilenameImage = dlg.FileName;
				}

			}));
		}

	}
}
