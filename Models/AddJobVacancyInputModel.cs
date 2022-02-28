namespace DevJobs.API.Models
{
    public record AddJobVacancyInputModel(
        string Title,
        string Description,
        string Company,
        bool IsRemote,
        string SalaryRange)
    {
    }
}