using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Movie2.Controllers;

using BuissinessLogicLayer;
using DataAccessLayer.CRUDOperations;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

public class UserAccountControllerTests
{
    private readonly Mock<ManageRegistration> _mockManageRegistration;
    private readonly Mock<ManageLogin> _mockManageLogin;
    private readonly Mock<ManageUserProfile> _mockManageUserProfile;
    private readonly UserAccount _controller;

    public UserAccountControllerTests()
    {
        _mockManageRegistration = new Mock<ManageRegistration>();
        _controller = new UserAccount(_mockManageRegistration.Object,_mockManageLogin.Object,_mockManageUserProfile.Object);
    }

    [Fact]
    public void Register_WithValidModel_ShouldReturnSuccessView()
    {
        // Arrange
        var model = new RegistrationModel
        {
            User_Name = "Test User",
            Email = "testuser@example.com",
            Phone = "1234567890",
            Street = "123 Test St",
            City = "Test City",
            PostCode = "12345",
            Password = "SecurePassword",
            ConfirmPassword = "SecurePassword"
        };
        // Act: Call the Register method with a valid model
        var result = _controller.Register(model) as ViewResult;

        // Assert: Verify the correct view and success message
        Assert.NotNull(result);
        Assert.Equal("registered successfully", result?.ViewData["Message"]);
        _mockManageRegistration.Verify(m => m.addRegistration(model), Times.Once);
    }

   
}
