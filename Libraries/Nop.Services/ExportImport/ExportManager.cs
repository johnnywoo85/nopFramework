﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Nop.Core;
using Nop.Core.Domain.Customers;
using Nop.Services.Common;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Seo;
using Nop.Services.Stores;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Nop.Services.ExportImport
{
    /// <summary>
    /// Export manager
    /// </summary>
    public partial class ExportManager : IExportManager
    {
        #region Fields

        private readonly IPictureService _pictureService;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly IStoreService _storeService;

        #endregion

        #region Ctor

        public ExportManager(IPictureService pictureService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            IStoreService storeService)
        {
            this._pictureService = pictureService;
            this._newsLetterSubscriptionService = newsLetterSubscriptionService;
            this._storeService = storeService;
        }

        #endregion

        #region Utilities

        #endregion

        #region Methods

        /// <summary>
        /// Export customer list to XLSX
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <param name="customers">Customers</param>
        public virtual void ExportCustomersToXlsx(Stream stream, IList<Customer> customers)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            // ok, we can run the real code of the sample now
            using (var xlPackage = new ExcelPackage(stream))
            {
                // uncomment this line if you want the XML written out to the outputDir
                //xlPackage.DebugMode = true; 

                // get handle to the existing worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("Customers");
                //Create Headers and format them
                var properties = new string[]
                    {
                        "CustomerId",
                        "CustomerGuid",
                        "Email",
                        "Username",
                        "PasswordStr",//why can't we use 'Password' name?
                        "PasswordFormatId",
                        "PasswordSalt",
                        "Active",
                        "IsGuest",
                        "IsRegistered",
                        "IsAdministrator",
                        "FirstName",
                        "LastName",
                        "Gender",
                        "Company",
                        "StreetAddress",
                        "StreetAddress2",
                        "ZipPostalCode",
                        "City",
                        "CountryId",
                        "StateProvinceId",
                        "Phone",
                        "Fax",
                        "VatNumber",
                        "VatNumberStatusId",
                        "TimeZoneId",
                        "AvatarPictureId",
                        "Signature",
                    };
                for (int i = 0; i < properties.Length; i++)
                {
                    worksheet.Cells[1, i + 1].Value = properties[i];
                    worksheet.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(184, 204, 228));
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                }


                int row = 2;
                foreach (var customer in customers)
                {
                    int col = 1;

                    worksheet.Cells[row, col].Value = customer.Id;
                    col++;

                    worksheet.Cells[row, col].Value = customer.CustomerGuid;
                    col++;

                    worksheet.Cells[row, col].Value = customer.Email;
                    col++;

                    worksheet.Cells[row, col].Value = customer.Username;
                    col++;

                    worksheet.Cells[row, col].Value = customer.Password;
                    col++;

                    worksheet.Cells[row, col].Value = customer.PasswordFormatId;
                    col++;

                    worksheet.Cells[row, col].Value = customer.PasswordSalt;
                    col++;

                    worksheet.Cells[row, col].Value = customer.Active;
                    col++;

                    //roles
                    worksheet.Cells[row, col].Value = customer.IsGuest();
                    col++;

                    worksheet.Cells[row, col].Value = customer.IsRegistered();
                    col++;

                    worksheet.Cells[row, col].Value = customer.IsAdmin();
                    col++;
                    
                    //attributes
                    var firstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
                    var lastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName);
                    var gender = customer.GetAttribute<string>(SystemCustomerAttributeNames.Gender);
                    var company = customer.GetAttribute<string>(SystemCustomerAttributeNames.Company);
                    var streetAddress = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress);
                    var streetAddress2 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2);
                    var zipPostalCode = customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode);
                    var city = customer.GetAttribute<string>(SystemCustomerAttributeNames.City);
                    var countryId = customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId);
                    var stateProvinceId = customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId);
                    var phone = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
                    var fax = customer.GetAttribute<string>(SystemCustomerAttributeNames.Fax);
                    var vatNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.VatNumber);
                    var vatNumberStatusId = customer.GetAttribute<int>(SystemCustomerAttributeNames.VatNumberStatusId);
                    var timeZoneId = customer.GetAttribute<string>(SystemCustomerAttributeNames.TimeZoneId);

                    var avatarPictureId = customer.GetAttribute<int>(SystemCustomerAttributeNames.AvatarPictureId);
                    var signature = customer.GetAttribute<string>(SystemCustomerAttributeNames.Signature);

                    worksheet.Cells[row, col].Value = firstName;
                    col++;

                    worksheet.Cells[row, col].Value = lastName;
                    col++;

                    worksheet.Cells[row, col].Value = gender;
                    col++;

                    worksheet.Cells[row, col].Value = company;
                    col++;

                    worksheet.Cells[row, col].Value = streetAddress;
                    col++;

                    worksheet.Cells[row, col].Value = streetAddress2;
                    col++;

                    worksheet.Cells[row, col].Value = zipPostalCode;
                    col++;

                    worksheet.Cells[row, col].Value = city;
                    col++;

                    worksheet.Cells[row, col].Value = countryId;
                    col++;

                    worksheet.Cells[row, col].Value = stateProvinceId;
                    col++;

                    worksheet.Cells[row, col].Value = phone;
                    col++;

                    worksheet.Cells[row, col].Value = fax;
                    col++;

                    worksheet.Cells[row, col].Value = vatNumber;
                    col++;

                    worksheet.Cells[row, col].Value = vatNumberStatusId;
                    col++;
                    
                    worksheet.Cells[row, col].Value = timeZoneId;
                    col++;

                    worksheet.Cells[row, col].Value = avatarPictureId;
                    col++;

                    worksheet.Cells[row, col].Value = signature;
                    col++;

                    row++;
                }


                // we had better add some document properties to the spreadsheet 

                // set some core property values
                //var storeName = _storeInformationSettings.StoreName;
                //var storeUrl = _storeInformationSettings.StoreUrl;
                //xlPackage.Workbook.Properties.Title = string.Format("{0} customers", storeName);
                //xlPackage.Workbook.Properties.Author = storeName;
                //xlPackage.Workbook.Properties.Subject = string.Format("{0} customers", storeName);
                //xlPackage.Workbook.Properties.Keywords = string.Format("{0} customers", storeName);
                //xlPackage.Workbook.Properties.Category = "Customers";
                //xlPackage.Workbook.Properties.Comments = string.Format("{0} customers", storeName);

                // set some extended property values
                //xlPackage.Workbook.Properties.Company = storeName;
                //xlPackage.Workbook.Properties.HyperlinkBase = new Uri(storeUrl);

                // save the new spreadsheet
                xlPackage.Save();
            }
        }

        /// <summary>
        /// Export customer list to xml
        /// </summary>
        /// <param name="customers">Customers</param>
        /// <returns>Result in XML format</returns>
        public virtual string ExportCustomersToXml(IList<Customer> customers)
        {
            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var xmlWriter = new XmlTextWriter(stringWriter);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Customers");
            xmlWriter.WriteAttributeString("Version", NopVersion.CurrentVersion);

            foreach (var customer in customers)
            {
                xmlWriter.WriteStartElement("Customer");
                xmlWriter.WriteElementString("CustomerId", null, customer.Id.ToString());
                xmlWriter.WriteElementString("CustomerGuid", null, customer.CustomerGuid.ToString());
                xmlWriter.WriteElementString("Email", null, customer.Email);
                xmlWriter.WriteElementString("Username", null, customer.Username);
                xmlWriter.WriteElementString("Password", null, customer.Password);
                xmlWriter.WriteElementString("PasswordFormatId", null, customer.PasswordFormatId.ToString());
                xmlWriter.WriteElementString("PasswordSalt", null, customer.PasswordSalt);
                xmlWriter.WriteElementString("Active", null, customer.Active.ToString());


                xmlWriter.WriteElementString("IsGuest", null, customer.IsGuest().ToString());
                xmlWriter.WriteElementString("IsRegistered", null, customer.IsRegistered().ToString());
                xmlWriter.WriteElementString("IsAdministrator", null, customer.IsAdmin().ToString());

                xmlWriter.WriteElementString("FirstName", null, customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName));
                xmlWriter.WriteElementString("LastName", null, customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName));
                xmlWriter.WriteElementString("Gender", null, customer.GetAttribute<string>(SystemCustomerAttributeNames.Gender));
                xmlWriter.WriteElementString("Company", null, customer.GetAttribute<string>(SystemCustomerAttributeNames.Company));

                xmlWriter.WriteElementString("CountryId", null, customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId).ToString());
                xmlWriter.WriteElementString("StreetAddress", null, customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress));
                xmlWriter.WriteElementString("StreetAddress2", null, customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2));
                xmlWriter.WriteElementString("ZipPostalCode", null, customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode));
                xmlWriter.WriteElementString("City", null, customer.GetAttribute<string>(SystemCustomerAttributeNames.City));
                xmlWriter.WriteElementString("CountryId", null, customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId).ToString());
                xmlWriter.WriteElementString("StateProvinceId", null, customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId).ToString());
                xmlWriter.WriteElementString("Phone", null, customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone));
                xmlWriter.WriteElementString("Fax", null, customer.GetAttribute<string>(SystemCustomerAttributeNames.Fax));
                xmlWriter.WriteElementString("VatNumber", null, customer.GetAttribute<string>(SystemCustomerAttributeNames.VatNumber));
                xmlWriter.WriteElementString("VatNumberStatusId", null, customer.GetAttribute<int>(SystemCustomerAttributeNames.VatNumberStatusId).ToString());
                xmlWriter.WriteElementString("TimeZoneId", null, customer.GetAttribute<string>(SystemCustomerAttributeNames.TimeZoneId));

                foreach (var store in _storeService.GetAllStores())
                {
                    var newsletter = _newsLetterSubscriptionService.GetNewsLetterSubscriptionByEmailAndStoreId(customer.Email, store.Id);
                    bool subscribedToNewsletters = newsletter != null && newsletter.Active;
                    xmlWriter.WriteElementString(string.Format("Newsletter-in-store-{0}", store.Id), null, subscribedToNewsletters.ToString());
                }

                xmlWriter.WriteElementString("AvatarPictureId", null, customer.GetAttribute<int>(SystemCustomerAttributeNames.AvatarPictureId).ToString());
                xmlWriter.WriteElementString("Signature", null, customer.GetAttribute<string>(SystemCustomerAttributeNames.Signature));
                
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
            return stringWriter.ToString();
        }


        #endregion
    }
}
