public class ShiftCreateDTO
{
    public string Title { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int? AssignedUserId { get; set; }

    public ShiftCreateDTO(string shiftTitle, string shiftComment, DateTime shiftStart, DateTime shiftEnd, int shiftAttendeeId)
    {
        Title = shiftTitle;
        Comment = shiftComment;
        StartTime = shiftStart;
        EndTime = shiftEnd;
        AssignedUserId = shiftAttendeeId;
    }
}