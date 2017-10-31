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
using wpfMozaiq.Models;

namespace wpfMozaiq.ViewModel
{
    class NewProjectViewModel : ViewModelBase
    {
	    private Catalog catalog;
		private OriginalImage originalImage;


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
		            string[] split = FilenameMosaicPack.Split(new Char[] { '\\', '_' });
		            catalog = new Catalog(split[split.Length - 2], Convert.ToInt32(split[split.Length - 1]));
		            Messenger.Default.Send(catalog);
				}
			}));
        }


        private ICommand _choiseFileImage;
        public ICommand ChoiseFileImage
        {
            get => _choiseFileImage ?? (_choiseFileImage = new RelayCommand(() =>
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.FileName = "Document"; // Default file name
                dlg.DefaultExt = ".png"; // Default file extension
                dlg.Filter = "Файлы рисунков (*.bmp, *.png)|*.bmp;*.png"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    FilenameImage = dlg.FileName;
	                originalImage = new OriginalImage(FilenameImage);
	                Messenger.Default.Send(originalImage);
				}
            }));
        }


		private ICommand _okCommand;
	    public ICommand OkCommand
	    {
		    get => _okCommand ?? (_okCommand = new RelayCommand(() =>
		    {
				//Messenger.Default.Send(new NotificationMessage("gotovo"));
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
