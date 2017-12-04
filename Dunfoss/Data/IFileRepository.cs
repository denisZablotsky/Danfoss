using Dunfoss.Models;
using System.Linq;


namespace Dunfoss.Data
{
    public interface IFileRepository
    {
        IQueryable<File> Files { get; }
        File CreateFile(File file);
        File GetFileById(int id);
        
    }
}
