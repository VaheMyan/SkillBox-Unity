using System.Collections.Generic;
using System;
using System.Text;
using UnityGoogleDrive;
using UnityGoogleDrive.Data;

public static class GoogleDriveTools
{
    public static List<File> FileList()
    {
        List<File> output = new List<File>();
        GoogleDriveFiles.List().Send().OnDone += fileList => { output = fileList.Files;  }; // vercnum a fayler-y ev ugharkum a =>
        return output;
    }

    public static File Upload(String obj, Action onDone)
    {
        var file = new UnityGoogleDrive.Data.File { Name = "GameData.json", Content = Encoding.ASCII.GetBytes(obj)}; // nerarum a faylery
        GoogleDriveFiles.Create(file).Send();
        return file;
    }

    public static File Download(String fileId)
    {
        File output = new File();
        GoogleDriveFiles.Download(fileId).Send().OnDone += file => { output = file; }; // qashum a faylery
        return output;
    }
}
