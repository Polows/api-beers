using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace beersapi
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
	    public CsvOutputFormatter()
	    {
			SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));	
			SupportedEncodings.Add(Encoding.UTF8);
	    }

	    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
	    {
		    var response = context.HttpContext.Response;
		    var data = context.Object;
		    var type = data.GetType();
		    var props = type.GetProperties();
		    using (var writer = new StreamWriter(response.Body))
		    {
			    var names = props.Select(prop => prop.Name);
			    var header = string.Join(",", names);
			    await writer.WriteLineAsync(header);
			    var values = props.Select(prop => prop.GetValue(data)?.ToString() ?? string.Empty);
			    var line = string.Join(",", values);
			    await writer.WriteAsync(line);
		    }
	    }

	    protected override bool CanWriteType(Type type) => true;
    }
}
