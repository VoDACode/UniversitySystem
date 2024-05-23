using University.Domain.Entity.Lesson;
using University.Domain.Entity.Lesson.Requests;
using University.Domain.Entity.Lesson.Responses;
using University.Domain.Exceptions;
using University.Domain.Repositores;
using University.Domain.Requests;
using University.Domain.Responses;
using University.Domain.Services;

namespace University.Application.Services
{
    public class LessonService : ILessonService
    {
        protected readonly ILessonRepository lessonRepository;
        protected readonly IGroupRepository groupRepository;
        protected readonly ICourseRepository courseRepository;
        protected readonly IUserRepository userRepository;

        public LessonService(ILessonRepository lessonRepository, IGroupRepository groupRepository, ICourseRepository courseRepository, IUserRepository userRepository)
        {
            this.lessonRepository = lessonRepository;
            this.groupRepository = groupRepository;
            this.courseRepository = courseRepository;
            this.userRepository = userRepository;
        }

        public async Task<LessonResponse> CreateLesson(CreateLessonRequest request)
        {
            if(!await groupRepository.ExistsById(request.GroupId))
            {
                throw new BadRequestException("Group not found");
            }

            if (!await courseRepository.ExistsById(request.CourseId))
            {
                throw new BadRequestException("Course not found");
            }

            if (!await userRepository.ExistsById(request.TeacherId))
            {
                throw new BadRequestException("Teacher not found");
            }

            if(request.StartTime >= request.EndTime)
            {
                throw new BadRequestException("Start time must be less than end time");
            }

            var lesson = new LessonEntity
            {
                Name = request.Name,
                Description = request.Description,
                GroupId = request.GroupId,
                CourseId = request.CourseId,
                TeacherId = request.TeacherId,
                Date = DateOnly.FromDateTime(request.Date),
                StartTime = request.StartTime,
                EndTime = request.EndTime,
            };

            return await lessonRepository.CreateLesson(lesson);
        }

        public async Task DeleteLesson(int id)
        {
            if (!await lessonRepository.ExistsById(id))
            {
                throw new NotFoundException("Lesson not found");
            }

            await lessonRepository.DeleteLesson(id);
        }

        public async Task<LessonResponse> GetLesson(int id)
        {
            var lesson = await lessonRepository.GetLessonById(id);

            if (lesson == null)
            {
                throw new NotFoundException("Lesson not found");
            }

            return lesson;
        }

        public async Task<PageResponse<LessonResponse>> GetLessons(PageRequest request)
        {
            IQueryable<LessonEntity> lessons = await lessonRepository.GetLessons();
            IQueryable<LessonResponse> response = lessons.Select(lesson => new LessonResponse(lesson));

            return await PageResponse<LessonResponse>.Create(response, request);
        }

        public async Task<LessonResponse> UpdateLesson(int id, UpdateLessonRequest request)
        {
            if (!await lessonRepository.ExistsById(id))
            {
                throw new NotFoundException("Lesson not found");
            }

            if (!await userRepository.ExistsById(request.TeacherId))
            {
                throw new BadRequestException("Teacher not found");
            }

            if (request.StartTime >= request.EndTime)
            {
                throw new BadRequestException("Start time must be less than end time");
            }

            var lesson = await lessonRepository.GetLessonById(id);
            if (lesson == null)
            {
                throw new NotFoundException("Lesson not found");
            }

            lesson.Name = request.Name;
            lesson.Description = request.Description;
            lesson.TeacherId = request.TeacherId;
            lesson.Date = DateOnly.FromDateTime(request.Date);
            lesson.StartTime = request.StartTime;
            lesson.EndTime = request.EndTime;

            return await lessonRepository.UpdateLesson(lesson);
        }
    }
}
