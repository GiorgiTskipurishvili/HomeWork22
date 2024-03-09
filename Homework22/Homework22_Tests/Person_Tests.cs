using Homework22.Controllers;
using Homework22.Models;
using System;
using Xunit;

namespace Homework22_Tests
{
	public class Person_Tests
	{
		[Fact]
		public void Test1()
		{
			var personController = new PersonsController();
			//Arrange 
			var person = new Person
			{
				Id = 1,
				CreateDate = DateTime.Now,
				FirstName = "Test",
				LastName = "Something",
				JobPosition = "Back-end C#",
				Salary = 1111,
				WorkExperience = 5,
				PersonAddress = new Address
				{
					Country = "Georgia",
					City = "Tbilisi",
					HomeNumber = "123"
				}
			};


			//Act


			// Assert
			//Assert.Equal<Person>(result);
		}
	}
}
