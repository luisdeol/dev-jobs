using DevJobs.API.Entities;
using DevJobs.API.Models;
using DevJobs.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DevJobs.API.Controllers
{
    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;

        public JobVacanciesController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }

        // GET api/job-vacancies
        /// <summary>
        /// Listagem de Vagas
        /// </summary>
        /// <returns>Lista de Vagas</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var jobVacancies = _repository.GetAll();

            return Ok(jobVacancies);
        }

        // GET api/job-vacancies/{id}
        /// <summary>
        /// Detalhes da Vaga
        /// </summary>
        /// <param name="id">ID da Vaga</param>
        /// <returns>Mostra uma vaga</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            return Ok(jobVacancy);
        }

        // POST api/job-vacancies
        /// <summary>
        /// Cadastrar uma vaga de emprego
        /// </summary>
        /// <remarks>
        /// Requisição:
        /// {
        ///     "title": "Vaga .NET Jr",
        ///     "description": "Vaga para uma grande empresa.",
        ///     "company": "LuisDev",
        ///     "isRemote": true,
        ///     "salaryRange": "3000-5000"
        /// }
        /// </remarks>
        /// <param name="model">Dados de Vaga</param>
        /// <returns>Objeto criado</returns>
        /// <response code="201">Sucesso</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            Log.Information("Post executado.");

            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );

            _repository.Add(jobVacancy);

            return CreatedAtAction(
                "GetById",
                new { id = jobVacancy.Id },
                jobVacancy);
        }

        // PUT api/job-vacancies/{id}
        /// <summary>
        /// Atualiza uma Vaga
        /// </summary>
        /// <remarks>
        /// Requisição:
        /// {
        ///     "title": "Vaga .NET Pleno",
        ///     "description": "Vaga para uma grande empresa de São Paulo"
        /// }
        /// </remarks>
        /// <param name="id">ID da Vaga</param>
        /// <param name="model">Dados da Vaga</param>
        /// <returns>Objeto atualizado</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="400">Dados inválidos</response>
        /// <response code="404">Não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            jobVacancy.Update(model.Title, model.Description);

            _repository.Update(jobVacancy);

            return NoContent();
        }
    }
}