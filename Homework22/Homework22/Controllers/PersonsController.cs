﻿using Homework22.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;
namespace Homework22.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PersonsController : ControllerBase
	{

		private const string FilePath = "data.json";

		[HttpPost("adduser")]
		public IActionResult AddUser([FromBody] Person person)
		{
			var validator = new PersonValidator();
			var result = validator.Validate(person);

			if (!result.IsValid)
			{
				var errorMessage = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
				return BadRequest(errorMessage);
			}

			List<Person> personList;
			if (System.IO.File.Exists(FilePath))
			{
				var json = System.IO.File.ReadAllText(FilePath);
				personList = JsonConvert.DeserializeObject<List<Person>>(json);
			}
			else
			{
				personList = new List<Person>();
			}

			personList.Add(person);
			var serialized = JsonConvert.SerializeObject(personList, Formatting.Indented);
			System.IO.File.WriteAllText(FilePath, serialized);

			return Ok(personList);
		}

		[HttpGet]
		public IActionResult GetAllUsers()
		{
			if (!System.IO.File.Exists(FilePath))
			{
				return Ok(new List<Person>());
			}

			var json = System.IO.File.ReadAllText(FilePath);
			var personList = JsonConvert.DeserializeObject<List<Person>>(json);
			return Ok(personList);
		}

		[HttpGet("{id}")]
		public IActionResult GetUserById(int id)
		{
			if (!System.IO.File.Exists(FilePath))
			{
				return NotFound();
			}

			var json = System.IO.File.ReadAllText(FilePath);
			var personList = JsonConvert.DeserializeObject<List<Person>>(json);

			if (id < 0 || id >= personList.Count)
			{
				return NotFound();
			}

			var person = personList[id];
			return Ok(person);
		}

		[HttpGet("filter")]
		public IActionResult GetFilteredUsers([FromQuery] double minSalary, string city)
		{
			if (!System.IO.File.Exists(FilePath))
			{
				return Ok(new List<Person>());
			}

			var json = System.IO.File.ReadAllText(FilePath);
			var personList = JsonConvert.DeserializeObject<List<Person>>(json);

			var filteredList = personList.Where(p => p.Salary >= minSalary && p.PersonAddress.City == city).ToList();
			return Ok(filteredList);
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteUserById(int id)
		{
			if (!System.IO.File.Exists(FilePath))
			{
				return NotFound();
			}

			var json = System.IO.File.ReadAllText(FilePath);
			var personList = JsonConvert.DeserializeObject<List<Person>>(json);

			if (id < 0 || id >= personList.Count)
			{
				return NotFound();
			}

			personList.RemoveAt(id);
			var serialized = JsonConvert.SerializeObject(personList, Formatting.Indented);
			System.IO.File.WriteAllText(FilePath, serialized);

			return Ok(personList);
		}

		[HttpPut("{id}")]
		public IActionResult UpdateUserById(int id, [FromBody] Person updatedPerson)
		{
			if (!System.IO.File.Exists(FilePath))
			{
				return NotFound();
			}

			var json = System.IO.File.ReadAllText(FilePath);
			var personList = JsonConvert.DeserializeObject<List<Person>>(json);

			if (id < 0 || id >= personList.Count)
			{
				return NotFound();
			}

			personList[id] = updatedPerson;
			var serialized = JsonConvert.SerializeObject(personList, Formatting.Indented);
			System.IO.File.WriteAllText(FilePath, serialized);

			return Ok(personList);
		}
	}
}
