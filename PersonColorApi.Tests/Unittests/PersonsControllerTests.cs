
using Api.Controllers;
using Application.Persons.Commands;
using Application.Persons.DTOs;
using Application.Persons.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class PersonsControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly PersonsController _controller;

    public PersonsControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new PersonsController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnPersons()
    {
        var expected = new List<PersonDto> { new PersonDto { Id = 1, Name = "Hans", LastName = "Müller", Color = "blau" } };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllPersonsQuery>(), default))
            .ReturnsAsync(expected);

        var result = await _controller.GetAll();
        var ok = Assert.IsType<OkObjectResult>(result.Result);

        var persons = Assert.IsAssignableFrom<IEnumerable<PersonDto>>(ok.Value);
        Assert.Single(persons);
    }

    [Fact]
    public async Task GetById_ShouldReturnPerson_WhenExists()
    {
        var dto = new PersonDto { Id = 1, Name = "Hans", LastName = "Müller", Color = "blau" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPersonByIdQuery>(), default))
            .ReturnsAsync(dto);

        var result = await _controller.GetById(1);
        var ok = Assert.IsType<OkObjectResult>(result.Result);

        var person = Assert.IsType<PersonDto>(ok.Value);
        Assert.Equal(1, person.Id);
    }

    [Fact]
    public async Task GetByColor_ShouldReturnPersons()
    {
        var expected = new List<PersonDto> { new PersonDto { Id = 1, Name = "Hans", LastName = "Müller", Color = "blau" } };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetPersonsByColorQuery>(), default))
            .ReturnsAsync(expected);

        var result = await _controller.GetByColor("blau");
        var ok = Assert.IsType<OkObjectResult>(result.Result);

        var persons = Assert.IsAssignableFrom<IEnumerable<PersonDto>>(ok.Value);
        Assert.Single(persons);
    }

    [Fact]
    public async Task Post_ShouldReturnCreatedPerson()
    {
        var dto = new PersonDto { Id = 2, Name = "Max", LastName = "Test", Color = "rot" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<CreatePersonCommand>(), default))
            .ReturnsAsync(dto);

        var result = await _controller.Create(new CreatePersonCommand("Test", "Max", "12345", "Berlin", "rot"));
        var created = Assert.IsType<CreatedAtActionResult>(result.Result);

        var person = Assert.IsType<PersonDto>(created.Value);
        Assert.Equal(2, person.Id);
    }
}