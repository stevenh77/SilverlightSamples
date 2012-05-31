namespace SSRS.Services.DTO
{
    public class ReportExecutionRequest
    {
        public string Name { get; set; }
        public string Format { get; set; }
        public Parameter[] Parameters { get; set; }
    }
}
