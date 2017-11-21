using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace wpfMozaiq.ViewModel
{
    public class TechDocViewModel : ViewModelBase
    {
	    private string _techDocPath;
	    public string TechDocPath
		{
		    set
		    {
			    _techDocPath = value;
			    RaisePropertyChanged(() => TechDocPath);
		    }
		    get
		    {
			    return _techDocPath;
		    }
	    }

	    private string _techDocText;
	    public string TechDocText
	    {
		    set
		    {
			    _techDocText = value;
			    RaisePropertyChanged(() => TechDocText);
		    }
		    get
		    {
			    return _techDocText;
		    }
	    }



		public TechDocViewModel()
	    {
		    TechDocPath= Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\Temp\\" + "techDoc" + ".mzq";
		    TechDocText = File.ReadAllText(TechDocPath);
	    }
	}
}
