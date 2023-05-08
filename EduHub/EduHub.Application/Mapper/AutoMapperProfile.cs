using AutoMapper;
using EduHub.Application.DTOs.Admin;
using EduHub.Application.DTOs.Answer;
using EduHub.Application.DTOs.Course;
using EduHub.Application.DTOs.Question;
using EduHub.Application.DTOs.Test;
using EduHub.Application.DTOs.TestResult;
using EduHub.Application.DTOs.User;
using EduHub.Application.Models.Course;
using EduHub.Application.Models.Profile;
using EduHub.Application.Models.Question;
using EduHub.Application.Models.Test;
using EduHub.Domain.Constants;
using EduHub.Domain.Entities;

namespace EduHub.Application.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(s => s.UserImgUrl,
                    s => s.MapFrom(map => map.UserImgUrl == null ? null : HostConstant.CurrentHost + map.UserImgUrl));
            CreateMap<AnswerOption, AnswerOptionDTO>().ReverseMap();
            CreateMap<Question, QuestionDTO>().ReverseMap();
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<CourseDTO, EditCourseModel>().ReverseMap();
            CreateMap<TestDTO, EditTestModel>().ReverseMap();
            CreateMap<QuestionDTO, EditQuestionModel>().ReverseMap();
            CreateMap<Test, PassingTestDTO>().ReverseMap();

            CreateMap<Test, TestDTO>().ReverseMap();
            CreateMap<TestResult, TestResultDTO>().ReverseMap();

            CreateMap<TestResult, TestResultDTO>().ReverseMap();
            CreateMap<UserDTO, EditProfileModel>().ReverseMap();
            CreateMap<UserDTO, ChangePasswordModel>().ReverseMap();
            CreateMap<UserDTO, ChangePhotoModel>().ReverseMap();
            CreateMap<TeacherRequest, TeacherRequestDTO>()
                .ForMember(s => s.ProofImage, s => s.MapFrom(map => HostConstant.CurrentHost + map.ProofImage));
        }
    }
}