using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkScheduler.Api.Controllers;
using WorkScheduler.Api.Repo;

public class WorkControllerTests
{
    private readonly Mock<IWorkRepository> mock;
    private readonly WorkController controller;

    public WorkControllerTests()
    {
        mock = new Mock<IWorkRepository>();
        controller = new WorkController(mock.Object);
    }


    // Fetching
    [Fact]
    public async Task GetAllUsers_ReturnsOk()
    {
        mock.Setup(r => r.GetAllUsersAsync()).ReturnsAsync(new List<UserDisplayDTO> { new() { Username = "User1" } });
        var result = await controller.GetAllUsersAsync();
        var users = Assert.IsType<List<UserDisplayDTO>>(Assert.IsType<OkObjectResult>(result.Result).Value);
        Assert.Single(users);
    }
    [Fact]
    public async Task GetUserById_ReturnsOkOrNotFound()
    {
        mock.Setup(r => r.GetUserByIdAsync(1)).ReturnsAsync(new UserDisplayDTO { Username = "User1" });
        var resultOk = await controller.GetByIdAsync(1);
        Assert.IsType<OkObjectResult>(resultOk.Result);
        mock.Setup(r => r.GetUserByIdAsync(2)).ReturnsAsync((UserDisplayDTO)null);
        var resultNotFound = await controller.GetByIdAsync(2);
        Assert.IsType<NotFoundResult>(resultNotFound.Result);
    }
    [Fact]
    public async Task GetAllShifts_ReturnsOk()
    {
        mock.Setup(r => r.GetAllShiftsAsync()).ReturnsAsync(new List<ShiftDisplayDTO> { new() { Title = "Shift1" } });
        var result = await controller.GetAllShiftsAsync();
        var shifts = Assert.IsType<List<ShiftDisplayDTO>>(Assert.IsType<OkObjectResult>(result.Result).Value);
        Assert.Single(shifts);
    }
    [Fact]
    public async Task GetAllShiftsByUser_ReturnsOk()
    {
        mock.Setup(r => r.GetAllShiftsByUserAsync(1)).ReturnsAsync(new List<ShiftDisplayDTO> { new() { Title = "Shift1" } });
        var result = await controller.GetAllShiftsByUserIdAsync(1);
        var shifts = Assert.IsType<List<ShiftDisplayDTO>>(Assert.IsType<OkObjectResult>(result.Result).Value);
        Assert.Single(shifts);
    }

    // Deleting
    [Fact]
    public async Task DeleteShift_ReturnsOkOrBadRequest()
    {
        mock.Setup(r => r.DeleteShiftByIdAsync(1)).ReturnsAsync(true);
        var resultOk = await controller.DeleteShiftByIdAsync(1);
        Assert.IsType<OkObjectResult>(resultOk);
        mock.Setup(r => r.DeleteShiftByIdAsync(2)).ReturnsAsync(false);
        var resultBad = await controller.DeleteShiftByIdAsync(2);
        Assert.IsType<BadRequestObjectResult>(resultBad);
    }
    [Fact]
    public async Task DeleteUserByName_ReturnsOkOrBadRequest()
    {
        mock.Setup(r => r.DeleteUserByNameAsync("User")).ReturnsAsync(true);
        var resultOk = await controller.DeleteUserByNameAsync("User");
        Assert.IsType<OkObjectResult>(resultOk);
        mock.Setup(r => r.DeleteUserByNameAsync("NotExist")).ReturnsAsync(false);
        var resultBad = await controller.DeleteUserByNameAsync("NotExist");
        Assert.IsType<BadRequestObjectResult>(resultBad);
    }
    [Fact]
    public async Task DeleteUserById_ReturnsOkOrBadRequest()
    {
        mock.Setup(r => r.DeleteUserByIdAsync(1)).ReturnsAsync(true);
        var resultOk = await controller.DeleteUserByIdAsync(1);
        Assert.IsType<OkObjectResult>(resultOk);
        mock.Setup(r => r.DeleteUserByIdAsync(2)).ReturnsAsync(false);
        var resultBad = await controller.DeleteUserByIdAsync(2);
        Assert.IsType<BadRequestObjectResult>(resultBad);
    }

    // Create
    [Fact]
    public async Task CreateUser_ReturnsOk()
    {
        var dto = new UserCreateDTO("32StinclhezRetep", "peter@gmail.com", "secure_password523", Role.ShiftManager);
        mock.Setup(r => r.AddUserAsync(dto)).ReturnsAsync(new UserDisplayDTO { Username = "NewUser" });
        var result = await controller.CreateUserAsync(dto);
        var user = Assert.IsType<UserDisplayDTO>(Assert.IsType<OkObjectResult>(result).Value);
        Assert.Equal("NewUser", user.Username);
    }
    [Fact]
    public async Task CreateShift_ReturnsOk()
    {
        var dto = new ShiftCreateDTO("Janitorial", "Bathrooms, kitchen, offices", DateTime.Now, DateTime.Now, 1);
        mock.Setup(r => r.AddShiftAsync(dto)).ReturnsAsync(new ShiftDisplayDTO { Title = "NewShift" });
        var result = await controller.CreateShiftAsync(dto);
        var shift = Assert.IsType<ShiftDisplayDTO>(Assert.IsType<OkObjectResult>(result).Value);
        Assert.Equal("NewShift", shift.Title);
    }


    // Update
    [Fact]
    public async Task UpdateUser_ReturnsOkOrNotFound()
    {
        var dto = new UserUpdateDTO { Username = "Updated" };
        mock.Setup(r => r.UpdateUserAsync(dto, 1)).ReturnsAsync(new UserDisplayDTO { Username = "Updated" });
        var resultOk = await controller.UpdateUserAsync(1, dto);
        Assert.IsType<OkObjectResult>(resultOk.Result);
        mock.Setup(r => r.UpdateUserAsync(dto, 2)).ReturnsAsync((UserDisplayDTO)null);
        var resultNotFound = await controller.UpdateUserAsync(2, dto);
        Assert.IsType<NotFoundResult>(resultNotFound.Result);
    }
    [Fact]
    public async Task UpdateShift_ReturnsOkOrNotFound()
    {
        var dto = new ShiftUpdateDTO { Title = "Updated" };
        mock.Setup(r => r.UpdateShiftAsync(dto, 1)).ReturnsAsync(new ShiftDisplayDTO { Title = "Updated" });
        var resultOk = await controller.UpdateShiftAsync(1, dto);
        Assert.IsType<OkObjectResult>(resultOk.Result);
        mock.Setup(r => r.UpdateShiftAsync(dto, 2)).ReturnsAsync((ShiftDisplayDTO)null);
        var resultNotFound = await controller.UpdateShiftAsync(2, dto);
        Assert.IsType<NotFoundResult>(resultNotFound.Result);
    }
}

