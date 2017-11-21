using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using wpfMozaiq.Veiw;

namespace wpfMozaiq.ViewModel
{
	public class NewProjectViewModel : ViewModelBase
	{
		private Catalog catalog;
		private OriginalImage originalImage;
		public MozaicPanel panno;
		private ChoiseCatalogDialogView catalogDialogView;

		private ObservableCollection<int> _sizeArrInt100;
		public ObservableCollection<int> SizeArrInt100
		{
			set
			{
				_sizeArrInt100 = value;
			}
			get { return _sizeArrInt100; }
		}

		private ObservableCollection<int> _sizeArrInt1000;
		public ObservableCollection<int> SizeArrInt1000
		{
			set
			{
				_sizeArrInt1000 = value;
			}
			get { return _sizeArrInt1000; }
		}

		private ObservableCollection<int> _sizeArrInt10;
		public ObservableCollection<int> SizeArrInt10
		{
			set
			{
				_sizeArrInt10 = value;
			}
			get { return _sizeArrInt10; }
		}

		private ObservableCollection<double> _sizeArrDouble10;
		public ObservableCollection<double> SizeArrDouble10
		{
			set
			{
				_sizeArrDouble10 = value;
			}
			get { return _sizeArrDouble10; }
		}

		private ObservableCollection<string> _arrColors;
		public ObservableCollection<string> ArrColors
		{
			set
			{
				_arrColors = value;
			}
			get { return _arrColors; }
		}

		private int _selectedWidth;
		public int SelectedWidth
		{
			set
			{
				_selectedWidth = value;
				RaisePropertyChanged(() => SelectedWidth);

				if (originalImage != null)
				{
					SelectedHeight= (int)(originalImage.Picture.Height * SelectedWidth/ originalImage.Picture.Width);
				}

			}
			get { return _selectedWidth; }
		}

		private int _selectedHeight;
		public int SelectedHeight
		{
			set
			{
				_selectedHeight = value;
				RaisePropertyChanged(() => SelectedHeight);

			}
			get { return _selectedHeight; }
		}

		private int _matrixLines;
		public int MatrixLines
		{
			set
			{
				_matrixLines = value;
				RaisePropertyChanged(() => MatrixLines);

			}
			get { return _matrixLines; }
		}

		private int _matrixColumns;
		public int MatrixColumns
		{
			set
			{
				_matrixColumns = value;
				RaisePropertyChanged(() => MatrixColumns);
			}
			get { return _matrixColumns; }
		}

		private double _desiredMozaicGap;
		public double DesiredMozaicGap
		{
			set
			{
				_desiredMozaicGap = value;
				RaisePropertyChanged(() => DesiredMozaicGap);

			}
			get { return _desiredMozaicGap; }
		}

		private int _computerMozaicGap;
		public int ComputerMozaicGap
		{
			set
			{
				_computerMozaicGap = value;
				RaisePropertyChanged(() => ComputerMozaicGap);

			}
			get { return _computerMozaicGap; }
		}

		private int _computerMatrixGap;
		public int ComputerMatrixGap
		{
			set
			{
				_computerMatrixGap = value;
				RaisePropertyChanged(() => ComputerMatrixGap);

			}
			get { return _computerMatrixGap; }
		}

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


		public NewProjectViewModel()
		{
			 
			Messenger.Default.Register<Catalog>(this, (newCatalog) =>
			{
				
				this.catalog = newCatalog;
				FilenameMosaicPack = this.catalog.Name + "_" + this.catalog.MozaicRealSize;
			});

			Messenger.Default.Register<string>(this, (newMessage) =>
			{
				if (catalogDialogView != null && newMessage == "CloseWindowChoiseCatalogDialogViewModel")
				{
					catalogDialogView.Close();
				}
			});

			_sizeArrInt100 = new ObservableCollection<int>();
			_sizeArrInt10= new ObservableCollection<int>();
			_sizeArrInt1000 = new ObservableCollection<int>();

            _sizeArrInt10.Add(0);

            for (int i = 1; i < 1001; i++)
			{
				if (i < 11)
				{
					_sizeArrInt10.Add(i);
				}
				if (i < 101)
				{
					_sizeArrInt100.Add(i);
				}
				_sizeArrInt1000.Add(i);
			}

			_sizeArrDouble10=new ObservableCollection<double>();
			for (double i = 0.0; i < 10.1; i = i + 0.1)
			{
				_sizeArrDouble10.Add(Math.Round(i, 2));
			}

			_arrColors = new ObservableCollection<string>();
			_arrColors.Add("Red");
			_arrColors.Add("Green");
			_arrColors.Add("Blue");

			MatrixLines = SizeArrInt100[14];
			MatrixColumns = SizeArrInt100[14];

			DesiredMozaicGap = SizeArrDouble10[15];
			ComputerMozaicGap = SizeArrInt10[0];
			ComputerMatrixGap = SizeArrInt10[0];

			

		}

		private ICommand _choiseFileMosaicPack;
		public ICommand ChoiseFileMosaicPack
		{
			get => _choiseFileMosaicPack ?? (_choiseFileMosaicPack = new RelayCommand(() =>
			{
				catalogDialogView = new ChoiseCatalogDialogView();
				catalogDialogView.Show();

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
					SelectedWidth = SizeArrInt1000[49];
				}
			}));
		}

		private ICommand _okCommand;
		public ICommand OkCommand
		{
			get => _okCommand ?? (_okCommand = new RelayCommand(() =>
			{
				panno= new MozaicPanel(originalImage, catalog, SelectedWidth, MatrixLines, MatrixColumns, DesiredMozaicGap, ComputerMozaicGap, ComputerMatrixGap);
				Messenger.Default.Send(panno);
				Messenger.Default.Send("CloseWindowNewProjectViewModel");
			}));
		}

		private ICommand _cancelCommand;
		public ICommand CancelCommand
		{
			get => _cancelCommand ?? (_cancelCommand = new RelayCommand(() =>
			{
				Messenger.Default.Send("CloseWindowNewProjectViewModel");
			}));
		}
		
	}
}
