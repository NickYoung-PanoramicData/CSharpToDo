﻿using CSharpToDo.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpToDo.Repositories.Ef.Models
{
	public class ToDoModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public bool IsCompleted { get; set; }

		public DateTime CreatedUtc { get; set; } = DateTime.Now;

		public DateTime? LastModifiedUtc { get; set; }

		internal ToDo AsToDo()
		{
			return new ToDo
			{
				Id = Id,
				Name = Name,
				IsCompleted = IsCompleted,
				CreatedUtc = CreatedUtc,
				LastModifiedUtc = LastModifiedUtc
			};
		}

		internal static ToDoModel AsToDoModel(ToDo toDo)
		{
			return new ToDoModel
			{
				Id = toDo.Id,
				Name = toDo.Name,
				IsCompleted = toDo.IsCompleted,
				CreatedUtc = toDo.CreatedUtc,
				LastModifiedUtc = toDo.LastModifiedUtc
			};
		}
	}
}