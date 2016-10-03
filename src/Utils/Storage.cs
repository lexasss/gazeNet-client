using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace GazeNetClient.Utils
{
    public sealed class Storage
    {
        public static string Folder
        {
            get
            {
                AssemblyInfo assemblyInfo = new AssemblyInfo();
                var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) +
                    Path.DirectorySeparatorChar + assemblyInfo.Company +
                    Path.DirectorySeparatorChar + assemblyName +
                    Path.DirectorySeparatorChar;

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                return folder;
            }
        }

        public static string DataFolder
        {
            get
            {
                string folder = Folder + "data" + Path.DirectorySeparatorChar;

                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                return folder;
            }
        }

        public static string[] DataFolderFiles
        {
            get
            {
                List<string> fileNames = new List<string>();
                foreach (string fileName in Directory.EnumerateFiles(DataFolder))
                    fileNames.Add(Path.GetFileNameWithoutExtension(fileName));

                return fileNames.ToArray();
            }
        }

        public static bool saveData(Action<string> aAction, string aFileName = null, string aDefaultExt = null, string aFilter = null)
        {
            if (aAction == null)
                return false;

            string fileName = aFileName;
            if (string.IsNullOrEmpty(fileName))
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.DefaultExt = aDefaultExt ?? "txt";
                sfd.Filter = aFilter ?? "Text files|*." + sfd.DefaultExt;
                if (sfd.ShowDialog() == DialogResult.OK)
                    fileName = sfd.FileName;
            }
            else if (fileName.IndexOf(':') != 1)
            {
                if (fileName.IndexOf('.') > 1)
                    fileName = DataFolder + fileName;
                else
                    fileName = string.Format("{0}p{1}_{2}.{3}", DataFolder, DataFolderFiles.Length + 1,
                        aFileName, aDefaultExt ?? "txt");
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine(fileName);
                aAction(fileName);
                return true;
            }

            return false;
        }
    }

    public sealed class Storage<T>
    {
        private static string sOptionsFileExtension = ".xml";

        public static void save(object aObject)
        {
            if (aObject == null)
            {
                return;
            }

            Type type = typeof(T);
            string fileName = GetFilePath(type.ToString()) + sOptionsFileExtension;
            
            try
            {
                XmlSerializer serializer = new XmlSerializer(type);
                TextWriter writer = new StreamWriter(fileName);
                serializer.Serialize(writer, aObject);
                writer.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(String.Format("Error on saving the object of {0} type: {1}", type.ToString(), e.Message), "ObjectStorage");
            }
        }

        public static T load(string aFileName = "")
        {
            T result = default(T);
            Type type = typeof(T);
            string fileName = string.IsNullOrEmpty(aFileName) ? GetFilePath(type.ToString()) + sOptionsFileExtension : aFileName;

            try
            {
                XmlSerializer serializer = new XmlSerializer(type);
                FileStream fs = new FileStream(fileName, FileMode.Open);
                result = (T)serializer.Deserialize(fs);
                fs.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(String.Format("Error on loading the object of {0} type: {1}", type.ToString(), e.Message), "ObjectStorage");
                result = (T)Activator.CreateInstance(typeof(T));
            }

            return result;
        }

        private Storage() { }

        private static string GetFilePath(string aTypeName)
        {
            return Storage.Folder + aTypeName.Replace('+', '.');
            //string[] parts = aTypeName.ToString().Split('.', '+');
            //return parts[parts.Length - 1];
        }
    }
}
