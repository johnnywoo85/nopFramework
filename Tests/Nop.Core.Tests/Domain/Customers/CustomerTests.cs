﻿using System.Linq;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Tests;
using NUnit.Framework;

namespace Nop.Core.Tests.Domain.Customers
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void Can_check_IsInCustomerRole()
        {
            var customer = new Customer()
            {
                /*CustomerRoles = new List<CustomerRole>()
                {
                    new CustomerRole()
                    {
                        Active = true,
                        Name = "Test name 1",
                        SystemName = "Test system name 1",
                    },
                    new CustomerRole()
                    {
                        Active = false,
                        Name = "Test name 2",
                        SystemName = "Test system name 2",
                    },
                }*/
            };

            customer.CustomerRoles.Add(new CustomerRole()
            {
                Active = true,
                Name = "Test name 1",
                SystemName = "Test system name 1",
            });
            customer.CustomerRoles.Add(new CustomerRole()
            {
                Active = false,
                Name = "Test name 2",
                SystemName = "Test system name 2",
            });
            customer.IsInCustomerRole("Test system name 1", false).ShouldBeTrue();
            customer.IsInCustomerRole("Test system name 1", true).ShouldBeTrue();

            customer.IsInCustomerRole("Test system name 2", false).ShouldBeTrue();
            customer.IsInCustomerRole("Test system name 2", true).ShouldBeFalse();

            customer.IsInCustomerRole("Test system name 3", false).ShouldBeFalse();
            customer.IsInCustomerRole("Test system name 3", true).ShouldBeFalse();
        }
        [Test]
        public void Can_check_whether_customer_is_admin()
        {
            var customer = new Customer();

            customer.CustomerRoles.Add(new CustomerRole()
            {
                Active = true,
                Name = "Registered",
                SystemName = SystemCustomerRoleNames.Registered
            });
            customer.CustomerRoles.Add(new CustomerRole()
            {
                Active = true,
                Name = "Guests",
                SystemName = SystemCustomerRoleNames.Guests
            });

            customer.IsAdmin().ShouldBeFalse();

            customer.CustomerRoles.Add(
                new CustomerRole()
                {
                    Active = true,
                    Name = "Administrators",
                    SystemName = SystemCustomerRoleNames.Administrators
                });
            customer.IsAdmin().ShouldBeTrue();
        }
        [Test]
        public void Can_check_whether_customer_is_forum_moderator()
        {
            var customer = new Customer();

            customer.CustomerRoles.Add(new CustomerRole()
            {
                Active = true,
                Name = "Registered",
                SystemName = SystemCustomerRoleNames.Registered
            });
            customer.CustomerRoles.Add(new CustomerRole()
            {
                Active = true,
                Name = "Guests",
                SystemName = SystemCustomerRoleNames.Guests
            });
        }
        [Test]
        public void Can_check_whether_customer_is_guest()
        {
            var customer = new Customer();

            customer.CustomerRoles.Add(new CustomerRole()
            {
                Active = true,
                Name = "Registered",
                SystemName = SystemCustomerRoleNames.Registered
            });

            customer.CustomerRoles.Add(new CustomerRole()
            {
                Active = true,
                Name = "Administrators",
                SystemName = SystemCustomerRoleNames.Administrators
            });

            customer.IsGuest().ShouldBeFalse();

            customer.CustomerRoles.Add(
                new CustomerRole()
                {
                    Active = true,
                    Name = "Guests",
                    SystemName = SystemCustomerRoleNames.Guests

                }
                );
            customer.IsGuest().ShouldBeTrue();
        }
        [Test]
        public void Can_check_whether_customer_is_registered()
        {
            var customer = new Customer();
            customer.CustomerRoles.Add(new CustomerRole()
            {
                Active = true,
                Name = "Administrators",
                SystemName = SystemCustomerRoleNames.Administrators
            });

            customer.CustomerRoles.Add(new CustomerRole()
            {
                Active = true,
                Name = "Guests",
                SystemName = SystemCustomerRoleNames.Guests
            });

            customer.IsRegistered().ShouldBeFalse();

            customer.CustomerRoles.Add(
                new CustomerRole()
                {
                    Active = true,
                    Name = "Registered",
                    SystemName = SystemCustomerRoleNames.Registered
                });
            customer.IsRegistered().ShouldBeTrue();
        }



        [Test]
        public void New_customer_has_clear_password_type()
        {
            var customer = new Customer();
            customer.PasswordFormat.ShouldEqual(PasswordFormat.Clear);
        }

        [Test]
        public void Can_add_address()
        {
            var customer = new Customer();
            var address = new Address { Id = 1 };

            customer.Addresses.Add(address);

            customer.Addresses.Count.ShouldEqual(1);
            customer.Addresses.First().Id.ShouldEqual(1);
        }

        [Test]
        public void Can_add_rewardPointsHistoryEntry()
        {
            var customer = new Customer();
            customer.AddRewardPointsHistoryEntry(1, "Points for registration");

            customer.RewardPointsHistory.Count.ShouldEqual(1);
            customer.RewardPointsHistory.First().Points.ShouldEqual(1);
        }

        [Test]
        public void Can_get_rewardPointsHistoryBalance()
        {
            var customer = new Customer();
            customer.AddRewardPointsHistoryEntry(1, "Points for registration");
            //customer.AddRewardPointsHistoryEntry(3, "Points for registration");

            customer.GetRewardPointsBalance().ShouldEqual(1);
        }
    }
}
