using System;
using JetBrains.Annotations;
using LegacyApp;
using Xunit;

namespace LegacyApp.Tests;

[TestSubject(typeof(UserService))]
public class UserServiceTest
{

    [Fact]
    public void AddUser_Should_Return_False_Whan_FirstName_Is_Missing()
    {

        var userService = new UserService();
        var addResult = userService.AddUser("", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 1);
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_False_Whan_Email_Is_Incorect()
    {

        var userService = new UserService();
        var addResult = userService.AddUser("John", "Doe", "johndoegmailcom", DateTime.Parse("1982-03-21"), 1);
        Assert.False(addResult);
    }
    
    
    [Fact]
    public void AddUser_Should_Thorw_ArgumentException_When_User_Client_Doesnt_Exist()
    {

        var userService = new UserService();
        Assert.Throws<ArgumentException>(() =>
        {
            var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("1982-03-21"), 100);

        });
       
    }
    [Fact]
    public void AddUser_Should_Return_False_Whan_User_Is_Younger_Than_21_Yrs()
    {

        var userService = new UserService();
        var addResult = userService.AddUser("John", "Doe", "johndoe@gmail.com", DateTime.Parse("2022-03-21"), 1);
        Assert.False(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_True_Whan_Very_Important_Client()
    {

        var userService = new UserService();
        var addResult = userService.AddUser("John", "Malewski", "malewski@gmail.pl", DateTime.Parse("1990-03-21"), 2);
        Assert.True(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_True_Whan_Important_Client()
    {

        var userService = new UserService();
        var addResult = userService.AddUser("John", "Smith", "smith@gmail.pl", DateTime.Parse("1990-03-21"), 3);
        Assert.True(addResult);
    }
    
    [Fact]
    public void AddUser_Should_Return_True_Whan_Normal_Client()
    {
    
        var userService = new UserService();
        var addResult = userService.AddUser("John", "Kwiatkowski", "kwiatkowski@wp.pl", DateTime.Parse("1990-03-21"), 5);
        Assert.True(addResult);
    }
    [Fact]
    public void AddUser_Should_Return_False_Whan_Normal_Client_With_No_Credit_Limit()
    {
    
        var userService = new UserService();
        var addResult = userService.AddUser("John", "Kowalski", "kowalski@wp.pl", DateTime.Parse("1990-03-21"), 1);
        Assert.False(addResult);
    }

    
    // [Fact]
    // public void AddUser_Should_Return_False_Whan_Limit_Below_500()
    // {
    //
    //     var userService = new UserService();
    //     var addResult = userService.AddUser("John", "Kowalski", "kowalski@wp.pl", DateTime.Parse("1990-03-21"), 1);
    //     Assert.False(addResult);
    // }
    
    [Fact]
    public void AddUser_Should_Thorw_ArgumentException_When_Client_Doesnt_Have_CreditLimit()
    {

        var userService = new UserService();
        Assert.Throws<ArgumentException>(() =>
        {
            var addResult = userService.AddUser("John", "Andrzejewicz", "andrzejewicz@wp.pl", DateTime.Parse("1982-03-21"), 6);

        });
       
    }
}
