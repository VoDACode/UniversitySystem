using System.Collections;
using University.Domain.Entity.Course.Requests;
using University.Domain.Entity.Group.Requests;
using University.Domain.Entity.Lesson.Requests;
using University.Domain.Entity.Task.Requests;
using University.Domain.Entity.TaskAnswer.Requests;
using University.Domain.Entity.User;
using University.Domain.Entity.User.Requests;
using University.Domain.Repositores;
using University.Domain.Services;

namespace University.API.Tools
{
    public static class InsertData
    {
        private static Random random = new Random();

        public static async Task InsertDataToDatabase(IServiceProvider serviceProvider)
        {
            await IncertUsers(serviceProvider);

            await InsertCourses(serviceProvider);
            await InsertGroups(serviceProvider);

            await InsertStudentsToGroups(serviceProvider);
            await InsertGroupsToCourses(serviceProvider);

            await InsertLessons(serviceProvider);
            await InsertTasks(serviceProvider);
            await InsertTaskAnswers(serviceProvider);
        }

        private static async Task IncertUsers(IServiceProvider serviceProvider)
        {
            var userService = serviceProvider.GetRequiredService<IUserService>();

            for (int i = 0; i < 99; i++)
            {
                var user = new CreateUserRequest
                {
                    FirstName = $"FirstName{i}",
                    LastName = $"LastName{i}",
                    Email = $"student{i}@example.com",
                    Phone = $"+3801{i}",
                    TaxId = (i + 1),
                    DateOfBirth = DateTime.UtcNow.AddYears(-20).AddDays(i),
                    Role = UserRole.Student
                };

                await userService.CreateUser(user);
            }

            for (int i = 0; i < 10; i++)
            {
                var user = new CreateUserRequest
                {
                    FirstName = $"TFirstName{i}",
                    LastName = $"TLastName{i}",
                    Email = $"teacher{i}@example.com",
                    Phone = $"+3802{i}",
                    TaxId = 100 * (i + 1),
                    DateOfBirth = DateTime.UtcNow.AddYears(-40).AddDays(i),
                    Role = UserRole.Teacher
                };

                await userService.CreateUser(user);
            }
        }

        private static async Task InsertCourses(IServiceProvider serviceProvider)
        {
            var courseService = serviceProvider.GetRequiredService<ICourseService>();

            for (int i = 0; i < 10; i++)
            {
                var course = new CreateCourseRequest
                {
                    Name = $"Course{i}"
                };

                await courseService.CreateCourse(course);
            }
        }

        private static async Task InsertGroups(IServiceProvider serviceProvider)
        {
            var groupService = serviceProvider.GetRequiredService<IGroupService>();

            for (int i = 0; i < 10; i++)
            {
                var group = new CreateGroupRequest
                {
                    Name = $"Group{i}",
                    TeacherId = 100 + i,
                    IsSubGroup = i % 3 == 0
                };

                await groupService.CreateGroup(group);
            }
        }

        private static async Task InsertStudentsToGroups(IServiceProvider serviceProvider)
        {
            var groupRepository = serviceProvider.GetRequiredService<IGroupRepository>();
            var userRepository = serviceProvider.GetRequiredService<IUserRepository>();

            int lastStudentId = 1;

            for (int i = 0; i < 10; i++)
            {
                var group = await groupRepository.GetGroupById(i + 1);
                if (group == null)
                {
                    continue;
                }
                var studentsCount = random.Next(10, 15);
                for (int j = 0; j < studentsCount; j++)
                {
                    var user = await userRepository.GetUserById(lastStudentId++);
                    if (user == null)
                    {
                        continue;
                    }
                    group.Students.Add(user);
                }
                await groupRepository.UpdateGroup(group);
            }
        }

        private static async Task InsertGroupsToCourses(IServiceProvider serviceProvider)
        {
            var courseRepository = serviceProvider.GetRequiredService<ICourseRepository>();
            var groupRepository = serviceProvider.GetRequiredService<IGroupRepository>();

            for (int i = 0; i < 10; i++)
            {
                var course = await courseRepository.GetCourseById(i + 1);
                if (course == null)
                {
                    continue;
                }
                var groupsCount = random.Next(1, 5);
                for (int j = 0; j < groupsCount; j++)
                {
                    var group = await groupRepository.GetGroupById(random.Next(1, 10));
                    if (group == null)
                    {
                        continue;
                    }
                    course.Groups.Add(group);
                }
                await courseRepository.UpdateCourse(course);
            }
        }

