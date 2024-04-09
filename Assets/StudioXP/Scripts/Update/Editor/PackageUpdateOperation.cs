using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.WSA;

namespace StudioXP.Scripts.Update.Editor
{
    public class PackageUpdateOperation
    {
        public int Identifier;
        public OperationType Type;
        public string RelativePath;

        private string _absolutePath;
        private string _onlinePath;
        private string _updaterFilePath;
        private string _filename;

        private Action _processCompleted;

        private static WebClient _client;

        public enum OperationType
        {
            Create,
            Delete,
            Replace
        }

        public void Process(Action processCompleted)
        {
            _processCompleted = processCompleted;
            _absolutePath = $"{PackagePath.ProjectRoot}/{RelativePath}";
            _onlinePath = $"{PackagePath.PackageRootURL}/{RelativePath}";
            _filename = Path.GetFileName(RelativePath);
            _updaterFilePath = $"{PackagePath.ProjectRoot}/{PackagePath.UpdaterFolder}/{_filename}";
            
            _client ??= new WebClient();
            
            switch(Type)
            {
                case OperationType.Create:
                    _client.DownloadFileCompleted += Create;
                    Download();
                    break;
                case OperationType.Delete:
                    Delete();
                    _processCompleted.Invoke();
                    break;
                case OperationType.Replace:
                    _client.DownloadFileCompleted += Replace;
                    Download();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Download()
        {
            if(File.Exists(_updaterFilePath))
                File.Delete(_updaterFilePath);
            
            _client.DownloadFileAsync(new Uri(_onlinePath), _updaterFilePath);
        }

        private void Create(object sender, AsyncCompletedEventArgs args)
        {
            _client.DownloadFileCompleted -= Create;
            
            if (args.Cancelled)
            {
                Debug.LogError($"{RelativePath} download cancelled.");
                _processCompleted.Invoke();
                return;
            }
            
            if (args.Error != null)
            {
                Debug.LogError(args.Error.ToString());
                _processCompleted.Invoke();
                return;
            }
            
            File.Move(_updaterFilePath, _absolutePath);
            
            _processCompleted.Invoke();
        }
        
        private void Delete()
        {
            Backup(_absolutePath);
        }
        
        private void Replace(object sender, AsyncCompletedEventArgs args)
        {
            _client.DownloadFileCompleted -= Replace;
            
            if (args.Cancelled)
            {
                Debug.LogError($"{RelativePath} download cancelled.");
                _processCompleted.Invoke();
                return;
            }
            
            if (args.Error != null)
            {
                Debug.LogError(args.Error.ToString());
                _processCompleted.Invoke();
                return;
            }
            
            Backup(_absolutePath);
            File.Move(_updaterFilePath, _absolutePath);
            
            _processCompleted.Invoke();
        }
        
        private void Backup(string file)
        {
            var backupVersion = 1;
            while (File.Exists($"{file}.bak{backupVersion}"))
                backupVersion++;
            
            File.Move(file, $"{file}.bak{backupVersion}");
        }
    }
}
