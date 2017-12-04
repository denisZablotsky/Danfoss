using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dunfoss.Models;

namespace Dunfoss.Data
{
    public interface ILetterRepository
    {
        IQueryable<Letter> Letter { get; }
        Letter CreateLetter(Letter letter);
        Letter GetLetterById(int id);
    }
}
