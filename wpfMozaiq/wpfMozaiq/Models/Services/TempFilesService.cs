using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpfMozaiq.Models.Services
{
    public class TempFilesService
    {
	    public void DeleteTempFilesInDirectory(string directory)
	    {
		    DirectoryInfo dirInfo = new DirectoryInfo(directory);
			foreach (FileInfo file in dirInfo.GetFiles())
			{
				file.Delete();
			}
		}
    }
}
