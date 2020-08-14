using Mimic.WebApi.Helpers;
using Mimic.WebApi.Models;

namespace Mimic.WebApi.Repository.Interfaces
{
    public interface IWordRepository
    {
        PaginationList<Word> Get(WordUrlQuery query);
        Word GetById(int id);

        void Add(Word word);
        void Update(Word word);
        void Delete(int id);
    }
}
