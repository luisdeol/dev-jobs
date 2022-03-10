using DevJobs.API.Entities;
using DevJobs.API.Models;
using DevJobs.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DevJobs.API.Controllers
{
    [Route("api/job-vacancies/{id}/applications")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly DevJobsContext _context;

        public JobApplicationsController(DevJobsContext context)
        {
            _context = context;
        }

        // POST api/job-vacancies/{id}/applications
        /// <summary>
        /// Listagem de Candidatos
        /// </summary>
        /// <param name="id">ID da Vaga</param>
        /// <returns>Lista de Candidatos</returns>
        /// <response code="200">Sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Post(int id, AddJobApplicationInputModel model)
        {
            var jobVacancy = _context.JobVacancies
                .SingleOrDefault(jv => jv.Id == id);

            if (jobVacancy == null)
                return NotFound();

            var application = new JobApplication(
                model.ApplicantName,
                model.ApplicantEmail,
                id
            );

            _context.JobApplications.Add(application);
            _context.SaveChanges();

            return NoContent();
        }
    }
}