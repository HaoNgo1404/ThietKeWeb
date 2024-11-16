using System.Web.Mvc;

namespace ThietKeWeb.Areas.customer
{
    public class customerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "customer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "customer_default",
                "customer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}