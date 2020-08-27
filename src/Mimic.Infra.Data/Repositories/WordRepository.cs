using Microsoft.EntityFrameworkCore;
using Mimic.Domain.Models;
using Mimic.Infra.Data.DataContext;
using Mimic.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<IList<Word>> GetByQueryAsync
        (
            DateTime createdDate,
            int currentPage,
            int pageSize
        )
        {
            var words = mimicContext.Words
                .Where(w => w.CreatedAt >= createdDate)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking();

            return await words.ToListAsync();
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
