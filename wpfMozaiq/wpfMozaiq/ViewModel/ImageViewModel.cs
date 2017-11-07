using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

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

	    public ImageViewModel()
	    {
			ImagePath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\Temp\\" + "tempImage.bmp";
		    int i = 0;
	    }
	}
}
