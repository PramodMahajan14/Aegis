using Adveshta.Model.Master;
using Adveshta.Model.ProspectModel;
using Adveshta.Model.Vm.Prospect;
using AutoMapper;

namespace Adveshta.Services.Mapper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            #region  Prospects =================

            CreateMap<Prospect, ProspectListVm>()
              .ForMember(dest => dest.Status,opt=>opt.MapFrom(src=>src.ProspectStatus))
              .ForMember(dest=>dest.Temperature , opt=>opt.MapFrom(src=>src.ProspectTemperature));

            CreateMap<ProjectStage,ProjectStageVm>();
            CreateMap<ProspectStatus,ProspectStatusVm>();
            CreateMap<ProspectTemperature,ProspectTemperatureVm>();

            #endregion
        }
    }
}