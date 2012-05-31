namespace SSRS.Services.DTO
{
    public class Parameter
    {
        public string[] DefaultValues { get; set; }
        public string Name { get; set; }
        public bool Nullable { get; set; }
        public string ParameterType { get; set; }
        public string Value { get; set; }
    }
}
