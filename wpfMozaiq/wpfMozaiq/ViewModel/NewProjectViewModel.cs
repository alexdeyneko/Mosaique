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

namespace wpfMozaiq.ViewModel
{
    class NewProjectViewModel : ViewModelBase
    {
        
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
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.FileName = "Document"; // Default file name
                dlg.DefaultExt = ".lst"; // Default file extension
                dlg.Filter = "(.lst)|*.lst"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    FilenameMosaicPack = dlg.FileName;
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
                }
            }));
        }
        
    }
}
