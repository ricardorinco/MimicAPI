using Mimic.Domain.Arguments;
using Mimic.Domain.Models;
using System.Threading.Tasks;

namespace Mimic.Infra.Data.Interfaces
{
    public interface IWordRepository
    {
        PaginationList<Word> GetByQuery(WordQuery wordQuery);

        /// <summary>
        /// Realiza a busca de uma palavra através do Id informado
        /// </summary>
        /// <param name="id">Id da palavra</param>
        /// <returns>Palavra encontrada</returns>
        Task<Word> GetByIdAsync(int id);

        /// <summary>
        /// Realiza a inclusão da palavra informada
        /// </summary>
        /// <param name="ruleDto">Objeto Word</param>
        Task AddAsync(Word word);

        /// <summary>
        /// Realiza a atualização da palavra informada
        /// </summary>
        /// <param name="ruleDto">Objeto Word</param>
        Task UpdateAsync(Word word);
    }
}
