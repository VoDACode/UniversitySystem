namespace University.Domain.Entity.Group.Responses
{
    public class GroupResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int TeacherId { get; set; }
        public bool IsSubGroup { get; set; }

        public GroupResponse(GroupEntity group)
        {
            Id = group.Id;
            Name = group.Name;
            TeacherId = group.TeacherId;
            IsSubGroup = group.IsSubGroup;
        }

        public static implicit operator GroupResponse(GroupEntity group)
        {
            return new GroupResponse(group);
        }
    }
}
