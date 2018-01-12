using System;
using System.Collections.Generic;
using System.Linq;
using Java.IO;

namespace Core.Android.FIleUtility
{
   public class FileSearchingFromDevice
    {
        public string parentDirStringPath; 
        List<string> inFiles = new List<string>();
        private File parentDir;

        public FileSearchingFromDevice(string _parentDir)////must be some like this  Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
        {
            parentDirStringPath = _parentDir;
            parentDir = new File(parentDirStringPath);
        }

        public List<string> GetAllFileWithExtension(string extension)
        {
            return AllFileWithExtension(parentDir, parentDirStringPath, extension); 
        }

        public List<string> GetAllFileWithName(string extension)
        {
            return AllFileWithName(parentDir, parentDirStringPath, extension);
        }



        public List<string> AllFileWithExtension(File parentDir, string PathToParentDir,string extension)
        {
            //List<Melody> inFiles = new List<Melody>();
            string[] fileNames = parentDir.List();
            foreach (string fileName in fileNames)
            {
                if (fileName.ToLower().EndsWith(extension))////verify extensiion for each file.
                {
                    inFiles.Add(parentDir.Path + "/" + fileName);
                }
                else
                {
                    File file = new File(parentDir.Path + "/" + fileName);
                    if (file.IsDirectory)
                    {
                        inFiles.Union(AllFileWithExtension(file, PathToParentDir + "/" + fileName, extension));
                    }
                }
            }

            return inFiles;
        }

        public List<string> AllFileWithName(File parentDir, string PathToParentDir, string extension)
        {
            //List<Melody> inFiles = new List<Melody>();
            string[] fileNames = parentDir.List();
            foreach (string fileName in fileNames)
            {
                if (fileName.ToLower().Contains(extension))////verify name for each file.
                {
                    inFiles.Add(parentDir.Path + "/" + fileName);
                }
                else
                {
                    File file = new File(parentDir.Path + "/" + fileName);
                    if (file.IsDirectory)
                    {
                        inFiles.Union(AllFileWithExtension(file, PathToParentDir + "/" + fileName, extension));
                    }
                }
            }

            return inFiles;
        }

    }
}