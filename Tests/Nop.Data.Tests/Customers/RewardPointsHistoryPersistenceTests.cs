﻿using System;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Tests;
using NUnit.Framework;

namespace Nop.Data.Tests.Customers
{
    [TestFixture]
    public class RewardPointsHistoryPersistenceTests : PersistenceTest
    {
        [Test]
        public void Can_save_and_load_rewardPointsHistory()
        {
            var rewardPointsHistory = new RewardPointsHistory()
            {
                Customer = GetTestCustomer(),
                Points = 1,
                Message = "Points for registration",
                PointsBalance = 2,
                UsedAmount = 3.1M,
                CreatedOnUtc = new DateTime(2010, 01, 01)
            };

            var fromDb = SaveAndLoadEntity(rewardPointsHistory);
            fromDb.ShouldNotBeNull();
            fromDb.Points.ShouldEqual(1);
            fromDb.Message.ShouldEqual("Points for registration");
            fromDb.PointsBalance.ShouldEqual(2);
            fromDb.UsedAmount.ShouldEqual(3.1M);
            fromDb.CreatedOnUtc.ShouldEqual(new DateTime(2010, 01, 01));

            fromDb.Customer.ShouldNotBeNull();
        }
        [Test]
        public void Can_save_and_load_rewardPointsHistory_with_order()
        {
            var rewardPointsHistory = new RewardPointsHistory()
            {
                Customer = GetTestCustomer(),
                Points = 1,
                Message = "Points for registration",
                PointsBalance = 2,
                UsedAmount = 3,
                CreatedOnUtc = new DateTime(2010, 01, 01)
            };

            var fromDb = SaveAndLoadEntity(rewardPointsHistory);
            fromDb.ShouldNotBeNull();
        }
        
        protected Customer GetTestCustomer()
        {
            return new Customer
            {
                CustomerGuid = Guid.NewGuid(),
                AdminComment = "some comment here",
                Active = true,
                Deleted = false,
                CreatedOnUtc = new DateTime(2010, 01, 01),
                LastActivityDateUtc = new DateTime(2010, 01, 02)
            };
        }
    }
}