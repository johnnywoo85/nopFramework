using System;
using System.Collections.Generic;
using System.IO;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Directory;
using Nop.Services.ExportImport;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Stores;
using NUnit.Framework;
using Rhino.Mocks;

namespace Nop.Services.Tests.ExportImport
{
    [TestFixture]
    public class ExportManagerTests : ServiceTest
    {
        private IPictureService _pictureService;
        private INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private IExportManager _exportManager;
        private IStoreService _storeService;

        [SetUp]
        public new void SetUp()
        {
            _storeService = MockRepository.GenerateMock<IStoreService>();
            _pictureService = MockRepository.GenerateMock<IPictureService>();
            _newsLetterSubscriptionService = MockRepository.GenerateMock<INewsLetterSubscriptionService>();

            _exportManager = new ExportManager(_pictureService, _newsLetterSubscriptionService, _storeService);
        }

        //[Test]
        //public void Can_export_manufacturers_to_xml()
        //{
        //    var manufacturers = new List<Manufacturer>()
        //    {
        //        new Manufacturer()
        //        {
        //            Id = 1,
        //            Name = "Name",
        //            Description = "Description 1",
        //            MetaKeywords = "Meta keywords",
        //            MetaDescription = "Meta description",
        //            MetaTitle = "Meta title",
        //            PictureId = 0,
        //            PageSize = 4,
        //            PriceRanges = "1-3;",
        //            Published = true,
        //            Deleted = false,
        //            DisplayOrder = 5,
        //            CreatedOnUtc = new DateTime(2010, 01, 01),
        //            UpdatedOnUtc = new DateTime(2010, 01, 02),
        //        },
        //        new Manufacturer()
        //        {
        //            Id = 2,
        //            Name = "Name 2",
        //            Description = "Description 2",
        //            MetaKeywords = "Meta keywords",
        //            MetaDescription = "Meta description",
        //            MetaTitle = "Meta title",
        //            PictureId = 0,
        //            PageSize = 4,
        //            PriceRanges = "1-3;",
        //            Published = true,
        //            Deleted = false,
        //            DisplayOrder = 5,
        //            CreatedOnUtc = new DateTime(2010, 01, 01),
        //            UpdatedOnUtc = new DateTime(2010, 01, 02),
        //        }
        //    };

        //    string result = _exportManager.ExportManufacturersToXml(manufacturers);
        //    //TODO test it
        //    String.IsNullOrEmpty(result).ShouldBeFalse();
        //}

        protected Address GetTestBillingAddress()
        {
            return new Address()
            {
                FirstName = "FirstName 1",
                LastName = "LastName 1",
                Email = "Email 1",
                Company = "Company 1",
                City = "City 1",
                Address1 = "Address1a",
                Address2 = "Address1a",
                ZipPostalCode = "ZipPostalCode 1",
                PhoneNumber = "PhoneNumber 1",
                FaxNumber = "FaxNumber 1",
                CreatedOnUtc = new DateTime(2010, 01, 01),
                Country = GetTestCountry()
            };
        }

        protected Address GetTestShippingAddress()
        {
            return new Address()
            {
                FirstName = "FirstName 2",
                LastName = "LastName 2",
                Email = "Email 2",
                Company = "Company 2",
                City = "City 2",
                Address1 = "Address2a",
                Address2 = "Address2b",
                ZipPostalCode = "ZipPostalCode 2",
                PhoneNumber = "PhoneNumber 2",
                FaxNumber = "FaxNumber 2",
                CreatedOnUtc = new DateTime(2010, 01, 01),
                Country = GetTestCountry()
            };
        }

        protected Country GetTestCountry()
        {
            return new Country
            {
                Name = "United States",
                TwoLetterIsoCode = "US",
                ThreeLetterIsoCode = "USA",
                NumericIsoCode = 1,
                SubjectToVat = true,
                Published = true,
                DisplayOrder = 1
            };
        }

        protected Customer GetTestCustomer()
        {
            return new Customer
            {
                CustomerGuid = Guid.NewGuid(),
                AdminComment = "some comment here",
                Active = true,
                Deleted = false,
                CreatedOnUtc = new DateTime(2010, 01, 01)
            };
        }
    }
}
