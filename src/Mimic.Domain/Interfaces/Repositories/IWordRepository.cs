using Mimic.Domain.Arguments;
using Mimic.Domain.Models;
using System.Threading.Tasks;

namespace Mimic.Domain.Interfaces.Repositories
{
    public interface IWordRepository
    {
        PaginationList<Word> GetByQuery(WordQuery wordQuery);
        Word GetById(int id);

        Task AddAsync(Word word);
        Task UpdateAsync(Word word);
    }
}
