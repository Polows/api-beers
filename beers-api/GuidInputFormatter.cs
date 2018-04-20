using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace beersapi
{
	public class GuidInputFormatter : InputFormatter
	{
		public GuidInputFormatter()
		{
			SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/guid"));
		}

		public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
		{
			var request = context.HttpContext.Request;
			/*
			StreamReader reader = null;
			try
			{
				reader = new StreamReader(request.Body);
				var str = await reader.ReadLineAsync();
				reader.Dispose();
			}
			finally
			{
				reader?.Dispose();
			}
			*/
			using (var reader = new StreamReader(request.Body))
			{
				var str = await reader.ReadLineAsync();
				var sucess = Guid.TryParse(str, out var result);
				if (sucess)
				{
					return await InputFormatterResult.SuccessAsync(result);
				}
				else
				{
					return await InputFormatterResult.FailureAsync();
				}
			}
		}

		protected override bool CanReadType(Type type)
		{
			return type == typeof(Guid);
		}
	}
}
