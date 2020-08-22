using Microsoft.EntityFrameworkCore;
using Mimic.Domain.Arguments;
using Mimic.Domain.Interfaces.Repositories;
using Mimic.Domain.Models;
using Mimic.WebApi.Database.DataContext;
using System;
using System.Linq;

namespace Mimic.WebApi.V1.Repository
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
        public Word GetById(int id)
        {
            return mimicContext.Words.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Add(Word word)
        {
            mimicContext.Words.Add(word);
            mimicContext.SaveChanges();
        }
        public void Update(Word word)
        {
            mimicContext.Words.Update(word);
            mimicContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var foundWord = GetById(id);

            foundWord.Active = false;
            mimicContext.Words.Update(foundWord);
            mimicContext.SaveChanges();
        }
    }
}
