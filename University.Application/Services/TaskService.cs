using University.Domain.Entity.Task;
using University.Domain.Entity.Task.Requests;
using University.Domain.Entity.Task.Responses;
using University.Domain.Entity.TaskAnswer.Responses;
using University.Domain.Exceptions;
using University.Domain.Repositores;
using University.Domain.Requests;
using University.Domain.Responses;
using University.Domain.Services;

namespace University.Application.Services
{
    public class TaskService : ITaskService
    {
        protected readonly ITaskRepository taskRepository;
        protected readonly ICourseRepository courseRepository;
        protected readonly IGroupRepository groupRepository;
        protected readonly IUserRepository userRepository;

        public TaskService(ITaskRepository taskRepository, ICourseRepository courseRepository, IGroupRepository groupRepository, IUserRepository userRepository)
        {
            this.taskRepository = taskRepository;
            this.courseRepository = courseRepository;
            this.groupRepository = groupRepository;
            this.userRepository = userRepository;
        }

        public async Task<TaskResponse> CreateTask(CreateTaskRequest request)
        {
            if(request.Deadline != null && request.Deadline < DateTime.UtcNow)
            {
                throw new BadRequestException("Deadline must be in the future");
            }

            if(!await courseRepository.ExistsById(request.CourseId))
            {
                throw new BadRequestException("Course not found");
            }
            if (!await groupRepository.ExistsById(request.GroupId))
            {
                throw new BadRequestException("Group not found");
            }
            if (!await userRepository.ExistsById(request.TeacherId))
            {
                throw new BadRequestException("Teacher not found");
            }

            var task = new TaskEntity
            {
                Title = request.Title,
                Content = request.Content,
                CourseId = request.CourseId,
                GroupId = request.GroupId,
                TeacherId = request.TeacherId,
                Deadline = request.Deadline,
                MaxMark = request.MaxScore,
                MaxFiles = request.MaxFiles,
                CanUpdate = request.CanUpdate
            };

            return await taskRepository.CreateTask(task);
        }

        public async Task DeleteTask(int id)
        {
            if (!await taskRepository.ExistById(id))
            {
                throw new BadRequestException("Task not found");
            }

            await taskRepository.DeleteTask(id);
        }

        public async Task<TaskResponse> GetTask(int id)
        {
            if (!await taskRepository.ExistById(id))
            {
                throw new BadRequestException("Task not found");
            }

            return await taskRepository.GetTaskById(id);
        }

        public async Task<IEnumerable<TaskAnswerResponse>> GetTaskAnswersFromTask(int id)
        {
            if (!await taskRepository.ExistById(id))
            {
                throw new BadRequestException("Task not found");
            }

            var task = await taskRepository.GetTaskAnswersFromTask(id);

            return task.TaskAnswers.Select(taskAnswer => new TaskAnswerResponse(taskAnswer));
        }

        public async Task<PageResponse<TaskResponse>> GetTasks(PageRequest request)
        {
            IQueryable<TaskEntity> tasks = await taskRepository.GetAllTasks();
            IQueryable<TaskResponse> taskResponses = tasks.Select(task => new TaskResponse(task));
            return await PageResponse<TaskResponse>.Create(taskResponses, request);
        }

        public async Task<TaskResponse> UpdateTask(int id, UpdateTaskRequest request)
        {
            if (!await taskRepository.ExistById(id))
            {
                throw new BadRequestException("Task not found");
            }

            if (request.Deadline != null && request.Deadline < DateTime.UtcNow)
            {
                throw new BadRequestException("Deadline must be in the future");
            }

            var task = await taskRepository.GetTaskById(id);

            if(task == null)
            {
                throw new BadRequestException("Task not found");
            }

            task.Title = request.Title;
            task.Content = request.Content;
            task.Deadline = request.Deadline;
            task.MaxMark = request.MaxScore;
            task.MaxFiles = request.MaxFiles;
            task.CanUpdate = request.CanUpdate;

            return await taskRepository.UpdateTask(task);
        }
    }
}
