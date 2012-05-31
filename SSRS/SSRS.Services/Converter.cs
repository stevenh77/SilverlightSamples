using System.Collections.Generic;
using System.Linq;
using SSRS.Services.DTO;

namespace SSRS.Services
{
    public class Converter
    {
        public static IList<Parameter> Convert(ReportServiceReference.ItemParameter[] itemParameters)
        {
            if (itemParameters == null) return new List<Parameter>(0);

            var parameters = new List<Parameter>(itemParameters.Length);

            parameters.AddRange(
                itemParameters.Select(
                    parameter => new Parameter()
                    {
                        DefaultValues = parameter.DefaultValues,
                        Name = parameter.Name,
                        Nullable = parameter.Nullable,
                        ParameterType = parameter.ParameterTypeName
                    }));

            return parameters;
        }

        public static ReportExecutionServiceReference.ParameterValue[] Convert(IList<Parameter> parameters)
        {
            if (parameters == null) return new ReportExecutionServiceReference.ParameterValue[0];
            var query = from p in parameters
                        select new ReportExecutionServiceReference.ParameterValue()
                                   {
                                       Name = p.Name, Value = p.Value
                                   };

            return query.ToArray();
        }
    }
}
