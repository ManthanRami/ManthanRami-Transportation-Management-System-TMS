using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMS.Data;
using TMS.Exceptions;

namespace TMS_Test_Suite
{
    [TestClass]
    class TmsDalTest
    {
        [TestMethod]
        public void TestCreateUser()
        {
            TmsDal dal = new TmsDal();

            User user = new User();
            user.Username = "admin";
            user.Password = "password";
            user.Email = "admin@test.com";
            user.FirstName = "Testing";
            user.LastName = "Testerson";
            user.Type = UserType.Admin;

            bool excepted = false;

            try
            {
                dal.CreateUser(user);
            }
            catch (CouldNotInsertException e)
            {
                excepted = true;
            }

            Assert.IsFalse(excepted);
        }

        [TestMethod]
        public void TestGetUserID()
        {

        }
    }
}
