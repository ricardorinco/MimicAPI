using Mimic.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mimic.Infra.Data.Interfaces
{
    public interface IWordRepository
    {
        /// <summary>
        /// Realiza uma consulta de palavras de acordo com os filtros informados
        /// </summary>
        /// <param name="createdDate">Data de criação da palavra</param>
        /// <param name="currentPage">Número da página atual</param>
        /// <param name="pageSize">Número de itens por página</param>
        /// <returns>Lista das palavras encontradas</returns>
        Task<IList<Word>> GetByQueryAsync
        (
            DateTime createdDate,
            int currentPage,
            int pageSize
        );

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
