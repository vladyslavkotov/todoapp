namespace WebApplication6.Models
{
   public class ToDoItem
   {
      public ToDoItem()
      {
         this.Created = DateTime.UtcNow;
         this.IsCompleted = false;
      }

      public int ID { get; set; }   

      public string? Text { get; set; }

      public DateTime Created { get; set; }

      public bool IsCompleted { get; set; }
   }
}