using OrderManager.Application.UserEceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.UserExceptions
{
	public class ObjectNotFoundException : CustomException
	{
		public ObjectNotFoundException()
		{
		}

		public ObjectNotFoundException(string message) : base(message)
		{
		}

		public ObjectNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}
