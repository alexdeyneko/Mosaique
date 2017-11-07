using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using wpfMozaiq.Veiw;

namespace wpfMozaiq.ViewModel
{
    public class ImageViewModel : ViewModelBase
    {
	    private string _imagePath;
	    public string ImagePath
	    {
		    set
		    {
			    _imagePath = value;
			    RaisePropertyChanged(() => ImagePath);
		    }
		    get
		    {
			    return _imagePath;
		    }
	    }

	    private int _imageWidth;
	    public int ImageWidth
		{
		    set
		    {
			    _imageWidth = value;
			    RaisePropertyChanged(() => ImageWidth);
		    }
		    get
		    {
			    return _imageWidth;
		    }
	    }

	    private int _imageHeight;
	    public int ImageHeight
	    {
		    set
		    {
			    _imageHeight = value;
			    RaisePropertyChanged(() => ImageHeight);
		    }
		    get
		    {
			    return _imageHeight;
		    }
	    }

		public ImageViewModel()
	    {
			ImagePath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\Temp\\" + "tempImage.bmp";
		    ImageHeight = 400;
		    ImageWidth = 400;

		}

	    private ICommand _pressPlus;
	    public ICommand PressPlus
		{
		    get => _pressPlus ?? (_pressPlus = new RelayCommand(() =>
		    {
			    ImageHeight += 25;
			    ImageWidth += 25;
			    int i = 0;

		    }));
	    }

	    private ICommand _pressMinus;
	    public ICommand PressMinus
		{
		    get => _pressMinus ?? (_pressMinus = new RelayCommand(() =>
			{
				ImageHeight -= 25;
				ImageWidth -= 25;
				int i = 0;
			}));
	    }


	}
}
