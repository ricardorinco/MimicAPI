using Mimic.Application.Arguments.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Domain.Models;

namespace Mimic.Application.Rules.Words.Add
{
    public class R01CreateWord : IRuleHandler<AddWordDto, Word>
    {
        public IRuleHandler<AddWordDto, Word> Next { get; set; }

        public Word Apply(AddWordDto ruleDto, Word word)
        {
            word.Description = ruleDto.Description;
            word.Points = ruleDto.Points;

            return Next.Apply(ruleDto, word);
        }
    }
}