        private static async Task InsertLessons(IServiceProvider serviceProvider)
        {
            var lessonService = serviceProvider.GetRequiredService<ILessonService>();

            for (int groupId = 1; groupId < 10; groupId++)
            {
                DateTime lastDate = DateTime.Parse("2024-07-01");
                for (int i = 0; i < 180; i++)
                {
                    var lessonInDay = random.Next(1, 5);
                    lastDate = lastDate.AddDays(1);

                    TimeSpan time = new TimeSpan(08, 30, 00);
                    for (int j = 0; j < lessonInDay; j++)
                    {
                        var lesson = new CreateLessonRequest
                        {
                            Name = $"Lesson{i} - {j}",
                            GroupId = groupId,
                            Date = lastDate,
                            CourseId = random.Next(1, 10),
                            TeacherId = 100 + random.Next(0, 10),
                            Description = random.Next(0, 10) % 2 == 0 ? null : $"Description {i} - {j}",
                            StartTime = time.Add(new TimeSpan(0, 45 * j, 0)),
                            EndTime = time.Add(new TimeSpan(0, 45 * (j + 1), 0))
                        };

                        time = time.Add(new TimeSpan(0, 45 * lessonInDay, 0));

                        await lessonService.CreateLesson(lesson);
                    }
                }
            }
        }

        private static async Task InsertTasks(IServiceProvider serviceProvider)
        {
            var taskService = serviceProvider.GetRequiredService<ITaskService>();
            var lessonService = serviceProvider.GetRequiredService<ILessonService>();

            List<int> takedLessons = new List<int>();

            for (int i = 0; i < 3000; i++)
            {
                var lessonId = random.Next(1, 4000);

                var lessonEntity = await lessonService.GetLesson(lessonId);
                if (takedLessons.Contains(lessonId) || lessonEntity == null)
                {
                    continue;
                }

                takedLessons.Add(lessonId);

                bool hasDeadline = random.Next(0, 10) % 2 == 0;
                var time = TimeOnly.FromTimeSpan(TimeSpan.FromHours(random.Next(0, 24)));
                var task = new CreateTaskRequest
                {
                    Deadline = hasDeadline ? lessonEntity.Date.AddDays(random.Next(1, 10)).ToDateTime(time) : null,
                    MaxFiles = random.Next(1, 5),
                    CanUpdate = random.Next(0, 10) % 2 == 0,
                    Content = $"Content {i}",
                    CourseId = lessonEntity.CourseId,
                    GroupId = lessonEntity.GroupId,
                    MaxScore = random.Next(1, 10) % 2 == 0 ? 100 : 12,
                    TeacherId = lessonEntity.TeacherId,
                    Title = $"Title {i}",
                };

                await taskService.CreateTask(task);
            }
        }

        private static async Task InsertTaskAnswers(IServiceProvider serviceProvider)
        {
            var taskAnswerService = serviceProvider.GetRequiredService<ITaskAnswerService>();
            var taskService = serviceProvider.GetRequiredService<ITaskService>();

            IDictionary<int, IList<int>> takedTasks = new Dictionary<int, IList<int>>();

            for (int i = 0; i < 7000; i++)
            {
                var taskId = random.Next(1, 1000);
                var taskEntity = await taskService.GetTask(taskId);
                if (taskEntity == null)
                {
                    continue;
                }

                var taskAnswer = new CreateTaskAnswerRequest
                {
                    TaskId = taskId,
                    StudentId = random.Next(1, 100)
                };

                if(!takedTasks.ContainsKey(taskAnswer.StudentId))
                {
                    takedTasks[taskAnswer.StudentId] = new List<int>();
                }

                if (takedTasks[taskAnswer.StudentId].Contains(taskId))
                {
                    continue;
                }
                takedTasks[taskAnswer.StudentId].Add(taskId);

                var files = new SomeFormFileCollection();

                for (int j = 0; j < taskEntity.MaxFiles - 1; j++)
                {
                    files.Add(new SomeFormFile("file", $"file{j}.txt", "text/plain", random.Next(1, 1000)));
                }

                await taskAnswerService.CreateTaskAnswer(taskAnswer, files);
            }
        }

        private class SomeFormFileCollection : IFormFileCollection
        {
            private readonly IList<IFormFile> files = new List<IFormFile>();

            public IFormFile? this[string name] => files.FirstOrDefault(f => f.Name == name);

            public IFormFile this[int index] => files[index];

            public int Count => files.Count;

            public IEnumerator<IFormFile> GetEnumerator()
            {
                return files.GetEnumerator();
            }

            public IFormFile? GetFile(string name)
            {
                return files.FirstOrDefault(f => f.Name == name);
            }

            public IReadOnlyList<IFormFile> GetFiles(string name)
            {
                return files.Where(f => f.Name == name).ToList();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return files.GetEnumerator();
            }

            public void Add(IFormFile file)
            {
                files.Add(file);
            }
        }

        private class SomeFormFile : IFormFile
        {
            public string ContentType { get; }

            public string ContentDisposition => "";

            public IHeaderDictionary Headers => throw new NotImplementedException();

            public long Length { get; }

            public string Name { get; }

            public string FileName { get; }

            public SomeFormFile(string name, string fileName, string contentType, long length)
            {
                Name = name;
                FileName = fileName;
                ContentType = contentType;
                Length = length;
            }

            public void CopyTo(Stream target)
            {
                throw new NotImplementedException();
            }

            public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public Stream OpenReadStream()
            {
                throw new NotImplementedException();
            }
        }
    }
}
