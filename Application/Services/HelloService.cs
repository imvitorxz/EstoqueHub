using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Interfaces;

namespace Application.Services
{
	public class HelloService : IHelloWorldService
	{
		public string GetHelloWorld()
		{
			return "Hello World!";
		}
	}
}
