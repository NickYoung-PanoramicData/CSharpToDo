using CSharpToDo.Server.Controllers;
using CSharpToDo.Shared.Models;
using CSharpToDo.Tests.Fakes;
//using CSharpToDo.Repositories.InMemory;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace CSharpToDo.Tests
{
	public class ToDosControllerTests
	{
		[Fact]
		public async Task GetAsync_RequestAllItems_ReturnsEmptyIEnumerable()
		{
			var sut = new ToDosController(new FakeToDosRepository());
			var response = await sut.GetAsync(CancellationToken.None);

			response.Should().BeEmpty();
		}

		[Fact]
		public async Task GetAsync_RequestItem123_ReturnsNotFoundResult()
		{
			var sut = new ToDosController(new FakeToDosRepository());
			var response = await sut.GetAsync(123, CancellationToken.None);

			response.Result.Should().NotBeNull();
			response.Result.Should().BeOfType<NotFoundResult>();
		}

		[Fact]
		public async Task GetAsync_GetExistingItem_ReturnsOkResult()
		{
			var repository = new FakeToDosRepository();
			var item = await repository.AddAsync(new ToDo(), CancellationToken.None);
			var sut = new ToDosController(repository);
			var response = await sut.GetAsync(item.Id, CancellationToken.None);

			response.Value.Should().NotBeNull();
			response.Value!.Id.Should().Be(item.Id);
		}

		[Fact]
		public async Task PutAsync_UpdateUnknownItem_ReturnsNotFoundResult()
		{
			var sut = new ToDosController(new FakeToDosRepository());
			var response = await sut.PutAsync(123, new ToDo(), CancellationToken.None);

			response.Should().NotBeNull();
			response.Should().BeOfType<NotFoundResult>();
		}

		[Fact]
		public async Task PutAsync_UpdateExistingItem_ReturnsOkResult()
		{
			var repository = new FakeToDosRepository();
			var item = await repository.AddAsync(new ToDo(), CancellationToken.None);
			var sut = new ToDosController(repository);
			var response = await sut.PutAsync(item.Id, item, CancellationToken.None);

			response.Should().NotBeNull();
			response.Should().BeOfType<OkResult>();
		}

		[Fact]
		public async Task DeleteAsync_DeleteUnknownItem_ReturnsNotFoundResult()
		{
			var sut = new ToDosController(new FakeToDosRepository());
			var response = await sut.DeleteAsync(123, CancellationToken.None);

			response.Should().NotBeNull();
			response.Should().BeOfType<NotFoundResult>();
		}

		[Fact]
		public async Task DeleteAsync_DeleteExistingItem_ReturnsOkResult()
		{
			var repository = new FakeToDosRepository();
			var item = await repository.AddAsync(new ToDo(), CancellationToken.None);
			var sut = new ToDosController(repository);
			var response = await sut.DeleteAsync(item.Id, CancellationToken.None);

			response.Should().NotBeNull();
			response.Should().BeOfType<OkResult>();
		}
	}
}