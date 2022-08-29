using System;
using System.IO;

namespace CadastroMotorista.Infra.Shared.Utils
{
    public class WriteLog
    {
        public void WriteLogError(string AppName, string Name, string error)
        {
             string write = "Date: " + DateTime.Now + Environment.NewLine +
                           "File_Name_Method: " + Name + Environment.NewLine +
                           "Error: " + error + Environment.NewLine +
                           "VersionCompiled: ";

            string path = CreateFileLogError(Name, AppName);

            try
            {
                File.AppendAllText(path, write);
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public string CreateFileLogError(string method, string App)
        {
            string pathFinish = Constants.WriteDirectoryLog(Constants.PathLog()) + App + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + ".txt";

            try
            {
                if(!File.Exists(pathFinish))
                {
                    var myFile = File.Create(pathFinish);
                    myFile.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return pathFinish;
        }
    }
}