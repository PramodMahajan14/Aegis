using Adveshta.Model.Auth;
using Adveshta.Model.ContactModel;
using Adveshta.Model.DTO.Contacts;
using Adveshta.Model.DTO.MeetingDto;
using Adveshta.Model.EmployeeModels;
using Adveshta.Model.Master;
using Adveshta.Model.MeetingModel;
using Adveshta.Model.ProspectModel;
using Adveshta.Model.Vm.Contacts;
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
                   .ForMember(
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
                   .ForMember(dest => dest.Source,
                        opt => opt.MapFrom(src => src.ProspectSource)
                    )
                   .ForMember(
                        dest => dest.Progress,
                        opt => opt.MapFrom(src => src.ProjectStage)
                    );

            CreateMap<ProjectStage, ProjectStageVm>();
            CreateMap<ProspectStatus, ProspectStatusVm>();
            CreateMap<ProspectTemperature, ProspectTemperatureVm>();
            CreateMap<ProspectSource, ProspectSourceVm>();
            CreateMap<JobRole,BasicJobRoleVm>();

            CreateMap<Employee, BasicEmployeeVm>();
            CreateMap<Prospect, BasicProspectVm>();

            #endregion


            #region Contact

            CreateMap<Contact, ContactListVm>()
              .ForMember(dest => dest.JobRole, opt => opt.MapFrom(src => src.ProjectContactRole));


            CreateMap<ManageContactDto,Contact>()
             .ForMember(dest=>dest.Id,obt=>obt.Ignore())
             .ForMember(dest=>dest.CreatedById,obt=>obt.Ignore())
             .ForMember(dest=>dest.CreatedAt, obt=>obt.Ignore())
             .ForMember(dest=>dest.ProspectId,obt=>obt.Ignore())
             .ForMember(dest=>dest.OrganizationId,obt=>obt.Ignore())
             .ForMember(dest=>dest.UpdateAt,obt=>obt.Ignore())
             .ForMember(dest=>dest.UpdateAt,obt=>obt.Ignore());

            #endregion



            #region  Employee


            CreateMap<Employee,UserProfileVm>();

            #endregion


            #region Meeting
            CreateMap<ManageMeetingDto,Meeting>();
            CreateMap<MeetingParticipantList,MeetingParticipant>();
            #endregion
        }
    }
}