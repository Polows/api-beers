using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace beersapi.Middleware
{
    public class TimerMiddlewareOptions
    {

		public string Text { get; private set; }

	    public TimerMiddlewareOptions()
	    {
		    Text = "took";
	    }

	    public void SetDefaultMessage(string value)
	    {
		    Text = value;
	    }
    }
}
