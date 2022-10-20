using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Common.Models
{
    public class FormDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ListJsonModel { get; set; }
        public string JsonContent { get; set; }
        public bool IsCustom { get; set; }
        public string CustomURL { get; set; }
        public string EmptyMessage { get; set; }
        public int StatusId { get; set; }
        public List<FormFieldInputResponse> FormFieldInputs { get; set; }
    }
    public class FormFieldInputResponse
    {

        public string ToolTip { get; set; }

        public string Label { get; set; }
        public string Name { get; set; }

        public string Value { get; set; }

        public string Style { get; set; }

        public string Column { get; set; }

        public string ControlType { get; set; }

        public int MaxLength { get; set; }

        public int MinLength { get; set; }
        public bool IsRequired { get; set; }

        public List<OptionsDto>? options { get; set; }

        public string DependentKey { get; set; }
        public List<string>? Dependants { get; set; }

         public CustomLookupDto? CustomLookupDto { get; set; }
        
    }

    public class OptionsDto
    {
        public int Key { get; set; }
        public string Value { get; set; }
        public bool IsChecked { get; set; }
    }
    public class CustomLookupDto
    {

        public string Endpoint { get; set; }
        public string Key { get; set; }
        public string CustomValue { get; set; }
        public string DependentProperty { get; set; }
    }
}
