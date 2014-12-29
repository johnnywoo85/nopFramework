using System;
using Nop.Web.Controllers;
using NUnit.Framework;

namespace Nop.Web.MVC.Tests.Public.Infrastructure
{
    [TestFixture]
    public class RoutesTests : RoutesTestsBase
    {
        [Test]
        public void Default_route()
        {
            "~/".ShouldMapTo<HomeController>(c => c.Index());
        }

        [Test]
        public void Customer_routes()
        {
            //"~/login/".ShouldMapTo<CustomerController>(c => c.Login(null, null, false));
            "~/login/checkoutasguest/".ShouldMapTo<CustomerController>(c => c.Login(true));
            //"~/register/".ShouldMapTo<CustomerController>(c => c.Register(null, false));
            "~/logout/".ShouldMapTo<CustomerController>(c => c.Logout());
            "~/registerresult/2".ShouldMapTo<CustomerController>(c => c.RegisterResult(2));
            "~/passwordrecovery/".ShouldMapTo<CustomerController>(c => c.PasswordRecovery());
            "~/passwordrecovery/confirm".ShouldMapTo<CustomerController>(c => c.PasswordRecoveryConfirm(null, null));

            "~/customer/info/".ShouldMapTo<CustomerController>(c => c.Info());
            "~/customer/addresses/".ShouldMapTo<CustomerController>(c => c.Addresses());

            "~/customer/rewardpoints/".ShouldMapTo<CustomerController>(c => c.RewardPoints());
            "~/customer/changepassword/".ShouldMapTo<CustomerController>(c => c.ChangePassword());
            "~/customer/avatar/".ShouldMapTo<CustomerController>(c => c.Avatar());
            //"~/customer/activation?token=cc74c80f-1edd-43f7-85df-a3cccc1b47b9&email=test@test.com".ShouldMapTo<CustomerController>(c => c.AccountActivation("cc74c80f-1edd-43f7-85df-a3cccc1b47b9", "test@test.com"));
            "~/customer/addressdelete/6".ShouldMapTo<CustomerController>(c => c.AddressDelete(6));
            "~/customer/addressedit/7".ShouldMapTo<CustomerController>(c => c.AddressEdit(7));
            "~/customer/addressadd".ShouldMapTo<CustomerController>(c => c.AddressAdd());
        }

        [Test]
        public void Profile_routes()
        {
            "~/profile/1".ShouldMapTo<ProfileController>(c => c.Index(1, null));
            "~/profile/1/page/2".ShouldMapTo<ProfileController>(c => c.Index(1, 2));
        }

        [Test]
        public void Common_routes()
        {
            "~/contactus".ShouldMapTo<CommonController>(c => c.ContactUs());
            "~/sitemap".ShouldMapTo<CommonController>(c => c.Sitemap());
            "~/sitemap.xml".ShouldMapTo<CommonController>(c => c.SitemapXml());
        }

        [Test]
        public void Newsletter_routes()
        {
            //TODO cannot validate true parameter
            //"~/newsletter/subscriptionactivation/bb74c80f-1edd-43f7-85df-a3cccc1b47b9/true".ShouldMapTo<NewsletterController>(c => c.SubscriptionActivation(new Guid("bb74c80f-1edd-43f7-85df-a3cccc1b47b9"), true));
        }

        [Test]
        public void News_routes()
        {
            "~/news".ShouldMapTo<NewsController>(c => c.List(null));
            "~/news/rss/1".ShouldMapTo<NewsController>(c => c.ListRss(1));
            //"~/news/2/".ShouldMapTo<NewsController>(c => c.NewsItem(2));
            //"~/news/2/se-name".ShouldMapTo<NewsController>(c => c.NewsItem(2));
        }

        //[Test]
        //public void Topic_routes()
        //{
        //    "~/t/somename".ShouldMapTo<TopicController>(c => c.TopicDetails("somename"));
        //    "~/t-popup/somename".ShouldMapTo<TopicController>(c => c.TopicDetailsPopup("somename"));
        //}
    }
}
