namespace DevJobs.API.Controllers
{
    using DevJobs.API.Entities;
    using DevJobs.API.Models;
    using DevJobs.API.Persistence;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        private readonly DevJobsContext _context;
        public JobVacanciesController(DevJobsContext context)
        {
            _context = context;
        }
        
        // GET api/job-vacancies
        [HttpGet]
        public IActionResult GetAll()
        {
            var jobVacancies = _context.JobVacancies;

            return Ok(jobVacancies);
        }

        // GET api/job-vacancies/4
        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var jobVacancy = _context.JobVacancies
                .SingleOrDefault(jv => jv.Id == id);

            if (jobVacancy == null)
                return NotFound();

            return Ok(jobVacancy);
        }

        // POST api/job-vacancies
        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model) {
            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );

            _context.JobVacancies.Add(jobVacancy);

            return CreatedAtAction(
                "GetById", 
                new { id = jobVacancy.Id },
                jobVacancy);
        }

        // PUT api/job-vacancies/4
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model) {
            var jobVacancy = _context.JobVacancies
                .SingleOrDefault(jv => jv.Id == id);

            if (jobVacancy == null)
                return NotFound();
            
            jobVacancy.Update(model.Title, model.Description);
            
            return NoContent();
        }
    }
}