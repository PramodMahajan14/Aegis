using Adveshta.Model.EmployeeModels;
using Adveshta.Model.Master;
using Adveshta.Model.ProspectModel;
using Adveshta.Model.Vm.Employee;
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
              .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ProspectStatus))
              .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.ProspectTemperature));

            CreateMap<Prospect, ProspectDetailsVm>()
                   .ForMember(
                      dest => dest.Status,
                      opt => opt.MapFrom(src => src.ProspectStatus)
                    )
     .             ForMember(
                       dest => dest.Temperature,
                       opt => opt.MapFrom(src => src.ProspectTemperature)
                    )
                   .ForMember(
                       dest => dest.Location,
                       opt => opt.MapFrom(src => src.ProjectLocation)
                    )
                   .ForMember(
                       dest => dest.CreatedBy,
                       opt => opt.MapFrom(src => src.CreatedBy)
                    )
                  .ForMember(
                       dest => dest.UpdatedBy,
                       opt => opt.MapFrom(src => src.UpdatedBy)
                    )
                  .ForMember(
                       dest => dest.UpdatedAt,
                       opt => opt.MapFrom(src => src.UpdateAt)
                    )
                    .ForMember(
                        dest=>dest.Source,
                        opt =>opt.MapFrom(src=>src.ProspectSource)
                    )
                    .ForMember(
                        dest=>dest.Progress,
                        opt =>opt.MapFrom(src=>src.ProjectStage)
                    );;

            CreateMap<ProjectStage, ProjectStageVm>();
            CreateMap<ProspectStatus, ProspectStatusVm>();
            CreateMap<ProspectTemperature, ProspectTemperatureVm>();
            CreateMap<ProspectSource, ProspectSourceVm>();

            CreateMap<Employee, BasicEmployeeVm>();

            #endregion


            #region Contact

            // CreateMap<

            #endregion
        }
    }
}