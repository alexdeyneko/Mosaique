using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using GalaSoft.MvvmLight;

namespace wpfMozaiq.ViewModel
{
    public class TechDocViewModel : ViewModelBase
    {
	    String line;

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

	    private string[] _line;
	    public string[] Line
		{
		    set
		    {
			    _line = value;
			    RaisePropertyChanged(() => Line);
		    }
		    get
		    {
			    return _line;
		    }
	    }



		public TechDocViewModel()
	    {
		    TechDocPath= Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory())) + "\\Temp\\" + "techDoc" + ".mzq";
			TechDocText = File.ReadAllText(TechDocPath);
		    //string[] Line = File.ReadAllLines(TechDocPath);
	    }
	}
}
