using FiorellaBackend.Models;

namespace FiorellaBackend.ViewModels.Home
{
    public class HomeVM
    {
        public List<SliderImg> SliderImgs { get; set; }

        public SliderInfos SliderInfos { get; set; }

        public List<Blog> Blogs { get; set; }

        public List<Product> Products { get; set; }

        public List<Category> Categories { get; set; }

        public AboutMain AboutMains { get; set; }

        public List<AboutFeature> AboutFeatures { get; set; }
        public List<Expert> Experts { get; set; }

        public Subscribe Subscribes { get; set; }

        public List<Say> Says { get; set; }
        public List<Instagram> InstagramPhotos { get; set; }


    }
}
