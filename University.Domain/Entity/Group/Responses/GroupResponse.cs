namespace University.Domain.Entity.Group.Responses
{
    public class GroupResponse
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public int TeacherId { get; set; } = 0;
        public bool IsSubGroup { get; set; } = false;

        public GroupResponse(GroupEntity group)
        {
            Id = group.Id;
            Name = group.Name;
            TeacherId = group.TeacherId;
            IsSubGroup = group.IsSubGroup;
        }

        public GroupResponse()
        {
        }

        public static implicit operator GroupResponse(GroupEntity group)
        {
            return new GroupResponse(group);
        }
    }
}
