using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emarket.Core.Application.ViewModels.Ads
{
    public class AdvertisementCardViewModel
    {
        public int AdvertisementId { get; set; }
        public string ProductName { get; set; }
        public string FirstImage { get; set; }
        public string SecondImage { get; set; }
        public string ThirdImage { get; set; }
        public string FourthImage { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public int UserId { get; set; }

        //Business Requirements
        public string CategoryName { get; set; }
        public DateTime DateofCreating { get; set; }
        public string AdvertiserName { get; set; }
        public string AdvertiserPhone { get; set; }
        public string AdvertiserEmail { get; set; }

    }
}
