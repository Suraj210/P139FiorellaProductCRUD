using FiorellaBackend.Services.Interfaces;
using FiorellaBackend.ViewModels;

namespace FiorellaBackend.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly IBasketService _basketService;
        private readonly ISettingService _settingService;

        public LayoutService(IBasketService basketService, ISettingService settingService)
        {
            _basketService = basketService;
            _settingService = settingService;

        }

        public HeaderVM GetHeaderDatas()
        {
            Dictionary<string, string> settingDatas = _settingService.GetSettings();
            int basketCount = _basketService.GetCount();

            return new HeaderVM
            {
                BasketCount = basketCount,
                HeaderLogo = settingDatas["HeaderLogo"]
            };
        }
    }
}
