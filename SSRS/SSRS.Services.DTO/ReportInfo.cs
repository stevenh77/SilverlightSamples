using System.Collections.Generic;

namespace SSRS.Services.DTO
{
    public class ReportInfo
    {
        public string Name { get; set; }
        public IList<Parameter> Parameters { get; set; }
        public string Path { get; set; }

        public static ReportInfo Create(string name, IList<Parameter> parameters, string path)
        {
            return new ReportInfo()
                {
                    Name = name, 
                    Parameters = parameters, 
                    Path = path
                };
        }
    }
}
