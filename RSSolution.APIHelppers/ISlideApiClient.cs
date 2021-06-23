using RSSolution.ViewModels.Utilities.Slides;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RSSolution.APIHelpers
{
    public interface ISlideApiClient
    {
        Task<List<SlideVm>> GetAll();
    }
}