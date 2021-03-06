﻿using Microsoft.AspNetCore.Mvc;
using Mimic.Application.Interfaces;
using Mimic.WebApi.Arguments.Dtos.Words;
using Mimic.WebApi.Helpers.Mappers;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Mimic.WebApi.V2.Controllers
{
    /// <summary>
    /// Controller de palavras
    /// 
    /// Versão: v2.0
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/words")]
    [ApiVersion("2")]
    public class WordsController : ControllerBase
    {
        private readonly IWordService wordService;

        /// <summary>
        /// Construtor da classe
        /// </summary>
        /// <param name="wordService">Implementação de IWordService</param>
        public WordsController(IWordService wordService)
        {
            this.wordService = wordService;
        }

        /// <summary>
        /// Realiza uma consulta de palavras de acordo com os filtros informados
        /// </summary>
        /// <param name="requestDto">Objeto QueryWordRequestDto</param>
        /// <returns>Lista das palavras encontradas</returns>
        [HttpGet("", Name = "GetAllBySearch")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [MapToApiVersion("2")]
        public async Task<IActionResult> GetAllBySearch([FromQuery] QueryWordRequestDto requestDto)
        {
            var ruleDto = WordMappers.QueryWordequestDtoToQueryWordDto(requestDto);
            var words = await wordService.GetByQueryAsync(ruleDto);

            if (words.Count == 0)
            {
                return NotFound();
            }

            var responseDto = new List<QueryWordResponseDto>();
            foreach (var word in words)
            {
                responseDto.Add((QueryWordResponseDto)word);
            }

            return Ok(responseDto);
        }
    }
}
