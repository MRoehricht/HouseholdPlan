﻿using HouseholdPlan.Core.Factories.People;
using HouseholdPlan.Core.Models.People;
using HouseholdPlan.Data.Access;
using HouseholdPlan.Data.Entities.People;
using NUnit.Framework;

namespace HouseholdPlan.Core.Test.Factories.People
{
    public class UserFactoryTest : FactoryTestBase
    {
        public const string UserName1 = "Test Nutzer1";
        public const string UserId1 = "1";
        public const string UserName2 = "Test Nutzer2";
        public const string UserId2 = "2";

        [Test]
        public void LoadTest()
        {
            CreateUsers();

            UserFactory userFactory = new UserFactory(access);

            User user1 = userFactory.LoadUser("1");
            Assert.AreEqual(UserId1, user1.Id);
            Assert.AreEqual(UserName1, user1.Name);

            User user2 = userFactory.LoadUser("2");
            Assert.AreEqual(UserId2, user2.Id);
            Assert.AreEqual(UserName2, user2.Name);
        }

        [Test]
        public void CreateUsers()
        {
            UserEntity user = new UserEntity
            {
                Id = UserId1,
                Name = UserName1
            };

            access.CreateUser(user);

            UserEntity user2 = new UserEntity
            {
                Id = UserId2,
                Name = UserName2
            };

            access.CreateUser(user2);
        }

        public void CreateUsers(SQLiteAccess access)
        {
            this.access = access;
            CreateUsers();
        }
    }
}