using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using wpfMozaiq.Models;

namespace wpfMozaiq.ViewModel
{
	class NewProjectViewModel : ViewModelBase
	{
		private Catalog catalog;
		private OriginalImage originalImage;
		private MozaicPanel panno;

		private double desiredWidth;
		private int matrixLines;
		private int matrixColumns;
		private double desiredMozaicGap;
		private int computerMozaicGap;
		private int computerMatrixGap;

		private string _filenameMosaicPack;
		public string FilenameMosaicPack
		{
			set
			{
				_filenameMosaicPack = value;
				RaisePropertyChanged(() => FilenameMosaicPack);

			}
			get { return _filenameMosaicPack; }
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

		private ICommand _choiseFileMosaicPack;
		public ICommand ChoiseFileMosaicPack
		{
			get => _choiseFileMosaicPack ?? (_choiseFileMosaicPack = new RelayCommand(() =>
			{
				FolderBrowserDialog FBD = new FolderBrowserDialog();
				FBD.ShowNewFolderButton = false;
				if (FBD.ShowDialog() == DialogResult.OK)
				{
					FilenameMosaicPack = FBD.SelectedPath;
					string[] split = FilenameMosaicPack.Split(new Char[] {'\\', '_'});
					catalog = new Catalog(split[split.Length - 2], Convert.ToInt32(split[split.Length - 1]));
				}
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
					originalImage = new OriginalImage(FilenameImage);
				}
			}));
		}

		private ICommand _okCommand;
		public ICommand OkCommand
		{
			get => _okCommand ?? (_okCommand = new RelayCommand(() =>
			{
				Messenger.Default.Send(originalImage);
				Messenger.Default.Send(catalog);
				Messenger.Default.Send(new NotificationMessage("gotovo"));
				

			}));
		}

		private ICommand _cancelCommand;
		public ICommand CancelCommand
		{
			get => _choiseFileImage ?? (_choiseFileImage = new RelayCommand(() =>
			{
			}));
		}



	}
}
