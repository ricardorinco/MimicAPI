using Mimic.Application.Arguments.Dtos.Words;
using Mimic.Application.Interfaces;
using Mimic.Domain.Models;

namespace Mimic.Application.Rules.Words.Update
{
    public class R01UpdateWord : IRuleHandler<UpdateWordDto, Word>
    {
        public IRuleHandler<UpdateWordDto, Word> Next { get; set; }

        public Word Apply(UpdateWordDto ruleDto, Word foundWord)
        {
            foundWord.Description = ruleDto.Description;
            foundWord.Points = ruleDto.Points;
            foundWord.Active = ruleDto.Active;

            return Next.Apply(ruleDto, foundWord);
        }
    }
}
