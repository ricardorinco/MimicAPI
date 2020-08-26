using Mimic.Application.Dtos.Words;
using Mimic.Domain.Models;
using System.Threading.Tasks;

namespace Mimic.Application.Interfaces
{
    public interface IWordService
    {
        /// <summary>
        /// Realiza a busca de uma palavra através do Id informado
        /// </summary>
        /// <param name="id">Id da palavra</param>
        /// <returns>Palavra encontrada</returns>
        Task<Word> GetByIdAsync(int id);

        /// <summary>
        /// Realiza a inclusão da palavra informada
        /// </summary>
        /// <param name="ruleDto">Objeto AddWordRuleDto</param>
        /// <returns>Palavra adicionada</returns>
        Task<Word> AddAsync(AddWordRuleDto ruleDto);

        /// <summary>
        /// Realiza a atualização da palavra informada
        /// </summary>
        /// <param name="ruleDto">Objeto UpdateWordRuleDto</param>
        /// <returns>Palavra atualizada</returns>
        Task<Word> UpdateAsync(UpdateWordRuleDto ruleDto);

        /// <summary>
        /// Realiza a exclusão lógica de uma palavra através do Id informado
        /// </summary>
        /// <param name="ruleDto">Objeto DeleteWordRuleDto</param>
        Task DeleteAsync(DeleteWordRuleDto ruleDto);
    }
}
