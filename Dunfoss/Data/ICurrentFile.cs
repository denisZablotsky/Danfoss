using Dunfoss.Models;
using System.Linq;


namespace Dunfoss.Data
{
    interface ICurrentFile
    {
        IQueryable<CurrentFile> CurrentFiles { get; }
        CurrentFile UpdateCurrentFile(CurrentFile currentFile);
        CurrentFile InitializeCurrentFile(CurrentFile currentFile);
        CurrentFile GetCurrentFile();
        void UpdateFile1(string path);
        void UpdateFile2(string path);
        void UpdateFile3(string path);
        void UpdateFileID1(int id);
        void UpdateFileID2(int id);
        void UpdateFileID3(int id);
    }
}
