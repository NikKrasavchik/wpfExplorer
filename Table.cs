﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;


namespace WpfApp2
{
    public class ComplexData
    {
        public ComplexData()
        {
            this.name = "...";
        }
        public ComplexData(FileClass file)
        {
            this.name = file.name;
            this.type = file.type;
            this.dateCreated = file.dateCreated.ToString();
            this.dateLastWrite = file.dateLastWrite.ToString();
        }
        
        public ComplexData(DirectoryClass dir)
        {
            this.name = dir.name;
            this.type = dir.type;
        }

        public string name { get; set; }
        public string type { get; set; }
        public string dateCreated { get; set; }
        public string dateLastWrite { get; set; }
    }

    public class TableData
    {
        public bool determineFiles(string path)
        {
            try
            {
                string[] allFiles = Directory.GetFiles(path);
                files = new FileClass[allFiles.Length];

                string[] allDirectories = Directory.GetDirectories(path);
                dirs = new DirectoryClass[allDirectories.Length];

                for (int i = 0; i < allFiles.Length; i++)
                    files[i] = new FileClass(allFiles[i]);

                for (int i = 0; i < allDirectories.Length; i++)
                    dirs[i] = new DirectoryClass(allDirectories[i]);
            }
            catch (Exception e)
            {
                string messageBoxText = "Error while loading file";
                string caption = "Error";
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;

                MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

                return true;
            }
            return false;
        }

        public int getDataCount()
        {
            return files.Length + dirs.Length;
        }

        public int getFileCount()
        {
            return files.Length;
        }

        public int getDirCount()
        {
            return dirs.Length;
        }

        public FileClass getFile(int index)
        {
            return files[index];
        }
        public DirectoryClass getDir(int index)
        {
            return dirs[index];
        }

        private string path;
        FileClass[] files;
        DirectoryClass[] dirs;
    }
    public class FileClass
    {
        public FileClass(string fullFilename)
        {
            int indLastSlash = fullFilename.LastIndexOf('\\');
            int indDot = fullFilename.LastIndexOf('.');

            string fileName = "";
            for (int i = indLastSlash + 1; i < indDot; i++)
                fileName += fullFilename[i];

            string fileType = "";
            for (int i = indDot + 1; i < fullFilename.Length; i++)
                fileType += fullFilename[i];

            this.name = fileName;
            this.type = fileType;

            FileInfo fileInfo = new FileInfo(fullFilename);
            this.dateCreated = fileInfo.CreationTime;
            this.dateLastWrite = fileInfo.LastWriteTime;
        }

        public string name { get; set; }
        public string type { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateLastWrite { get; set; }
    }

    public class DirectoryClass
    {
        public DirectoryClass(string fullDirectoryName)
        {
            int indLastSlash = fullDirectoryName.LastIndexOf('\\');

            string directoryName = "";
            for (int i = indLastSlash + 1; i < fullDirectoryName.Length; i++)
                directoryName += fullDirectoryName[i];

            this.name = directoryName;
        }

        public string name { get; set; }
        public string type { get; set; }
        public DateTime date { get; set; }
    }
}
