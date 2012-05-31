using System;

namespace SSRS.Data
{
    public class DynamicFormField
    {
        public Guid Id { get; private set; }
        public string Caption { get; set; }
        public bool Nullable { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        public DynamicFormField()
        {
            Id = Guid.NewGuid();
        }
    }
}
