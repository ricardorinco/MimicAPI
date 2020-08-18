using Microsoft.EntityFrameworkCore;
using Mimic.WebApi.Database.DataContext;
using Mimic.WebApi.Helpers;
using Mimic.WebApi.V1.Models;
using Mimic.WebApi.V1.Repository.Interfaces;
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

        public PaginationList<Word> Get(WordUrlQuery query)
        {
            var paginationList = new PaginationList<Word>();
            var words = mimicContext.Words.AsNoTracking().AsQueryable();

            if (query.SearchDate.HasValue)
            {
                words = words.Where(x => x.CreatedAt > query.SearchDate);
            }

            if (query.Page.HasValue)
            {
                var totalData = words.Count();
                words = words.Skip((query.Page.Value - 1) * query.DataAmount.Value).Take(query.DataAmount.Value);
                var pagination = new Pagination()
                {
                    Number = query.Page.Value,
                    Total = (int)Math.Ceiling((double)totalData / query.DataAmount.Value),

                    DataPerPage = query.DataAmount.Value,
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
