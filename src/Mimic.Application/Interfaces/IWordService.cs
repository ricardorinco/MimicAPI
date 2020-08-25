using Mimic.Application.Dtos.Words;
using Mimic.Domain.Models;
using System.Threading.Tasks;

namespace Mimic.Application.Interfaces
{
    public interface IWordService
    {
        Word GetByIdAsync(int id);

        /// <summary>
        /// Adiciona uma nova palavra
        /// </summary>
        /// <param name="ruleDto">AddWordRuleDto para adicionar uma nova palavra</param>
        /// <returns>Palavra adicionada</returns>
        Task<Word> AddAsync(AddWordRuleDto ruleDto);

        /// <summary>
        /// Atualiza uma palavra existente
        /// </summary>
        /// <param name="ruleDto">UpdateWordRuleDto para atualizar uma palavra</param>
        /// <returns>Objeto de palavra atualizado</returns>
        Task<Word> UpdateAsync(UpdateWordRuleDto ruleDto);

        /// <summary>
        /// Exclusão lógica de uma palavra
        /// </summary>
        /// <param name="id">Id do registro</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
