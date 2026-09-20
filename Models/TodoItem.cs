namespace TodoApi.Models{
    public class TodoItem : ToDoCreateRequest
    {
        /// <summary>
        /// Unique identifier of the todo item.
        /// </summary>
        public int ToDoItemId { get; set; }

        /// <summary>
        /// Indicates whether the todo item has been completed.
        /// </summary>
        public bool IsCompleted { get; set; } = false;

        /// <summary>
        /// Date and time when the todo item was created 
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}