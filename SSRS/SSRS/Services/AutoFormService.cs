using System.Collections.ObjectModel;
using SSRS.Data;
using SSRS.Services.DTO;

namespace SSRS.Services
{
    public class AutoFormService
    {
        public Collection<DynamicFormField> GetForm(ReportInfo report)
        {
            var form = new Collection<DynamicFormField>();

            foreach (var parameter in report.Parameters)
            {
                form.Add(new DynamicFormField()
                             {
                                 Caption = parameter.Name,
                                 Nullable = parameter.Nullable,
                                 Type = parameter.ParameterType,
                                 Value = parameter.DefaultValues[0]
                             });
            }
            return form;
        }
    }
}
