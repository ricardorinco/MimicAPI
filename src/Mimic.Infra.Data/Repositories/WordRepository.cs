using Microsoft.EntityFrameworkCore;
using Mimic.Domain.Arguments;
using Mimic.Domain.Models;
using Mimic.Infra.Data.DataContext;
using Mimic.Infra.Data.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mimic.Infra.Data.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly MimicContext mimicContext;

        public WordRepository(MimicContext mimicContext)
        {
            this.mimicContext = mimicContext;
        }

        public PaginationList<Word> GetByQuery(WordQuery wordQuery)
        {
            var paginationList = new PaginationList<Word>();
            var words = mimicContext.Words.AsNoTracking().AsQueryable();

            if (wordQuery.SearchDate.HasValue)
            {
                words = words.Where(x => x.CreatedAt > wordQuery.SearchDate);
            }

            if (wordQuery.Page.HasValue)
            {
                var totalData = words.Count();
                words = words.Skip((wordQuery.Page.Value - 1) * wordQuery.DataAmount.Value).Take(wordQuery.DataAmount.Value);
                var pagination = new Pagination()
                {
                    Number = wordQuery.Page.Value,
                    Total = (int)Math.Ceiling((double)totalData / wordQuery.DataAmount.Value),

                    DataPerPage = wordQuery.DataAmount.Value,
                    TotalData = totalData
                };

                paginationList.Pagination = pagination;
            }

            paginationList.Results.AddRange(words.ToList());

            return paginationList;
        }
        public async Task<Word> GetByIdAsync(int id)
        {
            return await mimicContext.Words.FindAsync(id);
        }

        public async Task AddAsync(Word word)
        {
            await mimicContext.Words.AddAsync(word);
            await mimicContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Word word)
        {
            await mimicContext.SaveChangesAsync();
        }
    }
}
