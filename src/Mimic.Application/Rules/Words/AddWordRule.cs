using Mimic.Application.Dtos.Words;
using Mimic.Domain.Models;
using System;

namespace Mimic.Application.Rules.Words
{
    public static class AddWordRule
    {
        public static Word Apply(AddWordRequestDto request)
        {
            var word = new Word()
            {
                Description = request.Description,
                Points = request.Points,
                CreatedAt = DateTime.Now,
                Active = true
            };

            return word;
        }
    }
}
