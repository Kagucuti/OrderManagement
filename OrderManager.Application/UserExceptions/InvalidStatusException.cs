using OrderManager.Application.UserEceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.UserExceptions
{
	public class InvalidStatusException : CustomException
	{
		public InvalidStatusException()
		{
		}

		public InvalidStatusException(string message) : base(message)
		{
		}

		public InvalidStatusException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
