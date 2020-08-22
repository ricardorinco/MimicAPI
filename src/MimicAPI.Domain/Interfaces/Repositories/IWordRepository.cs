using Mimic.Domain.Arguments;
using Mimic.Domain.Models;

namespace Mimic.Domain.Interfaces.Repositories
{
    public interface IWordRepository
    {
        PaginationList<Word> GetByQuery(WordQuery wordQuery);
        Word GetById(int id);

        void Add(Word word);
        void Update(Word word);
        void Delete(int id);
    }
}
